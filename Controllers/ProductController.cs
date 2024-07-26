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

//Get: Prodcut details
public async Task<IActionResult> Details(int id) 
{
    if (id == null) 
    {
        return NotFound();
    }

    var product = await _context.Products.FindAsync(id);
    if (product == null) 
    {
        return NotFound();
    } 

    var vm = new ProductDetailVM
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        QuantityInStock = product.QuantityInStock
    };

    return View(vm);
    } 

    // Get: Product/Create 
         // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

    //Post: Post/Create 
  [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,QuantityInStock,Price")] ProductCreateVM product)
        {
            if (ModelState.IsValid)
            {
                var entity = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    QuantityInStock = product.QuantityInStock
                };

                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        
} 

       // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,QuantityInStock,Price")] ProductEditVM product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var entity = await _context.Products.FindAsync(id);
                if (entity is null)
                    return RedirectToAction(nameof(Index));

                entity.Name = product.Name;
                entity.Price = product.Price;
                entity.QuantityInStock = product.QuantityInStock;

                _context.Products.Update(entity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
}