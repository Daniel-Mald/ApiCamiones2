using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Viajegasto
{
    public int Id { get; set; }

    public int IdViaje { get; set; }

    public int IdGasto { get; set; }

    public virtual Gasto IdGastoNavigation { get; set; } = null!;

    public virtual Viaje IdViajeNavigation { get; set; } = null!;
}
