using Microsoft.AspNetCore.Identity;

namespace Authentication.Entities
{
    public class User: IdentityUser
    {
        public string mlb { get; set; }
        public string name { get; set; }
        public string surname { get; set; }



    }
}
