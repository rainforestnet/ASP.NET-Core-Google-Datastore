using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using WmNosql.Proxy.Core;
using WmNosql.Proxy.Model;

namespace WmNosql.RazorPage.Pages.Item
{
    public class IndexModel : PageModel
    {
        public IList<ItemMaster> Items { get; set; }

        public async Task OnGetAsync()
        {
            /* 
                 2 - Namespace in Datastore. It is used as TenantID in multi-tenant system here
                 PROD - Environment. Can be Production, Testing, Development or etc  
            */
            ItemMasterProxy iprox = new ItemMasterProxy("2", "PROD");
            Items = await iprox.ListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            /* 
                 2 - Namespace in Datastore. It is used as TenantID in multi-tenant system here
                 PROD - Environment. Can be Production, Testing, Development or etc  
            */
            ItemMasterProxy iprox = new ItemMasterProxy("2", "PROD");
            await iprox.DeleteAsync(id);

            return RedirectToPage();
        }
    }
}