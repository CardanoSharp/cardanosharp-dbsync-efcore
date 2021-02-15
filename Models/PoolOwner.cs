using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class PoolOwner
    {
        public long Id { get; set; }
        public byte[] Hash { get; set; }
        public long PoolHashId { get; set; }
        public long RegisteredTxId { get; set; }

        public virtual PoolHash PoolHash { get; set; }
        public virtual Tx RegisteredTx { get; set; }
    }
}
