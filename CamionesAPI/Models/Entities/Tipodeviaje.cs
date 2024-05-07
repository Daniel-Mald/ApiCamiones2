using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Tipodeviaje
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Viaje> Viaje { get; set; } = new List<Viaje>();
}
