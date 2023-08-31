using System.ComponentModel.DataAnnotations;

namespace Common.Models.Base;

public class IdBase
{
    [Key] public int Id { get; set; }
}