using DemoWebAPI.Data;
using DemoWebAPI.Model;
using DemoWebAPI.Services.Designation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TblDesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;

        public TblDesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDesignations()
        {
            var designations = await _designationService.GetDesignations();
            return Ok(designations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDesignation(int id)
        {
            var designation = await _designationService.GetDesignation(id);
            if (designation == null) return NotFound();
            return Ok(designation);
        }

        [HttpPost]
        public async Task<IActionResult> AddDesignation([FromBody] TblDesignation designation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newDesignation = await _designationService.AddDesignation(designation);
            return CreatedAtAction(nameof(GetDesignation), new { id = newDesignation }, designation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDesignation(int id, TblDesignation designation)
        {
            if (id != designation.DesId) return BadRequest();
            var updatedDesignation = await _designationService.UpdateDesignation(designation);
            return Ok(updatedDesignation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            var result = await _designationService.DeleteDesignation(id);

            if(!result) return NotFound();
          
            return  NoContent();
        }
           
    }
}
