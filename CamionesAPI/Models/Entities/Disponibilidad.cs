using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Disponibilidad
{
    public int Id { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Chofer> Chofer { get; set; } = new List<Chofer>();

    public virtual ICollection<Unidad> Unidad { get; set; } = new List<Unidad>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();

    public virtual ICollection<Viaje> Viaje { get; set; } = new List<Viaje>();
}
