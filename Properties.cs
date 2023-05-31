using System;
namespace L4751
{
    public class Properties
    {
        public int Number { get; set; }
        public int[] Digits { get; set; }
        public int DS { get; set; }
        public int DP { get; set; }
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

            DS = 0;
            DP = 1;
            foreach (var digit in GetDigits(Number))
            {
                DS += digit;
                DP *= digit;
            }

            int mp = DP;
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

        public int NumberOfFactors()
        {
            int retval = 2;

            for ( int i = 2; i <= Number / 2; i++)
            {
                if (Number % i == 0)
                    retval++;
            }
            return retval;
        }

        public int DigitAtPosition(int pos)
        {
            return Digits[pos];
        }

        public override string ToString()
        {
            return $"  {Number,4} {DS,4} {DP,4} {MultiplicativePersistance,4}";
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

