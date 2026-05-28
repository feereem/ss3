public class CustomersController : ControllerBase
{
    private readonly BelleCroissantLyonnaisContext db;
    public CustomersController(BelleCroissantLyonnaisContext con)
    {
        db = con;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
    {
        var cu = db.Customers.AsQueryable();
        return await cu.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomerByID(int id)
    {
        var cu = await db.Customers.FindAsync(id);
        if (cu == null) return NotFound(new { message = "No Customer Found" });
        return cu;
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> AddCustomer(Customer cu)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        db.Customers.Add(cu);
        await db.SaveChangesAsync();
        return Ok("Add Success");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Customer>> EditCustomer(int id,Customer cu)
    {
        if (id != cu.CustomerId) return BadRequest(new { message = "Customer Information" });
        db.Entry(cu).State = EntityState.Modified;
        await db.SaveChangesAsync();
        return Ok("Edit Success");
    }

}
