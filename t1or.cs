public class OrdersController : ControllerBase
{
    private readonly BelleCroissantLyonnaisContext db;
    public OrdersController(BelleCroissantLyonnaisContext con)
    {
        db = con;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrder(string? str)
    {
        var q = db.Orders.Include(x => x.Customer).AsQueryable();
        if (!string.IsNullOrEmpty(str))
        {
            q = q.Where(x => x.TransactionId.ToString().Contains(str) ||
                             x.Customer.FirstName.Contains(str) ||
                             x.Customer.LastName.Contains(str) ||
                             x.OrderDate.ToString().Contains(str));
        }
        return await q.OrderBy(x => x.TransactionId).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrderByID(int id)
    {
        var order = await db.Orders.Include(x => x.Customer)
                                   .Include(x => x.OrderItems)
                                   .ThenInclude(x => x.Product)
                                   .FirstOrDefaultAsync(x => x.TransactionId == id);

        if(order == null) return NotFound("Order Not Found");
        return Ok(order);
    }
    [HttpPost]
    public async Task<ActionResult<Order>> AddOrder(Order order)
    {
        if (!ModelState.IsValid) return BadRequest("Order Information Incorrect");
        db.Orders.Add(order);
        await db.SaveChangesAsync();

        return Ok("Add Success");
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteOrder(int id)
    {
        var order = await db.Orders.FindAsync(id);
        if (order == null) return NotFound("Order Not Found");
        order.Status = "Completed";
        await db.SaveChangesAsync();
        return Ok("Order marker as completed");
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(int id)
    {
        var order = await db.Orders.FindAsync(id);
        if (order == null) return NotFound("Order Not Found");
        order.Status = "Completed";
        await db.SaveChangesAsync();
        return Ok("Order marker as Cancelled");
    }

}
