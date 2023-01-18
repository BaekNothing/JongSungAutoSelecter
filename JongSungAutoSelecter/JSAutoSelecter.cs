using System;
using System.Text;
using System.Threading;

namespace JongSungAutoSelecter;
public class JSAutoSelecter
{
    StrUtility strUtility = new StrUtility();

    public string SetParagraphWithThread(in string inputString)
    {
        StringBuilder sb = new StringBuilder();
        string[] words = inputString.Split(' ');
        string[] result = new string[words.Length];
        Thread[] threads = new Thread[words.Length];

        for (int i = 0, count = words.Length; i < count; i++)
        {
            threads[i] = new Thread(() => result[i] = SetOneWord(words[i]));
            threads[i].Start();
        }

        for (int i = 0, count = words.Length; i < count; i++)
        {
            threads[i].Join();
            sb.Append(result[i]);
            if (i != count - 1)
                sb.Append(' ');
        }

        return sb.ToString();
    }

    public string SetParagraph(in string inputString)
    {
        StringBuilder sb = new StringBuilder();
        string[] words = inputString.Split(' ');

        for (int i = 0, count = words.Length; i < count; i++)
        {
            sb.Append(SetOneWord(words[i]));
            if (i != count - 1)
                sb.Append(' ');
        }
        return sb.ToString();
    }

    public string SetOneWord(string inputString)
    {
        Tuple<bool, string> result;

        if((result = strUtility.CheckAndSelect(inputString)).Item1 == true)
        {
            return result.Item2;
        }
        else if ((result = strUtility.CheckAndCorrect(inputString)).Item1 == true)
        {
            return result.Item2;
        }
        else
        {
            return inputString;
        }
    }
}
