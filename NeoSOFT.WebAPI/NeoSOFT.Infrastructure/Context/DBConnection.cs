using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoSOFT.Infrastructure.Context
{
    public class DBConnection : IDbContext
    {
        public string CollectionName { get; set; } = string.Empty;
        public string ConnectionString { get ; set ; }=string.Empty;
        public string DatabaseName { get ; set; }=string.Empty;
    }
}
