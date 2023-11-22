using System.Text.RegularExpressions;

namespace ApiFoto.Helpers
{
    public static class Utils
    {
        public static bool ValidEmail(string email)
        {
            try
            {
                string expresion;
                expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                if (Regex.IsMatch(email, expresion))
                {
                    if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
