using System.Web.Mvc;
using SoyalWorkTimeWebManager.Controllers.BaseControllers;
using SoyalWorkTimeWebManager.Models.LacoationContexts;

namespace SoyalWorkTimeWebManager.Controllers.Begany
{
    [Authorize(Roles = "BeganyCardAdmin")]
    public class BeganyCardController : BaseCardController<BeganyContext>
    {
        
    }
}