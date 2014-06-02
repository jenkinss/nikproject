using System.Collections.Generic;

namespace SoyalWorkTimeWebManager.ViewModels
{
    public class MoveReportViewModel
    {
        public string Name { get; set; }
        public string Birth { get; set; }
        public Dictionary<string, List<string>> MoveDictionary { get; set; }
    }
}