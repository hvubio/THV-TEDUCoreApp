using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Contact")]
    public class Contact : DomainEntity<string>, ISwitchable
    {
        [Required] [StringLength(256)] public string Name { get; set; }

        [Required] [StringLength(400)] public string Address { get; set; }

        [StringLength(20)] public string Phone { get; set; }

        [Required] [StringLength(250)] public string Email { get; set; }

        [StringLength(20)] public string Fax { get; set; }
        [StringLength(250)] public string Website { get; set; }
        
        public double? Longtitude { get; set; }
        public double? Latitude { get; set; }
        public string Other { get; set; }
        public Status Status { get; set; }
    }
}