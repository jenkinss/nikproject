using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Data.Edm;

namespace SoyalWorkTimeWebManager.ViewModels.Assignments
{
    public class AssignAccesMode
    {
        private List<string> _accesModes = new List<string> {"INVALID", "READ ONLY", "CARD OR PIN", "CARD AND PIN"};

        public int SelectedAccesModeId { get; set; }

        public IEnumerable<SelectListItem> AccesMode
        {
            get { return new SelectList(AccesModes); }
        }

        public List<string> AccesModes
        {
            get { return _accesModes; }
            set { _accesModes = value; }
        }
    }
}