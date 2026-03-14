using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private static List<Customer> _customers = new List<Customer>();

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterCustomerDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = new Customer
            {
                Id = _customers.Count + 1,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                Password = registerDto.Password,
                CardNumber = "",
                Role = "User",
                IsBlocked = false,
                CreatedAt = DateTime.Now
            };

            _customers.Add(customer);

            var profile = new CustomerProfileDto
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                CardNumber = customer.CardNumber,
                Role = customer.Role,
                CreatedAt = customer.CreatedAt
            };

            return Ok(profile);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred", details = ex.Message });
        }
    }

    [HttpGet("profile/{id}")]
    public IActionResult GetProfile(int id)
    {
        try
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound(new { error = $"Customer with id {id} not found" });

            var profile = new CustomerProfileDto
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNumber = customer.PhoneNumber,
                CardNumber = customer.CardNumber,
                Role = customer.Role,
                CreatedAt = customer.CreatedAt
            };

            return Ok(profile);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("public/{id}")]
    public IActionResult GetPublicInfo(int id)
    {
        try
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound(new { error = $"Customer with id {id} not found" });

            var publicInfo = new CustomerPublicDto
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            return Ok(publicInfo);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("profile/{id}")]
    public IActionResult UpdateProfile(int id, [FromBody] CustomerProfileDto profileDto)
    {
        try
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound(new { error = $"Customer with id {id} not found" });

            var updatedProfile = new CustomerProfileDto
            {
                Id = customer.Id,
                Email = profileDto.Email ?? customer.Email,
                FirstName = profileDto.FirstName ?? customer.FirstName,
                LastName = profileDto.LastName ?? customer.LastName,
                PhoneNumber = profileDto.PhoneNumber ?? customer.PhoneNumber,
                CardNumber = profileDto.CardNumber ?? customer.CardNumber,
                Role = customer.Role,
                CreatedAt = customer.CreatedAt
            };

            customer.Email = updatedProfile.Email;
            customer.FirstName = updatedProfile.FirstName;
            customer.LastName = updatedProfile.LastName;
            customer.PhoneNumber = updatedProfile.PhoneNumber;
            customer.CardNumber = updatedProfile.CardNumber;

            return Ok(updatedProfile);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("all")]
    public IActionResult GetAllCustomers()
    {
        try
        {
            var publicInfos = _customers.Select(c => new CustomerPublicDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName
            }).ToList();

            return Ok(publicInfos);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("check-email")]
    public IActionResult CheckEmail([FromQuery] string email)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(new { error = "Email is required" });

            var emailExists = _customers.Any(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            return Ok(new
            {
                email = email,
                isAvailable = !emailExists,
                message = emailExists ? "Email is already taken" : "Email is available"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "An unexpected error occurred", details = ex.Message });
        }
    }
}