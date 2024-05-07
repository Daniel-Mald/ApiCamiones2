namespace CamionesAPI.Models.DTOs.User
{
    public class RegisterDTO : UserPasswordDTO
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
    }
}
