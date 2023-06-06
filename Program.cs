namespace L4751;

using System;
using System.Linq;

class Program
{
    static int[] triangle = { 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, 66, 78, 91, 105, 120, 136, 153, 171, 190, 210, 231, 253, 276, 300, 325, 351, 378, 406, 435, 465, 496, 528, 561, 595, 630, 666, 703, 741, 780, 820, 861, 903, 946, 990 };
    static int[] happy = { 1, 7, 10, 13, 19, 23, 28, 31, 32, 44, 49, 68, 70, 79, 82, 86, 91, 94, 97 };
    static int[] lucky = { 1, 3, 7, 9, 13, 15, 21, 25, 31, 33, 37, 43, 49, 51, 63, 67, 69, 73, 75, 79, 87, 93, 99 };
    static int[] squares = { 1, 4, 9, 16, 25, 36, 49, 64, 81 };
    static int[] primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };

    static int[] pyramid = { 14, 30, 55, 91 };
    static int[] fibonacci = { 13, 21, 34, 55, 89 };

    static void Main(string[] args)
    {
        // --------------------------------------------------------------------------------------
        var dn02 = new Clue("dn02", "Palindrome and multiple of 5 with MP of 2", 3);

        // -- We know that the first and last digits are both 5

        for (int num = 505; num < 600; num += 10)
        {
            var p = new Properties(num);
            if (p.MP != 2)
                continue;

            dn02.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var ac01 = new Clue("ac01", "Triangular with triangular DP", 3);

        foreach (var num in triangle.Where(z => z > 99))
        {
            var p = new Properties(num);
            if (p.DigitAtPosition(1) == 0)
                continue;
            if (!triangle.Contains(p.DP))
                continue;

            ac01.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var ac23 = new Clue("ac23", "Palindromic prime", 2);
        var dn24 = new Clue("dn24", "DP is square", 2);
        var dn19 = new Clue("dn19", "DP equals another entry", 3);

        ac23.Add(new Properties(11));

        for (int num = 11; num < 20; num++)
        {
            var p = new Properties(num);
            if (!squares.Contains(p.DP))
                continue;

            dn24.Add(p);
        }

        for (int num = 110; num < 1000; num++)
        {
            var p = new Properties(num);
            if (p.DP < 10)
                continue;
            if (p.DigitAtPosition(1) != 1)
                continue;

            dn19.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var ac10 = new Clue("ac10", "Palindrome", 2);

        for (int i = 1; i < 10; i++)
        {
            ac10.Add(new Properties(11 * i));
        }

        // --------------------------------------------------------------------------------------
        var ac13 = new Clue("ac13", "Digits have opposite parity and DP equals another entry", 2);

        for (int i = 1; i < 10; i++)
        {
            int frm = 1 - i % 2;
            for (int j = frm; j < 10; j += 2)
            {
                if (i * j < 10)
                    continue;

                ac13.Add(new Properties(i * 10 + j));
            }
        }

        // --------------------------------------------------------------------------------------
        var ac14 = new Clue("ac14", "Consecutive odd digits in ascending or descending order, with triangular DP", 3);

        for (int i = 1; i < 6; i += 2)
        {
            int dp = i * (i + 2) * (i + 4);
            if (!triangle.Contains(dp))
                continue;

            ac14.Add(new Properties(100 * i + 10 * (i + 2) + (i + 4))); // -- ascending
            ac14.Add(new Properties(100 * (i + 4) + 10 * (i + 2) + i)); // -- descending
        }

        // --------------------------------------------------------------------------------------
        var ac17 = new Clue("ac17", "DS is triangular", 3);

        for (int num = 101; num < 1000; num++)
        {
            var p = new Properties(num);
            if (num % 10 == 0)  // -- final digit can't be zero
                continue;
            if (!triangle.Contains(p.DS))
                continue;

            ac17.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var ac20 = new Clue("ac20", "Prime whose DP is square and DS is a factor of 13dn", 4);

        for (int num = 1110; num < 10000; num++)
        {
            if (!isPrime(num))
                continue;

            var p = new Properties(num);
            var root = (int)(Math.Sqrt(p.DP) + 0.001);
            if (root * root != p.DP)
                continue;
            if (p.DP == 0)      // -- **** should we allow zero here or not?
                continue;

            ac20.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var ac25 = new Clue("ac25", "Prime", 2);

        foreach (var num in primes.Where(z => z > 9))
        {
            ac25.Add(new Properties(num));
        }

        // --------------------------------------------------------------------------------------
        var ac26 = new Clue("ac26", "DP is a cube", 2);

        for (int num = 10; num < 100; num++)
        {
            var p = new Properties(num);
            if (num % 10 == 0)  // -- final digit can't be zero
                continue;
            if (!isCube(p.DP))
                continue;

            ac26.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var ac28 = new Clue("ac28", "DP equals 180", 3);

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
        var dn01 = new Clue("dn01", "(DS + DP) is odd multiple of 5", 2);

        for (int num = 10; num < 100; num++)
        {
            var p = new Properties(num);
            if ((p.DS + p.DP) % 10 != 5)
                continue;

            dn01.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var dn04 = new Clue("dn04", "Greater than 8dn and DS equals another entry", 2);

        for (int num = 10; num < 100; num++)
        {
            var p = new Properties(num);
            if (p.DS < 10)
                continue;

            dn04.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var dn05 = new Clue("dn05", "2 times a square", 2);

        for (int i = 3; i * i < 50; i++)
        {
            dn05.Add(new Properties(2 * i * i));
        }

        // --------------------------------------------------------------------------------------
        var dn06 = new Clue("dn06", "Has 8 factors including 1 and itself", 3);

        for (int num = 100; num < 1000; num++)
        {
            var p = new Properties(num);
            if (p.NumberOfFactors() != 8)
                continue;

            dn06.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var dn08 = new Clue("dn08", "Square-pyramidal number (ie sum of the first n squares)", 2);

        foreach (var num in pyramid)
        {
            dn08.Add(new Properties(num));
        }

        // --------------------------------------------------------------------------------------
        var dn11 = new Clue("dn11", "Square", 2);

        foreach (var num in squares.Where(z => z > 9))
        {
            dn11.Add(new Properties(num));
        }

        // --------------------------------------------------------------------------------------
        var dn12 = new Clue("dn12", "DP is a single digit even number", 2);

        for (int num = 11; num < 100; num++)
        {
            var p = new Properties(num);
            if (num % 10 == 0)  // -- final digit cannot be zero
                continue;
            if (p.DP > 9)
                continue;
            if (p.DP % 2 != 0)
                continue;

            dn12.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var dn13 = new Clue("dn13", "Multiple of 7", 2);

        for (int num = 14; num < 100; num += 7)
        {
            dn13.Add(new Properties(num));
        }

        // --------------------------------------------------------------------------------------
        var dn15 = new Clue("dn15", "DP equals another entry", 2);

        for (int num = 10; num < 100; num++)
        {
            var p = new Properties(num);
            if (p.DP < 10)
                continue;

            dn15.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var dn16 = new Clue("dn16", "Prime", 2);

        foreach (var num in primes.Where(z => z > 9))
        {
            dn16.Add(new Properties(num));
        }

        // --------------------------------------------------------------------------------------
        var dn22 = new Clue("dn22", "DP is a power of 2", 3);

        for (int num = 110; num < 1000; num++)
        {
            var p = new Properties(num);
            if (p.DigitAtPosition(1) == 0)
                continue;
            if (!isAPowerOfTwo(p.DP))
                continue;

            dn22.Add(p);
        }

        // --------------------------------------------------------------------------------------
        var dn25 = new Clue("dn25", "Lucky number", 2);
        foreach (var num in lucky.Where(z => z > 9))
        {
            dn25.Add(new Properties(num));
        }

        // --------------------------------------------------------------------------------------
        var dn27 = new Clue("dn27", "Fibonacci number", 2);

        foreach (int num in fibonacci)
        {
            dn27.Add(new Properties(num));
        }

        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // -- This lot are sort of cross-declared

        var ac03 = new Clue("ac03", "Square number with a square DS", 3);
        var dn03 = new Clue("dn03", "3ac plus or minus 3", 3);

        for (int i = 11; i * i < 1000; i++)
        {
            int num = i * i;
            var props = new Properties(num);

            if (props.DigitAtPosition(1) == 0)
                continue;
            if (props.DigitAtPosition(2) == 0)
                continue;
            if (!squares.Contains(props.DS))
                continue;

            ac03.Add(props);
            dn03.Add(new Properties(num - 3));
            dn03.Add(new Properties(num + 3));
        }

        // --------------------------------------------------------------------------------------
        var dn18 = new Clue("dn18", "Lucky and happy number", 2);
        var ac09 = new Clue("ac09", "18dn + 26", 2);

        foreach (var num in happy.Where(z => z > 9))
        {
            if (!lucky.Contains(num))
                continue;
            if (num + 26 > 99)
                continue;

            dn18.Add(new Properties(num));
            ac09.Add(new Properties(num + 26));
        }

        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // -- dn21 seems to be cross referenced A LOT !
        // -- from ac07 we can see that max value is 30

        var dn21 = new Clue("dn21", "Multiple of 10", 2);

        dn21.Add(new Properties(10));
        dn21.Add(new Properties(20));
        dn21.Add(new Properties(30));

        var ac29 = new Clue("ac29", "DS equals 21dn and DP is a cube", 3);
        var ac19 = new Clue("ac19", "DP equals 21dn", 2);
        var dn20 = new Clue("dn20", "(DS + DP) is a multiple of 21dn", 3);
        var ac07 = new Clue("ac07", "DS is half of 21dn", 2);

        // -- "ac29", "DS equals 21dn and DP is a cube"
        for (int num = 100; num < 1000; num++)
        {
            var p = new Properties(num);

            if (!dn21.AllNumbers().Contains(p.DS))
                continue;
            if (p.DP == 0)
                continue;
            if (!isCube(p.DP))
                continue;

            ac29.Add(p);
        }

        dn21 = new Clue(dn21);
        foreach (var p in ac29.PossAnswers)
        {
            if (!dn21.AllNumbers().Contains(p.DS))
                dn21.Add(new Properties(p.DS));
        }

        // -- "ac19", "DP equals 21dn"
        for (int num = 10; num < 100; num++)
        {
            var p = new Properties(num);

            if (!dn21.AllNumbers().Contains(p.DP))
                continue;

            ac19.Add(p);
        }

        dn21 = new Clue(dn21);
        foreach (var p in ac19.PossAnswers)
        {
            if (!dn21.AllNumbers().Contains(p.DP))
                dn21.Add(new Properties(p.DP));
        }

        // -- "dn20", "(DS + DP) is a multiple of 21dn"
        for (int num = 100; num < 1000; num++)
        {
            var p = new Properties(num);
            if ((p.DS + p.DP) % 10 != 0)
                continue;

            dn20.Add(p);
        }

        // -- "ac07", "DS is half of 21dn"
        for (int num = 10; num < 100; num++)
        {
            var p = new Properties(num);

            if (dn21.AllNumbers().Contains(2 * p.DS))
                ac07.Add(p);
        }

        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // -- Let's try to build ac11 from current "known" down values

        var ac11 = new Clue("ac11", "DS equals 21dn", 4);

        foreach (int a in dn11.DigitsAtPosition(0))
        {
            foreach (int b in dn02.DigitsAtPosition(2))
            {
                foreach (int c in dn08.DigitsAtPosition(1))
                {
                    foreach (var d in dn03.DigitsAtPosition(2))
                    {
                        if (dn21.PossAnswers.Where(z => z.Number == a + b + c + d).Count() == 0)
                            continue;

                        ac11.Add(new Properties(((a * 10 + b) * 10 + c) * 10 + d));
                    }
                }
            }
        }

        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // -- cross check ac20 and dn13 (***note these don't intersect!!)
        var newPoss = new Clue(ac20);

        foreach (var p in ac20.PossAnswers)
        {
            foreach (var q in dn13.PossAnswers)
            {
                if (q.Number % p.DS == 0)
                {
                    newPoss.Add(p);
                    break;
                }
            }
        }
        ac20 = newPoss;

        // --------------------------------------------------------------------------------------
        // -- cross check ac09 and dn18 (***note these don't intersect!!)
        Clue newAc09 = new Clue(ac09);
        Clue newDn18 = new Clue(dn18);

        foreach (var p in ac09.PossAnswers)
        {
            foreach (var q in dn18.PossAnswers)
            {
                if (p.Number == q.Number + 26)
                {
                    newAc09.Add(p);
                    newDn18.Add(q);
                }
            }
        }
        ac09 = newAc09;
        dn18 = newDn18;

        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------
        // --------------------------------------------------------------------------------------

        bool changed;
        do
        {
            changed = false;

            changed |= CrossCheck(ref ac01, 0, ref dn01, 0);
            changed |= CrossCheck(ref ac01, 1, ref dn02, 0);

            changed |= CrossCheck(ref ac03, 0, ref dn03, 0);
            changed |= CrossCheck(ref ac03, 1, ref dn04, 0);
            changed |= CrossCheck(ref ac03, 2, ref dn05, 0);

            changed |= CrossCheck(ref ac07, 0, ref dn01, 1);
            changed |= CrossCheck(ref ac07, 1, ref dn02, 1);

            changed |= CrossCheck(ref ac09, 0, ref dn03, 1);
            changed |= CrossCheck(ref ac09, 1, ref dn04, 1);

            changed |= CrossCheck(ref ac10, 0, ref dn05, 1);
            changed |= CrossCheck(ref ac10, 1, ref dn06, 1);

            changed |= CrossCheck(ref ac11, 0, ref dn11, 0);
            changed |= CrossCheck(ref ac11, 1, ref dn02, 2);
            changed |= CrossCheck(ref ac11, 2, ref dn08, 1);
            changed |= CrossCheck(ref ac11, 3, ref dn03, 2);

            changed |= CrossCheck(ref ac13, 0, ref dn13, 0);
            changed |= CrossCheck(ref ac13, 1, ref dn06, 2);

            changed |= CrossCheck(ref ac14, 0, ref dn11, 1);
            changed |= CrossCheck(ref ac14, 1, ref dn15, 0);
            changed |= CrossCheck(ref ac14, 2, ref dn16, 0);

            changed |= CrossCheck(ref ac17, 0, ref dn12, 1);
            changed |= CrossCheck(ref ac17, 1, ref dn13, 1);
            changed |= CrossCheck(ref ac17, 2, ref dn18, 0);

            changed |= CrossCheck(ref ac19, 0, ref dn19, 0);
            changed |= CrossCheck(ref ac19, 1, ref dn15, 1);

            changed |= CrossCheck(ref ac20, 0, ref dn20, 0);
            changed |= CrossCheck(ref ac20, 1, ref dn21, 0);
            changed |= CrossCheck(ref ac20, 2, ref dn22, 0);
            changed |= CrossCheck(ref ac20, 3, ref dn18, 1);

            changed |= CrossCheck(ref ac23, 0, ref dn19, 1);
            changed |= CrossCheck(ref ac23, 1, ref dn24, 0);

            changed |= CrossCheck(ref ac25, 0, ref dn25, 0);
            changed |= CrossCheck(ref ac25, 1, ref dn20, 1);

            changed |= CrossCheck(ref ac26, 0, ref dn22, 1);
            changed |= CrossCheck(ref ac26, 1, ref dn27, 0);

            changed |= CrossCheck(ref ac28, 0, ref dn24, 1);
            changed |= CrossCheck(ref ac28, 1, ref dn25, 1);
            changed |= CrossCheck(ref ac28, 2, ref dn20, 2);

            changed |= CrossCheck(ref ac29, 1, ref dn22, 2);
            changed |= CrossCheck(ref ac29, 2, ref dn27, 1);
        }
        while (changed);

        // --------------------------------------------------------------------------------------
        newPoss = new Clue(dn04);
        foreach (var p in dn04.PossAnswers)
        {
            if (p.Number <= dn08.AllNumbers().Min())
                continue;

            newPoss.Add(p);
        }
        dn04 = newPoss;

        newPoss = new Clue(dn08);
        foreach (var p in dn08.PossAnswers)
        {
            if (p.Number >= dn04.AllNumbers().Max())
                continue;

            newPoss.Add(p);
        }
        dn08 = newPoss;

        do
        {
            changed = false;

            changed |= CrossCheck(ref ac01, 0, ref dn01, 0);
            changed |= CrossCheck(ref ac01, 1, ref dn02, 0);

            changed |= CrossCheck(ref ac03, 0, ref dn03, 0);
            changed |= CrossCheck(ref ac03, 1, ref dn04, 0);
            changed |= CrossCheck(ref ac03, 2, ref dn05, 0);

            changed |= CrossCheck(ref ac07, 0, ref dn01, 1);
            changed |= CrossCheck(ref ac07, 1, ref dn02, 1);

            changed |= CrossCheck(ref ac09, 0, ref dn03, 1);
            changed |= CrossCheck(ref ac09, 1, ref dn04, 1);

            changed |= CrossCheck(ref ac10, 0, ref dn05, 1);
            changed |= CrossCheck(ref ac10, 1, ref dn06, 1);

            changed |= CrossCheck(ref ac11, 0, ref dn11, 0);
            changed |= CrossCheck(ref ac11, 1, ref dn02, 2);
            changed |= CrossCheck(ref ac11, 2, ref dn08, 1);
            changed |= CrossCheck(ref ac11, 3, ref dn03, 2);

            changed |= CrossCheck(ref ac13, 0, ref dn13, 0);
            changed |= CrossCheck(ref ac13, 1, ref dn06, 2);

            changed |= CrossCheck(ref ac14, 0, ref dn11, 1);
            changed |= CrossCheck(ref ac14, 1, ref dn15, 0);
            changed |= CrossCheck(ref ac14, 2, ref dn16, 0);

            changed |= CrossCheck(ref ac17, 0, ref dn12, 1);
            changed |= CrossCheck(ref ac17, 1, ref dn13, 1);
            changed |= CrossCheck(ref ac17, 2, ref dn18, 0);

            changed |= CrossCheck(ref ac19, 0, ref dn19, 0);
            changed |= CrossCheck(ref ac19, 1, ref dn15, 1);

            changed |= CrossCheck(ref ac20, 0, ref dn20, 0);
            changed |= CrossCheck(ref ac20, 1, ref dn21, 0);
            changed |= CrossCheck(ref ac20, 2, ref dn22, 0);
            changed |= CrossCheck(ref ac20, 3, ref dn18, 1);

            changed |= CrossCheck(ref ac23, 0, ref dn19, 1);
            changed |= CrossCheck(ref ac23, 1, ref dn24, 0);

            changed |= CrossCheck(ref ac25, 0, ref dn25, 0);
            changed |= CrossCheck(ref ac25, 1, ref dn20, 1);

            changed |= CrossCheck(ref ac26, 0, ref dn22, 1);
            changed |= CrossCheck(ref ac26, 1, ref dn27, 0);

            changed |= CrossCheck(ref ac28, 0, ref dn24, 1);
            changed |= CrossCheck(ref ac28, 1, ref dn25, 1);
            changed |= CrossCheck(ref ac28, 2, ref dn20, 2);

            changed |= CrossCheck(ref ac29, 1, ref dn22, 2);
            changed |= CrossCheck(ref ac29, 2, ref dn27, 1);
        }
        while (changed);

        // --------------------------------------------------------------------------------------
        // -- ac11 : DS equals 21dn
        newPoss = new Clue(dn21);
        foreach (var p in dn21.PossAnswers)
        {
            if (ac11.PossAnswers.Where(z => z.DS == p.Number).Count() == 0)
                continue;

            newPoss.Add(p);
        }
        dn21 = newPoss;

        // -- ac07 : DS is half of 21dn
        newPoss = new Clue(ac07);
        foreach (var p in ac07.PossAnswers)
        {
            if (!dn21.AllNumbers().Contains(p.DS * 2))
                continue;

            newPoss.Add(p);
        }
        ac07 = newPoss;

        // -- "ac29", "DS equals 21dn and DP is a cube"
        newPoss = new Clue(ac29);
        foreach (var p in ac29.PossAnswers)
        {
            if (!dn21.AllNumbers().Contains(p.DS))
                continue;

            newPoss.Add(p);
        }
        ac29 = newPoss;

        // -- "ac19", "DP equals 21dn"
        newPoss = new Clue(ac19);
        foreach (var p in ac19.PossAnswers)
        {
            if (!dn21.AllNumbers().Contains(p.DP))
                continue;

            newPoss.Add(p);
        }
        ac19 = newPoss;

        // -- "dn20", "(DS + DP) is a multiple of 21dn"
        newPoss = new Clue(dn20);
        foreach (var p in dn20.PossAnswers)
        {
            int dn21Value = dn21.AllNumbers()[0];
            if ((p.DS + p.DP) % dn21Value != 0)
                continue;

            newPoss.Add(p);
        }
        dn20 = newPoss;

        // --------------------------------------------------------
        // -- ****** This is the "suck it and see" section - just try things out

        // -- ****** Let's pretend that we KNOW dn04 =65 (with a DS of 11)

        dn04 = new Clue(dn04);
        dn04.Add(new Properties(65));

        // -- ****** 13dn is a multiple of 14

        dn13 = new Clue(dn13);
        for (int i = 14; i < 100; i += 14)
            dn13.Add(new Properties(i));

        // -- ****** Perhaps the first word that the digits map to is REVERSE
        // -- ****** Let's assume that ac07 starts with an 8 ( which maps to 'R')

        newPoss = new Clue(ac07);
        foreach ( var p in ac07.PossAnswers)
        {
            if (p.DigitAtPosition(0) != 8)
                continue;
            newPoss.Add(p);
        }
        ac07 = newPoss;

        // -- ****** force dn06 to end in a 9 (which maps to 'S')

        newPoss = new Clue(dn06);
        foreach ( var p in dn06.PossAnswers)
        {
            if (p.DigitAtPosition(2) != 9)
                continue;

            newPoss.Add(p);
        }
        dn06 = newPoss;

        // -- ***** And perhaps the third word in the map is ENTRIES
        // -- ***** so last digit of dn19 is 5

        newPoss = new Clue(dn19);
        foreach (var p in dn19.PossAnswers)
        {
            if (p.DigitAtPosition(2) != 5)
                continue;

            newPoss.Add(p);
        }
        dn19 = newPoss;

        // -- ***** last digit of dn20 is 4

        newPoss = new Clue(dn20);
        foreach (var p in dn20.PossAnswers)
        {
            if (p.DigitAtPosition(2) != 4)
                continue;

            newPoss.Add(p);
        }
        dn20 = newPoss;

        // -- ***** last digit of dn24 is 9

        newPoss = new Clue(dn24);
        foreach (var p in dn24.PossAnswers)
        {
            if (p.DigitAtPosition(1) != 9)
                continue;

            newPoss.Add(p);
        }
        dn24 = newPoss;

        // -- ***** last digit of dn25 is 5

        newPoss = new Clue(dn25);
        foreach (var p in dn25.PossAnswers)
        {
            if (p.DigitAtPosition(1) != 5)
                continue;

            newPoss.Add(p);
        }
        dn25 = newPoss;

        // -- ***** last digit of dn27 is 9

        newPoss = new Clue(dn27);
        foreach (var p in dn27.PossAnswers)
        {
            if (p.DigitAtPosition(1) != 9)
                continue;

            newPoss.Add(p);
        }
        dn27 = newPoss;

        // -- ***** maybe the second word is ACROSS
        // -- ***** dn12 must end with 3 ( ie 'C')

        newPoss = new Clue(dn12);
        foreach (var p in dn12.PossAnswers)
        {
            if (p.DigitAtPosition(1) != 3)
                continue;
            newPoss.Add(p);
        }
        dn12 = newPoss;

        // -- ***** dn15 must end with 5 ( ie 'O')

        newPoss = new Clue(dn15);
        foreach (var p in dn15.PossAnswers)
        {
            if (p.DigitAtPosition(1) != 5)
                continue;
            newPoss.Add(p);
        }
        dn15 = newPoss;

        // -- ***** dn16 must end with 9 ( ie 'S')

        newPoss = new Clue(dn16);
        foreach (var p in dn16.PossAnswers)
        {
            if (p.DigitAtPosition(1) != 9)
                continue;
            newPoss.Add(p);
        }
        dn16 = newPoss;

        // --------------------------------------------------------
        do
        {
            changed = false;

            changed |= CrossCheck(ref ac01, 0, ref dn01, 0);
            changed |= CrossCheck(ref ac01, 1, ref dn02, 0);

            changed |= CrossCheck(ref ac03, 0, ref dn03, 0);
            changed |= CrossCheck(ref ac03, 1, ref dn04, 0);
            changed |= CrossCheck(ref ac03, 2, ref dn05, 0);

            changed |= CrossCheck(ref ac07, 0, ref dn01, 1);
            changed |= CrossCheck(ref ac07, 1, ref dn02, 1);

            changed |= CrossCheck(ref ac09, 0, ref dn03, 1);
            changed |= CrossCheck(ref ac09, 1, ref dn04, 1);

            changed |= CrossCheck(ref ac10, 0, ref dn05, 1);
            changed |= CrossCheck(ref ac10, 1, ref dn06, 1);

            changed |= CrossCheck(ref ac11, 0, ref dn11, 0);
            changed |= CrossCheck(ref ac11, 1, ref dn02, 2);
            changed |= CrossCheck(ref ac11, 2, ref dn08, 1);
            changed |= CrossCheck(ref ac11, 3, ref dn03, 2);

            changed |= CrossCheck(ref ac13, 0, ref dn13, 0);
            changed |= CrossCheck(ref ac13, 1, ref dn06, 2);

            changed |= CrossCheck(ref ac14, 0, ref dn11, 1);
            changed |= CrossCheck(ref ac14, 1, ref dn15, 0);
            changed |= CrossCheck(ref ac14, 2, ref dn16, 0);

            changed |= CrossCheck(ref ac17, 0, ref dn12, 1);
            changed |= CrossCheck(ref ac17, 1, ref dn13, 1);
            changed |= CrossCheck(ref ac17, 2, ref dn18, 0);

            changed |= CrossCheck(ref ac19, 0, ref dn19, 0);
            changed |= CrossCheck(ref ac19, 1, ref dn15, 1);

            changed |= CrossCheck(ref ac20, 0, ref dn20, 0);
            changed |= CrossCheck(ref ac20, 1, ref dn21, 0);
            changed |= CrossCheck(ref ac20, 2, ref dn22, 0);
            changed |= CrossCheck(ref ac20, 3, ref dn18, 1);

            changed |= CrossCheck(ref ac23, 0, ref dn19, 1);
            changed |= CrossCheck(ref ac23, 1, ref dn24, 0);

            changed |= CrossCheck(ref ac25, 0, ref dn25, 0);
            changed |= CrossCheck(ref ac25, 1, ref dn20, 1);

            changed |= CrossCheck(ref ac26, 0, ref dn22, 1);
            changed |= CrossCheck(ref ac26, 1, ref dn27, 0);

            changed |= CrossCheck(ref ac28, 0, ref dn24, 1);
            changed |= CrossCheck(ref ac28, 1, ref dn25, 1);
            changed |= CrossCheck(ref ac28, 2, ref dn20, 2);

            changed |= CrossCheck(ref ac29, 1, ref dn22, 2);
            changed |= CrossCheck(ref ac29, 2, ref dn27, 1);
        }
        while (changed);

        // --------------------------------------------------------------------------------------

        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine(ac01);
        Console.WriteLine(ac03);
        Console.WriteLine(ac07);
        Console.WriteLine(ac09);
        Console.WriteLine(ac10);
        Console.WriteLine(ac11);
        Console.WriteLine(ac13);
        Console.WriteLine(ac14);
        Console.WriteLine(ac17);
        Console.WriteLine(ac19);
        Console.WriteLine(ac20);
        Console.WriteLine(ac23);
        Console.WriteLine(ac25);
        Console.WriteLine(ac26);
        Console.WriteLine(ac28);
        Console.WriteLine(ac29);
        Console.WriteLine("--------------------------------------------------------------");
        Console.WriteLine(dn01);
        Console.WriteLine(dn02);
        Console.WriteLine(dn03);
        Console.WriteLine(dn04);
        Console.WriteLine(dn05);
        Console.WriteLine(dn06);
        Console.WriteLine(dn08);
        Console.WriteLine(dn11);
        Console.WriteLine(dn12);
        Console.WriteLine(dn13);
        Console.WriteLine(dn15);
        Console.WriteLine(dn16);
        Console.WriteLine(dn18);
        Console.WriteLine(dn19);
        Console.WriteLine(dn20);
        Console.WriteLine(dn21);
        Console.WriteLine(dn22);
        Console.WriteLine(dn24);
        Console.WriteLine(dn25);
        Console.WriteLine(dn27);
        Console.WriteLine("--------------------------------------------------------------");

    }

    private static bool CrossCheck(ref Clue alpha, int v1, ref Clue beta, int v2)
    {
        bool retval = false;
        bool changed = true;

        while (changed)
        {
            var newPoss = new Clue(alpha);
            foreach (var p in alpha.PossAnswers)
            {
                if (beta.DigitsAtPosition(v2).Contains(p.DigitAtPosition(v1)))
                    newPoss.Add(p);
            }
            changed = (alpha.PossAnswers.Count != newPoss.PossAnswers.Count);
            alpha = newPoss;

            newPoss = new Clue(beta);
            foreach (var p in beta.PossAnswers)
            {
                if (alpha.DigitsAtPosition(v1).Contains(p.DigitAtPosition(v2)))
                    newPoss.Add(p);
            }
            changed = changed || (beta.PossAnswers.Count != newPoss.PossAnswers.Count);
            beta = newPoss;
            if (changed) retval = true;
        }
        return retval;
    }

    private static bool isCube(int num)
    {
        int root = (int)(Math.Pow(num, 1.0 / 3.0) + 0.0001);
        return (root * root * root == num);
    }

    private static bool isAPowerOfTwo(int num)
    {
        bool retval = false;

        int nn = 1;
        while (nn <= num)
        {
            if (nn == num)
            {
                retval = true;
                break;
            }
            nn *= 2;
        }
        return retval;
    }

    private static bool isPrime(int num)
    {
        if (num == 1)
            return false;

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