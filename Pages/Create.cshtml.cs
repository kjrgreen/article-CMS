using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using article_CMS.Data;
using article_CMS.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.IO;

namespace article_CMS.Pages
{
    public class CreateModel : PageModel
    {
        private readonly article_CMS.Data.ApplicationDbContext _context;

        public CreateModel(article_CMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Article Article { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Upload == null) return Page(); //Guard clause; don't do anything unless an image is included
            if (Upload.ContentType != "image/jpeg") return Page(); //Don't allow users to upload files that are not jpegs. I'm not certain this is sufficient validation.

            Article.Created = DateTime.UtcNow;
            _context.Articles.Add(Article);
            await _context.SaveChangesAsync();

            var file = Path.Combine("wwwroot", "Uploads", Article.Id.ToString() + ".jpg");
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }

            return RedirectToPage("./Index");
        }
    }
}
