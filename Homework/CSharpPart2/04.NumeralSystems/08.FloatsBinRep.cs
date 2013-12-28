// Write a program that shows the internal binary representation of given 
// 32-bit signed floating-point number in IEEE 754 format (the C# type float). 
// Example: -27,25  sign = 1, exponent = 10000011, mantissa = 10110100000000000000000
// You can test the program at http://www.h-schmidt.net/FloatConverter/
using System;

class FloatBinRep
{
    static void Main()
    {
        string input = Console.ReadLine().ToLower(); // IEE754 supports the next 3 "special values".
        if (input == "nan") { Console.WriteLine("S E\t   M\n0 11111111 11111111111111111111111"); return; }
        else if (input == "infinity") { Console.WriteLine("S E\t   M\n0 11111111 00000000000000000000000"); return; }
        else if (input == "-infinity") { Console.WriteLine("S E\t   M\n1 11111111 00000000000000000000000"); return; }

        float number = float.Parse(input);
        string result = number >= 0 ? "0" : "1";
        
        if (number < 0) number = -number;

        string intPartBin = GetBinaryInt((int)number);
        string floPartBin = GetBinaryFraction(number - (int)number);
                                               // Adding the exponent
        if (intPartBin != "0") result += " " + GetBinaryInt(127 + intPartBin.Length - 1).PadLeft(8, '0');
        else result += " " + GetBinaryInt(127 - (floPartBin.IndexOf('1') + 1)).PadLeft(8, '0');
                                               // Adding the mantissa
        if (intPartBin != "0") result += " " + (intPartBin + floPartBin).Substring(1).PadRight(23, '0');
        else result += " " + (intPartBin + floPartBin).Substring(floPartBin.IndexOf('1') + 2).PadRight(23, '0');

        // If the number entered is 0, we differentiate between "-0" and "0", as per IEEE754.
        if (intPartBin == "0" && floPartBin == "0") result = new string('0', 8) + " " + new string('0', 23);
        if (input[0] == '-' && intPartBin == "0" && floPartBin == "0") result = "1 " + result;
        else if (input[0] != '-' && intPartBin == "0" && floPartBin == "0") result = "0 " + result;

        Console.WriteLine("S E\t   M\n" + result);
    }

    static string GetBinaryInt(int num)
    {
        string result = "";

        do
        {
            result = (num & 1) + result;
            num >>= 1;
        } while (num != 0);

        return result;
    }

    static string GetBinaryFraction(float fl)
    {
        string result = "";

        do
        {
            fl = fl * 2;
            result += (int)fl;
            fl = fl - (int)fl;
        } while (fl != 0 || result.Length == 23); // Because we can only use 23 bits 
                                                  // for the mantissa in a 32 bit float.
        return result;
    }
}
