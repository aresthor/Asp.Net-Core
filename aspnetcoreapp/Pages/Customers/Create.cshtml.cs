using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesContacts.Data;
namespace RazorPagesContacts.Pages
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateModel(AppDbContext db)
        {
            _db = db;
        }

        [TempData]
        public string Message { get; set; }

        [BindProperty( SupportsGet = true )]
        public Customer Customer { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _db.Customers.Add(Customer);
            await _db.SaveChangesAsync();
            Message = $"Customer {Customer.Name} added.";
            return RedirectToPage("/Index");
        }
    }
}