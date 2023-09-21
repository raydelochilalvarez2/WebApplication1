using WebApplication1.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Modelos;
using WebApplication1.Datos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly AplicationDbContext _context;
        
        public VillaController(ILogger<VillaController> logger, AplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas() {
            _logger.LogInformation("Obteniendo lista de Villas");
            return Ok(_context.Villas.ToList());
        }
    
     //-------------------------------------------------------------------------------------------   
        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0) {
                _logger.LogError("el id no puede ser 0");
               return BadRequest();
            }       
            var villatore = _context.Villas.FirstOrDefault(villa => villa.Id == id);
            if (villatore == null) {
               return  NotFound();
            }
            return Ok(villatore);
        }
    //-------------------------------------------------------------------------------------------

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       
        public ActionResult<VillaDTO> PostVilla([FromBody] VillaDTO villaDto) {
            Console.WriteLine(ModelState.Values);
            Console.WriteLine(ModelState);
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            Console.WriteLine(_context.Villas);
            
            if (_context.Villas.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower() ) != null)
            {
                ModelState.AddModelError("error_Nombre", "No puede existir 2 nombres iguales " + villaDto.Nombre);
                return BadRequest(ModelState);
            }
            if (villaDto == null) { return BadRequest(); }
            if (villaDto.Id > 0) { return StatusCode(StatusCodes.Status500InternalServerError); }

            Villa data = new()
            {
                
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                Tarifa = villaDto.Tarifa,
                Ocupantes = villaDto.Ocupantes,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                ImageUrl = villaDto.ImageUrl,
                Amenidad = villaDto.Amenidad
            };
            _context.Villas.Add(data);
            _context.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villaDto.Id}, villaDto);
        }
    //-------------------------------------------------------------------------------------------

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DelectVilla(int id) {
            if (id==0) {
                return BadRequest();
            }
            var villa = _context.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null) {
                return NotFound();
            }
            _context.Villas.Remove(villa);
            _context.SaveChanges();
            return NoContent();
        }
    //-------------------------------------------------------------------------------------------
        [HttpPut("id")]
        public IActionResult PutVilla(int id, [FromBody] VillaDTO villaDto)
        {
            if (villaDto == null || id == 0 || id != villaDto.Id) { return BadRequest(); }
            var villa = _context.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null) { return NotFound(); }

            Villa data = new()
            {
                Id = villaDto.Id,
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                Tarifa = villaDto.Tarifa,
                Ocupantes = villaDto.Ocupantes,
                MetrosCuadrados = villaDto.MetrosCuadrados,
                ImageUrl = villaDto.ImageUrl,
                Amenidad = villaDto.Amenidad
            };
            _context.Villas.Update(data);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PatchVilla(int id, JsonPatchDocument<VillaDTO> patchVilla)
        {
            
            if (id == 0 || patchVilla == null){ return BadRequest(); }
            var villa = _context.Villas.AsNoTracking().FirstOrDefault(item => item.Id == id);
            if (villa == null) { return NotFound(); }

           
            VillaDTO villaDTO = new()
            {
                Id = villa.Id,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                Tarifa = villa.Tarifa,
                Ocupantes = villa.Ocupantes,
                MetrosCuadrados = villa.MetrosCuadrados,
                ImageUrl = villa.ImageUrl,
                Amenidad = villa.Amenidad
            };
            

            patchVilla.ApplyTo(villaDTO, ModelState);

            if (!ModelState.IsValid) { return BadRequest(); }

            Villa modelo = new()
            {
                Id = villaDTO.Id,
                Nombre = villaDTO.Nombre,
                Detalle = villaDTO.Detalle,
                Ocupantes = villaDTO.Ocupantes,
                MetrosCuadrados = villaDTO.MetrosCuadrados,
                Tarifa = villaDTO.Tarifa,
                ImageUrl = villaDTO.ImageUrl,
                Amenidad = villaDTO.Amenidad
            };
            _context.Villas.Update(modelo);
            _context.SaveChanges();
            

            return NoContent();
        }

        }
}
