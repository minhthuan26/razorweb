using EFIntegrated.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EFIntegrated.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DataContext _dataContext;
    public IndexModel(ILogger<IndexModel> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
    }

    public void OnGet()
    {
        var posts = (from p in _dataContext.Article
                     orderby p.PublishDate descending
                     select p).ToList();
        ViewData["posts"] = posts;
    }
}
