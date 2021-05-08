using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Systemywp.Api.Pages
{
    public class BasePage : PageModel
    {
        public IList<string> CustomErrors { get; set; } = new List<string>();


    }
}