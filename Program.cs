using System;
using System.Collections.Generic;
using System.Globalization;
using static ReviewTask;

class Program
{
    static int failed = 0;
    static void Main()
    {
        Check("T01", () => string.Join(",", Run(new[] { true, false, true, true }, new[] { false, false, true, false })), "0,3");
        Check("T02", () => string.Join(",", Run(new[] { true }, new[] { false })), "0");
        Check("T03", () => string.Join(",", Run(new[] { false, false }, new[] { false, true })), "");
        Check("T04", () => string.Join(",", Run(Array.Empty<bool>(), Array.Empty<bool>())), "");
        Check("T05", () => ((Func<object>)(() => { bool[] s = { false, true }; Run(new[] { true, false }, s); return s[0]; }))(), "False");
        Console.WriteLine("Failed: " + failed);
        Environment.ExitCode = failed == 0 ? 0 : 1;
    }

    static string Display(object value)
    {
        if (value is decimal d)
            return d.ToString("G29", CultureInfo.InvariantCulture);
        return Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    static void Check(string id, Func<object> action, string expected)
    {
        try
        {
            string actual = Display(action());
            bool ok = actual == expected;
            if (!ok) failed++;
            Console.WriteLine(id + (ok ? " PASS" : " FAIL")
                + " expected=[" + expected + "] actual=[" + actual + "]");
        }
        catch (Exception error)
        {
            failed++;
            Console.WriteLine(id + " FAIL exception=" + error.GetType().Name
                + " expected=[" + expected + "]");
        }
    }
}
