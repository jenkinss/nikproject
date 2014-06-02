using SoyalBISAPI.Common;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;

namespace SoyalEventServer.CorrectionHelpers
{
    public class WorkStyleDetails
    {
        public static string GetStyle<TContext>(Event cevent, EFUnitOfWork<TContext> worker) where TContext : WorkTimeManagerContext
        {
            string result = "";
            var person = worker.PersonRepository.GetByID(cevent.UserAddress);
            string style = person.WorkStyle;
            if (style != null)
                if (style[2] != ']')
                {
                    result += style[1];
                    result += style[2];
                }
                else
                {
                    result += style[1];
                }
            return result;
        }
    }
}
