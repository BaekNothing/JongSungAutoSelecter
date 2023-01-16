using JongSungAutoSelecter;
using Xunit;
using System.Reflection;
using System.Numerics;

namespace JongSungAutoSelecterTests;

public class StrUtilTester
{
    [Fact]
    public void Tester()
    {   
        StrUtility strUtility = new StrUtility();
        Assert.True(strUtility.IsString("Hello"));
    }
}