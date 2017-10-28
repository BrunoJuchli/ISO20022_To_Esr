using FileHelpers;
using System;
using System.Numerics;

namespace VESR.ESR
{
    // gemäss ZKB https://www.zkb.ch/media/dok/efinance/vesr-handbuch-fk.pdf Seite 23
    [FixedLengthRecord]
    public class EsrRecord
    {
        private const string DateFormat = "yyMMdd";

        [FieldFixedLength(3)]
        [FieldAlign(AlignMode.Right, '0')]
        public int Transaktionsart;

        /// <summary>
        /// Letzte Ziffer Prüfziffer. Gerechnet nach Modulo 10, rekursiv.
        /// </summary>
        [FieldFixedLength(9)]
        [FieldAlign(AlignMode.Right, '0')]
        public int Teilnehmernummer;

        /// <summary>
        /// 0-5: Kundenid-Nummer
        /// 6-25: Referenz Kunde
        /// 26: Prüfziffer
        /// </summary>
        [FieldFixedLength(27)]
        [FieldAlign(AlignMode.Right, '0')]
        [FieldConverter(typeof(BigIntegerConverter))]
        public BigInteger ReferenzNummer;

        [FieldFixedLength(10)]
        [FieldConverter(typeof(MoneyConverter))]
        [FieldAlign(AlignMode.Right, '0')]
        public Decimal Betrag;

        /// <summary>
        /// Bank-interne Angabe, für Nachforschungen.
        /// </summary>
        [FieldFixedLength(10)]
        public string Aufgabereferenz;

        /// <summary>
        /// Bei LSV: Überweisungsdatum der Bankstelle.
        /// </summary>
        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, DateFormat)]
        public DateTime GutschriftsDatum1;

        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, DateFormat)]
        public DateTime GutschriftsDatum2;

        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, DateFormat)]
        public DateTime GutschriftsDatum3;

        /// <summary>
        /// Bank interne Angabe für Nachforschungen.
        /// </summary>
        [FieldFixedLength(9)]
        [FieldAlign(AlignMode.Right, '0')]
        public int MikrofilmNummer;

        /// <summary>
        /// 0 = nicht rejectiert
        /// 1 = rejectiert
        /// 5 = Massenreject
        /// </summary>
        [FieldFixedLength(1)]
        [FieldAlign(AlignMode.Right, '0')]
        public int RejectCode;

        [FieldFixedLength(6)]
        [FieldConverter(ConverterKind.Date, DateFormat)]
        public DateTime ValutaDatum;

        /// <summary>
        /// "Füllnullen"
        /// </summary>
        [FieldFixedLength(3)]
        [FieldAlign(AlignMode.Right, '0')]
        public readonly int ValutaPostfix = 0;

        [FieldFixedLength(4)]
        [FieldConverter(typeof(MoneyConverter))]
        [FieldAlign(AlignMode.Right, '0')]
        public decimal Einzahlungstaxe;
    }
}
