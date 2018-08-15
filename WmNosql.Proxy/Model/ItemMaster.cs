using System;
using System.Collections.Generic;
using System.Text;

namespace WmNosql.Proxy.Model
{
    public class ItemMaster
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string BaseUOM { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserEmail { get; set; }
    }
}
