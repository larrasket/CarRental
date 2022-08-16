using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Models.DataModels;

public class BaseModel
{
    [Key] public long Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDateTime { get; set; }

    public BaseModel()
    {
        CreatedDateTime = DateTime.Now;
    }
}