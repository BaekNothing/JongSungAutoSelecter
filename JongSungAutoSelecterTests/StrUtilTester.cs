using JongSungAutoSelecter;
using Xunit;
using System;

namespace JongSungAutoSelecterTests;

public class Tester_StrUtility
{
    [Fact]
    public void Test_ConvertStringToUInt32()
    {
        StrUtility strUtility = new StrUtility();
        Assert.Equal(new uint[]{(uint)0xAC00}, strUtility.ConvertStringToUInt32("가"));
        Assert.Equal(new uint[]{(uint)0xD7A3}, strUtility.ConvertStringToUInt32("힣"));
    }

    [Fact]
    public void GetLastKoreanWord()
    {
        StrUtility strUtility = new StrUtility();
        Assert.Equal(strUtility.ConvertStringToUInt32("가")[0], 
            strUtility.GetLastKoreanWord(strUtility.ConvertStringToUInt32("가")));
        Assert.Equal(strUtility.ConvertStringToUInt32("힣")[0], 
            strUtility.GetLastKoreanWord(strUtility.ConvertStringToUInt32("힣")));
        Assert.Equal(strUtility.ConvertStringToUInt32("가")[0], 
            strUtility.GetLastKoreanWord(strUtility.ConvertStringToUInt32("가나다가")));
        Assert.Equal(strUtility.ConvertStringToUInt32("힣")[0], 
            strUtility.GetLastKoreanWord(strUtility.ConvertStringToUInt32("가나다힣")));
        Assert.Equal(strUtility.ConvertStringToUInt32("가")[0], 
            strUtility.GetLastKoreanWord(strUtility.ConvertStringToUInt32("가나다가asdf")));
        Assert.Equal(strUtility.ConvertStringToUInt32("힣")[0], 
            strUtility.GetLastKoreanWord(strUtility.ConvertStringToUInt32("가나다힣 ")));
        Assert.Equal(strUtility.ConvertStringToUInt32("가")[0], 
            strUtility.GetLastKoreanWord(strUtility.ConvertStringToUInt32("가나다가1234")));
        Assert.Equal(strUtility.ConvertStringToUInt32("힣")[0], 
            strUtility.GetLastKoreanWord(strUtility.ConvertStringToUInt32("가나다힣@#$@!")));
    }

    [Fact]
    public void CheckUInt32IsKorean()
    {
        StrUtility strUtility = new StrUtility();
        Assert.True(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("가")[0]));
        Assert.True(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("힣")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("a")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("z")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("A")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("Z")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("0")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("9")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("!")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("#")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("_")[0]));
        Assert.False(strUtility.CheckUInt32IsKorean(strUtility.ConvertStringToUInt32("=")[0]));
    }

    [Fact]
    public void CheckWordHasJongSung()
    {
        StrUtility strUtility = new StrUtility();
        Assert.True(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("힣")[0]));
        Assert.True(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("갈")[0]));
        Assert.True(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("낟")[0]));
        Assert.True(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("멸")[0]));
        Assert.True(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("귉")[0]));
        Assert.True(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("휼")[0]));

        Assert.False(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("가")[0]));
        Assert.False(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("나")[0]));
        Assert.False(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("마")[0]));
        Assert.False(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("바")[0]));
        Assert.False(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("사")[0]));
        Assert.False(strUtility.CheckWordHasJongSung(strUtility.ConvertStringToUInt32("자")[0]));
    }

}