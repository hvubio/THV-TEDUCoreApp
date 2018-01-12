using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeduCoreApp.Data.EF.Extentions;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Data.EF.Configurations
{
    public class AdvertisementPositionConfiguration: DbEntityConfiguration<AdvertisementPosition>
    {
        public override void Configuration(EntityTypeBuilder<AdvertisementPosition> entity)
        {
            entity.Property(c=>c.Id).HasMaxLength(20).IsRequired();
        }
    }
}
