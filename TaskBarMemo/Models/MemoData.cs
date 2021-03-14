using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskBarMemo.Models
{
    public class MemoData
    {
        /// <summary>
        /// メモID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// メモ本文
        /// </summary>
        public string MemoBody { get; set; }

        /// <summary>
        /// メモをした時間
        /// </summary>
        public DateTime MemoTime { get; set; }
    }
}
