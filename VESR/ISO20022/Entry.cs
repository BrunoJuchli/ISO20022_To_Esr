using System;
using System.Collections.Generic;
using System.Linq;

namespace VESR.ISO20022
{
    class Entry
    {
        public Entry(int referenceNumber, DateTime bookingDate, DateTime valutaDate, IEnumerable<TransactionDetails> transactions)
        {
            this.ReferenceNumber = referenceNumber;
            this.BookingDate = bookingDate;
            this.ValutaDate = valutaDate;
            this.Transactions = transactions.ToList();
        }

        public int ReferenceNumber { get; }
        public DateTime BookingDate { get; }
        public DateTime ValutaDate { get; }
        public IReadOnlyList<TransactionDetails> Transactions { get; }
    }
}
