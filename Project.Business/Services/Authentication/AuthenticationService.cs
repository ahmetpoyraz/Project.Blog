using AutoMapper;
using Project.Business.Services.Module;
using Project.Business.Validation.FluentValidation.Dtos.Authentication;
using Project.Business.Validation.FluentValidation.Tables.EvrimDbMSSQL;
using Project.Core.Core.Entities.Result.Abstract;
using Project.Core.Core.Entities.Result.Concrete;
using Project.Core.Core.Entities.Security;
using Project.Core.CrossCuttingConcerns.Validation;
using Project.Core.Enums;
using Project.Core.Enums.ApiEnum;
using Project.Core.Utilities.Security.Hashing;
using Project.Core.Utilities.Security.Jwt;
using Project.Data;
using Project.Entity.Dtos.Authentication;
using Project.Entity.Dtos.Module;
using Project.Entity.Filters.Authentication;
using Project.Entity.Filters.Module;
using Project.Entity.Procedures.Authentication;
using Project.Entity.Procedures.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Business.Authentication
{
    #region CONCRETE
    public class AuthenticationService : IAuthenticationService
    {
        IUnitOfWork _unitOfWork;
        ITokenHelper _tokenHelper;
        IFileLinkService _fileLinkService;
        IMapper _mapper;

        public AuthenticationService(IUnitOfWork unitOfWork, ITokenHelper tokenHelper, IFileLinkService fileLinkService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenHelper = tokenHelper;
            _fileLinkService = fileLinkService;
            _mapper = mapper;
        }

        public async Task<IDataResult<AccessToken>> Login(LoginDto loginDto)
        {
            var userToCheck = await _unitOfWork.UserRepository.GetByUserNameAsync(loginDto.Username);
            if (userToCheck == null)
            {
                return new ErrorDataResult<AccessToken>(OperationTexts.USER_NOT_FOUND);
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<AccessToken>(OperationTexts.WRONG_PASSWORD);
            }

            var result = await this.CreateAccessToken(userToCheck);

            return new SuccessDataResult<AccessToken>(System.Net.HttpStatusCode.Accepted, result.Data, OperationTexts.SUCCESSFUL_LOGIN);
        }

        //public async Task<IDataResult<int>> Register(RegisterDto registerDto)
        //{
        //    byte[] passwordHash, passwordSalt;
        //    HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);
        //    var user = new Core.Core.Entities.Security.User
        //    {
        //        UserName = registerDto.UserName,
        //        FirstName = registerDto.FirstName,
        //        LastName = registerDto.LastName,
        //        PasswordHash = passwordHash,
        //        PasswordSalt = passwordSalt,
        //        IsEnabled =true,
        //        DateCreated = DateTime.Now,
        //        UserCreated = 1,

        //    };

        //    var result = await _userService.Insert(user);
        //    return new SuccessDataResult<int>(result.Data, OperationTexts.USER_CREATED);
        //}

        public async Task<IDataResult<AccessToken>> CreateAccessToken(Core.Core.Entities.Security.User user)
        {
            var claims = await this.GetClaims(user.Id);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data.ToList());
            return new SuccessDataResult<AccessToken>(accessToken, OperationTexts.TOKEN_CREATED);
        }

        public async Task<IDataResult<Core.Core.Entities.Security.User>> GetByUsernameAsync(string username)
        {
            try
            {
                var result = await _unitOfWork.UserRepository.GetByUserNameAsync(username);
                return new SuccessDataResult<Core.Core.Entities.Security.User>(System.Net.HttpStatusCode.OK, result, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<Core.Core.Entities.Security.User>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IResult> InsertUserAsync(UserDto dto)
        {
            try
            {
                if (dto.Password=="00000000")
                {
                    dto.Password =null;
                }
                ValidationTool.Validate(new UserDtoValidator(false), dto);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);
                var entity = new Core.Core.Entities.Security.User
                {
                    UserName = dto.UserName,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    IsEnabled = true,
                    DateCreated = DateTime.Now,
                    UserCreated = (int)dto.UserCreated,

                };


                var id = await _unitOfWork.UserRepository.InsertAsync(entity);

                if (dto.ProfilPhoto != null)
                {
                    var fileLinkDto = new FileLinkDto()
                    {

                        File = dto.ProfilPhoto.File,
                        FileName = dto.ProfilPhoto.FileName,
                        FolderPath = Paths.PROFILE_PHOTOS,
                        ModulePostTypeId = ModulePostTypes.USER_PROFILE_PHOTOS,
                        ModulePostTypeDataId = id,
                    };
                    await _fileLinkService.SaveFileAndInsertDb(fileLinkDto);
                }
                else
                {
                    var fileLinkDto = new FileLinkDto()
                    {
                        FileName = "defult-user",
                        FileType = ".png",
                        FolderPath = Paths.PROFILE_PHOTOS,
                        ModulePostTypeId = ModulePostTypes.USER_PROFILE_PHOTOS,
                        ModulePostTypeDataId = id,
                    };


                    await _fileLinkService.InsertDbWithoutSaveFile(fileLinkDto);
                }




                //var userOperationClaim = new UserOperationClaim()
                //{
                //    OperationClaimId = dto.ClaimId,
                //    UserId = id
                //};

                //await _unitOfWork.UserOperationClaimRepository.InsertAsync(userOperationClaim);

                return new SuccessResult(System.Net.HttpStatusCode.OK, OperationTexts.USER_CREATED);
            }
            catch (Exception ex)
            {

                _unitOfWork.Rollback();
                return new ErrorResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {

            }
        }

        public async Task<IDataResult<IEnumerable<Core.Core.Entities.Security.OperationClaim>>> GetClaims(int userId)
        {
            try
            {
                var result = await _unitOfWork.OperationClaimRepository.GetClaimsByUserIdAsync(userId);

                return new SuccessDataResult<IEnumerable<Core.Core.Entities.Security.OperationClaim>>(System.Net.HttpStatusCode.OK, result, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<IEnumerable<Core.Core.Entities.Security.OperationClaim>>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IDataResult<IEnumerable<SPUserList>>> GetUserListAsync(UserFilter filter)
        {
            try
            {
                var list = await _unitOfWork.UserRepository.GetUserListAsync(filter);


                return new SuccessDataResult<IEnumerable<SPUserList>>(System.Net.HttpStatusCode.OK, list, OperationTexts.GET);

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<IEnumerable<SPUserList>>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);
            }
            finally
            {
                _unitOfWork.Rollback();
            }
        }

        public async Task<IResult> DeleteUserAsync(UserFilter filter)
        {
            try
            {
                await _unitOfWork.UserRepository.DeleteAsync((int)filter.Id, (int)filter.UserId);

                _unitOfWork.Commit();

                return new SuccessResult(System.Net.HttpStatusCode.OK, OperationTexts.DELETE);
            }
            catch (Exception ex)
            {

                return new ErrorResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IDataResult<IEnumerable<OperationClaimDto>>> GetOperationClaimList(OperationClaimFilter filter)
        {
            try
            {
                var list = await _unitOfWork.OperationClaimRepository.GetOperationClaimList(filter);
                var mapped = _mapper.Map<IEnumerable<OperationClaim>, IEnumerable<OperationClaimDto>>(list);

                return new SuccessDataResult<IEnumerable<OperationClaimDto>>(System.Net.HttpStatusCode.OK, mapped, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<IEnumerable<OperationClaimDto>>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IDataResult<IEnumerable<SpUserOperationClaimList>>> GetUserOperationClaimList(UserOperationClaimFilter filter)
        {
            try
            {
                var list = await _unitOfWork.UserOperationClaimRepository.GetUserOperationClaimList(filter);

                return new SuccessDataResult<IEnumerable<SpUserOperationClaimList>>(System.Net.HttpStatusCode.OK, list, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<IEnumerable<SpUserOperationClaimList>>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IDataResult<UserDto>> GetUserByIdAsync(int id)
        {
            try
            {
                var entity = await _unitOfWork.UserRepository.GetById(id);

                var mapped = _mapper.Map<Core.Core.Entities.Security.User, UserDto>(entity);
                var fileLinkFilter = new FileLinkFilter() { ModulePostTypeId = ModulePostTypes.USER_PROFILE_PHOTOS, ModulePostTypeDataId = (int)mapped.Id };
                var operationiClaimFilter = new OperationClaimFilter() { UserId = mapped.Id };

                var fileLinkDto = await _fileLinkService.GetFileLinkByModulePostTypeAndDataId(fileLinkFilter);
                var operationClaimDtoList = await this.GetOperationClaimList(operationiClaimFilter);

                mapped.ProfilPhoto = fileLinkDto.Data;
                mapped.OperationClaims = operationClaimDtoList.Data;

                return new SuccessDataResult<UserDto>(System.Net.HttpStatusCode.OK, mapped, OperationTexts.GET);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<UserDto>(System.Net.HttpStatusCode.BadRequest, null, ex.Message);

            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<IResult> UpdateUser(UserDto dto)
        {
            try
            {
                if (dto.Password == "00000000")
                {
                    dto.Password = null;
                }
                ValidationTool.Validate(new UserDtoValidator(true), dto);
                var oldData = await _unitOfWork.UserRepository.GetById((int)dto.Id);


                var entity = new Core.Core.Entities.Security.User();
                entity.Id = oldData.Id;
                entity.DateCreated = oldData.DateCreated;
                entity.UserCreated = oldData.UserCreated;
                entity.IsEnabled = oldData.IsEnabled;
                entity.DateDeleted = oldData.DateDeleted;
                entity.UserDeleted = oldData.UserDeleted;
                entity.UserModified = dto.UserModified;
                entity.FirstName = dto.FirstName;
                entity.LastName = dto.LastName;
                entity.UserName = dto.UserName;
                entity.Email = dto.Email;

                if (dto.Password != null)
                {
                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);
                    entity.PasswordHash = passwordHash;
                    entity.PasswordSalt = passwordSalt;

                }
                else
                {

                    entity.PasswordHash = oldData.PasswordHash;
                    entity.PasswordSalt = oldData.PasswordSalt;


                }
                var result = await _unitOfWork.UserRepository.UpdateAsync(entity);

                _unitOfWork.Commit();

                return new SuccessResult(System.Net.HttpStatusCode.OK, OperationTexts.UPDATE);

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ErrorResult(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

    }
    #endregion CONCRETE


    #region ABSTRACT
    public interface IAuthenticationService
    {
        Task<IDataResult<AccessToken>> Login(LoginDto loginDto);
        //Task<IDataResult<int>> Register(RegisterDto registerDto);
        Task<IDataResult<Core.Core.Entities.Security.User>> GetByUsernameAsync(string username);
        Task<IResult> InsertUserAsync(UserDto dto);
        Task<IDataResult<IEnumerable<OperationClaim>>> GetClaims(int userId);
        Task<IDataResult<IEnumerable<SPUserList>>> GetUserListAsync(UserFilter filter);
        Task<IDataResult<IEnumerable<SpUserOperationClaimList>>> GetUserOperationClaimList(UserOperationClaimFilter filter);
        Task<IResult> DeleteUserAsync(UserFilter filter);
        Task<IDataResult<IEnumerable<OperationClaimDto>>> GetOperationClaimList(OperationClaimFilter filter);
        Task<IDataResult<UserDto>> GetUserByIdAsync(int id);
        Task<IResult> UpdateUser(UserDto dto);


    }

    #endregion ABSTRACT
}



