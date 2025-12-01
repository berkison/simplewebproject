using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore; // DbContext sınıfı buradan gelir
using Simple.Models;       // Entity (Urun, Kategori vb.) sınıfların buradadır



namespace Simple.Data
{
    public class SimpleContext : IdentityDbContext
    {
        public SimpleContext(DbContextOptions<SimpleContext> options) : base(options)
        {

        }
        
        public DbSet<Kitaplar> Kitaplar { get; set; }
    }
}