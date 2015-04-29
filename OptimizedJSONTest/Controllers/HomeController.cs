using Optimized_JSON_Compression;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OptimizedJSONTest.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Compress(string str)
        {
            //str = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
            //    "Etiam malesuada lectus id lectus scelerisque pulvinar. " +
            //    "Etiam ut turpis libero, at condimentum turpis. " +
            //    "Integer in purus ut purus eleifend posuere vel ut nibh. " +
            //    "Praesent interdum rhoncus nisl, id consectetur orci luctus quis. " +
            //    "Aliquam molestie ornare nisi a sollicitudin. " +
            //    "Integer nec erat ut ipsum placerat tristique. " +
            //    "In pulvinar sapien at erat ultricies iaculis. " +
            //    "Maecenas mollis est sit amet urna scelerisque eu accumsan lacus rutrum. " +
            //    "Donec purus metus, blandit dignissim varius id, porttitor sed metus. " +
            //    "Cras nec sapien vitae sem porta dapibus eget sit amet eros. " +
            //    "Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. " +
            //    "Suspendisse potenti. Sed id diam eget nisi laoreet scelerisque vitae vulputate lacus. " +
            //    "Phasellus quis leo sapien, id hendrerit mi. Morbi feugiat mollis nisl id egestas.";

            var compressedResults = Compressor.Compress(str);
            ViewBag.CompressedResults = compressedResults;

            ViewBag.OriginalString = str;
            ViewBag.DecompressedResults = Compressor.Decompress(compressedResults); 

            return View();
        }

        //[HttpPost]
        //public ActionResult Decompress(string str2)
        //{
        //    CompressedResult result = new CompressedResult
        //    {
        //        base64String = str2
        //    };
        //    return Content(Compressor.Decompress(result).decompressedString);
        //}

        public ActionResult rsa()
        {
            byte[] toEncryptData = Encoding.ASCII.GetBytes("hello world");

            //Generate keys
            RSACryptoServiceProvider rsaGenKeys = new RSACryptoServiceProvider();
            string privateXml = rsaGenKeys.ToXmlString(true);
            string publicXml = rsaGenKeys.ToXmlString(false);

            //Encode with public key
            RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
            rsaPublic.FromXmlString(publicXml);
            byte[] encryptedRSA = rsaPublic.Encrypt(toEncryptData, false);
            string EncryptedResult = Encoding.Default.GetString(encryptedRSA);


            //Decode with private key
            var rsaPrivate = new RSACryptoServiceProvider();
            rsaPrivate.FromXmlString(privateXml);
            byte[] decryptedRSA = rsaPrivate.Decrypt(encryptedRSA, false);
            string originalResult = Encoding.Default.GetString(decryptedRSA);



            return Content(EncryptedResult);
        }
    }
}