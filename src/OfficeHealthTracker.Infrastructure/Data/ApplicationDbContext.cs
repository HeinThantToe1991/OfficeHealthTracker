using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using OfficeHealthTracker.Domain.Model;
using UI_Layer.Data.SeedData;

namespace OfficeHealthTracker.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
      

        #region STPL Technical Assessment

        public DbSet<FieldType> FieldTypes { get; set; }
        public DbSet<Template> Templates { get; set; }

        public DbSet<TemplateField> TemplateFields { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<FilledForm> FilledForms { get; set; }

        public DbSet<PayloadData> PayloadDatas { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region SeedData
            ModelBuilderExtensions.SeedUser(modelBuilder);
            ModelBuilderExtensions.SeedFieldType(modelBuilder);
            #endregion
        }

    }
}
