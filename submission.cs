using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://markusm3.github.io/CSE445_Assignment4/Hotels.xml";
        public static string xmlErrorURL = "https://markusm3.github.io/CSE445_Assignment4/HotelErrors.xml";
        public static string xsdURL = "https://markusm3.github.io/CSE445_Assignment4/Hotels.xsd";
        
        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("", xsdUrl);

                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);

                string errors = "";
                doc.Schemas.Add(schemas);
                doc.Validate((sender, e) =>
                {
                    errors += e.Message + "\n";
                });

                return string.IsNullOrEmpty(errors) ? "No errors are found" : errors.Trim();
            }
            catch (Exception ex)
            {
                return "Error during validation: " + ex.Message;
            }
        }

        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);

                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);

                XmlDocument testDoc = JsonConvert.DeserializeXmlNode(jsonText);

                return jsonText;
            }
            catch (Exception ex)
            {
                return "Error during XML to JSON conversion: " + ex.Message;
            }
        }
    }

}



