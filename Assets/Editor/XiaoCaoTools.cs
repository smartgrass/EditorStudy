using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;


public static class StringTool{

    #region 扩展方法
    public static List<string> ToLineList(this string str)
    {
        var strs = str.Split('\n');
        return new List<string>(strs);
    }
    public static string LogStr(this string str,string title = "Log")
    {
        Debug.LogFormat("{0}: {1}",title ,str);
        return str;
    }

    public static bool IsEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    #endregion


}


public static class FileTool
{
    public static bool FindReplacePath(string path, string find, string repalce)
    {
        var strList = ReadFileLines(path);
        bool res = FindReplace(ref strList, find, repalce);
        WriteToFile(strList, path);
        return res;
    }
    public static bool FindReplace(ref List<string> strList, string find, string replace)
    {
        int line = FindStrLine(strList, find);
        if(line >= 0)
        {
            Debug.Log(strList[line] + " -> " + replace);
            strList[line] = replace;
            return true;
        } 
        return false;
    }
    public static int FindStrLine(List<string> strList,string find)
    {
        int _len = strList.Count;
        for (int i = 0; i < _len; i++)
        {
            var item = strList[i];
            if (item.Contains(find))
            {
                return i;
            }
        }
        return -1;
    }

    public static void WriteToFile(string str, string filePath)
    {
        using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            sw.Write(str);
            sw.Close();
        }
    }

    public static void WriteToFile(List<string> strList, string filePath)
    {
        string tempfile = Path.GetTempFileName();
        using (var writer = new StreamWriter(tempfile))
        {
            foreach (var str in strList)
            {
                writer.WriteLine(str);
            }
        }
        File.Copy(tempfile, filePath, true);
        //删除临时文件
        if (File.Exists(tempfile))
        {
            Debug.Log("删除临时文件: " + tempfile);
            File.Delete(tempfile);
        }

    }

    public static List<string> ReadFileLines(string filePath)
    {
        List<string> strList = new List<string>();
        try
        {
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    strList.Add(reader.ReadLine());
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        return strList;
    }

    public static string ReadFileString(string filePath)
    {
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(filePath);
        }
        catch (Exception)
        {
            return "";
        }
        return sr.ReadToEnd();
    }

    public static string ReadFileWebUrl(string url)
    {
        WebClient client = new WebClient();
        byte[] buffer = client.DownloadData(new Uri(url));
        string res = Encoding.UTF8.GetString(buffer);
        return res;
    }

    public static string DownloadUrlText(string url,string localfilePath)
    {
        string str = ReadFileWebUrl(url);
        WriteToFile(str, localfilePath);
        return str;
    }
}