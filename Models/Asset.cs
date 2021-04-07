using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class Asset
    {
        public string AssetId { get; set; }
        public string AssetName { get; set; }
        public string Description { get; set; }
        public string Fingerprint { get; set; }
        public string Logo { get; set; }
        public int? MetadataFetchAttempts { get; set; }
        public string MetadataHash { get; set; }
        public string Name { get; set; }
        public byte[] PolicyId { get; set; }
        public string Ticker { get; set; }
        public string Url { get; set; }
    }
}
