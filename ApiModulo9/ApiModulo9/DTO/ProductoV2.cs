namespace ApiModulo9.DTO
{
    public class ProductoV2
    {
        public Guid InternalId { get; set; } = new Guid();
        public int? Id { get; set; }
        public string? Title { get; set; }
        public float? Price { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? Image { get; set; }
     
    }

    public class ProductV2ResponseData
    {
        public ProductoV2[]? Data { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
