using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class TxMetadatum
    {
        public long Id { get; set; }
        public decimal Key { get; set; }
        public string Json { get; set; }
        public long TxId { get; set; }
        public byte[] Bytes { get; set; }

        public virtual Tx Tx { get; set; }
    }
}
