using System.Web.Mvc;
using SoyalWorkTimeWebManager.Controllers.BaseControllers;
using SoyalWorkTimeWebManager.Models.LacoationContexts;

namespace SoyalWorkTimeWebManager.Controllers.Bereg
{
    [Authorize(Roles = "BeregPersonAdmin")]
    public class BeregPersonController : BasePersonController<HOHContext>
    {
        
    }
}