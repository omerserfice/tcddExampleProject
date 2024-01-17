namespace tcddExampleProject.Models.DTO
{
    public class AddUserDto
    {
        public string TcNo { get; set; } = null!;
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public DateTime? DogumTarihi { get; set; }
    }
}
