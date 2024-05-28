using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KhumaloCraftEmporium.Models;
using KhumaloCraftEmporium.Data; // Replace YourNamespace with your actual namespace

public class ContactController : Controller
{
    private readonly KhumaloCraftDbContext _context;

    public ContactController(KhumaloCraftDbContext context)
    {
        _context = context;
    }

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Submit(Customer customer)
    {
        if (ModelState.IsValid)
        {
            // Save customer data to database
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Return a thank you message or prompt
            ViewBag.Message = "Thank you for your message! We will get in touch soon.";
            return View("Index"); // Return to the contact page
        }

        // If model is not valid, return to the contact page with errors
        return View("Index", customer);
    }
}
