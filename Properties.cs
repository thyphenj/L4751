using System;
namespace L4751
{
    public class Properties
    {
        public int Number { get; set; }
        public int[] Digits { get; set; }
        public int DigitSum { get; set; }
        public int DigitProduct { get; set; }
        public int MultiplicativePersistance { get; set; }

        public Properties(int num)
        {
            Number = num;

            string numstr = Number.ToString();
            Digits = new int[numstr.Length];
            for ( int i = 0; i < numstr.Length; i++)
            {
                Digits[i] = int.Parse(numstr[i].ToString());
            }

            DigitSum = 0;
            DigitProduct = 1;
            foreach (var digit in GetDigits(Number))
            {
                DigitSum += digit;
                DigitProduct *= digit;
            }

            int mp = DigitProduct;
            MultiplicativePersistance = 1;
            while (mp > 9)
            {
                MultiplicativePersistance++;
                int product = 1;
                foreach (var digit in GetDigits(mp))
                {
                    product *= digit;
                }
                mp = product;
            }
        }

        public int DigitAtPosition(int pos)
        {
            return Digits[pos];
        }

        public override string ToString()
        {
            return $"{Number,4} {DigitSum,4} {DigitProduct,4} {MultiplicativePersistance,4}";
        }

        private List<int> GetDigits(int num)
        {
            var retval = new List<int>();
            while (num > 0)
            {
                retval.Add(num % 10);
                num /= 10;
            }
            return retval;
        }
    }
}

