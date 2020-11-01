using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using article_CMS.Data;
using article_CMS.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace article_CMS.Pages
{
    public class EditModel : PageModel
    {
        private readonly article_CMS.Data.ApplicationDbContext _context;

        public EditModel(article_CMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Article Article { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Article = await _context.Articles.FirstOrDefaultAsync(m => m.Id == id);

            if (Article == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Upload != null && Upload.ContentType != "image/jpeg") return Page(); //Don't allow users to upload files that are not jpegs. I'm not certain this is sufficient validation.

            Article.LastUpdated = DateTime.UtcNow;
            _context.Attach(Article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(Article.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (Upload != null)
            {
                var file = Path.Combine("Uploads", Article.Id.ToString() + ".jpg");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
