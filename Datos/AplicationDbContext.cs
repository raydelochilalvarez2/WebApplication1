using Microsoft.EntityFrameworkCore;
using WebApplication1.Modelos;

namespace WebApplication1.Datos
{
    public class AplicationDbContext: DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<Villa>().HasData(
                new Villa() {
                    Id= 1,
                    Nombre = "VillaReal",
                    Detalle = "Detalles de la Villa....",
                    Tarifa = 25.43,
                    Ocupantes = 10,
                    MetrosCuadrados = 10,
                    ImageUrl = "",
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,

                },
                new Villa()
                {
                    Id = 2,
                    Nombre = "VillaReal2",
                    Detalle = "Detalles de la Villa2....",
                    Tarifa = 60,
                    Ocupantes = 20,
                    MetrosCuadrados = 20,
                    ImageUrl = "",
                    Amenidad = "",
                    FechaCreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,

                }
                );
        }
    }
}
