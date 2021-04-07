using CardanoSharp.DbSync.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CardanoSharp.DbSync.EntityFramework
{
    public partial class CardanoContext : DbContext
    {
        private readonly IConfiguration _config;
        public CardanoContext()
        {
        }

        public CardanoContext(DbContextOptions<CardanoContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }
        public virtual DbSet<AdaPot> AdaPots { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Delegation> Delegations { get; set; }
        public virtual DbSet<Epoch> Epoches { get; set; }
        public virtual DbSet<EpochParam> EpochParams { get; set; }
        public virtual DbSet<EpochStake> EpochStakes { get; set; }
        public virtual DbSet<MaTxMint> MaTxMints { get; set; }
        public virtual DbSet<MaTxOut> MaTxOuts { get; set; }
        public virtual DbSet<Metum> Meta { get; set; }
        public virtual DbSet<OrphanedReward> OrphanedRewards { get; set; }
        public virtual DbSet<ParamProposal> ParamProposals { get; set; }
        public virtual DbSet<PoolHash> PoolHashes { get; set; }
        public virtual DbSet<PoolMetaDatum> PoolMetaData { get; set; }
        public virtual DbSet<PoolOwner> PoolOwners { get; set; }
        public virtual DbSet<PoolRelay> PoolRelays { get; set; }
        public virtual DbSet<PoolRetire> PoolRetires { get; set; }
        public virtual DbSet<PoolUpdate> PoolUpdates { get; set; }
        public virtual DbSet<Reserve> Reserves { get; set; }
        public virtual DbSet<Reward> Rewards { get; set; }
        public virtual DbSet<SchemaVersion> SchemaVersions { get; set; }
        public virtual DbSet<SlotLeader> SlotLeaders { get; set; }
        public virtual DbSet<StakeAddress> StakeAddresses { get; set; }
        public virtual DbSet<StakeDeregistration> StakeDeregistrations { get; set; }
        public virtual DbSet<StakeRegistration> StakeRegistrations { get; set; }
        public virtual DbSet<Treasury> Treasuries { get; set; }
        public virtual DbSet<Tx> Txes { get; set; }
        public virtual DbSet<TxIn> TxIns { get; set; }
        public virtual DbSet<TxMetadatum> TxMetadata { get; set; }
        public virtual DbSet<TxOut> TxOuts { get; set; }
        public virtual DbSet<UtxoView> UtxoViews { get; set; }
        public virtual DbSet<Withdrawal> Withdrawals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_config.GetConnectionString("Cardano"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("pgcrypto")
                .HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<AdaPot>(entity =>
            {
                entity.ToTable("ada_pots");

                entity.HasIndex(e => e.BlockId, "unique_ada_pots")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BlockId).HasColumnName("block_id");

                entity.Property(e => e.Deposits)
                    .HasPrecision(20)
                    .HasColumnName("deposits");

                entity.Property(e => e.EpochNo).HasColumnName("epoch_no");

                entity.Property(e => e.Fees)
                    .HasPrecision(20)
                    .HasColumnName("fees");

                entity.Property(e => e.Reserves)
                    .HasPrecision(20)
                    .HasColumnName("reserves");

                entity.Property(e => e.Rewards)
                    .HasPrecision(20)
                    .HasColumnName("rewards");

                entity.Property(e => e.SlotNo).HasColumnName("slot_no");

                entity.Property(e => e.Treasury)
                    .HasPrecision(20)
                    .HasColumnName("treasury");

                entity.Property(e => e.Utxo)
                    .HasPrecision(20)
                    .HasColumnName("utxo");

                entity.HasOne(d => d.Block)
                    .WithOne(p => p.AdaPot)
                    .HasForeignKey<AdaPot>(d => d.BlockId)
                    .HasConstraintName("ada_pots_block_id_fkey");
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("Asset");

                entity.Property(e => e.AssetId).HasColumnName("assetId");

                entity.Property(e => e.AssetName).HasColumnName("assetName");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Fingerprint)
                    .HasMaxLength(44)
                    .HasColumnName("fingerprint")
                    .IsFixedLength(true);

                entity.Property(e => e.Logo).HasColumnName("logo");

                entity.Property(e => e.MetadataFetchAttempts).HasColumnName("metadataFetchAttempts");

                entity.Property(e => e.MetadataHash)
                    .HasMaxLength(40)
                    .HasColumnName("metadataHash")
                    .IsFixedLength(true);

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.PolicyId).HasColumnName("policyId");

                entity.Property(e => e.Ticker).HasColumnName("ticker");

                entity.Property(e => e.Url).HasColumnName("url");
            });

            modelBuilder.Entity<Block>(entity =>
            {
                entity.ToTable("block");

                entity.HasIndex(e => e.BlockNo, "idx_block_block_no");

                entity.HasIndex(e => e.EpochNo, "idx_block_epoch_no");

                entity.HasIndex(e => e.Hash, "idx_block_hash");

                entity.HasIndex(e => e.PreviousId, "idx_block_previous_id");

                entity.HasIndex(e => e.SlotLeaderId, "idx_block_slot_leader_id");

                entity.HasIndex(e => e.SlotNo, "idx_block_slot_no");

                entity.HasIndex(e => e.Time, "idx_block_time");

                entity.HasIndex(e => e.Hash, "unique_block")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BlockNo).HasColumnName("block_no");

                entity.Property(e => e.EpochNo).HasColumnName("epoch_no");

                entity.Property(e => e.EpochSlotNo).HasColumnName("epoch_slot_no");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash");

                entity.Property(e => e.MerkleRoot).HasColumnName("merkle_root");

                entity.Property(e => e.OpCert).HasColumnName("op_cert");

                entity.Property(e => e.PreviousId).HasColumnName("previous_id");

                entity.Property(e => e.ProtoMajor).HasColumnName("proto_major");

                entity.Property(e => e.ProtoMinor).HasColumnName("proto_minor");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.Property(e => e.SlotLeaderId).HasColumnName("slot_leader_id");

                entity.Property(e => e.SlotNo).HasColumnName("slot_no");

                entity.Property(e => e.Time).HasColumnName("time");

                entity.Property(e => e.TxCount).HasColumnName("tx_count");

                entity.Property(e => e.VrfKey)
                    .HasColumnType("character varying")
                    .HasColumnName("vrf_key");

                entity.HasOne(d => d.Previous)
                    .WithMany(p => p.InversePrevious)
                    .HasForeignKey(d => d.PreviousId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("block_previous_id_fkey");

                entity.HasOne(d => d.SlotLeader)
                    .WithMany(p => p.Blocks)
                    .HasForeignKey(d => d.SlotLeaderId)
                    .HasConstraintName("block_slot_leader_id_fkey");
            });

            modelBuilder.Entity<Delegation>(entity =>
            {
                entity.ToTable("delegation");

                entity.HasIndex(e => e.ActiveEpochNo, "idx_delegation_active_epoch_no");

                entity.HasIndex(e => e.AddrId, "idx_delegation_addr_id");

                entity.HasIndex(e => e.PoolHashId, "idx_delegation_pool_hash_id");

                entity.HasIndex(e => e.TxId, "idx_delegation_tx_id");

                entity.HasIndex(e => new { e.AddrId, e.PoolHashId, e.TxId }, "unique_delegation")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActiveEpochNo).HasColumnName("active_epoch_no");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.CertIndex).HasColumnName("cert_index");

                entity.Property(e => e.PoolHashId).HasColumnName("pool_hash_id");

                entity.Property(e => e.SlotNo).HasColumnName("slot_no");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.Delegations)
                    .HasForeignKey(d => d.AddrId)
                    .HasConstraintName("delegation_addr_id_fkey");

                entity.HasOne(d => d.PoolHash)
                    .WithMany(p => p.Delegations)
                    .HasForeignKey(d => d.PoolHashId)
                    .HasConstraintName("delegation_pool_hash_id_fkey");

                entity.HasOne(d => d.Tx)
                    .WithMany(p => p.Delegations)
                    .HasForeignKey(d => d.TxId)
                    .HasConstraintName("delegation_tx_id_fkey");
            });

            modelBuilder.Entity<Epoch>(entity =>
            {
                entity.ToTable("epoch");

                entity.HasIndex(e => e.No, "unique_epoch")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BlkCount).HasColumnName("blk_count");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.Fees)
                    .HasPrecision(20)
                    .HasColumnName("fees");

                entity.Property(e => e.No).HasColumnName("no");

                entity.Property(e => e.OutSum)
                    .HasPrecision(39)
                    .HasColumnName("out_sum");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.Property(e => e.TxCount).HasColumnName("tx_count");
            });

            modelBuilder.Entity<EpochParam>(entity =>
            {
                entity.ToTable("epoch_param");

                entity.HasIndex(e => e.BlockId, "idx_epoch_param_block_id");

                entity.HasIndex(e => new { e.EpochNo, e.BlockId }, "unique_epoch_param")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BlockId).HasColumnName("block_id");

                entity.Property(e => e.Decentralisation).HasColumnName("decentralisation");

                entity.Property(e => e.Entropy).HasColumnName("entropy");

                entity.Property(e => e.EpochNo).HasColumnName("epoch_no");

                entity.Property(e => e.Influence).HasColumnName("influence");

                entity.Property(e => e.KeyDeposit)
                    .HasPrecision(20)
                    .HasColumnName("key_deposit");

                entity.Property(e => e.MaxBhSize).HasColumnName("max_bh_size");

                entity.Property(e => e.MaxBlockSize).HasColumnName("max_block_size");

                entity.Property(e => e.MaxEpoch).HasColumnName("max_epoch");

                entity.Property(e => e.MaxTxSize).HasColumnName("max_tx_size");

                entity.Property(e => e.MinFeeA).HasColumnName("min_fee_a");

                entity.Property(e => e.MinFeeB).HasColumnName("min_fee_b");

                entity.Property(e => e.MinPoolCost)
                    .HasPrecision(20)
                    .HasColumnName("min_pool_cost");

                entity.Property(e => e.MinUtxoValue)
                    .HasPrecision(20)
                    .HasColumnName("min_utxo_value");

                entity.Property(e => e.MonetaryExpandRate).HasColumnName("monetary_expand_rate");

                entity.Property(e => e.Nonce).HasColumnName("nonce");

                entity.Property(e => e.OptimalPoolCount).HasColumnName("optimal_pool_count");

                entity.Property(e => e.PoolDeposit)
                    .HasPrecision(20)
                    .HasColumnName("pool_deposit");

                entity.Property(e => e.ProtocolMajor).HasColumnName("protocol_major");

                entity.Property(e => e.ProtocolMinor).HasColumnName("protocol_minor");

                entity.Property(e => e.TreasuryGrowthRate).HasColumnName("treasury_growth_rate");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.EpochParams)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("epoch_param_block_id_fkey");
            });

            modelBuilder.Entity<EpochStake>(entity =>
            {
                entity.ToTable("epoch_stake");

                entity.HasIndex(e => e.AddrId, "idx_epoch_stake_addr_id");

                entity.HasIndex(e => e.BlockId, "idx_epoch_stake_block_id");

                entity.HasIndex(e => e.PoolId, "idx_epoch_stake_pool_id");

                entity.HasIndex(e => new { e.AddrId, e.EpochNo }, "unique_stake")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.Amount)
                    .HasPrecision(20)
                    .HasColumnName("amount");

                entity.Property(e => e.BlockId).HasColumnName("block_id");

                entity.Property(e => e.EpochNo).HasColumnName("epoch_no");

                entity.Property(e => e.PoolId).HasColumnName("pool_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.EpochStakes)
                    .HasForeignKey(d => d.AddrId)
                    .HasConstraintName("epoch_stake_addr_id_fkey");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.EpochStakes)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("epoch_stake_block_id_fkey");

                entity.HasOne(d => d.Pool)
                    .WithMany(p => p.EpochStakes)
                    .HasForeignKey(d => d.PoolId)
                    .HasConstraintName("epoch_stake_pool_id_fkey");
            });

            modelBuilder.Entity<MaTxMint>(entity =>
            {
                entity.ToTable("ma_tx_mint");

                entity.HasIndex(e => e.TxId, "idx_ma_tx_mint_tx_id");

                entity.HasIndex(e => new { e.Policy, e.Name, e.TxId }, "unique_ma_tx_mint")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Policy)
                    .IsRequired()
                    .HasColumnName("policy");

                entity.Property(e => e.Quantity)
                    .HasPrecision(20)
                    .HasColumnName("quantity");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.HasOne(d => d.Tx)
                    .WithMany(p => p.MaTxMints)
                    .HasForeignKey(d => d.TxId)
                    .HasConstraintName("ma_tx_mint_tx_id_fkey");
            });

            modelBuilder.Entity<MaTxOut>(entity =>
            {
                entity.ToTable("ma_tx_out");

                entity.HasIndex(e => e.TxOutId, "idx_ma_tx_out_tx_out_id");

                entity.HasIndex(e => new { e.Policy, e.Name, e.TxOutId }, "unique_ma_tx_out")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Policy)
                    .IsRequired()
                    .HasColumnName("policy");

                entity.Property(e => e.Quantity)
                    .HasPrecision(20)
                    .HasColumnName("quantity");

                entity.Property(e => e.TxOutId).HasColumnName("tx_out_id");

                entity.HasOne(d => d.TxOut)
                    .WithMany(p => p.MaTxOuts)
                    .HasForeignKey(d => d.TxOutId)
                    .HasConstraintName("ma_tx_out_tx_out_id_fkey");
            });

            modelBuilder.Entity<Metum>(entity =>
            {
                entity.ToTable("meta");

                entity.HasIndex(e => e.StartTime, "unique_meta")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NetworkName)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("network_name");

                entity.Property(e => e.StartTime).HasColumnName("start_time");
            });

            modelBuilder.Entity<OrphanedReward>(entity =>
            {
                entity.ToTable("orphaned_reward");

                entity.HasIndex(e => e.AddrId, "idx_orphaned_reward_addr_id");

                entity.HasIndex(e => e.BlockId, "idx_orphaned_reward_block_id");

                entity.HasIndex(e => e.PoolId, "idx_orphaned_reward_pool_id");

                entity.HasIndex(e => new { e.AddrId, e.BlockId }, "unique_orphaned")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.Amount)
                    .HasPrecision(20)
                    .HasColumnName("amount");

                entity.Property(e => e.BlockId).HasColumnName("block_id");

                entity.Property(e => e.EpochNo).HasColumnName("epoch_no");

                entity.Property(e => e.PoolId).HasColumnName("pool_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.OrphanedRewards)
                    .HasForeignKey(d => d.AddrId)
                    .HasConstraintName("orphaned_reward_addr_id_fkey");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.OrphanedRewards)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("orphaned_reward_block_id_fkey");

                entity.HasOne(d => d.Pool)
                    .WithMany(p => p.OrphanedRewards)
                    .HasForeignKey(d => d.PoolId)
                    .HasConstraintName("orphaned_reward_pool_id_fkey");
            });

            modelBuilder.Entity<ParamProposal>(entity =>
            {
                entity.ToTable("param_proposal");

                entity.HasIndex(e => e.RegisteredTxId, "idx_param_proposal_registered_tx_id");

                entity.HasIndex(e => new { e.Key, e.RegisteredTxId }, "unique_param_proposal")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Decentralisation).HasColumnName("decentralisation");

                entity.Property(e => e.Entropy).HasColumnName("entropy");

                entity.Property(e => e.EpochNo).HasColumnName("epoch_no");

                entity.Property(e => e.Influence).HasColumnName("influence");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasColumnName("key");

                entity.Property(e => e.KeyDeposit)
                    .HasPrecision(20)
                    .HasColumnName("key_deposit");

                entity.Property(e => e.MaxBhSize).HasColumnName("max_bh_size");

                entity.Property(e => e.MaxBlockSize).HasColumnName("max_block_size");

                entity.Property(e => e.MaxEpoch).HasColumnName("max_epoch");

                entity.Property(e => e.MaxTxSize).HasColumnName("max_tx_size");

                entity.Property(e => e.MinFeeA).HasColumnName("min_fee_a");

                entity.Property(e => e.MinFeeB).HasColumnName("min_fee_b");

                entity.Property(e => e.MinPoolCost)
                    .HasPrecision(20)
                    .HasColumnName("min_pool_cost");

                entity.Property(e => e.MinUtxoValue)
                    .HasPrecision(20)
                    .HasColumnName("min_utxo_value");

                entity.Property(e => e.MonetaryExpandRate).HasColumnName("monetary_expand_rate");

                entity.Property(e => e.OptimalPoolCount).HasColumnName("optimal_pool_count");

                entity.Property(e => e.PoolDeposit)
                    .HasPrecision(20)
                    .HasColumnName("pool_deposit");

                entity.Property(e => e.ProtocolMajor).HasColumnName("protocol_major");

                entity.Property(e => e.ProtocolMinor).HasColumnName("protocol_minor");

                entity.Property(e => e.RegisteredTxId).HasColumnName("registered_tx_id");

                entity.Property(e => e.TreasuryGrowthRate).HasColumnName("treasury_growth_rate");

                entity.HasOne(d => d.RegisteredTx)
                    .WithMany(p => p.ParamProposals)
                    .HasForeignKey(d => d.RegisteredTxId)
                    .HasConstraintName("param_proposal_registered_tx_id_fkey");
            });

            modelBuilder.Entity<PoolHash>(entity =>
            {
                entity.ToTable("pool_hash");

                entity.HasIndex(e => e.HashRaw, "unique_pool_hash")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HashRaw)
                    .IsRequired()
                    .HasColumnName("hash_raw");

                entity.Property(e => e.View)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("view");
            });

            modelBuilder.Entity<PoolMetaDatum>(entity =>
            {
                entity.ToTable("pool_meta_data");

                entity.HasIndex(e => e.RegisteredTxId, "idx_pool_meta_data_registered_tx_id");

                entity.HasIndex(e => new { e.Url, e.Hash }, "unique_pool_meta_data")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash");

                entity.Property(e => e.RegisteredTxId).HasColumnName("registered_tx_id");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("url");

                entity.HasOne(d => d.RegisteredTx)
                    .WithMany(p => p.PoolMetaData)
                    .HasForeignKey(d => d.RegisteredTxId)
                    .HasConstraintName("pool_meta_data_registered_tx_id_fkey");
            });

            modelBuilder.Entity<PoolOwner>(entity =>
            {
                entity.ToTable("pool_owner");

                entity.HasIndex(e => e.PoolHashId, "idx_pool_owner_pool_hash_id");

                entity.HasIndex(e => e.RegisteredTxId, "idx_pool_owner_registered_tx_id");

                entity.HasIndex(e => new { e.Hash, e.PoolHashId, e.RegisteredTxId }, "unique_pool_owner")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash");

                entity.Property(e => e.PoolHashId).HasColumnName("pool_hash_id");

                entity.Property(e => e.RegisteredTxId).HasColumnName("registered_tx_id");

                entity.HasOne(d => d.PoolHash)
                    .WithMany(p => p.PoolOwners)
                    .HasForeignKey(d => d.PoolHashId)
                    .HasConstraintName("pool_owner_pool_hash_id_fkey");

                entity.HasOne(d => d.RegisteredTx)
                    .WithMany(p => p.PoolOwners)
                    .HasForeignKey(d => d.RegisteredTxId)
                    .HasConstraintName("pool_owner_registered_tx_id_fkey");
            });

            modelBuilder.Entity<PoolRelay>(entity =>
            {
                entity.ToTable("pool_relay");

                entity.HasIndex(e => e.UpdateId, "idx_pool_relay_update_id");

                entity.HasIndex(e => new { e.UpdateId, e.Ipv4, e.Ipv6, e.DnsName }, "unique_pool_relay")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DnsName)
                    .HasColumnType("character varying")
                    .HasColumnName("dns_name");

                entity.Property(e => e.DnsSrvName)
                    .HasColumnType("character varying")
                    .HasColumnName("dns_srv_name");

                entity.Property(e => e.Ipv4)
                    .HasColumnType("character varying")
                    .HasColumnName("ipv4");

                entity.Property(e => e.Ipv6)
                    .HasColumnType("character varying")
                    .HasColumnName("ipv6");

                entity.Property(e => e.Port).HasColumnName("port");

                entity.Property(e => e.UpdateId).HasColumnName("update_id");

                entity.HasOne(d => d.Update)
                    .WithMany(p => p.PoolRelays)
                    .HasForeignKey(d => d.UpdateId)
                    .HasConstraintName("pool_relay_update_id_fkey");
            });

            modelBuilder.Entity<PoolRetire>(entity =>
            {
                entity.ToTable("pool_retire");

                entity.HasIndex(e => e.AnnouncedTxId, "idx_pool_retire_announced_tx_id");

                entity.HasIndex(e => e.HashId, "idx_pool_retire_hash_id");

                entity.HasIndex(e => new { e.HashId, e.AnnouncedTxId }, "unique_pool_retiring")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AnnouncedTxId).HasColumnName("announced_tx_id");

                entity.Property(e => e.CertIndex).HasColumnName("cert_index");

                entity.Property(e => e.HashId).HasColumnName("hash_id");

                entity.Property(e => e.RetiringEpoch).HasColumnName("retiring_epoch");

                entity.HasOne(d => d.AnnouncedTx)
                    .WithMany(p => p.PoolRetires)
                    .HasForeignKey(d => d.AnnouncedTxId)
                    .HasConstraintName("pool_retire_announced_tx_id_fkey");

                entity.HasOne(d => d.Hash)
                    .WithMany(p => p.PoolRetires)
                    .HasForeignKey(d => d.HashId)
                    .HasConstraintName("pool_retire_hash_id_fkey");
            });

            modelBuilder.Entity<PoolUpdate>(entity =>
            {
                entity.ToTable("pool_update");

                entity.HasIndex(e => e.ActiveEpochNo, "idx_pool_update_active_epoch_no");

                entity.HasIndex(e => e.HashId, "idx_pool_update_hash_id");

                entity.HasIndex(e => e.MetaId, "idx_pool_update_meta_id");

                entity.HasIndex(e => e.RegisteredTxId, "idx_pool_update_registered_tx_id");

                entity.HasIndex(e => e.RewardAddr, "idx_pool_update_reward_addr");

                entity.HasIndex(e => new { e.HashId, e.RegisteredTxId }, "unique_pool_update")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActiveEpochNo).HasColumnName("active_epoch_no");

                entity.Property(e => e.CertIndex).HasColumnName("cert_index");

                entity.Property(e => e.FixedCost)
                    .HasPrecision(20)
                    .HasColumnName("fixed_cost");

                entity.Property(e => e.HashId).HasColumnName("hash_id");

                entity.Property(e => e.Margin).HasColumnName("margin");

                entity.Property(e => e.MetaId).HasColumnName("meta_id");

                entity.Property(e => e.Pledge)
                    .HasPrecision(20)
                    .HasColumnName("pledge");

                entity.Property(e => e.RegisteredTxId).HasColumnName("registered_tx_id");

                entity.Property(e => e.RewardAddr)
                    .IsRequired()
                    .HasColumnName("reward_addr");

                entity.Property(e => e.VrfKeyHash)
                    .IsRequired()
                    .HasColumnName("vrf_key_hash");

                entity.HasOne(d => d.Hash)
                    .WithMany(p => p.PoolUpdates)
                    .HasForeignKey(d => d.HashId)
                    .HasConstraintName("pool_update_hash_id_fkey");

                entity.HasOne(d => d.Meta)
                    .WithMany(p => p.PoolUpdates)
                    .HasForeignKey(d => d.MetaId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("pool_update_meta_id_fkey");

                entity.HasOne(d => d.RegisteredTx)
                    .WithMany(p => p.PoolUpdates)
                    .HasForeignKey(d => d.RegisteredTxId)
                    .HasConstraintName("pool_update_registered_tx_id_fkey");
            });

            modelBuilder.Entity<Reserve>(entity =>
            {
                entity.ToTable("reserve");

                entity.HasIndex(e => e.AddrId, "idx_reserve_addr_id");

                entity.HasIndex(e => e.TxId, "idx_reserve_tx_id");

                entity.HasIndex(e => new { e.AddrId, e.TxId }, "unique_reserves")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.Amount)
                    .HasPrecision(20)
                    .HasColumnName("amount");

                entity.Property(e => e.CertIndex).HasColumnName("cert_index");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.AddrId)
                    .HasConstraintName("reserve_addr_id_fkey");

                entity.HasOne(d => d.Tx)
                    .WithMany(p => p.Reserves)
                    .HasForeignKey(d => d.TxId)
                    .HasConstraintName("reserve_tx_id_fkey");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.ToTable("reward");

                entity.HasIndex(e => e.AddrId, "idx_reward_addr_id");

                entity.HasIndex(e => e.BlockId, "idx_reward_block_id");

                entity.HasIndex(e => e.PoolId, "idx_reward_pool_id");

                entity.HasIndex(e => new { e.AddrId, e.BlockId }, "unique_reward")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.Amount)
                    .HasPrecision(20)
                    .HasColumnName("amount");

                entity.Property(e => e.BlockId).HasColumnName("block_id");

                entity.Property(e => e.EpochNo).HasColumnName("epoch_no");

                entity.Property(e => e.PoolId).HasColumnName("pool_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.Rewards)
                    .HasForeignKey(d => d.AddrId)
                    .HasConstraintName("reward_addr_id_fkey");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.Rewards)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("reward_block_id_fkey");

                entity.HasOne(d => d.Pool)
                    .WithMany(p => p.Rewards)
                    .HasForeignKey(d => d.PoolId)
                    .HasConstraintName("reward_pool_id_fkey");
            });

            modelBuilder.Entity<SchemaVersion>(entity =>
            {
                entity.ToTable("schema_version");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StageOne).HasColumnName("stage_one");

                entity.Property(e => e.StageThree).HasColumnName("stage_three");

                entity.Property(e => e.StageTwo).HasColumnName("stage_two");
            });

            modelBuilder.Entity<SlotLeader>(entity =>
            {
                entity.ToTable("slot_leader");

                entity.HasIndex(e => e.PoolHashId, "idx_slot_leader_pool_hash_id");

                entity.HasIndex(e => e.Hash, "unique_slot_leader")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("description");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash");

                entity.Property(e => e.PoolHashId).HasColumnName("pool_hash_id");

                entity.HasOne(d => d.PoolHash)
                    .WithMany(p => p.SlotLeaders)
                    .HasForeignKey(d => d.PoolHashId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("slot_leader_pool_hash_id_fkey");
            });

            modelBuilder.Entity<StakeAddress>(entity =>
            {
                entity.ToTable("stake_address");

                entity.HasIndex(e => e.HashRaw, "idx_stake_address_hash_raw");

                entity.HasIndex(e => e.RegisteredTxId, "idx_stake_address_registered_tx_id");

                entity.HasIndex(e => e.HashRaw, "unique_stake_address")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HashRaw)
                    .IsRequired()
                    .HasColumnName("hash_raw");

                entity.Property(e => e.RegisteredTxId).HasColumnName("registered_tx_id");

                entity.Property(e => e.View)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("view");

                entity.HasOne(d => d.RegisteredTx)
                    .WithMany(p => p.StakeAddresses)
                    .HasForeignKey(d => d.RegisteredTxId)
                    .HasConstraintName("stake_address_registered_tx_id_fkey");
            });

            modelBuilder.Entity<StakeDeregistration>(entity =>
            {
                entity.ToTable("stake_deregistration");

                entity.HasIndex(e => e.AddrId, "idx_stake_deregistration_addr_id");

                entity.HasIndex(e => e.TxId, "idx_stake_deregistration_tx_id");

                entity.HasIndex(e => new { e.AddrId, e.TxId }, "unique_stake_deregistration")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.CertIndex).HasColumnName("cert_index");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.StakeDeregistrations)
                    .HasForeignKey(d => d.AddrId)
                    .HasConstraintName("stake_deregistration_addr_id_fkey");

                entity.HasOne(d => d.Tx)
                    .WithMany(p => p.StakeDeregistrations)
                    .HasForeignKey(d => d.TxId)
                    .HasConstraintName("stake_deregistration_tx_id_fkey");
            });

            modelBuilder.Entity<StakeRegistration>(entity =>
            {
                entity.ToTable("stake_registration");

                entity.HasIndex(e => e.AddrId, "idx_stake_registration_addr_id");

                entity.HasIndex(e => e.TxId, "idx_stake_registration_tx_id");

                entity.HasIndex(e => new { e.AddrId, e.TxId }, "unique_stake_registration")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.CertIndex).HasColumnName("cert_index");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.StakeRegistrations)
                    .HasForeignKey(d => d.AddrId)
                    .HasConstraintName("stake_registration_addr_id_fkey");

                entity.HasOne(d => d.Tx)
                    .WithMany(p => p.StakeRegistrations)
                    .HasForeignKey(d => d.TxId)
                    .HasConstraintName("stake_registration_tx_id_fkey");
            });

            modelBuilder.Entity<Treasury>(entity =>
            {
                entity.ToTable("treasury");

                entity.HasIndex(e => e.AddrId, "idx_treasury_addr_id");

                entity.HasIndex(e => e.TxId, "idx_treasury_tx_id");

                entity.HasIndex(e => new { e.AddrId, e.TxId }, "unique_treasury")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.Amount)
                    .HasPrecision(20)
                    .HasColumnName("amount");

                entity.Property(e => e.CertIndex).HasColumnName("cert_index");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.Treasuries)
                    .HasForeignKey(d => d.AddrId)
                    .HasConstraintName("treasury_addr_id_fkey");

                entity.HasOne(d => d.Tx)
                    .WithMany(p => p.Treasuries)
                    .HasForeignKey(d => d.TxId)
                    .HasConstraintName("treasury_tx_id_fkey");
            });

            modelBuilder.Entity<Tx>(entity =>
            {
                entity.ToTable("tx");

                entity.HasIndex(e => e.BlockId, "idx_tx_block_id");

                entity.HasIndex(e => e.Hash, "idx_tx_hash");

                entity.HasIndex(e => e.Hash, "unique_tx")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BlockId).HasColumnName("block_id");

                entity.Property(e => e.BlockIndex).HasColumnName("block_index");

                entity.Property(e => e.Deposit).HasColumnName("deposit");

                entity.Property(e => e.Fee)
                    .HasPrecision(20)
                    .HasColumnName("fee");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasColumnName("hash");

                entity.Property(e => e.InvalidBefore)
                    .HasPrecision(20)
                    .HasColumnName("invalid_before");

                entity.Property(e => e.InvalidHereafter)
                    .HasPrecision(20)
                    .HasColumnName("invalid_hereafter");

                entity.Property(e => e.OutSum)
                    .HasPrecision(20)
                    .HasColumnName("out_sum");

                entity.Property(e => e.Size).HasColumnName("size");

                entity.HasOne(d => d.Block)
                    .WithMany(p => p.Txes)
                    .HasForeignKey(d => d.BlockId)
                    .HasConstraintName("tx_block_id_fkey");
            });

            modelBuilder.Entity<TxIn>(entity =>
            {
                entity.ToTable("tx_in");

                entity.HasIndex(e => e.TxOutId, "idx_tx_in_consuming_tx");

                entity.HasIndex(e => e.TxInId, "idx_tx_in_source_tx");

                entity.HasIndex(e => e.TxInId, "idx_tx_in_tx_in_id");

                entity.HasIndex(e => e.TxOutId, "idx_tx_in_tx_out_id");

                entity.HasIndex(e => new { e.TxOutId, e.TxOutIndex }, "unique_txin")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.TxInId).HasColumnName("tx_in_id");

                entity.Property(e => e.TxOutId).HasColumnName("tx_out_id");

                entity.Property(e => e.TxOutIndex).HasColumnName("tx_out_index");

                entity.HasOne(d => d.TxInNavigation)
                    .WithMany(p => p.TxInTxInNavigations)
                    .HasForeignKey(d => d.TxInId)
                    .HasConstraintName("tx_in_tx_in_id_fkey");

                entity.HasOne(d => d.TxOut)
                    .WithMany(p => p.TxInTxOuts)
                    .HasForeignKey(d => d.TxOutId)
                    .HasConstraintName("tx_in_tx_out_id_fkey");
            });

            modelBuilder.Entity<TxMetadatum>(entity =>
            {
                entity.ToTable("tx_metadata");

                entity.HasIndex(e => e.TxId, "idx_tx_metadata_tx_id");

                entity.HasIndex(e => new { e.Key, e.TxId }, "unique_tx_metadata")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bytes)
                    .IsRequired()
                    .HasColumnName("bytes");

                entity.Property(e => e.Json)
                    .HasColumnType("jsonb")
                    .HasColumnName("json");

                entity.Property(e => e.Key)
                    .HasPrecision(20)
                    .HasColumnName("key");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.HasOne(d => d.Tx)
                    .WithMany(p => p.TxMetadata)
                    .HasForeignKey(d => d.TxId)
                    .HasConstraintName("tx_metadata_tx_id_fkey");
            });

            modelBuilder.Entity<TxOut>(entity =>
            {
                entity.ToTable("tx_out");

                entity.HasIndex(e => e.Address, "idx_tx_out_address")
                    .HasMethod("hash");

                entity.HasIndex(e => e.PaymentCred, "idx_tx_out_payment_cred");

                entity.HasIndex(e => e.StakeAddressId, "idx_tx_out_stake_address_id");

                entity.HasIndex(e => e.TxId, "idx_tx_out_tx");

                entity.HasIndex(e => e.TxId, "idx_tx_out_tx_id");

                entity.HasIndex(e => new { e.TxId, e.Index }, "unique_txout")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("character varying")
                    .HasColumnName("address");

                entity.Property(e => e.AddressRaw)
                    .IsRequired()
                    .HasColumnName("address_raw");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.PaymentCred).HasColumnName("payment_cred");

                entity.Property(e => e.StakeAddressId).HasColumnName("stake_address_id");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.Property(e => e.Value)
                    .HasPrecision(20)
                    .HasColumnName("value");

                entity.HasOne(d => d.StakeAddress)
                    .WithMany(p => p.TxOuts)
                    .HasForeignKey(d => d.StakeAddressId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("tx_out_stake_address_id_fkey");

                entity.HasOne(d => d.Tx)
                    .WithMany(p => p.TxOuts)
                    .HasForeignKey(d => d.TxId)
                    .HasConstraintName("tx_out_tx_id_fkey");
            });

            modelBuilder.Entity<UtxoView>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("utxo_view");

                entity.Property(e => e.Address)
                    .HasColumnType("character varying")
                    .HasColumnName("address");

                entity.Property(e => e.AddressRaw).HasColumnName("address_raw");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.PaymentCred).HasColumnName("payment_cred");

                entity.Property(e => e.StakeAddressId).HasColumnName("stake_address_id");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.Property(e => e.Value)
                    .HasPrecision(20)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Withdrawal>(entity =>
            {
                entity.ToTable("withdrawal");

                entity.HasIndex(e => e.AddrId, "idx_withdrawal_addr_id");

                entity.HasIndex(e => e.TxId, "idx_withdrawal_tx_id");

                entity.HasIndex(e => new { e.AddrId, e.TxId }, "unique_withdrawal")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddrId).HasColumnName("addr_id");

                entity.Property(e => e.Amount)
                    .HasPrecision(20)
                    .HasColumnName("amount");

                entity.Property(e => e.TxId).HasColumnName("tx_id");

                entity.HasOne(d => d.Addr)
                    .WithMany(p => p.Withdrawals)
                    .HasForeignKey(d => d.AddrId)
                    .HasConstraintName("withdrawal_addr_id_fkey");

                entity.HasOne(d => d.Tx)
                    .WithMany(p => p.Withdrawals)
                    .HasForeignKey(d => d.TxId)
                    .HasConstraintName("withdrawal_tx_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

