using System.Web.Helpers;
using System.Web.Mvc;

namespace SoyalWorkTimeWebManager.Models.Helpers
{
    public static class Helpers
    {
        public static MvcHtmlString SortDirection(this HtmlHelper helper, ref WebGrid grid, string columnName)
        {
            string html = "";
            if (grid.SortColumn == columnName && grid.SortDirection == System.Web.Helpers.SortDirection.Ascending)
                html = "▲";
            else if (grid.SortColumn == columnName && grid.SortDirection == System.Web.Helpers.SortDirection.Descending)
                html = "▼";
            else
                html = "";

            return MvcHtmlString.Create(html);
        }
    }
}