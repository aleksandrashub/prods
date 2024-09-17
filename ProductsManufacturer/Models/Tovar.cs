using System;
using System.Collections.Generic;

namespace ProductsManufacturer.Models;

public partial class Tovar
{
    public string? NameTovar { get; set; }

    public float? Cost { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public int? IdManufacturer { get; set; }

    public int? IdStatus { get; set; }

    public int IdTovar { get; set; }

    public int? IdMainDop { get; set; }

    public virtual TovarDop? IdMainDopNavigation { get; set; }

    public virtual Manufacturer? IdManufacturerNavigation { get; set; }

    public virtual Status? IdStatusNavigation { get; set; }

    public virtual ICollection<SaleTovar> SaleTovars { get; set; } = new List<SaleTovar>();

    public virtual ICollection<TovarDop> TovarDopIdDopTovNavigations { get; set; } = new List<TovarDop>();

    public virtual ICollection<TovarDop> TovarDopIdMainTovNavigations { get; set; } = new List<TovarDop>();
}
