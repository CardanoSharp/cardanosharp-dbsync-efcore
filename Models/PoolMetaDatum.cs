using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class PoolMetaDatum
    {
        public PoolMetaDatum()
        {
            PoolUpdates = new HashSet<PoolUpdate>();
        }

        public long Id { get; set; }
        public string Url { get; set; }
        public byte[] Hash { get; set; }
        public long RegisteredTxId { get; set; }

        public virtual Tx RegisteredTx { get; set; }
        public virtual ICollection<PoolUpdate> PoolUpdates { get; set; }
    }
}
