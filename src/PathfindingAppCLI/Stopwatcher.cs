using System;
using System.Diagnostics;

public class Stopwatcher
{
    public static void Track(Action action, string message)
    {
        var w = new Stopwatch();
        try
        {
            w.Start();
            action();
        }
        finally
        {
            w.Stop();
            Console.WriteLine(message + $" Elapsed: {w.Elapsed}");
        }
    }

    public static T Track<T>(Func<T> func, string message)
    {
        var w = new Stopwatch();
        try
        {
            w.Start();
            return func();
        }
        finally
        {
            w.Stop();
            Console.WriteLine(message + $" Elapsed: {w.Elapsed}");
        }
    }
}