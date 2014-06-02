using System.Web.Mvc;
using SoyalWorkTimeWebManager.Controllers.BaseControllers;
using SoyalWorkTimeWebManager.Models.LacoationContexts;

namespace SoyalWorkTimeWebManager.Controllers.Bereg
{
    [Authorize(Roles = "RootAdmins")]
    public class BeregSiteController : BaseSiteController<HOHContext>
    {
    }
}