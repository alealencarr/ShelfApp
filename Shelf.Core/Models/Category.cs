namespace Shelf.Core.Models;

public class Category : Root
{
    public string Title { get; set; } = string.Empty;
    public string? Description  { get; set; } 
    public string UserID { get; set; } = string.Empty;
}