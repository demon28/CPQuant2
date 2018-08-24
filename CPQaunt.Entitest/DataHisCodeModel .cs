using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPQaunt.Entitest
{
    /// <summary>
    /// 数据库实体
    /// </summary>
   public class DataHisCodeModel
    {
        public int cid { get; set; }

        public string expect { get; set; }

        public string opencode { get; set; }

        public string datetime { get; set; }
    }
}
