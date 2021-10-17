using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using XiaoCao;


public class XiaoCaoEexample_1 : XiaoCaoWindow
{

    public string path  = "Assets";

    [MenuItem("Tools/XiaoCaoEexample_1")]
    static void Open()
    {
        OpenWindow<XiaoCaoEexample_1>();
    }

    [Button]
    private void Fun1()
    {
        //FindAndReplace(path);
        //FileTool.FindReplacePath(path, "替换", "545555555555555");
        FileTool.ReadFileWebUrl("http://127.0.0.1:8080/1.txt").LogStr();

        //AssetDatabase.Refresh();
    }

    private void FindAndReplace(string path ,string find,string repalce)
    {
        var strList = FileTool.ReadFileLines(path);
        FileTool.FindReplace(ref strList, find, repalce);
        FileTool.WriteToFile(strList, path);
    }
}
