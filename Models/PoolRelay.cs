using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class PoolRelay
    {
        public long Id { get; set; }
        public long UpdateId { get; set; }
        public string Ipv4 { get; set; }
        public string Ipv6 { get; set; }
        public string DnsName { get; set; }
        public string DnsSrvName { get; set; }
        public int? Port { get; set; }

        public virtual PoolUpdate Update { get; set; }
    }
}
