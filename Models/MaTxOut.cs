using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class MaTxOut
    {
        public long Id { get; set; }
        public byte[] Policy { get; set; }
        public byte[] Name { get; set; }
        public decimal Quantity { get; set; }
        public long TxOutId { get; set; }

        public virtual TxOut TxOut { get; set; }
    }
}
