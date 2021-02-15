using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class TxOut
    {
        public TxOut()
        {
            MaTxOuts = new HashSet<MaTxOut>();
        }

        public long Id { get; set; }
        public long TxId { get; set; }
        public short Index { get; set; }
        public string Address { get; set; }
        public byte[] AddressRaw { get; set; }
        public byte[] PaymentCred { get; set; }
        public long? StakeAddressId { get; set; }
        public decimal Value { get; set; }

        public virtual StakeAddress StakeAddress { get; set; }
        public virtual Tx Tx { get; set; }
        public virtual ICollection<MaTxOut> MaTxOuts { get; set; }
    }
}
