using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("AdvertisementPosition")]
    public class AdvertisementPosition : DomainEntity<int>
    {
        [StringLength(250)] public string Name { get; set; }

        public int PageId { get; set; }

        [ForeignKey("PageId")] public virtual AdvertisementPage AdvertisementPage { get; set; }

        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }
}