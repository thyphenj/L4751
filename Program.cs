namespace L4751;

using System;
using System.Linq;

class Program
{
    static int[] triangle = { 0, 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, 66, 78, 91, 105, 120, 136, 153, 171, 190, 210, 231, 253, 276, 300, 325, 351, 378, 406, 435, 465, 496, 528, 561, 595, 630, 666, 703, 741, 780, 820, 861, 903, 946, 990 };

    static void Main(string[] args)
    {
        var ac01 = new PropList();
        var ac03 = new PropList();
        var dn01 = new PropList();
        var dn02 = new PropList();

        // -- dn02 : Palindrome and multiple of 5 with MP of 2
        // -- so we know that the first and last digits are both 5
        Console.WriteLine("dn02 : Palindrome and multiple of 5 with MP of 2");
        for (int num = 505; num < 600; num += 10)
        {
            var props = new Properties(num);
            if (props.MultiplicativePersistance == 2)
            {
                Console.WriteLine(props);
                dn02.Add(props);
            }
        }
        Console.WriteLine();

        // -- ac01 : triangular with triangular DP
        Console.WriteLine("ac01 : triangular with triangular DP");
        var dn02Digit0 = dn02.DigitsAtPosition(0);
        foreach (var num in triangle.Where(x => x > 99))
        {
            var props = new Properties(num);
            if (dn02Digit0.Contains(props.DigitAtPosition(1)) &&
                triangle.Contains(props.DigitProduct))
            {
                Console.WriteLine(props);
                ac01.Add(props);
            }
        }
        Console.WriteLine();

        // -- dn01 : (DS + DP) is odd multiple of 5
        Console.WriteLine("dn01 : (DS + DP) is odd multiple of 5");
        var ac01Digit0 = ac01.DigitsAtPosition(0);
        for (int num = 10; num < 100; num++)
        {
            var props = new Properties(num);
            if ((props.DigitSum + props.DigitProduct) % 10 == 5 &&
                ac01Digit0.Contains(props.DigitAtPosition(0)))
            {
                Console.WriteLine(props);
                dn01.Add(props);
            }
        }
        Console.WriteLine();

        // -- ac03 : Square number with a square DS
        Console.WriteLine("ac03 : Square number with a square DS");
        for (int i = 10; i < 31; i++)
        {
            int num = i * i;
            var props = new Properties(num);
            if (props.DigitAtPosition(1) != 0 &&
                props.DigitAtPosition(2) != 0 &&
                new int[] { 1, 4, 9, 16, 25 }.Contains(props.DigitSum))
            {
                Console.WriteLine(props);
                ac03.Add(props);
            }
        }
        Console.WriteLine();
    }

}