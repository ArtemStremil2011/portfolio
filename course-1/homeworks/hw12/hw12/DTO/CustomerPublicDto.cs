using System;
using System.Text.RegularExpressions;

public class CustomerPublicDto
{
    private int _id;
    private string _firstName;
    private string _lastName;

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
}