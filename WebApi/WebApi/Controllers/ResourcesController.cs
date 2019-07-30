using Microsoft.AspNetCore.JsonPatch;
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
    public class ResourcesController : ControllerBase
    {
        private readonly WebApiContext _context;

        public ResourcesController(WebApiContext context)
        {
            _context = context;
        }

        // GET: api/Resources
        [HttpGet]
        public IEnumerable<Resource> GetResourceModel()
        {
            return _context.Resources;
        }

        // GET: api/Resources/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResourceModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resourceModel = await _context.Resources.FindAsync(id);

            if (resourceModel == null)
            {
                return NotFound();
            }

            return Ok(resourceModel);
        }

        // PUT: api/Resources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResourceModel([FromRoute] int id, [FromBody] Resource resourceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resourceModel.Id)
            {
                return BadRequest();
            }
            _context.Entry(resourceModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // PATCH: api/Resources/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchResourceModel([FromRoute] int id, [FromBody] JsonPatchDocument<Resource> resourceModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (resourceModel == null) BadRequest();

            var resourceDB = await _context.Resources.FirstOrDefaultAsync(x => x.Id == id); 
            if (resourceDB == null) NotFound();


            resourceModel.ApplyTo(resourceDB, ModelState);
            var isValid = TryValidateModel(resourceDB);
            if(!isValid) BadRequest();

            
            try
            {
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }

        // POST: api/Resources
        [HttpPost]
        public async Task<IActionResult> PostResourceModel([FromBody] Resource resourceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Resources.Add(resourceModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResourceModel", new { id = resourceModel.Id }, resourceModel);
        }

        // DELETE: api/Resources/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResourceModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resourceModel = await _context.Resources.FindAsync(id);
            if (resourceModel == null)
            {
                return NotFound();
            }

            _context.Resources.Remove(resourceModel);
            await _context.SaveChangesAsync();

            return Ok(resourceModel);
        }

        private bool ResourceModelExists(int id)
        {
            return _context.Resources.Any(e => e.Id == id);
        }
    }
}