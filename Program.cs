
namespace L4751;
class Program
{
    static int[] triangle = { 0, 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, 66, 78, 91, 105, 120, 136, 153, 171, 190, 210, 231, 253, 276, 300, 325, 351, 378, 406, 435, 465, 496, 528, 561, 595, 630, 666, 703, 741, 780, 820, 861, 903, 946, 990 };
    static void Main(string[] args)
    {
        // -- dn02 : Palindrome and multiple of 5 with MP of 2
        System.Console.WriteLine("dn02 : Palindrome and multiple of 5 with MP of 2");
        for ( int i = 0 ; i < 10 ; i++)
        {
            int num = 505 + 10 * i;
            var values = GetValues(num);
            if (values[3] == 2)
            PrintValues(values);
        }
        System.Console.WriteLine();

        // -- ac01 : ans is either 153 or 351
        System.Console.WriteLine("ac01 : triangular with triangular DP");
        foreach (var num in triangle)
        {
            if (num > 99 && (num / 10) % 10 == 5)
            {
                var values = GetValues(num);
                if (triangle.Contains(values[2]))
                    PrintValues(values);
            }
        }
        System.Console.WriteLine();

        // -- ac03 : Square number with a square DS 
        // -- dn01 :
        System.Console.WriteLine("dn01 : (DS + DP) is odd multiple of 5");
        for (int a = 10; a < 40; a += 20)
        {
            for (int b = 1; b < 10; b++)
            {
                int num = a + b;
                var values = GetValues(num);
                if ((values[1] + values[2]) % 10 == 5)
                    PrintValues(values);
            }
        }
        System.Console.WriteLine();
    }

    static void PrintValues(int[] values)
    {
        foreach (var val in values)
        {
            System.Console.Write($"{val,5}");
        }
        Console.WriteLine();

    }
    static int[] GetValues(int num)
    {
        int[] retval = { num, 0, 1, 1 };
        List<int> digits = GetDigits(num);
        foreach (var digit in GetDigits(num))
        {
            retval[1] += digit;
            retval[2] *= digit;
        }
        int mp = retval[2];
        while (mp > 9)
        {
            retval[3]++;
            int product = 1;
            foreach (var digit in GetDigits(mp))
            {
                product *= digit;
            }
            mp = product;
        }
        return retval;
    }
    static List<int> GetDigits(int num)
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