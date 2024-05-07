using System;
using System.Collections.Generic;

namespace CamionesAPI.Models.Entities;

public partial class Bitacora
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public DateOnly Fecha { get; set; }

    public string Movimiento { get; set; } = null!;

    public string Tabla { get; set; } = null!;

    public int UsuarioId { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
