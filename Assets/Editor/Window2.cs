using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Window2 : EditorWindow
{

    string str1;

    //菜单工具
    [MenuItem("Tools/InputWindow")]
    static void ShowWindow()
    {
        GetWindow<Window2>().Show();
    }


    //EditorGUILayout 组件
    //GUILayout 组件
    //EditorUtility 窗口
    //GUI.skin
    //EditorWindow
     private void OnGUI()
    {
        GUILayout.Label("这是标签");

        str1 = GUILayout.TextField(str1);

        if (GUILayout.Button(new GUIContent("确定")))
        {
            EditorUtility.DisplayDialog("标题", str1, "确定");
        }
    }

    
}
