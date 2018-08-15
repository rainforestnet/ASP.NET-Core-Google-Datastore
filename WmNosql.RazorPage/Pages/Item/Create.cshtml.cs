using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WmNosql.Proxy.Core;
using WmNosql.Proxy.Model;

namespace WmNosql.RazorPage.Pages.Item
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public ItemMaster Item { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            /* 
                 2 - Namespace in Datastore. It is used as TenantID in multi-tenant system here
                 PROD - Environment. Can be Production, Testing, Development or etc  
            */
            ItemMasterProxy iprox = new ItemMasterProxy("2", "PROD"); 

            await iprox.InsertAsync(
                Item.Code, 
                Item.Description, 
                Item.Brand,
                Item.Model,
                Item.BaseUOM,
                "user@mail.com");
            return RedirectToPage("/Item/Index");
        }
    }
}
 