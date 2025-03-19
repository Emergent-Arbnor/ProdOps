using Microsoft.AspNetCore.Mvc;
using Npgsql;
using backend.Models;
using backend.Repositories;

namespace backend.Controllers
{
    [ApiController]  // Marks the class as an API controller and enables model binding and validation.
    [Route("api/[controller]")]  // Sets the route pattern for this controller.
    public class ArticlesController : ControllerBase
    {
        
        private DatabaseRepository _databaseRepository;

        public ArticlesController(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        [HttpGet("get-quantities")]
        public IActionResult GetArticles()
        {
            try
            {
                
                string query = "SELECT article_count FROM articles";

                using var cmd = _databaseRepository.CallDatabase(query);

                using NpgsqlDataReader reader = cmd.ExecuteReader();
                var articleCounts = new Dictionary<string, int>();
                reader.Read();
                articleCounts.Add("ARTICLE_A", reader.GetInt32(0));
                reader.Read();
                articleCounts.Add("ARTICLE_B", reader.GetInt32(0));
                return Ok(articleCounts);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }


        // POST request to update quantities of articles
        [HttpPost("update-quantities")]
        public IActionResult UpdateQuantities([FromBody] ArticleUpdate request)
        {
            // Check for validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Invalid input data", details = ModelState });
            }

            // Update ARTICLE_A
            string query = "UPDATE articles SET article_count = article_count + @amount WHERE article_name = @name";
            using (var cmd = _databaseRepository.CallDatabase(query))
            {
                cmd.Parameters.AddWithValue("amount", request.ArticleAQuantity!);
                cmd.Parameters.AddWithValue("name", "ARTICLE_A");
                cmd.ExecuteNonQuery();
            }

            // Update ARTICLE_B
            using (var cmd = _databaseRepository.CallDatabase(query))
            {
                cmd.Parameters.AddWithValue("amount", request.ArticleBQuantity!);
                cmd.Parameters.AddWithValue("name", "ARTICLE_B");
                cmd.ExecuteNonQuery();
            }

            return Ok(new { message = "Quantities updated successfully." });
            
        }
    }
}
