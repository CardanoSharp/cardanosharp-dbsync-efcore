using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class SlotLeader
    {
        public SlotLeader()
        {
            Blocks = new HashSet<Block>();
        }

        public long Id { get; set; }
        public byte[] Hash { get; set; }
        public long? PoolHashId { get; set; }
        public string Description { get; set; }

        public virtual PoolHash PoolHash { get; set; }
        public virtual ICollection<Block> Blocks { get; set; }
    }
}
