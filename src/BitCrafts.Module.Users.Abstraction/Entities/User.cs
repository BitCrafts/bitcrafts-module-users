using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Entities;

public class User : BaseEntity
{
    [MaxLength(100)] public string FirstName { get; set; }

    [MaxLength(100)] public string LastName { get; set; }

    [MaxLength(100)] public string Email { get; set; }

    [MaxLength(50)] public string PhoneNumber { get; set; }

    public DateTime BirthDate { get; set; }

    [MaxLength(50)] public string NationalNumber { get; set; }

    [MaxLength(50)] public string PassportNumber { get; set; }

    [MaxLength(255)] public string HashedPassword { get; set; }

    [MaxLength(255)] public string PasswordSalt { get; set; }
    [IgnoreDataMember] public string Password { get; set; }

    public User()
    {
    }
}