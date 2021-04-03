using System;
using System.Collections.Generic;
using System.Linq;

namespace InteractiveConsole
{
    public enum DelimiterType
    {
        NewLine,
        Comma
    }

    public static class View
    {
        public static void Text(string text, bool newLine = false)
        {
            if (newLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
        }

        public static void ValuesOf<TEnum>(DelimiterType delimiterType = DelimiterType.NewLine)
            where TEnum : struct, Enum
        {
            var values = Enum.GetValues<TEnum>().Select(e
                => $"{e.ToString()} - {Convert.ToInt32(e)}");
            
            Values(values);
        }
        
        public static void Values(IEnumerable<string> values, DelimiterType delimiterType = DelimiterType.NewLine)
        {
            var delimiter = delimiterType switch
            {
                DelimiterType.Comma => ", ",
                DelimiterType.NewLine => Environment.NewLine,
                _ => throw new ArgumentException(nameof(delimiterType))
            };
            
            Console.WriteLine(string.Join(delimiter, values));
        }
    }
}