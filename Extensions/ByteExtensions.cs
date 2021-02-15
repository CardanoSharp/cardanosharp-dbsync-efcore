using System;
using System.Collections.Generic;
using System.Text;

namespace CardanoSharp.DbSync.EFCore.Extensions
{
    public static class ByteExtensions
    {
        public static string GetHex(this byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "").ToLower();
        }
    }
}
