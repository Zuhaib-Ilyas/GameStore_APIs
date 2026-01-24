
namespace GameStore_API.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        public string? Genre { get;  set; }
        public decimal Price { get;  set; }
        public DateTime ReleaseDate { get;  set; }
    }
}
