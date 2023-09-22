using WebApplication1.Modelos.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Modelos;
using WebApplication1.Datos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebApplication1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public VillaController(ILogger<VillaController> logger, AplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas() {
            _logger.LogInformation("Obteniendo lista de Villas");
            IEnumerable<Villa> villaListDTO = await _context.Villas.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<VillaDTO>>(villaListDTO));
        }
    
     //-------------------------------------------------------------------------------------------   
        [HttpGet("id", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult <VillaDTO>> GetVilla(int id)
        {
            if (id == 0) {
                _logger.LogError("el id no puede ser 0");
               return BadRequest();
            }       
            Villa villa =await _context.Villas.FirstOrDefaultAsync(villa => villa.Id == id);
            if (villa == null) {
               return  NotFound();
            }
            return Ok(_mapper.Map<VillaDTO>(villa));
        }
    //-------------------------------------------------------------------------------------------

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
       
        public async Task<ActionResult<VillaCreateDTO>> PostVilla([FromBody] VillaCreateDTO villacreateDto) {
  
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            if (await _context.Villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == villacreateDto.Nombre.ToLower() ) != null)
            {
                ModelState.AddModelError("error_nombre", "No puede existir 2 nombres iguales " + villacreateDto.Nombre);
                return BadRequest(ModelState);
            }
            if (villacreateDto == null) { return BadRequest(); }
            Villa villa = _mapper.Map<Villa>(villacreateDto);

            //Villa villa = new()
            //{
                
            //    Nombre = villacreateDto.Nombre,
            //    Detalle = villacreateDto.Detalle,
            //    Tarifa = villacreateDto.Tarifa,
            //    Ocupantes = villacreateDto.Ocupantes,
            //    MetrosCuadrados = villacreateDto.MetrosCuadrados,
            //    ImageUrl = villacreateDto.ImageUrl,
            //    Amenidad = villacreateDto.Amenidad
            //};
            await _context.Villas.AddAsync(villa);
            await _context.SaveChangesAsync();
           
            return CreatedAtRoute("GetVilla", new { id = villa.Id}, villa);
        }
    //-------------------------------------------------------------------------------------------

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DelectVilla(int id) {
            if (id==0) {
                return BadRequest();
            }
            var villa = await _context.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null) {
                return NotFound();
            }
             _context.Villas.Remove(villa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    //-------------------------------------------------------------------------------------------
        [HttpPut("id")]
        public async Task<IActionResult> PutVilla(int id, [FromBody] VillaUpdateDTO villaUpdateDto)
        {
            if (villaUpdateDto == null || id == 0 || id != villaUpdateDto.Id || !ModelState.IsValid) { return BadRequest(); }
            var villa =await _context.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null) { return NotFound(); }

            Villa data = _mapper.Map<Villa> (villaUpdateDto);

            //Villa data = new()
            //{
            //    Id = villaUpdateDto.Id,
            //    Nombre = villaUpdateDto.Nombre,
            //    Detalle = villaUpdateDto.Detalle,
            //    Tarifa = villaUpdateDto.Tarifa,
            //    Ocupantes = villaUpdateDto.Ocupantes,
            //    MetrosCuadrados = villaUpdateDto.MetrosCuadrados,
            //    ImageUrl = villaUpdateDto.ImageUrl,
            //    Amenidad = villaUpdateDto.Amenidad
            //};
            _context.Villas.Update(data);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchVilla)
        {
            
            if (id == 0 || patchVilla == null){ return BadRequest(); }
            Villa villa = await _context.Villas.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
            if (villa == null) { return NotFound(); }

            VillaUpdateDTO  villaUpdateDTO = _mapper.Map<VillaUpdateDTO> (villa);

            //VillaUpdateDTO villaDTO = new()
            //{
            //    Id = villa.Id,
            //    Nombre = villa.Nombre,
            //    Detalle = villa.Detalle,
            //    Tarifa = villa.Tarifa,
            //    Ocupantes = villa.Ocupantes,
            //    MetrosCuadrados = villa.MetrosCuadrados,
            //    ImageUrl = villa.ImageUrl,
            //    Amenidad = villa.Amenidad
            //};
            

            patchVilla.ApplyTo(villaUpdateDTO, ModelState);

            if (!ModelState.IsValid) { return BadRequest(); }

            Villa modelo = _mapper.Map<Villa>(villaUpdateDTO);

            //Villa modelo = new()
            //{
            //    Id = villaDTO.Id,
            //    Nombre = villaDTO.Nombre,
            //    Detalle = villaDTO.Detalle,
            //    Ocupantes = villaDTO.Ocupantes,
            //    MetrosCuadrados = villaDTO.MetrosCuadrados,
            //    Tarifa = villaDTO.Tarifa,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Amenidad = villaDTO.Amenidad
            //};
            _context.Villas.Update(modelo);
            await _context.SaveChangesAsync();
            

            return NoContent();
        }

        }
}
