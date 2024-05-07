namespace CamionesAPI.Models.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        //public int? IdViajes { get; set; }
        public int IdDisponibilidad { get; set; }
    }
}
