using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public int? IdViajes { get; set; }

    public int IdDisponibilidad { get; set; }

    public virtual ICollection<Bitacora> Bitacora { get; set; } = new List<Bitacora>();

    public virtual Disponibilidad IdDisponibilidadNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual Viaje? IdViajesNavigation { get; set; }
}
