using System.ComponentModel.DataAnnotations.Schema;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Entities;

public class Family : BaseEntity
{
    public int ResponsibleId { get; set; }
    [ForeignKey("ResponsibleId")]
    public User Responsible { get; set; }
    public ICollection<User> Members { get; set; }
}
