namespace Yggdrasil.Domain.Entities
{
    public class CAT_TasaIva
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(8, 4)")]
        public decimal ValorTasa { get; set; }

        [Required]
        [MaxLength(30)]
        public string NomTasaIva { get; set; } = "";

        public bool Activo { get; set; }
    }
}
