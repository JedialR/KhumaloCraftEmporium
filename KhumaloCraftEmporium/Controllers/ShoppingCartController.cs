using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KhumaloCraftEmporium.Data;
using KhumaloCraftEmporium.Models;

namespace KhumaloCraftEmporium.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly KhumaloCraftDbContext _context;

        public ShoppingCartController(KhumaloCraftDbContext context)
        {
            _context = context;
        }

        // ShoppingCart
        public async Task<IActionResult> Index()
        {
            var shoppingCartItems = await GetShoppingCartItemsAsync();
            return View(shoppingCartItems);
        }

        // Helper method to retrieve shopping cart items
        private async Task<List<OrderItem>> GetShoppingCartItemsAsync()
        {
            // Dummy logic to retrieve shopping cart items for the current user
            var shoppingCartItems = await _context.OrderItems
                .Where(oi => oi.Order.CustomerID == 1) // Dummy Logic
                .Include(oi => oi.Product)
                .ToListAsync();

            return shoppingCartItems;
        }

        // Add Function
        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(); // Dummy logic
            if (customer == null)
            {
                return NotFound("Customer not found.");
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.CustomerID == customer.ID && o.OrderDate == null);

            if (order == null)
            {
                // Create a new order if none exists
                order = new Order
                {
                    CustomerID = customer.ID,
                    OrderDate = DateTime.UtcNow, // Set the order date to current date/time
                    OrderItems = new List<OrderItem>()
                };

                _context.Orders.Add(order);
            }

            // Check if the product is already in the order items
            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.ProductID == id);

            if (orderItem != null)
            {
                // Update quantity if the product is already in the cart
                orderItem.Quantity++;
            }
            else
            {
                // Add new order item
                orderItem = new OrderItem
                {
                    ProductID = id,
                    Quantity = 1, // Initial quantity is 1
                    Price = product.Price
                };

                order.OrderItems.Add(orderItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Remove Function
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Checkout Function
        [HttpGet]
        public IActionResult Checkout()
        {
            // Simulate order placed message
            TempData["OrderPlacedMessage"] = "Order has been placed successfully!";

            return RedirectToAction(nameof(Index));
        }
    }
}
