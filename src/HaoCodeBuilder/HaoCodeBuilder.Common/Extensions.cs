using System;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections.Generic;
/// <summary>
///ExtString 的摘要说明
/// </summary>
public static class MyExtensions
{
    public static bool IsDecimal(this string str)
    {
        decimal test;
        return decimal.TryParse(str, out test);
    }
    public static bool IsDecimal(this string str, out decimal test)
    {
        return decimal.TryParse(str, out test);
    }

    public static bool IsInt(this string str)
    {
        int test;
        return int.TryParse(str, out test);
    }
    public static bool IsInt(this string str, out int test)
    {
        return int.TryParse(str, out test);
    }

    public static bool IsLong(this string str)
    {
        long test;
        return long.TryParse(str, out test);
    }
    public static bool IsLong(this string str, out long test)
    {
        return long.TryParse(str, out test);
    }

    public static bool IsDateTime(this string str)
    {
        DateTime test;
        return DateTime.TryParse(str, out test);
    }
    public static bool IsDateTime(this string str, out DateTime test)
    {
        return DateTime.TryParse(str, out test);
    }

    public static bool IsGuid(this string str)
    {
        Guid test;
        return Guid.TryParse(str, out test);
    }
    public static bool IsGuid(this string str, out Guid test)
    {
        return Guid.TryParse(str, out test);
    }
   
    public static bool IsUrl(this string str)
    {
        if (str.IsNullOrEmpty())
            return false;
        string pattern = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
        return Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase);
    }
    public static bool IsEmail(this string str)
    {
        return Regex.IsMatch(str, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
    }
    public static decimal ToDecimal(this string str)
    {
        decimal test;
        decimal.TryParse(str, out test);
        return test;
    }
    
    public static short ToShort(this string str)
    {
        short test;
        short.TryParse(str, out test);
        return test;
    }
    public static int ToInt(this string str)
    {
        int test;
        int.TryParse(str, out test);
        return test;
    }
    public static long ToLong(this string str)
    {
        long test;
        long.TryParse(str, out test);
        return test;
    }
    public static Int16 ToInt16(this string str)
    {
        Int16 test;
        Int16.TryParse(str, out test);
        return test;
    }
    public static Int32 ToInt32(this string str)
    {
        Int32 test;
        Int32.TryParse(str, out test);
        return test;
    }
    public static Int64 ToInt64(this string str)
    {
        Int64 test;
        Int64.TryParse(str, out test);
        return test;
    }
   
    public static DateTime ToDateTime(this string str)
    {
        DateTime test;
        DateTime.TryParse(str, out test);
        return test;
    }
    public static Guid ToGuid(this string str)
    {
        Guid test;
        Guid.TryParse(str, out test);
        return test;
    }
    
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }
   
    /// <summary>
    /// 过滤sql
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ReplaceSql(this string str)
    {
        str = str.Replace("'", "''").Replace("--", " ");
        return str;
    }
    /// <summary>
    /// 获取汉字拼音的第一个字母
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    public static string ToChineseSpell(this string strText)
    {
        int len = strText.Length;
        string myStr = "";
        for (int i = 0; i < len; i++)
        {
            myStr += getSpell(strText.Substring(i, 1));
        }
        return myStr.ToLower();
    }
    /// <summary>
    /// 获取汉字拼音
    /// </summary>
    /// <param name="cnChar"></param>
    /// <returns></returns>
    public static string getSpell(this string cnChar)
    {
        byte[] arrCN = Encoding.Default.GetBytes(cnChar);
        if (arrCN.Length > 1)
        {
            int area = (short)arrCN[0];
            int pos = (short)arrCN[1];
            int code = (area << 8) + pos;
            int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
            for (int i = 0; i < 26; i++)
            {
                int max = 55290;
                if (i != 25) max = areacode[i + 1];
                if (areacode[i] <= code && code < max)
                {
                    return Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                }
            }
            return "x";
        }
        else return cnChar;
    }

    /// <summary>
    /// 截取字符串，汉字两个字节，字母一个字节
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="strLength">字符串长度</param>
    /// <returns></returns>
    public static string Interruption(this string str, int len, string show)
    {
        ASCIIEncoding ascii = new ASCIIEncoding();
        int tempLen = 0;
        string tempString = "";
        byte[] s = ascii.GetBytes(str);
        for (int i = 0; i < s.Length; i++)
        {
            if ((int)s[i] == 63)
            { tempLen += 2; }
            else
            { tempLen += 1; }
            try
            { tempString += str.Substring(i, 1); }
            catch
            { break; }
            if (tempLen > len) break;
        }
        //如果截过则加上半个省略号 
        byte[] mybyte = System.Text.Encoding.Default.GetBytes(str);
        if (mybyte.Length > len)
            tempString += show; 
        tempString = tempString.Replace("&nbsp;", " ");
        tempString = tempString.Replace("&lt;", "<");
        tempString = tempString.Replace("&gt;", ">");
        tempString = tempString.Replace('\n'.ToString(), "<br>");
        return tempString;
    }

    /// <summary>
    /// 截取字符串，汉字两个字节，字母一个字节
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="strLength">字符串长度</param>
    /// <returns></returns>
    public static string CutString(this string str, int len, string show = "...")
    {
        return Interruption(str, len, show);
    }

    /// <summary>
    /// 得到实符串实际长度
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int Size(this string str)
    {
        byte[] strArray = System.Text.Encoding.Default.GetBytes(str);
        int res = strArray.Length;
        return res;
    }
    
    /// <summary>
    /// 产生随机字符串
    /// </summary>
    /// <returns>字符串位数</returns>
    public static string GetRandom(int length)
    {
        int number;
        char code;
        string checkCode = String.Empty;
        System.Random random = new Random();

        for (int i = 0; i < length + 1; i++)
        {
            number = random.Next();
            if (number % 2 == 0)
                code = (char)('0' + (char)(number % 10));
            else
                code = (char)('A' + (char)(number % 26));
            checkCode += code.ToString();
        }
        return checkCode;
    }
   
    /// <summary>
    /// 字符串是否包含标点符号(不包括_下画线)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool InPunctuation(this string str)
    {
        foreach (char c in str.ToCharArray())
        {
            if (char.IsPunctuation(c) && c!='_')
                return true;
        }
        return false;
       
    }
    /// <summary>
    /// 返回带星期的日期格式
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string ToDateWeekString(this DateTime date)
    {
        string week = string.Empty;
        switch (date.DayOfWeek)
        {
            case DayOfWeek.Friday: week = "五"; break;
            case DayOfWeek.Monday: week = "一"; break;
            case DayOfWeek.Saturday: week = "六"; break;
            case DayOfWeek.Sunday: week = "日"; break;
            case DayOfWeek.Thursday: week = "四"; break;
            case DayOfWeek.Tuesday: week = "二"; break;
            case DayOfWeek.Wednesday: week = "三"; break;
        }
        return date.ToString("yyyy年MM月dd日 ") + "星期" + week;
    }
    /// <summary>
    /// 是否为有效的路径字符串
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool IsPath(this string path)
    {
        return !path.IsNullOrEmpty() && !System.IO.Path.GetPathRoot(path).IsNullOrEmpty();
    }
    
}
