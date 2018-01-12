using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeduCoreApp.Data.EF.Extentions;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Infrastructure.ShareKernel;

namespace TeduCoreApp.Data.EF.Configurations
{
    public class SystemConfigConfiguration: DbEntityConfiguration<SystemConfig>
    {
        public override void Configuration(EntityTypeBuilder<SystemConfig> entity)
        {
            entity.Property(c => c.Id).HasMaxLength(255).IsRequired();
        }
    }
}
