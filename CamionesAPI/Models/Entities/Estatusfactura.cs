using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Estatusfactura
{
    public int Id { get; set; }

    public string Estatus { get; set; } = null!;

    public virtual ICollection<Factura> Factura { get; set; } = new List<Factura>();
}
