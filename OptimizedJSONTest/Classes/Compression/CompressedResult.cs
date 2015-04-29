using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Optimized_JSON_Compression
{
    public class CompressedResult
    {
        public CompressionMethod method = CompressionMethod.None;
        public byte[] bytes;
        public string base64String = "";
    }

    public class DecompressedResult
    {
        public CompressionMethod method = CompressionMethod.None;
        public byte[] bytes;
        public string decompressedString = "";
    }
}