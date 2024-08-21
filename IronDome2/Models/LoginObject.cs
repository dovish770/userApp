using System.ComponentModel.DataAnnotations;

namespace IronDome2.Models
{
    public class LoginObject
    {
        [Key]
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
