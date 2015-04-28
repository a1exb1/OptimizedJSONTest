using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;


namespace OptimizedJSONTest
{
    public class CompressedResult
    {
        public byte[] bytes;
        public string base64String = "";

    }

    public class DecompressedResult {

        
        public byte[] bytes;
        public string decompressedString = "";
    }

    public enum CompressionMethod
    {
        None,
        GZip,
        Huffman
    };

    public class Compressor
    {

        public static CompressedResult Compress(CompressionMethod method, string str)
        {
            var output = new CompressedResult();

            if (method == CompressionMethod.GZip)
            {
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] dataBytes = encoding.GetBytes(str);

                Console.WriteLine("Data length: " + dataBytes.Length);

                byte[] compressedData = GZip.Compress(dataBytes);

                Console.WriteLine("Compressed length: " + compressedData.Length);

                byte[] decompressedData = GZip.Decompress(compressedData);

                Console.WriteLine("Decompressed length: " + decompressedData.Length);
                Console.WriteLine("Decompressed string: " + encoding.GetString(decompressedData));

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

                Console.WriteLine("Data length: " + dataBytes.Length);

                byte[] decompressedData = GZip.Decompress(dataBytes);

                Console.WriteLine("Decompressed length: " + decompressedData.Length);
                Console.WriteLine("Decompressed string: " + Converter.BytesToString(decompressedData));

                output.bytes = decompressedData;
                output.decompressedString = Converter.BytesToString(decompressedData);
            }

            return output;
        }
    }
}