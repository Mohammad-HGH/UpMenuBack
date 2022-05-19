using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpMenu.DataLayer.Entities.Account;

namespace UpMenu.DataLayer.Context
{
    public class UpMenuDBContext : DbContext
    {
        public UpMenuDBContext(DbContextOptions<UpMenuDBContext> options) : base(options)
        {

        }

        #region Db Sets
        public DbSet<User> Users { get; set; }
        #endregion

        #region
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
