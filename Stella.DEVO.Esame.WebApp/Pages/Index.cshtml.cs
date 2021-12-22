
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stella.DEVO.Esame.DataAccess.Models;
using Stella.DEVO.Esame.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stella.DEVO.Esame.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWorker _sender;

        [BindProperty]
        public InputText Input { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, IWorker sender)
        {
            _logger = logger;
            _configuration = configuration;
            _sender = sender;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            Input.Date = DateTime.Now;

            if (Input.Text == null)
                Input.Text = " ";

            _sender.SendToQueue(Input);

            return RedirectToPage("Index");
        }
    }
}
