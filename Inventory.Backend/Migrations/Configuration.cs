namespace Inventory.Backend.Migrations {
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Inventory.Backend.Entities.InventoryContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Inventory.Backend.Entities.InventoryContext";
        }

        protected override void Seed(Inventory.Backend.Entities.InventoryContext context) {
            var stores = new List<Store>() {
                new Store {
                    Name = "Super Zapatos I", Address = "Caracas - Venezuela",
                    Articles = new List<Article>() {
                        new Article { Name = "MEN'S GALLANT PARKVIEW", Description = "Hush Puppies For Men", Price = 100, TotalInShelf = 15, TotalInVault = 50  },
                        new Article { Name = "WOMEN'S AYDIN CATELYN", Description = "Hush Puppies For Women", Price = 100, TotalInShelf = 15, TotalInVault = 50  },
                        new Article { Name = "Timberland Heritage 3-Eye", Description = "Classic Lug para hombre", Price = 100, TotalInShelf = 15, TotalInVault = 50  },
                    }
                },
                new Store {
                    Name = "Super Zapatos II", Address = "Maracaibo - Venezuela",
                    Articles = new List<Article>() {
                        new Article { Name = "MEN'S GALLANT PARKVIEW", Description = "Hush Puppies For Men", Price = 100, TotalInShelf = 15, TotalInVault = 50  },
                    }
                },
                new Store {
                    Name = "Super Zapatos III", Address = "Bogotá - Colombia",
                    Articles = new List<Article>() {
                        new Article { Name = "WOMEN'S AYDIN CATELYN", Description = "Hush Puppies For Women", Price = 100, TotalInShelf = 15, TotalInVault = 50  },
                        new Article { Name = "Timberland Heritage 3-Eye", Description = "Classic Lug para hombre", Price = 100, TotalInShelf = 15, TotalInVault = 50  },
                    }
                }
            };
            stores.ForEach(i => context.Stores.AddOrUpdate(s => s.StoreID, i));
            context.SaveChanges();
        }
    }
}
