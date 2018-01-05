using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Feedback")]
    public class Feedback: DomainEntity<int>, ISwitchable, IDateTracking
    {
        [Required] [StringLength(256)] public string Name { get; set; }
        [StringLength(256)] public string Email { get; set; }
        [StringLength(20)] public string Phone { get; set; }
        [StringLength(500)] public string Content { get; set; }
        
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
