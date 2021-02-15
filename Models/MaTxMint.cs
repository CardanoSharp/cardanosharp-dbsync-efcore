using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class MaTxMint
    {
        public long Id { get; set; }
        public byte[] Policy { get; set; }
        public byte[] Name { get; set; }
        public decimal Quantity { get; set; }
        public long TxId { get; set; }

        public virtual Tx Tx { get; set; }
    }
}
