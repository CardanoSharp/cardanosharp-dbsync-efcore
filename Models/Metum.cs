using System;
using System.Collections.Generic;

#nullable disable

namespace CardanoSharp.DbSync.EntityFramework.Models
{
    public partial class Metum
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public string NetworkName { get; set; }
    }
}
