using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class Tx
    {
        public Tx()
        {
            Delegations = new HashSet<Delegation>();
            MaTxMints = new HashSet<MaTxMint>();
            ParamProposals = new HashSet<ParamProposal>();
            PoolMetaData = new HashSet<PoolMetaDatum>();
            PoolOwners = new HashSet<PoolOwner>();
            PoolRetires = new HashSet<PoolRetire>();
            PoolUpdates = new HashSet<PoolUpdate>();
            Reserves = new HashSet<Reserve>();
            StakeAddresses = new HashSet<StakeAddress>();
            StakeDeregistrations = new HashSet<StakeDeregistration>();
            StakeRegistrations = new HashSet<StakeRegistration>();
            Treasuries = new HashSet<Treasury>();
            TxInTxInNavigations = new HashSet<TxIn>();
            TxInTxOuts = new HashSet<TxIn>();
            TxMetadata = new HashSet<TxMetadatum>();
            TxOuts = new HashSet<TxOut>();
            Withdrawals = new HashSet<Withdrawal>();
        }

        public long Id { get; set; }
        public byte[] Hash { get; set; }
        public long BlockId { get; set; }
        public int BlockIndex { get; set; }
        public decimal OutSum { get; set; }
        public decimal Fee { get; set; }
        public long Deposit { get; set; }
        public int Size { get; set; }
        public decimal? InvalidBefore { get; set; }
        public decimal? InvalidHereafter { get; set; }

        public virtual Block Block { get; set; }
        public virtual ICollection<Delegation> Delegations { get; set; }
        public virtual ICollection<MaTxMint> MaTxMints { get; set; }
        public virtual ICollection<ParamProposal> ParamProposals { get; set; }
        public virtual ICollection<PoolMetaDatum> PoolMetaData { get; set; }
        public virtual ICollection<PoolOwner> PoolOwners { get; set; }
        public virtual ICollection<PoolRetire> PoolRetires { get; set; }
        public virtual ICollection<PoolUpdate> PoolUpdates { get; set; }
        public virtual ICollection<Reserve> Reserves { get; set; }
        public virtual ICollection<StakeAddress> StakeAddresses { get; set; }
        public virtual ICollection<StakeDeregistration> StakeDeregistrations { get; set; }
        public virtual ICollection<StakeRegistration> StakeRegistrations { get; set; }
        public virtual ICollection<Treasury> Treasuries { get; set; }
        public virtual ICollection<TxIn> TxInTxInNavigations { get; set; }
        public virtual ICollection<TxIn> TxInTxOuts { get; set; }
        public virtual ICollection<TxMetadatum> TxMetadata { get; set; }
        public virtual ICollection<TxOut> TxOuts { get; set; }
        public virtual ICollection<Withdrawal> Withdrawals { get; set; }
    }
}
