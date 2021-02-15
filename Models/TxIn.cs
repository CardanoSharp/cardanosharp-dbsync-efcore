using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class TxIn
    {
        public long Id { get; set; }
        public long TxInId { get; set; }
        public long TxOutId { get; set; }
        public short TxOutIndex { get; set; }

        public virtual Tx TxInNavigation { get; set; }
        public virtual Tx TxOut { get; set; }
    }
}
