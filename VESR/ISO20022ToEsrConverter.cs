using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VESR.ESR;
using VESR.ISO20022;

namespace VESR
{
    public class ISO20022ToEsrConverter
    {
        public static void Convert(Stream inputXmlStream, Stream outputEsrStream)
        {
            var entries = ISO20022Parser.Parse(inputXmlStream);
            var esrRecords = entries.SelectMany(ConvertToEsrRecords).ToList();

            Console.WriteLine($"Es wurden {esrRecords.Count} Einträge gelesen");

            var engine = new FileHelperEngine<EsrRecord>();
            using (StreamWriter writer = new StreamWriter(outputEsrStream))
            {
                engine.WriteStream(writer, esrRecords);
            }
        }

        private static IEnumerable<EsrRecord> ConvertToEsrRecords(Entry entry)
        {
            foreach (var transaction in entry.Transactions)
            {
                yield return new EsrRecord
                {
                    Teilnehmernummer = entry.ReferenceNumber,
                    GutschriftsDatum1 = entry.BookingDate,
                    GutschriftsDatum2 = entry.BookingDate,
                    GutschriftsDatum3 = entry.BookingDate,
                    ValutaDatum = entry.ValutaDate,
                    Betrag = transaction.Ammount,
                    ReferenzNummer = transaction.ReferenceNumber,
                    Einzahlungstaxe = transaction.TotalChargesAndTaxes,
                };
            }
        }
    }
}
