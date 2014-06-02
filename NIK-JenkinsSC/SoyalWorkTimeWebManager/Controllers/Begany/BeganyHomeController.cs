using System.Web.Mvc;
using SoyalWorkTimeWebManager.Controllers.BaseControllers;
using SoyalWorkTimeWebManager.Models.LacoationContexts;

namespace SoyalWorkTimeWebManager.Controllers.Begany
{
    [Authorize(Roles = "BeganyRoles")]
    public class BeganyHomeController : BaseHomeController<BeganyContext>
    {
        
    }
}
