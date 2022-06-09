using HikiCoffee.Utilities;
using Microsoft.AspNetCore.DataProtection;
using System;

namespace HikiCoffee.AppManager.Service
{
    public class TokenService : ITokenService
    {
        public readonly IDataProtector _protector;

        public TokenService(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("$>8:11A:G9E27>B>42634=I=5?C:675<B0443>83J6545");
        }

        public string ReadToken()
        {
            string token1 = Rms.Read("TokenInfoVersion1", "Token", "");
            token1 = _protector.Unprotect(token1);

            string token2 = Rms.Read("TokenInfoVersion2", "Token", "");
            token2 = _protector.Unprotect(token2);

            return token1 + token2;
        }

        public bool SaveToken(string token)
        {
            int length = token.Length;

            int decomposed = Convert.ToInt32(Math.Floor(length*1.0 / 2)); ;

            string token1 = token.Substring(0, decomposed); 
            string token2 = token.Substring(decomposed, length - decomposed);

            string tokenHash1 = _protector.Protect(token1);
            string tokenHash2 = _protector.Protect(token2);

            Rms.Write("TokenInfoVersion1", "Token", tokenHash1);
            Rms.Write("TokenInfoVersion2", "Token", tokenHash2);

            return true;
        }
    }
}
