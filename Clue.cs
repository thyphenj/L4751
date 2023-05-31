namespace L4751;

using System;
using System.Linq;
using System.Text;

public class Clue
{
    public string Rubric { get; set; }
    public List<Properties> PossAnswers { get; set; }

    public Clue(string str)
    {
        Rubric = str;
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
        var s = new StringBuilder(Rubric + "\n");
        foreach (var p in PossAnswers)
            s.Append(p + "\n");
        return s.ToString();
    }
}
