namespace L4751;

using System;
using System.Linq;

class Program
{
    //static int[] fibonacci = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };
    static int[] fibonacci = { 13, 21, 34, 55, 89 };
    static int[] triangle = { 0, 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, 66, 78, 91, 105, 120, 136, 153, 171, 190, 210, 231, 253, 276, 300, 325, 351, 378, 406, 435, 465, 496, 528, 561, 595, 630, 666, 703, 741, 780, 820, 861, 903, 946, 990 };
    static int[] pyramid = { 1, 5, 14, 30, 55, 91 };
    static int[] happy = { 1, 7, 10, 13, 19, 23, 28, 31, 32, 44, 49, 68, 70, 79, 82, 86, 91, 94, 97 };
    static int[] lucky = { 1, 3, 7, 9, 13, 15, 21, 25, 31, 33, 37, 43, 49, 51, 63, 67, 69, 73, 75, 79, 87, 93, 99 };

    static void Main(string[] args)
    {

        // --------------------------------------------------------------------------------------
        var ac28 = new Clue("ac28 : DP equals 180");

        // -- the digits must be (5,6,6) or (4,5,9)

        ac28.Add(new Properties(566));
        ac28.Add(new Properties(656));
        ac28.Add(new Properties(665));
        ac28.Add(new Properties(459));
        ac28.Add(new Properties(495));
        ac28.Add(new Properties(549));
        ac28.Add(new Properties(594));
        ac28.Add(new Properties(945));
        ac28.Add(new Properties(954));

        // --------------------------------------------------------------------------------------
        var ac23 = new Clue("ac23 : Palindromic prime");

        ac23.Add(new Properties(11));

        // --------------------------------------------------------------------------------------
        var dn24 = new Clue("dn24 : DP is square");

        // -- from ac23 the first digit is 1

        for (int num = 10; num < 20; num++)
        {
            var props = new Properties(num);

            if (!new int[] { 1, 4, 9, 16, 25, 36, 49, 64, 81 }.Contains(props.DigitProduct))
                continue;

            dn24.Add(props);
        }

        // --------------------------------------------------------------------------------------
        var ac01 = new Clue("ac01 : Triangular with triangular DP");

        foreach (var num in triangle.Where(x => x > 99))
        {
            var props = new Properties(num);

            if (!triangle.Contains(props.DigitProduct))
                continue;
            if (props.DigitAtPosition(1) != 5)
                continue;

            ac01.Add(props);
        }

        // --------------------------------------------------------------------------------------
        var dn01 = new Clue("dn01 : (DS + DP) is odd multiple of 5");

        var ac01Digit0 = ac01.DigitsAtPosition(0);
        for (int num = 10; num < 100; num++)
        {
            var props = new Properties(num);

            if ((props.DigitSum + props.DigitProduct) % 10 != 5)
                continue;
            if (!ac01Digit0.Contains(props.DigitAtPosition(0)))
                continue;

            dn01.Add(props);
        }

        // --------------------------------------------------------------------------------------
        var dn02 = new Clue("dn02 : Palindrome and multiple of 5 with MP of 2");

        // -- We know that the first and last digits are both 5

        for (int num = 505; num < 600; num += 10)
        {
            var props = new Properties(num);

            if (props.MultiplicativePersistance != 2)
                continue;

            dn02.Add(props);
        }

        // --------------------------------------------------------------------------------------
        var dn11 = new Clue("dn11 : Square");

        for (int i = 4; i < 10; i++)
        {
            var props = new Properties(i * i);

            dn11.Add(props);
        }

        // --------------------------------------------------------------------------------------
        var ac14 = new Clue("ac14 : Consecutive odd digits in ascending or descending order, with triangular DP");

        for (int i = 1; i < 6; i++)
        {
            // -- Ascending digits
            var props = new Properties(i * 100 + (i + 2) * 10 + (i + 4));

            if (!triangle.Contains(props.DigitProduct))
                continue;
            if (!dn11.DigitsAtPosition(1).Contains(props.DigitAtPosition(0)))
                continue;

            ac14.Add(props);

            // -- Descending digits
            props = new Properties((i + 4) * 100 + (i + 2) * 10 + i);

            if (!dn11.DigitsAtPosition(1).Contains(props.DigitAtPosition(0)))
                continue;
            if (!triangle.Contains(props.DigitProduct))
                continue;

            ac14.Add(props);
        }

        // --------------------------------------------------------------------------------------
        var dn18 = new Clue("dn18 : Lucky and happy number");

        foreach (var num in happy.Where(z => z > 9))
        {
            if (!lucky.Contains(num))
                continue;
            if (num + 26 > 99)
                continue;

            dn18.Add(new Properties(num));
        }

        // --------------------------------------------------------------------------------------
        var ac09 = new Clue("ac09 : 18dn + 26");

        foreach (var p in dn18.PossAnswers)
        {
            ac09.Add(new Properties(p.Number + 26));
        }

        // --------------------------------------------------------------------------------------
        var ac03 = new Clue("ac03 : Square number with a square DS");

        for (int i = 10; i < 31; i++)
        {
            var props = new Properties(i * i);

            if (props.DigitAtPosition(1) == 0)
                continue;
            if (props.DigitAtPosition(2) == 0)
                continue;
            if (!new int[] { 1, 4, 9, 16, 25 }.Contains(props.DigitSum))
                continue;

            ac03.Add(props);
        }

        // --------------------------------------------------------------------------------------
        var dn03 = new Clue("dn03 : 3ac plus or minus 3");

        var ac09Digit0 = ac09.DigitsAtPosition(0);

        foreach (var p in ac03.PossAnswers)
        {
            foreach (int i in new int[] { -3, 3 })
            {
                var props = new Properties(p.Number + i);

                if (!ac09Digit0.Contains(props.DigitAtPosition(1)))
                    continue;

                dn03.Add(props);
            }
        }

        // --------------------------------------------------------------------------------------
        var ac20 = new Clue("ac20 : Prime whose DP is square and DS is a factor of 13dn");

        for (int i = 111; i < 1000; i++)
        {
            // -- digits at position 2 and 3 can't be zero
            if (i % 1000 < 100 || i % 100 < 10) continue;

            foreach (int j in dn18.DigitsAtPosition(1))
            {
                int num = i * 10 + j;

                if (!isPrime(num))
                    continue;

                var props = new Properties(num);

                var root = (int)(Math.Sqrt(props.DigitProduct));
                if (root * root != props.DigitProduct)
                    continue;

                ac20.Add(props);
            }
        }

        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // -- a bit of removal of chod from ac03
        var newPoss = new Clue(ac03.Rubric);

        foreach (var p in dn03.PossAnswers)
        {
            foreach (int i in new int[] { -3, 3 })
            {
                var props = new Properties(p.Number + i);
                foreach (var q in ac03.PossAnswers)
                {
                    if (q.Number == props.Number)
                        newPoss.Add(props);
                }
            }
        }
        ac03 = newPoss;

        // --------------------------------------------------------------------------------------
        // -- a bit of removal of chod from ac09
        newPoss = new Clue(ac09.Rubric);

        foreach (var p in ac09.PossAnswers)
        {
            if (dn03.DigitsAtPosition(1).Contains(p.DigitAtPosition(0)))
                newPoss.Add(p);
        }
        ac09 = newPoss;

        // -- dn11 : Square
        newPoss = new Clue(dn11.Rubric);

        foreach (var p in dn11.PossAnswers)
        {
            if (ac14.DigitsAtPosition(0).Contains(p.DigitAtPosition(1)))
                newPoss.Add(p);
        }
        dn11 = newPoss;

        Console.WriteLine(ac01);
        Console.WriteLine(ac03);
        Console.WriteLine(ac09);
        Console.WriteLine(ac14);
        Console.WriteLine(ac23);
        Console.WriteLine(ac28);
        Console.WriteLine("--------------------");
        Console.WriteLine(dn01);
        Console.WriteLine(dn02);
        Console.WriteLine(dn03);
        Console.WriteLine(dn11);
        Console.WriteLine(dn18);
        Console.WriteLine(dn24);
    }

    private static bool isPrime(int num)
    {
        if (num == 1)
            return false;

        int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

        bool retval = true;
        foreach (int p in primes)
        {
            if (num % p == 0)
            {
                retval = false;
                break;
            }
        }

        return retval;
    }
}