using System.Web.Mvc;
using SoyalWorkTimeWebManager.Controllers.BaseControllers;
using SoyalWorkTimeWebManager.Models.LacoationContexts;

namespace SoyalWorkTimeWebManager.Controllers.Beregovo
{
    [Authorize(Roles = "BeregovoPersonAdmin")]
    public class BeregovoPersonController : BasePersonController<BeregovoContext>
    {
        
    }
}