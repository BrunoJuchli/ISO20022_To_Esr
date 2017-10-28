using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Xml.Linq;

namespace VESR.ISO20022
{
    class ISO20022Parser
    {
        public static IEnumerable<Entry> Parse(Stream inputXmlStream)
        {
            XDocument doc = XDocument.Load(inputXmlStream);
            doc.Root.GetDefaultNamespace();

            var entries = doc.Root
                .Descendants(doc.ResolveName("BkToCstmrDbtCdtNtfctn"))
                .Descendants(doc.ResolveName("Ntry"));
            foreach (var entry in entries)
            {
                yield return new Entry(
                    int.Parse(entry.Element(doc.ResolveName("NtryRef")).Value),
                    ParseDate(entry.Element(doc.ResolveName("BookgDt")).Element(doc.ResolveName("Dt")).Value),
                    ParseDate(entry.Element(doc.ResolveName("ValDt")).Element(doc.ResolveName("Dt")).Value),
                    ParseTransactionDetails(entry));
            }
        }

        private static IEnumerable<TransactionDetails> ParseTransactionDetails(XElement entry)
        {
            var transactionDetails = entry
                .Descendants(entry.ResolveName("NtryDtls"))
                .Descendants(entry.ResolveName("TxDtls"));
            foreach (var transactionDetail in transactionDetails)
            {
                decimal ammount = Decimal.Parse(transactionDetail.Element(entry.ResolveName("Amt")).Value);
                BigInteger referenceNumber = BigInteger.Parse(transactionDetail
                        .Element(entry.ResolveName("RmtInf"))
                        .Element(entry.ResolveName("Strd"))
                        .Element(entry.ResolveName("CdtrRefInf"))
                        .Element(entry.ResolveName("Ref")).Value);
                yield return new TransactionDetails(ammount, 0, referenceNumber);
            }
        }

        private static DateTime ParseDate(string value)
        {
            return DateTime.ParseExact(value, "yyyy-mm-dd", CultureInfo.CurrentUICulture);
        }
    }
}
