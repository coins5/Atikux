using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Atikux.classes
{
    public class SunatServices
    {
        DocumentosElectronicoSunat.billServiceClient service;
        // string inputsPath = @"D:\nodejs\Atikux\inputs\";
        // string outputPath = @"D:\nodejs\Atikux\outputs\";

        string rootFolder = Environment.CurrentDirectory;
        string inputsPath = "";
        string outputPath = "";

        public SunatServices() {
            service = new DocumentosElectronicoSunat.billServiceClient();
            ServicePointManager.UseNagleAlgorithm = true;
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = true;

            inputsPath = rootFolder + @"\inputs\";
            outputPath = rootFolder + @"\outputs\";
        }

        public string sendDocument(string NameOfFileZip) {
            string response = "";

            string folder = inputsPath + NameOfFileZip;
            byte[] allbytes = File.ReadAllBytes(folder);

            string FechaHora = DateTime.Now.ToString("yyyy-MM-dd hh;mm;ss");

            try
            {
                service.Open();
                byte[] resultBytes = service.sendBill(NameOfFileZip, allbytes);
                service.Close();

                string outputFile = outputPath + " " + FechaHora + " " + NameOfFileZip;

                using (FileStream fs = new FileStream(outputFile, FileMode.Create))
                {
                    fs.Write(resultBytes, 0, resultBytes.Length);
                    fs.Close();
                }
                response = "Zip received, unzip and readXML";
                Console.WriteLine("OK: Zip received, unzip and readXML");

            }
            // catch (Exception ex) {
            catch (System.ServiceModel.FaultException ex)
            {
                Console.WriteLine("EXCEPTION :/");
                
                Console.WriteLine("#### [DEVELOPER] ####");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("#### [/DEVELOPER] ####");

                Console.WriteLine("#### [CLIENT] ####");
                Console.WriteLine(ex.Code.Name);
                Console.WriteLine("#### [/CLIENT] ####");

                response += ex.Code.Name;
            }

            return response;
        }
    }
}
