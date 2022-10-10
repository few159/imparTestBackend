namespace imparTest.Models.DTO
{
    public class CarUpdate
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public int? photoId { get; set; }
        public Photo? photo { get; set; }
    }
}
