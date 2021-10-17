using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.VisualElement;
using Object = UnityEngine.Object;

public class FindTextWindow : EditorWindow
{
    string str1;

    GameObject textObj;
    static GameObject targetObj;
    static GameObject prefabTextObj;

    static string textPath = "";

    static FindTextWindow instance;

    //菜单工具
    [MenuItem("Tools/FindText")]
    static void ShowWindow()
    {
        instance = GetWindow<FindTextWindow>();
        instance.Show();
        instance.titleContent = new GUIContent("这是标题");
        //位置和窗口大小
        instance.position = new Rect(50, 50, 330, 200);
    }

    static void FindAssetsUsingSearchFilter(string name)
    {
        if (name.EndsWith("(clone)"))
        {
            name = name.Substring(0, name.Length - 7 - 1);
        }
        // Find all assets labelled with 'concrete' :
        var guids = AssetDatabase.FindAssets(name, new string[] { "Assets/Resource" });
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
            if (obj.name == name)
            {
                targetObj = obj;
            }
        }
    }

    void Clear()
    {
        targetObj = null;
        textObj = null;
        str1 = null;
    }


    void SearchText()
    {
        string searchStr = str1;
        if (string.IsNullOrEmpty(searchStr))
            return;

        List<Text> allText = new List<Text>(Object.FindObjectsOfType<Text>());

        foreach (var item in allText)
        {
            if (item.text.Contains(searchStr))
            {
                Select(item.gameObject);
                textObj = item.gameObject;
                textPath = GetPath(textObj.transform);
                FindAssetsUsingSearchFilter(item.transform.root.gameObject.name);
                break;
            }
        }
    }

    static void FindPrefabText()
    {
        if (targetObj != null)
        {
            string path = AssetDatabase.GetAssetPath(targetObj as Object);
            ////PrefabUtility.LoadPrefabContents(path);
            var prefab = PrefabStageUtility.GetCurrentPrefabStage();//PrefabStageUtility.GetCurrentPrefabStage();
            if (prefab != null)
            {
                var root = prefab.prefabContentsRoot;
                var findTF = root.transform.Find(textPath);
                prefabTextObj = findTF.gameObject;
            }
        }
    }

    string GetPath(Transform tf)
    {
        string str = tf.name;
        while (tf.parent != null && tf.parent.parent != null)
        {
            tf = tf.parent;
            str = tf.name + "/" + str;
        }
        Debug.Log("yns path = " + str);
        return str;
    }


    public static void Select(Object obj)
    {
        Selection.objects = new Object[] { obj };
    }

    public static void Select(Object[] objs)
    {
        Selection.objects = objs;
    }

    private void OnGUI()
    {
        //搜索场景所有text
        EditorGUILayout.Separator();
        int fontSize = GUI.skin.label.fontSize;
        int inputFontSize = GUI.skin.textField.fontSize;


        GUI.skin.label.fontSize = 20; //字体
        GUI.skin.textField.fontSize = 18;



        GUILayout.Label("Text内容查找");

        EditorGUILayout.BeginHorizontal();
        {
            str1 = GUILayout.TextField(str1, new[] { GUILayout.Height(22), GUILayout.Width(240) });

            if (GUILayout.Button(new GUIContent("查找"), new[] { GUILayout.Height(20) }))
            {
                SearchText();
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Separator();
        EditorGUILayout.Separator();

        GUI.skin.label.fontSize = 10;
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("Text", new[] { GUILayout.Width(45), });
            textObj = EditorGUILayout.ObjectField("", textObj, typeof(GameObject), true) as GameObject;

        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("prefab", new[] { GUILayout.Width(45), });
            targetObj = EditorGUILayout.ObjectField("", targetObj, typeof(GameObject), true) as GameObject;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.Label("target", new[] { GUILayout.Width(45), });
            prefabTextObj = EditorGUILayout.ObjectField("", prefabTextObj, typeof(GameObject), true) as GameObject;
        }
        EditorGUILayout.EndHorizontal();

        //prefabTextObj

        EditorGUILayout.Separator();
        if (GUILayout.Button(new GUIContent("查找预制体"), new[] { GUILayout.Height(20), GUILayout.Width(80) }))
        {
            FindPrefabText();
        }



        EditorGUILayout.Separator();
        if (GUILayout.Button(new GUIContent("清空"), new[] { GUILayout.Height(20), GUILayout.Width(80) }))
        {
            Clear();
        }

        //还原字体大小
        //GUI.skin.label.fontSize = 10;
        //GUI.skin.textField.fontSize = 20;

        GUI.skin.label.fontSize = default;
        GUI.skin.textField.fontSize = default;
    }




}




public class EditorTextWindow : EditorWindow //继承
{

    #region init
    private static EditorTextWindow instance;
    [MenuItem("Tools/EditorTextWindow")]
    static void PrefabWrapTool()
    {
        //获取当前窗口实例
        instance = EditorWindow.GetWindow<EditorTextWindow>();
        instance.Show();
    }
    #endregion

     bool IsAndroid = true;
     bool IsIOS = true;
     bool ISWindows = true;

     string AbVersion = "2.6.5";
     string Version = "v31";

    private void Awake()
    {
         
    }

    void OnGUI()
    {
        //EditorGUILayout.PropertyField(this.)


    }

}



/// <summary>
/// 定义对带有 `CustomLabelAttribute` 特性的字段的面板内容的绘制行为。
/// </summary>
#if UNITY_EDITOR
//[CustomPropertyDrawer(typeof(DrawButtonAttribute))]
//public class CustomButtonDrawer : PropertyDrawer
//{
//    private GUIContent _label = null;
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        if (_label == null)
//        {
//            string name = (attribute as DrawButtonAttribute).name;
//            _label = new GUIContent(name);
//        }

//        EditorGUI.PropertyField(position, property, _label);
//        if (EditorGUI.DropdownButton(position,label,FocusType.Passive)) { }
//    }
//}
#endif


//public class CustomLabelAttribute : PropertyAttribute
//{
//    public string name;

//    /// <summary>
//    /// 使字段在Inspector中显示自定义的名称。
//    /// </summary>
//    /// <param name="name">自定义名称</param>
//    public CustomLabelAttribute(string name)
//    {
//        this.name = name;
//    }
//}

///// <summary>
///// 定义对带有 `CustomLabelAttribute` 特性的字段的面板内容的绘制行为。
///// </summary>
//#if UNITY_EDITOR
//[CustomPropertyDrawer(typeof(CustomLabelAttribute))]
//public class CustomLabelDrawer : PropertyDrawer
//{
//    private GUIContent _label = null;
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        if (_label == null)
//        {
//            string name = (attribute as CustomLabelAttribute).name;
//            _label = new GUIContent(name);
//        }

//        EditorGUI.PropertyField(position, property, _label);
//    }
//}
//#endif