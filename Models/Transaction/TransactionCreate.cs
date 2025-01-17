using System.ComponentModel.DataAnnotations;

namespace GeneralStoreMVC.Models.Transaction;

public class TransactionCreateVM
{
    [Required] 
    public int CustomerId {get; set;} 

    [Required] 
    [Display(Name = "Products")]
    public int ProdcutId { get; set; } 
    [Required] 
    [Range(1, 100)] 
    public int Quantity { get; set; }
}