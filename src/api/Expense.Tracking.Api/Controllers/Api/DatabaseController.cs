using Expense.Tracking.Api.Domain.Models;
using Expense.Tracking.Api.Infrastrucure.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense.Tracking.Api.Controllers.Api
{
    [Route("api/database")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public DatabaseController(ExpenseContext context)
        {
            _context = context;
        }

        [HttpGet("initialise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InitialiseDatabase()
        {
            if (!_context.Category.Any() && !_context.ImportRule.Any())
            {
                var rent = new Category { Name = "Rent" };
                var energyBill = new Category { Name = "Energy Bill" };
                var phoneBill = new Category { Name = "Phone Bill" };
                var carFuel = new Category { Name = "Car Fuel" };
                var carInsurance = new Category { Name = "Car Insurance" };
                var internetBill = new Category { Name = "Internet Bill" };
                var waterBill = new Category { Name = "Water Bill" };
                var supermarketHelloFresh = new Category { Name = "Supermarket" };
                var subscriptions = new Category { Name = "Subscriptions" };
                var gymMembership = new Category { Name = "Gym Membership" };

                await _context.Category.AddRangeAsync(rent, energyBill, phoneBill, carFuel, carInsurance, internetBill, waterBill, supermarketHelloFresh, subscriptions, gymMembership);

                await _context.SaveChangesAsync();

                _context.Add(new ImportRule() { Name = "Rent", CategoryId = rent.Id, Condition = "Type == \"Automatic Payment\" && Details.Contains(\"Faceup Rent\")" });
                _context.Add(new ImportRule() { Name = "Energy Bill", CategoryId = energyBill.Id, Condition = "Type == \"Direct Debit\" && Details.Contains(\"Contact Energy\")" });
                _context.Add(new ImportRule() { Name = "Water Bill", CategoryId = waterBill.Id, Condition = "Type == \"Payment\" && Details.Contains(\"Faceup Rent\")" });
                _context.Add(new ImportRule() { Name = "Internet Bill", CategoryId = internetBill.Id, Condition = "Details.Contains(\"2Degrees\")" });
                _context.Add(new ImportRule() { Name = "Phone Bill", CategoryId = phoneBill.Id, Condition = "Details.Contains(\"One Nz Ptpy\")" });
                _context.Add(new ImportRule() { Name = "Car Insurance", CategoryId = carInsurance.Id, Condition = "Details.Contains(\"Aa Insurance Pre\")\r\n" });
                _context.Add(new ImportRule() { Name = "Car Fuel - BP", CategoryId = carFuel.Id, Condition = "Details.Contains(\"Bp Connect\") || Details.Contains(\"Bp 2Go\")" });
                _context.Add(new ImportRule() { Name = "Car Fuel - Pak n Save Fuel", CategoryId = carFuel.Id, Condition = "Details.Contains(\"Pak N Save Fuel West\")" });
                _context.Add(new ImportRule() { Name = "Car Fuel - Gull", CategoryId = carFuel.Id, Condition = "Details.StartsWith(\"Gull\")" });
                _context.Add(new ImportRule() { Name = "Car Fuel - Mobil", CategoryId = carFuel.Id, Condition = "Details.StartsWith(\"Mobil\")" });
                _context.Add(new ImportRule() { Name = "Supermarket - Countdown", CategoryId = supermarketHelloFresh.Id, Condition = "Details.StartsWith(\"Countdown\")" });
                _context.Add(new ImportRule() { Name = "Supermarket - Pak n Save", CategoryId = supermarketHelloFresh.Id, Condition = "Details.StartsWith(\"Pak N Save\") && !Details.Contains(\"Fuel\")" });
                _context.Add(new ImportRule() { Name = "Supermarket - New World", CategoryId = supermarketHelloFresh.Id, Condition = "Details.StartsWith(\"New World\")" });
                _context.Add(new ImportRule() { Name = "Supermarket - Woolworths", CategoryId = supermarketHelloFresh.Id, Condition = "Details.StartsWith(\"Woolworths\")" });
                _context.Add(new ImportRule() { Name = "Supermarket - Fresh Choice", CategoryId = supermarketHelloFresh.Id, Condition = "Details.StartsWith(\"Fresh Choice\")" });
                _context.Add(new ImportRule() { Name = "Supermarket - Hellofresh", CategoryId = supermarketHelloFresh.Id, Condition = "Details.StartsWith(\"Hellofresh\")" });
                _context.Add(new ImportRule() { Name = "Subscriptions - Spotify", CategoryId = subscriptions.Id, Condition = "Details.StartsWith(\"Spotify\")" });
                _context.Add(new ImportRule() { Name = "Subscriptions - Disney Plus", CategoryId = subscriptions.Id, Condition = "Details.StartsWith(\"Disney Plus\")" });
                _context.Add(new ImportRule() { Name = "Subscriptions - Amazon Prime", CategoryId = subscriptions.Id, Condition = "Details.Contains(\"Amazon Pri\")" });
                _context.Add(new ImportRule() { Name = "Subscriptions - Google Storage", CategoryId = subscriptions.Id, Condition = "Details.StartsWith(\"Google Storage\")" });
                _context.Add(new ImportRule() { Name = "Gym Membership", CategoryId = gymMembership.Id, Condition = "Type == \"Credit Card\" && Details.Contains(\"Anytim\")" });

                await _context.SaveChangesAsync();
            }

            return Ok();
        }
    }
}
