using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Chofer
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public decimal Sueldo { get; set; }

    public string IdentificadorChofer { get; set; } = null!;

    public int IdDisponibilidad { get; set; }

    public virtual Disponibilidad IdDisponibilidadNavigation { get; set; } = null!;

    public virtual ICollection<Viaje> Viaje { get; set; } = new List<Viaje>();
}
