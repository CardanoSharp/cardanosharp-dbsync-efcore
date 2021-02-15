using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class Epoch
    {
        public long Id { get; set; }
        public decimal OutSum { get; set; }
        public decimal Fees { get; set; }
        public int TxCount { get; set; }
        public int BlkCount { get; set; }
        public int No { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
