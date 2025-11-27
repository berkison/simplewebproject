using Microsoft.EntityFrameworkCore; // DbContext sınıfı buradan gelir
using Simple.Models;       // Entity (Urun, Kategori vb.) sınıfların buradadır

namespace Simple.Data
{
    public class SimpleContext : DbContext
    {
        public SimpleContext(DbContextOptions<SimpleContext> options) : base(options)
        {

        }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Kitaplar> kitaplar { get; set; }
    }
}