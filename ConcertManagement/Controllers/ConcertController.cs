using Microsoft.AspNetCore.Mvc;
using ConcertManagement.Models;
using ConcertManagement.Services;

namespace ConcertManagement.Controllers
{
    [ApiController]
    [Route("api/concerts")]
    public class ConcertController : ControllerBase
    {
        // Hämtar alla konserter
        [HttpGet]
        public IActionResult GetConcerts()
        {
            try
            {
                ConcertManager.LoadFromFile();
                return Ok(ConcertManager.GetConcerts());
            }
            catch (Exception ex)
            {
                // Returnerar 500 om det uppstår ett fel vid hämtning
                return StatusCode(500, "Error fetching concerts: " + ex.Message);
            }
        }

        // Hämtar en specifik konsert med hjälp av ID
        [HttpGet("{id}")]
        public IActionResult GetConcert(int id)
        {
            try
            {
                var concert = ConcertManager.GetConcerts().FirstOrDefault(c => c.Id == id);
                if (concert == null)
                {
                    return NotFound("Concert not found");
                }
                return Ok(concert);
            }
            catch (Exception ex)
            {
                // Returnerar 500 om det uppstår ett fel vid hämtning
                return StatusCode(500, "Error fetching concert: " + ex.Message);
            }
        }

        // Lägger till en ny konsert
        [HttpPost]
        public IActionResult AddConcert([FromBody] Concert concert)
        {
            try
            {
                if (concert == null)
                {
                    return BadRequest("Concert data is missing");
                }

                // Lägg till konserten och spara till fil
                ConcertManager.AddConcertFromApi(concert);
                ConcertManager.SaveToFile();

                return CreatedAtAction(nameof(GetConcert), new { id = concert.Id }, concert);
            }
            catch (Exception ex)
            {
                // Returnerar 500 om det uppstår ett fel vid tillägg
                return StatusCode(500, "Error adding concert: " + ex.Message);
            }
        }

        // Uppdaterar en befintlig konsert
        [HttpPut("{id}")]
        public IActionResult UpdateConcert(int id, [FromBody] Concert updatedConcert)
        {
            try
            {
                if (updatedConcert == null)
                {
                    return BadRequest("Concert data is missing");
                }

                var concerts = ConcertManager.GetConcerts();
                var concert = concerts.FirstOrDefault(c => c.Id == id);
                if (concert == null)
                {
                    return NotFound("Concert not found");
                }

                concert.Location = updatedConcert.Location;
                concert.Performer = updatedConcert.Performer;
                concert.DateAndTime = updatedConcert.DateAndTime;
                concert.Capacity = updatedConcert.Capacity;
                concert.Price = updatedConcert.Price;

                ConcertManager.SaveToFile();
                return Ok(concert);
            }
            catch (Exception ex)
            {
                // Returnerar 500 om det uppstår ett fel vid uppdatering
                return StatusCode(500, "Error updating concert: " + ex.Message);
            }
        }

        // Tar bort en konsert
        [HttpDelete("{id}")]
        public IActionResult DeleteConcert(int id)
        {
            try
            {
                var concerts = ConcertManager.GetConcerts();
                var concert = concerts.FirstOrDefault(c => c.Id == id);
                if (concert == null)
                {
                    return NotFound("Concert not found");
                }

                concerts.Remove(concert);
                ConcertManager.SaveToFile();
                return NoContent(); // Returnerar 204 NoContent om borttagningen är lyckad
            }
            catch (Exception ex)
            {
                // Returnerar 500 om det uppstår ett fel vid borttagning
                return StatusCode(500, "Error deleting concert: " + ex.Message);
            }
        }
    }
}