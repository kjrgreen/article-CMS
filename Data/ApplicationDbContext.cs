using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using article_CMS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace article_CMS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public async Task<List<Article>> GetAll()
        {
            List<Article> articles = await (
         from article in Articles.AsNoTracking() select article)
        .ToListAsync();
            return articles;
        }

        public DbSet<Article> Articles { get; set; }
    }
}
