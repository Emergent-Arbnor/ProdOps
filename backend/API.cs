using Microsoft.AspNetCore.Mvc;
using Npgsql; // Import Npgsql for PostgreSQL
using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace
{
    [ApiController]  // Marks the class as an API controller and enables model binding and validation.
    [Route("api/[controller]")]  // Sets the route pattern for this controller.
    public class ArticlesController : ControllerBase
    {
        private readonly string _connectionString = "Host=localhost;Database=prodops;Username=admin;Password=";

        [HttpGet("")]
        public IActionResult GetHome()
        {
            return Ok("Welcome to the API!");
        }

        // POST request to update quantities of articles
        [HttpPost("update-quantities")]
        public IActionResult UpdateQuantities([FromBody] UpdateArticlesRequest request)
        {
            // Check if the model (request) is valid using automatic validation from data annotations
            if (!ModelState.IsValid)
            {
                // Return a BadRequest with validation error details if the model is invalid
                return BadRequest(ModelState);
            }

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    // Update articleA
                    using (var cmd = new NpgsqlCommand("UPDATE articles SET quantity = quantity + @amount WHERE name = @name", connection))
                    {
                        cmd.Parameters.AddWithValue("amount", request.ArticleAQuantity);
                        cmd.Parameters.AddWithValue("name", "articleA");
                        cmd.ExecuteNonQuery();
                    }

                    // Update articleB
                    using (var cmd = new NpgsqlCommand("UPDATE articles SET quantity = quantity + @amount WHERE name = @name", connection))
                    {
                        cmd.Parameters.AddWithValue("amount", request.ArticleBQuantity);
                        cmd.Parameters.AddWithValue("name", "articleB");
                        cmd.ExecuteNonQuery();
                    }
                }

                // Return success message if the quantities were updated successfully
                return Ok(new { message = "Quantities updated successfully." });
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error with exception details if an error occurs
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }

    // Define the request model
    public class UpdateArticlesRequest
    {
        [Required(ErrorMessage = "ArticleAQuantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "ArticleAQuantity must be a non-negative integer.")]
        public int ArticleAQuantity { get; set; }

        [Required(ErrorMessage = "ArticleBQuantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "ArticleBQuantity must be a non-negative integer.")]
        public int ArticleBQuantity { get; set; }
    }
}
