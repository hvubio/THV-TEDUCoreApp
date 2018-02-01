using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("AdvertisementPosition")]
    public class AdvertistmentPosition : DomainEntity<string>
    {
        [StringLength(250)] public string Name { get; set; }

        public string PageId { get; set; }

        [ForeignKey("PageId")] public virtual AdvertistmentPage AdvertisementPage { get; set; }

        public virtual ICollection<Advertistment> Advertisements { get; set; }
    }
}