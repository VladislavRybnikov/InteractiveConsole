using System;
using System.Linq;

namespace InteractiveConsole
{
    public static class Question
    {
        public static Answer Ask(string question, bool retryOnInvalidInput = true) =>
            AskOneOf<Answer>(
                () =>
                {
                    var postfix = question.EndsWith(" ") ? "(Y/n)" : " (Y/n)";
                    Console.WriteLine(question + postfix);
                    return Console.ReadLine();
                },
                input =>
                {
                    Console.WriteLine($"Invalid input: '{input}'");
                });

        public static TEnum AskOneOf<TEnum>(string question = null, bool retryOnInvalidInput = true) 
            where TEnum : struct, Enum =>
            AskOneOf<TEnum>(
                () =>
                {
                    var values = Enum.GetValues<TEnum>().Select(e
                        => $"{e.ToString()} - {Convert.ToInt32(e)}");
                    question ??= $"Choose one of:";
                    question += $"({string.Join(", ", values)})";
                    
                    Console.WriteLine(question);
                    return Console.ReadLine();
                },
                input =>
                {
                    Console.WriteLine($"Invalid input: '{input}'");
                }, true);

        private static TEnum AskOneOf<TEnum>(
            Func<string> ask, 
            Action<string> onInvalidInput, 
            bool valuesLookUp = false,
            bool retryOnInvalidInput = true) 
            where TEnum : struct, Enum
        {
            TEnum answer;
            var parsed = true;
            var input = string.Empty;
            do
            {
                if (!parsed) onInvalidInput(input);
                
                input = ask();
            } 
            while (!(parsed = TryParseOneOf(input, valuesLookUp, out answer)) && retryOnInvalidInput);

            return answer;
        }

        private static bool TryParseOneOf<TEnum>(string text, bool valuesLookUp, out TEnum answer) 
            where TEnum : struct, Enum
        {
            answer = default;
            var type = typeof(TEnum);

            foreach (var name in type.GetEnumNames())
            {
                if ((text.Length == 1 && name.StartsWith(text, StringComparison.OrdinalIgnoreCase))
                    || text.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return Enum.TryParse(name, out answer);
                }
            }

            if (valuesLookUp && int.TryParse(text, out var textValue))
            {
                foreach (var value in type.GetEnumValues())
                {
                    if (value.Equals(textValue))
                    {
                        answer = (TEnum)value;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
