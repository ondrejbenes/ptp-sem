using System;

namespace Sem_Benes.Logic
{
    public class IcoValidator
    {
        public static bool IsValid(int ico)
        {
            var icoString = ico.ToString();
            if (icoString.Length != 8) return false;
            var sum = 0;
            for (int vaha = 8; vaha > 1; vaha--)
            {
                sum += Convert.ToInt32(icoString.Substring(icoString.Length - vaha, 1));
            }
            return sum%11 == 0;
        }
    }
}
