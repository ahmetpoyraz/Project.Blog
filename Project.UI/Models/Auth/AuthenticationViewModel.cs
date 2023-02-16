using Project.Business.Authentication;
using Project.Entity.Dtos.Authentication;

namespace Project.UI.Models.Auth
{
    public class AuthenticationViewModel
    {
        IAuthenticationService _authenticationService;
        public UserDto UserDetail { get; set; }

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService=authenticationService;
        }
        public void FillUserDetail(int id){
            UserDetail =_authenticationService.GetUserByIdAsync(id).Result.Data;
        }
    }
}
