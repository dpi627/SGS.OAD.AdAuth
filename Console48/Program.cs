using SGS.OAD.AdAuth;
using System;

namespace Console48
{
    internal class Program
    {
        static void Main()
        {
            var isVaild = AdAuthHelper.IsValid("YourAdAccount", "YourAdPassword");
            Console.WriteLine($"{isVaild}");
        }
    }
}
