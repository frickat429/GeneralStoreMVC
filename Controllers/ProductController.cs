using GeneralStoreMVC.Data;
using GeneralStoreMVC.Models.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeneralStoreMVC.Controllers ; 

public class ProductController : Controller
{
private readonly GeneralStoreDbContext _context;
public ProductController(GeneralStoreDbContext context) 
{
    _context = context;
}    

//Get product 
public async Task<IActionResult>  Index() 
{
    var products = await _context.Products
    .Select(p => new ProductIndexVM{
        Id = p.Id,
        Name = p.Name, 
        QuantityInStock = p.QuantityInStock
    }) 
    .ToListAsync();
    return View(products);
}
}