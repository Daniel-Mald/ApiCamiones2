using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Vwvistageneral
{
    public int Id { get; set; }

    public string Cliente { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string Origen { get; set; } = null!;

    public string Destino { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string Unidad { get; set; } = null!;

    public decimal Monto { get; set; }

    public string NumeroViaje { get; set; } = null!;

    public string NumeroFactura { get; set; } = null!;

    public DateOnly FechaApagar { get; set; }

    public string Estatus { get; set; } = null!;

    public sbyte Semana { get; set; }

    public string? Observaciones { get; set; }
}
