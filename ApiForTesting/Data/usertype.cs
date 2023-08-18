using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ApiForTesting.Data
{
    public class usertype 
    {
        public usertype()
        {
            this.appusers = new HashSet<appuser>();
        }
        [Key]
        public Guid? UserTypeId { get; set; }
        public string? UserType { get; set; }

        public string? Description { get; set; }

        public int? IsActive { get; set; }
        public ICollection<appuser> appusers { get;  }

    }
}
