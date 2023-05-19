using System.ComponentModel;

namespace PasswordManagerWEBAPP.Models
{
    public class Passwordmngr
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        [DisplayName("Account")]
        public string Accountr { get; set; } = default!;
        [DisplayName("Password")]
        public string Passwordr { get; set; }

        public string Description { get; set; }
    }
}
