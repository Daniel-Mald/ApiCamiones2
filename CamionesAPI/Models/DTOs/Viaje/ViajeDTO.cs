namespace CamionesAPI.Models.DTOs.Viaje
{
    public class ViajeDTO
    {
        public int Id { get; set; }
        public string NumeroViaje { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public string Destino { get; set; } = null!;
        public decimal GananciaMonetaria { get; set; }
        public DateOnly Fecha { get; set; }
        public DateOnly FechaApagar { get; set; }
        public int EstatusFactura { get; set; }
        public int TipoViaje { get; set; }
        public int Unidad { get; set; }
        public int Chofer { get; set; }
        public string NombreCliente { get; set; } = null!;
        public sbyte Semana { get; set; }
        public string? Observaciones { get; set; }
    }
}
