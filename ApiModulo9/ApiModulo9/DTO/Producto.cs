namespace ApiModulo9.DTO
{
    public class Producto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public float? Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }

        public Rating? Rating { get; set; } = new Rating();
    }

    public class ProductResponseData
    {
        public Producto[]? Data { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
