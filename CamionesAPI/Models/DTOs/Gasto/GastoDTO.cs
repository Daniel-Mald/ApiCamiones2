namespace CamionesAPI.Models.DTOs.Gasto
{
    public class GastoDTO
    {
        public int IdViaje { get; set; } = 0;
        public string Nombre { get; set; } = null!;
        public decimal Monto { get; set; }
        public int IdGasto { get; set; } = 0;
    }
}
