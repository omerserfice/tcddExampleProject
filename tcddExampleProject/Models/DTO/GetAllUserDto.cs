namespace tcddExampleProject.Models.DTO
{
    public class GetAllUserDto
    {
        public Parameters _parametters { get; set; }
        public GetAllUserDto()
        {
            _parametters = new Parameters();
        }
        public int Id { get; set; }
        public string TcNo { get; set; } = null!;
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public DateTime? DogumTarihi { get; set; }
    }
}
