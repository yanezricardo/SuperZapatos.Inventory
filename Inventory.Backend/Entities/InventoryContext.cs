using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Inventory.Backend.Entities {
    public class InventoryContext : DbContext {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Article> Articles { get; set; }

        public InventoryContext() : base("InventoryContext") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}