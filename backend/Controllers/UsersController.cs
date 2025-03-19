using Microsoft.AspNetCore.Mvc;
using Npgsql;
using BCrypt.Net;
using backend.Models;
using backend.Repositories;

namespace backend.APIControllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private DatabaseRepository _databaseRepository;

        public UserController(DatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser([FromBody] UserCredentials userCredientials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Bad Request" });
            }

            string query = "SELECT * from users WHERE username = @username";
            using NpgsqlCommand cmd = _databaseRepository.CallDatabase(query);

            cmd.Parameters.AddWithValue("username", userCredientials.Username);

            using NpgsqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                string storedHash = reader.GetString(2);
                bool correctPassword = BCrypt.Net.BCrypt.Verify(userCredientials.Password, storedHash);
                if (correctPassword)
                {
                    return Ok(new { isAdmin = reader.GetBoolean(3) });
                }
                else
                {
                    return Unauthorized(new { error = "Incorrect password" });
                }

            }

            return NotFound(new { error = "Username not found" });
        }

        [HttpPost("new")]
        public IActionResult InsertNewUser([FromBody] NewUser newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newUser.Password);

            string query = @"
            INSERT INTO users (username, password, isAdmin)
            VALUES (@username, @password, @isAdmin)";

            using NpgsqlCommand cmd = _databaseRepository.CallDatabase(query);

            cmd.Parameters.AddWithValue("username", newUser.Username);
            cmd.Parameters.AddWithValue("password", hashedPassword);
            cmd.Parameters.AddWithValue("isAdmin", newUser.IsAdmin);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                return Ok(new { message = "User created successfully." });
            }
            else
            {
                return BadRequest(new { error = "Couldn't add user" });
            }
        }
      
    }
}
