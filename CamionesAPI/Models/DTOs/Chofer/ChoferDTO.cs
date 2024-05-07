namespace CamionesAPI.Models.DTOs.Chofer
{
    public class ChoferDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public decimal Sueldo { get; set; }
        public string IdentificadorChofer { get; set; } = null!;
    }
}
