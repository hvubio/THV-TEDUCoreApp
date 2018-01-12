using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TeduCoreApp.Data.EF.Extentions
{
    /// <summary>
    /// Make extiontion for entity 
    /// Edit configuration Id of entity class
    /// </summary>
    public static class ModelBuilderExtentions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder,
            DbEntityConfiguration<TEntity> entityConfiguration) where TEntity : class
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configuration);
        }
       
    }

    public abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configuration(EntityTypeBuilder<TEntity> entity);
    }

    
}
