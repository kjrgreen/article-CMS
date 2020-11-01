using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using article_CMS.Data;
using article_CMS.Models;

namespace article_CMS.Pages
{
    public class IndexModel : PageModel
    {
        private readonly article_CMS.Data.ApplicationDbContext _context;

        public IndexModel(article_CMS.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }

        public async Task OnGetAsync()
        {
            Article = await _context.Articles.ToListAsync();
        }
    }
}
