using static IronDome2.Models.Status;
using System.ComponentModel.DataAnnotations;
namespace IronDome2.Models
{
    public class Attack
    {
        [Key]
        public int? id {  get; set; }

        [AllowedValues("iran", "hutim")]
        public string origine {  get; set; }
        public string[] type { get; set; }
        public string? status { get; set; }
        public DateTime? time { get; set; }        
    }
}
