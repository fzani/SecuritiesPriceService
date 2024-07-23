using System.ComponentModel.DataAnnotations;

namespace BNP.SecuritiesPriceService.Models;

public class Security
{
    public int Id { get; set; }

    [StringLength(12)]
    public string ISIN { get; set; }

    public decimal Price { get; set; }
}

