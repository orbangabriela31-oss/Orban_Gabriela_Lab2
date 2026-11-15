using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Orban_Gabriela_Lab2.Data;
using Orban_Gabriela_Lab2.Models;

namespace Orban_Gabriela_Lab2.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly Orban_Gabriela_Lab2.Data.Orban_Gabriela_Lab2Context _context;

        public CreateModel(Orban_Gabriela_Lab2.Data.Orban_Gabriela_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "ID");
        ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
