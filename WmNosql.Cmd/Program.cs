using System;
using WmNosql.Proxy.Core;

namespace WmNosql.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            ItemMasterProxy iprox = new ItemMasterProxy("2", "PROD");

            for(int i=1; i < 50; i++)
            {
                var code = (1000 + i).ToString();
                var desc = $"Item {code}";
                var brand = "Avenger";
                var model = $"Justice{i}";
                var baseUOM = "UNIT";

                var id = iprox.InsertAsync(code, desc, brand, model, baseUOM, "user@mail.com").Result;
                Console.WriteLine($"{id}");
            }

            foreach (var x in iprox.ListAsync().Result)
            {
                Console.WriteLine($"ID = {x.Id} Code = {x.Code} Name = {x.Description}");
            }
        }
    }
}
