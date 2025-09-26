using System.Text;

namespace Authentication.Services
{
    public interface ITwoFactorService
    {

        Task<bool> SendTwoFactorCodeAsync(string email);

        Task<bool> VerifyTwoFactorCodeAsync(string email, string code);
    }
}
