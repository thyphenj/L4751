namespace L4751;

using System;

public class PropList
{
    public List<Properties> PossibleProps { get; set; }

    public PropList()
    {
        PossibleProps = new List<Properties>();
    }

    public void Add ( Properties prop)
    {
        PossibleProps.Add(prop);
    }

    public List<int> DigitsAtPosition ( int pos)
    {
        List<int> retval = new List<int>();

        HashSet<int> set = new HashSet<int>();
        foreach ( var prop in PossibleProps)
        {
            set.Add(prop.DigitAtPosition(pos));
        }
        foreach ( var num in set)
        {
            retval.Add(num);
        }
        return retval;
    }
}
