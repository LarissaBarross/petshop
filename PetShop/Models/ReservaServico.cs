using System;

namespace PetShop.Models;

public partial class ReservaServico
{
    public int Codigo { get; set; }

    public DateTime DataHora { get; set; }

    public decimal Valor { get; set; }

    public string StatusServico { get; set; } = string.Empty;

    public int ClienteId { get; set; }

    public int VeterinarioId { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Veterinario? Veterinario { get; set; }
}
