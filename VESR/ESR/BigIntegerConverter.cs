using FileHelpers;
using System.Numerics;

namespace VESR.ESR
{
    public class BigIntegerConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            BigInteger value = BigInteger.Parse(from);
            return value;
        }

        public override string FieldToString(object fieldValue)
        {
            string value = ((BigInteger)fieldValue).ToString();
            return value;
        }
    }
}
