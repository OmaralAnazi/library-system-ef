using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Data.Entities;

public abstract class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; } = null;
    public string Key { get; set; } = Guid.NewGuid().ToString();
}