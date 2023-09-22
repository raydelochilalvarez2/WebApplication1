using WebApplication1.Modelos.DTO;

namespace WebApplication1.Datos
{
    public static class VillaStore
    {
        public static List<VillaDTO> villalist = new List<VillaDTO>
            {
                new VillaDTO{Id = 1, Nombre="Raydel", Ocupantes= 4},
                new VillaDTO{Id = 2, Nombre="Daniel", Ocupantes= 6},
            };
    }
}
