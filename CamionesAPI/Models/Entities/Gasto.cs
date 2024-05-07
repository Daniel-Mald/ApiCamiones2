using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Gasto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Monto { get; set; }

    public virtual ICollection<Viajegasto> Viajegasto { get; set; } = new List<Viajegasto>();
}
