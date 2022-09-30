using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DataModels;

public class Creator
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public Creator()
    {
        CreatedDateTime = DateTime.Now;
    }

    public ClientUser User { get; set; }
    public long UserId { get; set; }
}