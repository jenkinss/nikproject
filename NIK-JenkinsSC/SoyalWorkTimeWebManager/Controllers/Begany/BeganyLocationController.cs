using System.Web.Mvc;
using SoyalWorkTimeWebManager.Controllers.BaseControllers;
using SoyalWorkTimeWebManager.Models.LacoationContexts;

namespace SoyalWorkTimeWebManager.Controllers.Begany
{
    [Authorize(Roles = "RootAdmins")]
    public class BeganyLocationController : BaseLocationController<BeganyContext>
    { 
    }
}