namespace GameStore_API.DTOs
{
    public class ItemRequestResponse
    {
        public ItemRequestResponse(int id, string? name, DateTime releaseDate, string? genre)
        {
            Id = id;
            Name = name;
            ReleaseDate = releaseDate;
            Genre = genre;
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
