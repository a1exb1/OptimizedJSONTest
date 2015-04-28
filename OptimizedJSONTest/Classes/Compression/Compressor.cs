using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using Optimized_JSON_Compression;


namespace OptimizedJSONTest
{

    public enum CompressionMethod
    {
        None,
        GZip,
        Huffman
    };

    public class Compressor
    {
        private static Encoding encoder = UTF8Encoding.UTF8;

        public static CompressedResult Compress(CompressionMethod method, string str)
        {
            var output = new CompressedResult();

            if (method == CompressionMethod.GZip)
            {
                byte[] dataBytes = encoder.GetBytes(str);
                byte[] compressedData = GZip.Compress(dataBytes);

                output.bytes = compressedData;
                output.base64String = Convert.ToBase64String(compressedData);
            }

            return output;
        }

        public static DecompressedResult Decompress(CompressionMethod method, CompressedResult compressedResult)
        {
            var output = new DecompressedResult();

            if (method == CompressionMethod.GZip)
            {
                byte[] dataBytes = Convert.FromBase64String(compressedResult.base64String);
                byte[] decompressedData = GZip.Decompress(dataBytes);

                output.bytes = decompressedData;
                output.decompressedString = Converter.BytesToString(decompressedData);
            }

            return output;
        }
    }

}