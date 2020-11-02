using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using article_CMS.Data;
using article_CMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace article_CMS.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            return await _context.Articles.ToListAsync();
        }
    }
}
