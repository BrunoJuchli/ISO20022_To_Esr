using FileHelpers;
using System;

namespace VESR.ESR
{
    public class MoneyConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            decimal value = Convert.ToDecimal(Decimal.Parse(from) / 100);
            return value;
        }

        public override string FieldToString(object fieldValue)
        {
            string value = ((decimal)fieldValue).ToString("#.00#").Replace(".", "");
            return value;
        }
    }
}
