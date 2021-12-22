using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stella.DEVO.Esame.DataAccess.Models;
using Stella.DEVO.Esame.DataAccess.Services;
using System.Collections.Generic;

namespace Stella.DEVO.Esame.WebApp.Pages
{
    public class ListDataModel : PageModel
    {
        private readonly IInsertData _insertData;
        public IEnumerable<SqlModel> ListData { get; set; }

        public ListDataModel(IInsertData insertData)
        {
            _insertData = insertData;
        }

        public IActionResult OnGet()
        {
            ListData = _insertData.GetData();

            return Page();
        }
    }
}
