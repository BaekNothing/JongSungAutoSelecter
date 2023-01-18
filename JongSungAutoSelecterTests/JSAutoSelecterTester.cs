using JongSungAutoSelecter;
using Xunit;
using System;

namespace JongSungAutoSelecterTests;

public class JSAutoSelecterTester
{
    [Fact]
    public void SetOneWord()
    {
        JSAutoSelecter selecter = new JSAutoSelecter();
        string originStr = "테스트은(는)";
        Assert.Equal("테스트는", selecter.SetOneWord(originStr));
        originStr = "상품은(는)";
        Assert.Equal("상품은", selecter.SetOneWord(originStr));
        
        originStr = "테스트은/는";
        Assert.Equal("테스트는", selecter.SetOneWord(originStr));
        originStr = "상품은/는";
        Assert.Equal("상품은", selecter.SetOneWord(originStr));
        
        originStr = "테스트을(를)";
        Assert.Equal("테스트를", selecter.SetOneWord(originStr));
        originStr = "상품을(를)";
        Assert.Equal("상품을", selecter.SetOneWord(originStr));
       
        originStr = "테스트을/를";
        Assert.Equal("테스트를", selecter.SetOneWord(originStr));
        originStr = "상품을/를";
        Assert.Equal("상품을", selecter.SetOneWord(originStr));
       
        originStr = "테스트이(가)";
        Assert.Equal("테스트가", selecter.SetOneWord(originStr));
        originStr = "상품이(가)";
        Assert.Equal("상품이", selecter.SetOneWord(originStr));
       
        originStr = "테스트이/가";
        Assert.Equal("테스트가", selecter.SetOneWord(originStr));
        originStr = "상품이/가";
        Assert.Equal("상품이", selecter.SetOneWord(originStr));
       
    }

    [Fact]
    public void SetParagraph()
    {
        
    }

    [Fact]
    public void SetParagraphWithThread()
    {
           
    }
}