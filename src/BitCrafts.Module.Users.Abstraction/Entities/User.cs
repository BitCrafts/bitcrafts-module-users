using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Entities;

public class User : BaseEntity
{
    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(50)]
    public string PhoneNumber { get; set; }

    public DateTime BirthDate { get; set; }

    [MaxLength(50)]
    public string NationalNumber { get; set; }

    [MaxLength(50)]
    public string PassportNumber { get; set; }

    public UserAccount UserAccount { get; set; }

    public ICollection<Family> Families { get; set; }
    public FamilyRole FamilyRole { get; set; }

    public User()
    {
        UserAccount = new UserAccount();
        Families = new Collection<Family>();
    }
}