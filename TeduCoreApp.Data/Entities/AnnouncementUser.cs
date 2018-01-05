using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("AnnouncementUser")]
    public class AnnouncementUser: DomainEntity<int>
    {
        [Required][StringLength(128)]
        public int AnnouncementId { get; set; }
        public bool? HasRead { get; set; }
        [StringLength(450)][Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        [ForeignKey("AnnouncementId")]
        public virtual Announcement Announcement { get; set; }
    }
}
