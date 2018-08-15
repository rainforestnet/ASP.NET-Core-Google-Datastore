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
    public class EditModel : PageModel
    {

        [BindProperty]
        public ItemMaster Item { get; set; }

        public async Task<ActionResult> OnGetAsync(long id)
        {
            /* 
                 2 - Namespace in Datastore. It is used as TenantID in multi-tenant system here
                 PROD - Environment. Can be Production, Testing, Development or etc  
            */
            ItemMasterProxy iprox = new ItemMasterProxy("2", "PROD");
            Item = await iprox.DetailAsync(id);

            if (Item == null)
            {
                return RedirectToPage("/Item/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                /* 
                     2 - Namespace in Datastore. It is used as TenantID in multi-tenant system here
                     PROD - Environment. Can be Production, Testing, Development or etc  
                */
                ItemMasterProxy iprox = new ItemMasterProxy("2", "PROD");
                await iprox.UpdateAsync(
                    Item.Id, 
                    Item.Code, 
                    Item.Description,
                    Item.Brand,
                    Item.Model,
                    Item.BaseUOM,
                    "user@mail.com");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToPage("/Item/Index");
        }
    }
}