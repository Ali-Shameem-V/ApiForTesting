using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiForTesting.Data
{
    public class appuser 
    {
        [Key]
        public Guid? AppUserId { get; set; }
        [ForeignKey("usertypes_shameem")]
        public Guid? UserTypeId { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }
        public int? IsActive { get; set; }
        [JsonIgnore]
        public usertype? UserType { get; set; }



    }
}
