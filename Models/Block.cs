using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class Block
    {
        public Block()
        {
            EpochParams = new HashSet<EpochParam>();
            EpochStakes = new HashSet<EpochStake>();
            InversePrevious = new HashSet<Block>();
            OrphanedRewards = new HashSet<OrphanedReward>();
            Rewards = new HashSet<Reward>();
            Txes = new HashSet<Tx>();
        }

        public long Id { get; set; }
        public byte[] Hash { get; set; }
        public int? EpochNo { get; set; }
        public int? SlotNo { get; set; }
        public int? EpochSlotNo { get; set; }
        public int? BlockNo { get; set; }
        public long? PreviousId { get; set; }
        public byte[] MerkleRoot { get; set; }
        public long SlotLeaderId { get; set; }
        public int Size { get; set; }
        public DateTime Time { get; set; }
        public long TxCount { get; set; }
        public int ProtoMajor { get; set; }
        public int ProtoMinor { get; set; }
        public string VrfKey { get; set; }
        public byte[] OpCert { get; set; }

        public virtual Block Previous { get; set; }
        public virtual SlotLeader SlotLeader { get; set; }
        public virtual AdaPot AdaPot { get; set; }
        public virtual ICollection<EpochParam> EpochParams { get; set; }
        public virtual ICollection<EpochStake> EpochStakes { get; set; }
        public virtual ICollection<Block> InversePrevious { get; set; }
        public virtual ICollection<OrphanedReward> OrphanedRewards { get; set; }
        public virtual ICollection<Reward> Rewards { get; set; }
        public virtual ICollection<Tx> Txes { get; set; }
    }
}
