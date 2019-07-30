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
    public class OutputsController : ControllerBase
    {
        private readonly WebApiContext _context;

        public OutputsController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/Outputs
        [HttpGet]
        public IEnumerable<Output> GetOutputs()
        {
            return _context.Outputs;
        }

        // GET: api/Outputs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOutput([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var output = await _context.Outputs.FindAsync(id);

            if (output == null)
            {
                return NotFound();
            }

            return Ok(output);
        }

        
        // POST: api/Outputs
        [HttpPost]
        public async Task<IActionResult> PostOutput([FromBody] Output model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Outputs.Add(model);
            await _context.SaveChangesAsync();

            var resource = _context.Resources.FirstOrDefault(x => x.Id == model.ResourceId);
            resource.Quantity = (resource.Quantity - model.Quantity);

            await UpdateQtdResource(model.ResourceId, resource);


            return CreatedAtAction("GetOutput", new { id = model.Id }, model);
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
        
        private bool InputExists(int id)
        {
            return _context.Inputs.Any(e => e.Id == id);
        }
    }
}