using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Unidad
{
    public int Id { get; set; }

    public string Placa { get; set; } = null!;

    public string Capacidad { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public DateOnly FechaDeRegistro { get; set; }

    public int IdDisponibilidad { get; set; }

    public virtual Disponibilidad IdDisponibilidadNavigation { get; set; } = null!;

    public virtual ICollection<Viaje> Viaje { get; set; } = new List<Viaje>();
}
