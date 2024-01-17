using System;
using System.Collections.Generic;

namespace tcddExampleProject.Models
{
    public partial class User
    {
        
        public int Id { get; set; }
        public string TcNo { get; set; } = null!;
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public DateTime? DogumTarihi { get; set; }
    }
}
