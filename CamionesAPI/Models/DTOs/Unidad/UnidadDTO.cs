namespace CamionesAPI.Models.DTOs.Unidad
{
    public class UnidadDTO
    {
        public int Id { get; set; }
        public string Placa { get; set; } = null!;
        public string Capacidad { get; set; } = null!;
        public string Modelo { get; set; } = null!;
    }
}
