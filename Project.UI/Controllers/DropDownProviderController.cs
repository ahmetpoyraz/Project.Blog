using Microsoft.AspNetCore.Mvc;
using Project.Business.Authentication;
using Project.Entity.Filters.Authentication;

namespace Project.UI.Controllers
{
    public class DropDownProviderController : BaseController<DataTableProviderController>
    {
        IAuthenticationService _authenticationService;
        public DropDownProviderController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationClaimList()
        {
            var filter = new OperationClaimFilter();
            var result = await _authenticationService.GetOperationClaimList(filter);

            if (result.Success)
            {
                
                var dropDown = result.Data.Select(x => new { val = x.Id, text = x.Name });
               
                return Ok(dropDown);
            }

            return BadRequest(result);
        }
    }
}
