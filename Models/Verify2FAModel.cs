using System.ComponentModel.DataAnnotations;
namespace Authentication.Controllers
{
    public class Verify2FAModel
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public bool RememberMe { get; set; }
    }
}