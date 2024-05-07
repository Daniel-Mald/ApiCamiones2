using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Factura
{
    public int Id { get; set; }

    public int IdEstatus { get; set; }

    public string NumeroFactura { get; set; } = null!;

    public decimal Monto { get; set; }

    public virtual Estatusfactura IdEstatusNavigation { get; set; } = null!;

    public virtual ICollection<Viaje> Viaje { get; set; } = new List<Viaje>();
}
