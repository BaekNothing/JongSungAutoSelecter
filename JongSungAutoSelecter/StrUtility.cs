using System;
using System.Text;
using System.Text.RegularExpressions;

namespace JongSungAutoSelecter;

public class StrUtility
{
#region single word utility
    public bool CheckWordHasJongSung(in string str)
    {
        UInt32[] unicodeInts = ConvertStringToUInt32(str);
        UInt32 lastKoreanWord = GetLastKoreanWord(unicodeInts);
        return CheckWordHasJongSung(lastKoreanWord);
    }

    public UInt32[] ConvertStringToUInt32(in string str)
    {
        if(string.IsNullOrEmpty(str))
            return new UInt32[]{};
        Encoding utf32 = Encoding.UTF32;
        byte[] bytes = utf32.GetBytes(str);
        UInt32[] unicodeInts = new UInt32[bytes.Length / 4];
        for(int i = 0, count = unicodeInts.Length; i < count; i++)
        {
            byte[] temp = new byte[4];
            Array.Copy(bytes, i * 4, temp, 0, 4);
            unicodeInts[i] = BitConverter.ToUInt32(temp);
        }
        return unicodeInts;
    }

    public UInt32 GetLastKoreanWord(in UInt32[] UnicodeByte)
    {
        UInt32 lastKoreanWord = 0;
        int index = UnicodeByte.Length - 1;

        while(index >= 0)
        {
            if(CheckUInt32IsKorean(UnicodeByte[index]))
                break;
            index--;
        }

        if(0 <= index && index < UnicodeByte.Length)
            lastKoreanWord = UnicodeByte[index];
        return lastKoreanWord;
    }

    public bool CheckUInt32IsKorean(in UInt32 inputByte)
    {
        const UInt32 firestChar = 0xAC00; //U+AC00 가
        const UInt32 lastChar = 0xD7A3; //U+D7A3 힣

        if(firestChar <= inputByte && inputByte <= lastChar )
            return true;
        return false;
    }

    public bool CheckWordHasJongSung(in UInt32 inputByte)
    {
        return (inputByte - 0xAC00) % 28 != 0;
    }

    const string TOPIC_JONGSUNG = "은";
    const string TOPIC_WITHOUT_JONGSUNG = "는";
    public readonly Regex topicRegex = new Regex(@"은[/(]는");
    public readonly Regex topicEndRegex = new Regex(@"[은는]$");
    
    const string OBJECT_JONGSUNG = "을";
    const string OBJECT_WITHOUT_JONGSUNG = "를";
    public readonly Regex objectRegex = new Regex(@"을[/(]를");
    public readonly Regex objectEndRegex = new Regex(@"[을를]$");

    const string SUBJECT_JONGSUNG = "이";
    const string SUBJECT_WITHOUT_JONGSUNG = "가";
    public readonly Regex subjectRegex = new Regex(@"이[/(]가");
    public readonly Regex subjectEndRegex = new Regex(@"[이가]$");
    
    public string SelectJongSungInPattern(Match match, in string str, in string withJongSung, in string withoutJongSung)
    {
        StringBuilder sb = new StringBuilder();
        string textBody = str.Substring(0, match.Index);
        if(CheckWordHasJongSung(textBody))
        {
            sb.Append(textBody);
            sb.Append(withJongSung);
        }
        else
        {
            sb.Append(textBody);
            sb.Append(withoutJongSung);
        }
        return sb.ToString();
    }

    public string CorrectJongSung(Match match, in string str, in string withJongSung, in string withoutJongSung)
    {
        StringBuilder sb = new StringBuilder();
        string textBody = str.Substring(0, match.Index);
        if(CheckWordHasJongSung(textBody))
        {
            sb.Append(textBody);
            sb.Append(withJongSung);
        }
        else
        {
            sb.Append(textBody);
            sb.Append(withoutJongSung);
        }
        return sb.ToString();
    }

#endregion

#region paragraph utility
    public Tuple<bool, string> CheckAndSelect(in string inputString)
    {
        Tuple<bool, string> result;
        Match match;

        if((match = topicRegex.Match(inputString)).Success)
        {
            result = new Tuple<bool, string>(true,
                SelectJongSungInPattern(match, inputString, 
                TOPIC_JONGSUNG, TOPIC_WITHOUT_JONGSUNG));
        }
        else if((match = objectRegex.Match(inputString)).Success)
        {
            result = new Tuple<bool, string>(true,
                SelectJongSungInPattern(match, inputString, 
                OBJECT_JONGSUNG, OBJECT_WITHOUT_JONGSUNG));
        }
        else if((match = subjectRegex.Match(inputString)).Success)
        {
            result = new Tuple<bool, string>(true,
                SelectJongSungInPattern(match, inputString,
                SUBJECT_JONGSUNG, SUBJECT_WITHOUT_JONGSUNG));
        }
        else
        {
            result = new Tuple<bool, string>(false, string.Empty);
        }

        return result;
    }

    public Tuple<bool, string> CheckAndCorrect(in string inputString)
    {
        Tuple<bool, string> result;
        Match match;

        if((match = topicEndRegex.Match(inputString)).Success)
        {
            result = new Tuple<bool, string>(true,
                CorrectJongSung(match, inputString,
                TOPIC_JONGSUNG, TOPIC_WITHOUT_JONGSUNG));
        }
        else if((match = objectEndRegex.Match(inputString)).Success)
        {
            result = new Tuple<bool, string>(true,
                CorrectJongSung(match, inputString, 
                OBJECT_JONGSUNG, OBJECT_WITHOUT_JONGSUNG));
        }
        else if((match = subjectEndRegex.Match(inputString)).Success)
        {
            result = new Tuple<bool, string>(true,
                CorrectJongSung(match, inputString,
                SUBJECT_JONGSUNG, SUBJECT_WITHOUT_JONGSUNG));
        }
        else
        {
            result = new Tuple<bool, string>(false, string.Empty);
        }
        return result;
    }
#endregion
}