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

        public static Dictionary<string, CompressedResult> Compress(string str)
        {
            var output = new Dictionary<string, CompressedResult>();
            byte[] dataBytes = encoder.GetBytes(str);

            //GZip
            byte[] gZipCompressedData = GZip.Compress(dataBytes);

            var gZipResult = new CompressedResult();
            gZipResult.bytes = gZipCompressedData;
            gZipResult.base64String = Convert.ToBase64String(gZipCompressedData);

            output["GZip"] = gZipResult;

            //LZ77

            byte[] lz77CompressedData = LZ77.Compress(dataBytes);

            var lz77Result = new CompressedResult();
            lz77Result.bytes = lz77CompressedData;
            lz77Result.base64String = Convert.ToBase64String(lz77CompressedData);

            output["LZ77"] = lz77Result;

            return output;
        }

        public static Dictionary<String, DecompressedResult> Decompress(Dictionary<String, CompressedResult> compressedResults)
        {
            var output = new Dictionary<String, DecompressedResult>();
            
            // Gzip
            var gZipCompressedResult = compressedResults["GZip"];
            byte[] dataBytes = Convert.FromBase64String(gZipCompressedResult.base64String);
            byte[] decompressedData = GZip.Decompress(dataBytes);

            var gZipResult = new DecompressedResult();
            gZipResult.bytes = decompressedData;
            gZipResult.decompressedString = Converter.BytesToString(decompressedData);

            output["GZip"] = gZipResult;

            // lz77
            var lz77CompressedResult = compressedResults["LZ77"];
            dataBytes = Convert.FromBase64String(lz77CompressedResult.base64String);
            decompressedData = LZ77.Decompress(dataBytes);

            var lz77Result = new DecompressedResult();
            lz77Result.bytes = decompressedData;
            lz77Result.decompressedString = Converter.BytesToString(decompressedData);

            output["LZ77"] = gZipResult;

            return output;
        }
    }

}