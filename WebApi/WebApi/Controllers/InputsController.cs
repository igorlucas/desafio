using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InputsController : ControllerBase
    {
        private readonly WebApiContext _context;

        public InputsController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/IOs
        [HttpGet]
        public IEnumerable<Input> GetIOModel()
        {
            return _context.Inputs;
        }

        // GET: api/IOs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIOModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var iOModel = await _context.Inputs.FindAsync(id);

            if (iOModel == null)
            {
                return NotFound();
            }

            return Ok(iOModel);
        }

        
        // POST: api/IOs
        [HttpPost]
        public async Task<IActionResult> PostIOModel([FromBody] Input iOModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Inputs.Add(iOModel);
            await _context.SaveChangesAsync();

            var resource = _context.Resources.FirstOrDefault(x=> x.Id == iOModel.ResourceId);
            resource.Quantity = (resource.Quantity + iOModel.Quantity);

            await UpdateQtdResource(iOModel.ResourceId, resource);

            return CreatedAtAction("GetIOModel", new { id = iOModel.Id }, iOModel);
        }

        public async Task UpdateQtdResource(int ResourceId, Resource resourceModel)
        {
            
            _context.Entry(resourceModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                    throw;
            }

        }

        private bool IOModelExists(int id)
        {
            return _context.Inputs.Any(e => e.Id == id);
        }
    }
}