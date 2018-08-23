using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CPQaunt.Entitest
{
    public class MessageScriptModel
    {
        /// <summary>
        /// 消息类型，list 代表 生成的code集合。 log代表 返回的只是信息
        /// </summary>
         public MessageType type { get; set; }

        /// <summary>
        /// 如果类型为list时，加载生成的下注号码
        /// </summary>
        public List<NumberModel> numbers { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
    }
}
