using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class PoolRetire
    {
        public long Id { get; set; }
        public long HashId { get; set; }
        public int CertIndex { get; set; }
        public long AnnouncedTxId { get; set; }
        public int RetiringEpoch { get; set; }

        public virtual Tx AnnouncedTx { get; set; }
        public virtual PoolHash Hash { get; set; }
    }
}
