using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class StakeAddress
    {
        public StakeAddress()
        {
            Delegations = new HashSet<Delegation>();
            EpochStakes = new HashSet<EpochStake>();
            OrphanedRewards = new HashSet<OrphanedReward>();
            Reserves = new HashSet<Reserve>();
            Rewards = new HashSet<Reward>();
            StakeDeregistrations = new HashSet<StakeDeregistration>();
            StakeRegistrations = new HashSet<StakeRegistration>();
            Treasuries = new HashSet<Treasury>();
            TxOuts = new HashSet<TxOut>();
            Withdrawals = new HashSet<Withdrawal>();
        }

        public long Id { get; set; }
        public byte[] HashRaw { get; set; }
        public string View { get; set; }
        public long RegisteredTxId { get; set; }

        public virtual Tx RegisteredTx { get; set; }
        public virtual ICollection<Delegation> Delegations { get; set; }
        public virtual ICollection<EpochStake> EpochStakes { get; set; }
        public virtual ICollection<OrphanedReward> OrphanedRewards { get; set; }
        public virtual ICollection<Reserve> Reserves { get; set; }
        public virtual ICollection<Reward> Rewards { get; set; }
        public virtual ICollection<StakeDeregistration> StakeDeregistrations { get; set; }
        public virtual ICollection<StakeRegistration> StakeRegistrations { get; set; }
        public virtual ICollection<Treasury> Treasuries { get; set; }
        public virtual ICollection<TxOut> TxOuts { get; set; }
        public virtual ICollection<Withdrawal> Withdrawals { get; set; }
    }
}
