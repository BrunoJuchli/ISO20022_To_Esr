using System.Numerics;

namespace VESR.ISO20022
{
    class TransactionDetails
    {
        public TransactionDetails(decimal ammount, decimal totalChargesAndTaxes, BigInteger referenceNumber)
        {
            this.Ammount = ammount;
            this.TotalChargesAndTaxes = totalChargesAndTaxes;
            this.ReferenceNumber = referenceNumber;
        }

        public decimal Ammount { get; }
        public decimal TotalChargesAndTaxes { get; }
        public BigInteger ReferenceNumber { get; }
    }
}
