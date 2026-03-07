using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private static List<Customer> _customers = new List<Customer>();
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterCustomerDto registerDto)
    {
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

    [HttpGet("profile/{id}")]
    public IActionResult GetProfile(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer == null) return NotFound();

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

    [HttpGet("public/{id}")]
    public IActionResult GetPublicInfo(int id)
    {
        var customer = _customers.FirstOrDefault(c => c.Id == id);
        if (customer == null) return NotFound();

        var publicInfo = new CustomerPublicDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName
        };

        return Ok(publicInfo);
    }

}