using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Window1 : EditorWindow
{
    //菜单工具
    [MenuItem("Tools/EmptyWindow")]
    static void ShowWindow()
    {
        GetWindow<Window1>().Show();
    }
    private void OnGUI()
    {
        int size = GUI.skin.label.fontSize;
        GUI.skin.label.fontSize = 40; //字体
        GUILayout.Label("这是标签");

        GUI.skin.label.fontSize = size;
    }

}
