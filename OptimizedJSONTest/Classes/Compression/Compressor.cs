using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;
using Optimized_JSON_Compression;


namespace Optimized_JSON_Compression
{

    public enum CompressionMethod
    {
        None,
        GZip,
        LZ77
    };

    public class Compressor
    {
        private static Encoding encoder = UTF8Encoding.UTF8;

        public static List<CompressedResult> Compress(List<CompressionMethod> methods, string str)
        {
            var output = new List<CompressedResult>();
            byte[] dataBytes = encoder.GetBytes(str);


            foreach (var method in methods)
            {
                byte [] compressedData = Converter.StringToBytes("");

                switch (method)
	            {
                    case CompressionMethod.GZip: 

                        compressedData = GZip.Compress(dataBytes);

                        break;

                    case CompressionMethod.LZ77: 

                        compressedData = LZ77.Compress(dataBytes);

                        break;

                    default: break;
	            }

                var result = new CompressedResult();
                result.bytes = compressedData;
                result.base64String = Convert.ToBase64String(compressedData);
                result.method = method;

                output.Add(result);
            }

            return output;
        }

        public static List<DecompressedResult> Decompress(List<CompressedResult> compressedResults)
        {
            var output = new List<DecompressedResult>();

            foreach (var item in compressedResults)
            {
                CompressedResult compressedResult = item;
                byte[] dataBytes = Convert.FromBase64String(compressedResult.base64String);
                byte[] decompressedData = Converter.StringToBytes("");

                switch (compressedResult.method)
                {
                    case CompressionMethod.GZip:

                        decompressedData = GZip.Decompress(dataBytes);

                        break;

                    case CompressionMethod.LZ77:

                        decompressedData = LZ77.Decompress(dataBytes);

                        break;

                    default: break;
                }

                var result = new DecompressedResult();
                result.bytes = decompressedData;
                result.decompressedString = Converter.BytesToString(decompressedData);
                result.method = compressedResult.method;

                output.Add(result);
            }

            return output;
        }
    }

}