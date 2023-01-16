﻿using System;
using System.Text;

namespace JongSungAutoSelecter;


public class StrUtility
{
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
}