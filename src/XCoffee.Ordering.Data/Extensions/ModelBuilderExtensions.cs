using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

using XCoffee.Core.Data;
using XCoffee.Core.Domain;

namespace XCoffee.Ordering.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        #region Extension Methods

        public static ModelBuilder LoadAllConfigurations(this ModelBuilder modelBuilder)
        {
            foreach (var configuration in Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IEntityConfiguration).IsAssignableFrom(t)))
            {
                dynamic instance = Activator.CreateInstance(configuration);
                ModelBuilderExtensions.AddConfiguration(modelBuilder, instance);
            }

            return modelBuilder;
        }

        public static ModelBuilder AddConfiguration<TEntity>(this ModelBuilder modelBuilder, EntityConfiguration<TEntity> entityConfiguration)
            where TEntity : class, IDomainEntity
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
            modelBuilder.Entity<TEntity>().HasData(entityConfiguration.Seed());

            return modelBuilder;
        }

        #endregion
    }
}
