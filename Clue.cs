namespace L4751;

using System;
using System.Linq;
using System.Text;

public class Clue
{
    public string Number { get; set; }
    public string Rubric { get; set; }
    public int Length { get; set; }
    public List<Properties> PossAnswers { get; set; }

    public Clue(Clue clue)
    {
        Number = clue.Number;
        Rubric = clue.Rubric;
        Length = clue.Length;

        PossAnswers = new List<Properties>();
    }

    public Clue(string num, string str, int len)
    {
        Number = num;
        Rubric = str;
        Length = len;
        PossAnswers = new List<Properties>();
    }

    public void Add(Properties prop)
    {
        PossAnswers.Add(prop);
    }

    public void Delete(int num)
    {
        var prop = PossAnswers.Where(z => z.Number == num).FirstOrDefault();
        if (prop != null)
            PossAnswers.Remove(prop);
    }

    public List<int> DigitsAtPosition(int pos)
    {
        List<int> retval = new List<int>();

        HashSet<int> set = new HashSet<int>();
        foreach (var prop in PossAnswers)
        {
            set.Add(prop.DigitAtPosition(pos));
        }
        foreach (var num in set)
        {
            retval.Add(num);
        }
        return retval;
    }

    public List<int> AllNumbers()
    {
        List<int> retval = new List<int>();

        foreach (var prop in PossAnswers)
        {
            retval.Add(prop.Number);
        }

        return retval;
    }

    public override string ToString()
    {
        StringBuilder[] lines = { new StringBuilder(), new StringBuilder(), new StringBuilder(), new StringBuilder(), new StringBuilder(), new StringBuilder() };
        for (int i = 0; i < PossAnswers.Count; i++)
        {
            var p = PossAnswers[i];

            lines[i % 6].Append("  " + p.ToString());
        }

        var s = new StringBuilder($"{Number} ({PossAnswers.Count}) : {Rubric}\n\n");
        foreach (var str in lines)
        {
            if (str.ToString() != "")
                s.AppendLine(str.ToString());
        }
        return s.ToString();
    }
}
