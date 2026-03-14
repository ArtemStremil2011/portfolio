using System;
using System.Text.RegularExpressions;

public class CustomerProfileDto
{
    private int _id;
    private string _email;
    private string _firstName;
    private string _lastName;
    private string _phoneNumber;
    private string _cardNumber;
    private string _role;
    private DateTime _createdAt;

    public int Id
    {
        get => _id;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Id must be greater than 0");
            _id = value;
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email is required");

            if (!IsValidEmail(value))
                throw new ArgumentException("Invalid email format");

            if (value.Length > 256)
                throw new ArgumentException("Email cannot exceed 256 characters");

            _email = value.Trim();
        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("First name is required");

            value = value.Trim();

            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("First name must be between 2 and 50 characters");

            if (!Regex.IsMatch(value, @"^[a-zA-Zа-яА-Я\s\-']+$"))
                throw new ArgumentException("First name can only contain letters, spaces, hyphens and apostrophes");

            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Last name is required");

            value = value.Trim();

            if (value.Length < 2 || value.Length > 50)
                throw new ArgumentException("Last name must be between 2 and 50 characters");

            if (!Regex.IsMatch(value, @"^[a-zA-Zа-яА-Я\s\-']+$"))
                throw new ArgumentException("Last name can only contain letters, spaces, hyphens and apostrophes");

            _lastName = value;
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number is required");

            value = value.Trim();

            // Простая валидация для демонстрации, можно заменить на более сложную логику
            if (!Regex.IsMatch(value, @"^\+?[1-9]\d{1,14}$"))
                throw new ArgumentException("Please enter a valid phone number in international format (e.g., +1234567890)");

            _phoneNumber = value;
        }
    }

    public string CardNumber
    {
        get => _cardNumber;
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim().Replace(" ", "").Replace("-", "");

                // Простая валидация номера карты (16 цифр)
                if (!Regex.IsMatch(value, @"^\d{16}$"))
                    throw new ArgumentException("Card number must contain exactly 16 digits");
            }

            _cardNumber = value;
        }
    }

    public string Role
    {
        get => _role;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Role is required");

            value = value.Trim();

            string[] validRoles = { "Customer", "Admin", "Manager" };
            if (!Array.Exists(validRoles, role => role.Equals(value, StringComparison.OrdinalIgnoreCase)))
                throw new ArgumentException($"Role must be one of: {string.Join(", ", validRoles)}");

            _role = value;
        }
    }

    public DateTime CreatedAt
    {
        get => _createdAt;
        set
        {
            if (value > DateTime.Now)
                throw new ArgumentException("CreatedAt cannot be in the future");

            if (value < new DateTime(2000, 1, 1))
                throw new ArgumentException("CreatedAt cannot be before year 2000");

            _createdAt = value;
        }
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}