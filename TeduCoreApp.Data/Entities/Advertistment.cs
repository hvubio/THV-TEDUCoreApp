using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Advertisement")]
    public class Advertistment : DomainEntity<int>, ISwitchable, IDateTracking
    {
        [StringLength(250)] public string Name { get; set; }
        [StringLength(250)] public string Description { get; set; }
        [StringLength(20)] public int PositionId { get; set; }
        [StringLength(250)] public string Image { get; set; }
        [StringLength(250)] public string Url { get; set; }

        [ForeignKey("PositionId")] public virtual AdvertistmentPosition AdvertisementPositions { get; set; }


        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}