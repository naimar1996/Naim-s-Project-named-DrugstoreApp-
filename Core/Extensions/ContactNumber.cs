using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ContactNumber
    {
        public const string motif = @"^([\+]?994[-]?|[0])?[1-9][0-9]{8}$";

        public static bool IsContactNumber(this string number)
        {
            if (number != null)
            {
                return Regex.IsMatch(number, motif);
            }
            else
            {
                return false;
            }
        }
    }
}
