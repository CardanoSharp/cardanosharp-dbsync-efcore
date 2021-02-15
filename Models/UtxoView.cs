using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class UtxoView
    {
        public long? Id { get; set; }
        public long? TxId { get; set; }
        public short? Index { get; set; }
        public string Address { get; set; }
        public byte[] AddressRaw { get; set; }
        public byte[] PaymentCred { get; set; }
        public long? StakeAddressId { get; set; }
        public decimal? Value { get; set; }
    }
}
