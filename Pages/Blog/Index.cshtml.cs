using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EFIntegrated.Models;

namespace EFIntegrated.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly EFIntegrated.Models.DataContext _context;

        public IndexModel(EFIntegrated.Models.DataContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;
        
        public const int ITEMS_PER_PAGE = 15;

        [BindProperty(SupportsGet = true, Name = "_page")]
        public int currentPage { get; set; }

        public int countPages { get; set; }

        public async Task OnGetAsync(string SearchString)
        {
            if (_context.Article != null)
            {
                int totalArticle = await _context.Article.CountAsync();
                countPages = (int)Math.Ceiling((double)totalArticle / ITEMS_PER_PAGE);
                if (currentPage < 1)
                    currentPage = 1;
                if (currentPage > countPages)
                    currentPage = countPages;

                //Article = await _context.Article.ToListAsync();
                var query = (from article in _context.Article
                            orderby article.PublishDate descending
                            select article)
                            .Skip((currentPage-1)* ITEMS_PER_PAGE)
                            .Take(ITEMS_PER_PAGE);
                
                if (!string.IsNullOrEmpty(SearchString))
                {
                    Article = await query.Where(article => article.Title.Contains(SearchString)).ToListAsync();
                }
                else
                {
                    Article = await query.ToListAsync();
                }
                
            }
        }
    }
}
