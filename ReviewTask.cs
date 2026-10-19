using System;
using System.Collections.Generic;
using System.Globalization;

public static class ReviewTask
{
    public static int[] Run(bool[] paid, bool[] shipped)
    {
        var result = new List<int>();
        for (int i = 0; i < paid.Length - 1; i++)
        {
            if (paid[i] || !shipped[i])
            {
                result.Add(i);
                shipped[i] = true;
            }
        }
        return result.ToArray();
    }
}
