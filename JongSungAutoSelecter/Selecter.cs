namespace JongSungAutoSelecter;
public class JSAutoSelecter
{
    StrUtility strUtility = new StrUtility();

    const string SUBJECT_JONGSUNG = "은";
    const string SUBJECT_WITHOUT_JONGSUNG = "는";

    readonly string[] JONGSUNG_PATTERNS = new string[]
    {
        "은/는",
        "은(는)",
        "은{는}",
    };

    public string SetJongSung(string originStr)
    {
        originStr = originStr.Trim();
        string[] strs = originStr.Split(' ');
        
        for(int i = 0, count = strs.Length; i < count; i++)
        {
            if (strUtility.CheckWordHasJongSung(strs[i]))
            {
                strs[i] += SUBJECT_JONGSUNG;
            }
            else
            {
                strs[i] += SUBJECT_WITHOUT_JONGSUNG;
            }
        }    

        return strs.Aggregate((a, b) => a + " " + b);
    }
}
