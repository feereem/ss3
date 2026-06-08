public class ProductsController : ControllerBase
{
    private readonly BelleCroissantLyonnaisContext db;
    public ProductsController(BelleCroissantLyonnaisContext con)
    {
        db = con;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct(string? str)
    {
        var pd = db.Products.AsQueryable();
        if (!string.IsNullOrEmpty(str))
        {
            pd = pd.Where(x => x.ProductName.Contains(str) || x.Category.Contains(str));
        }
        return await pd.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProductByID(int id)
    {
        var pd = await db.Products.FindAsync(id);
        if(pd == null) return NotFound(new {message = "No Product Found"});
        return pd;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product pd)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        db.Products.Add(pd);
        await db.SaveChangesAsync();
        return Ok("Add Success");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> EditProduct(int id,Product pd)
    {
        if (id != pd.ProductId) return BadRequest(new { message = "Product Information is incorrect" });
        db.Entry(pd).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return Ok("Edit Success");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var pd = await db.Products.FindAsync(id);
        if (pd == null) return NotFound(new { message = "No Product Found" });
        await db.SaveChangesAsync();
        return Ok("Delete Success");
    }
}
