using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Window3 : EditorWindow
{

    string str1;

    string str2;

    //菜单工具
    [MenuItem("Tools/LayoutWindow")]
    static void ShowWindow()
    {
        GetWindow<Window3>().Show();
    }


    //EditorGUILayout 
    //GUILayout 
    //EditorUtility
    //EditorWindow
    //GUI.skin

    /*
    EditorWindow类
    GetWindow<T>().Show();

    GUILayout类
    GUILayout.TextField 输入框
    GUILayout.Button 按钮
    GUILayout.Label 这是标签

    EditorGUILayout类
    EditorGUILayout.BeginHorizontal(); 水平布局
        ...
    EditorGUILayout.EndHorizontal();

    GUI.skin类
    GUI.skin.label.fontSize  调整labe的风格(全局)

    
    EditorUtility类
    EditorUtility.DisplayDialog 对话框

  
     */


    private void OnGUI()
    {
        //排序布局
        
        GUILayout.Label("这是标签");

        EditorGUILayout.BeginHorizontal();
        str1 = GUILayout.TextField(str1,GUILayout.Width(240));

        if (GUILayout.Button(new GUIContent("确定")))
        {
            EditorUtility.DisplayDialog("标题", str1, "确定");
        }
        EditorGUILayout.EndHorizontal();

        str2 = GUILayout.TextField(str2);

        if (GUILayout.Button(new GUIContent("确定")))
        {
            EditorUtility.DisplayDialog("标题", str2, "确定");
        }
    }

    


}
