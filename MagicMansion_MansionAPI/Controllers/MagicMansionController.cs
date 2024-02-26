using MagicMansion_MansionAPI.Data;
using MagicMansion_MansionAPI.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicMansion_MansionAPI.Controllers
{
    [Route("api/MagicMansion")]
    [ApiController]
    public class MagicMansionController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MansionDTO>> GetAllMansions()
        {
            return Ok(MansionStore.mansionDTO);
        }

        [HttpGet("{id}", Name = "GetMansion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MansionDTO> GetMansion(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var mansion = MansionStore.mansionDTO.FirstOrDefault(c => c.Id == id);
            if (mansion == null)
            {
                return NotFound();
            }
            return Ok(mansion);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MansionDTO> CreateMansion(MansionDTO mansion)
        {
            if (mansion == null)
            {
                return BadRequest(mansion);
            }
            if (mansion.Id != 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            mansion.Id = MansionStore.mansionDTO.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
            MansionStore.mansionDTO.Add(mansion);
            return CreatedAtRoute("GetMansion", new { id = mansion.Id }, mansion);
        }

        [HttpDelete("{id}", Name = "DeleteMansion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteMansion(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var mansion = MansionStore.mansionDTO.FirstOrDefault(c => c.Id == id);
            if (mansion == null)
            {
                return NotFound();
            }
            MansionStore.mansionDTO.Remove(mansion);
            return NoContent();
        }

        [HttpPut("{id}", Name = "UpdateMansion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateMansion(int id, [FromBody] MansionDTO mansionDTO)
        {
            if (id != mansionDTO.Id || mansionDTO == null)
            {
                return BadRequest();
            }

            var mansion = MansionStore.mansionDTO.FirstOrDefault(c => c.Id == id);
            mansion.Id = mansionDTO.Id;
            mansion.Name = mansionDTO.Name;
            mansion.Description = mansionDTO.Description;
            return NoContent();
        }

        [HttpPatch("{id}", Name = "UpdatePartialMansion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PartialUpdateMansion(int id, JsonPatchDocument<MansionDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();    
            }
            var mansion = MansionStore.mansionDTO.FirstOrDefault(c => c.Id == id);
            if(mansion == null)
            {
                return NotFound();
            }
            patchDTO.ApplyTo(mansion, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent() ;
        }
    }
    
    
    
}
