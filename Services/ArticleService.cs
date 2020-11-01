using article_CMS.Data;
using article_CMS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace article_CMS.Services
{
    public class ArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Article>> GetAll()
        {
            Console.WriteLine("before");
            List<Article> articles = await (
         from article in _context.Articles.AsNoTracking() select article)
        .ToListAsync();
            Console.Write("after");
            return articles;
        }

//        public async Task<List<Article>> GetById()
 //       {
  //          return await (from article in _context.Articles.AsNoTracking() select article where).ToListAsync();
      //  }
    }
}
