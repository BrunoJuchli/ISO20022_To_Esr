using System;
using System.IO;

namespace VESR
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = args[0];
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine($"Die Datei {inputFilePath} existiert nicht");
            }

            string directoryPath = Path.GetDirectoryName(inputFilePath);
            string outputFileName = Path.GetFileNameWithoutExtension(inputFilePath);
            string outputFilePath = Path.Combine(directoryPath, $"{outputFileName}.txt");

            Console.WriteLine($"Lese XML Datei {inputFilePath}");
            Console.WriteLine($"und konvertiere nach ESR Datei {outputFilePath}");

            try
            {
                using (Stream inputXmlStream = File.OpenRead(inputFilePath), outputEsrStream = File.Open(outputFilePath, FileMode.Create))
                {
                    ISO20022ToEsrConverter.Convert(inputXmlStream, outputEsrStream);
                }
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.ToString());
            }

            Console.WriteLine("Konvertierung abgeschlossen");
            Console.WriteLine("[Enter]-Taste drücken um Fenster zu schliessen");
            Console.ReadLine();
        }
    }
}
