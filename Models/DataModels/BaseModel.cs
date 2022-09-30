using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Models.DataModels;

public class BaseModel
{
    [Key] public long Id { get; set; }

    public Creator? Creation { get; set; }
}