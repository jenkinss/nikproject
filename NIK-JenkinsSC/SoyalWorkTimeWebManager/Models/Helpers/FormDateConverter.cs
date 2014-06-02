using System;
using Microsoft.Ajax.Utilities;

namespace SoyalWorkTimeWebManager.Models.Helpers
{
    public class FormDateConverter
    {
        public static DateTime Convert(string date)
        {
            var result = new DateTime();

            if (!date.IsNullOrWhiteSpace())
            {
                result = System.Convert.ToDateTime(date);
            }

            return result;
        }
    }
}