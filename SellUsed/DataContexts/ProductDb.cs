using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using SellUsed.Models;

namespace SellUsed.DataContexts
{
    public class ProductDb : DbContext
    {
        public ProductDb()
            : base("DefaultConnection")
        {
            Database.Log = sql => Debug.Write(sql);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserFavoriteAd> UserFavoriteAds { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductVisit> ProductVisits { get; set; }
    }
}