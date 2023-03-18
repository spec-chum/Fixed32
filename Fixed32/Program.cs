namespace Types;

internal class Program
{
    static void Main()
    {
        var twoAndAHalf = new Fixed32(16, 5) / new Fixed32(16, 2);

        Console.WriteLine((double)twoAndAHalf);
    }
}
