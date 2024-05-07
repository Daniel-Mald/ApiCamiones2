using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Viaje
{
    public int Id { get; set; }

    public string NombreCliente { get; set; } = null!;

    public string NumeroViaje { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string Origen { get; set; } = null!;

    public string Destino { get; set; } = null!;

    public int IdTipoViaje { get; set; }

    public decimal GananciaMonetaria { get; set; }

    public DateOnly FechaApagar { get; set; }

    public int IdFactura { get; set; }

    public int UnidadId { get; set; }

    public int ChoferId { get; set; }

    public sbyte Semana { get; set; }

    public string? Observaciones { get; set; }

    public int IdDisponibilidad { get; set; }

    public virtual Chofer Chofer { get; set; } = null!;

    public virtual Disponibilidad IdDisponibilidadNavigation { get; set; } = null!;

    public virtual Factura IdFacturaNavigation { get; set; } = null!;

    public virtual Tipodeviaje IdTipoViajeNavigation { get; set; } = null!;

    public virtual Unidad Unidad { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();

    public virtual ICollection<Viajegasto> Viajegasto { get; set; } = new List<Viajegasto>();
}
