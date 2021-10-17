using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor;
using System.Reflection;
using NaughtyAttributes.Editor;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
using System;

public class Test : MonoBehaviour
{
    public string str = "";


    public void fun1()
    {

    }

    [Btn("11")]
    public void fun2()
    {
        Debug.Log("yns fun2");
    }


}



#if UNITY_EDITOR
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class BtnAttribute : SpecialCaseDrawerAttribute
{
    public string Text { get; private set; }

    public BtnAttribute(string text = null)
    {
        this.Text = text;
    }
}


//[CanEditMultipleObjects]
//[CustomEditor(typeof(UnityEngine.Object), true)]
public class TestEditor : Editor
{
    private IEnumerable<MethodInfo> _methods;

    protected virtual void OnEnable()
    {
        _methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).Where(a=>a.GetCustomAttribute<BtnAttribute>()!=null);
        string str = "";
        foreach (var item in _methods)
        {
            str += item.Name;
            str += "   ";
        }
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawButtons();

    }
    protected void DrawButtons()
    {
        if (_methods.Any())
        {
            foreach (var method in _methods)
            {
                DrawButton2(target, method);
            }
        }
    }


    public void DrawButton2(UnityEngine.Object target, MethodInfo methodInfo)
    {
       // EditorGUI.BeginDisabledGroup(false);
        BtnAttribute buttonAttribute = (BtnAttribute)methodInfo.GetCustomAttributes(typeof(BtnAttribute), true)[0];
        string buttonText = string.IsNullOrEmpty(buttonAttribute.Text) ? ObjectNames.NicifyVariableName(methodInfo.Name) : buttonAttribute.Text;

        if (GUILayout.Button(buttonText))
        {
            object[] defaultParams = methodInfo.GetParameters().Select(p => p.DefaultValue).ToArray();
            IEnumerator methodResult = methodInfo.Invoke(target, defaultParams) as IEnumerator;

            if (!Application.isPlaying)
            {
                // Set target object and scene dirty to serialize changes to disk
                EditorUtility.SetDirty(target);

                PrefabStage stage = PrefabStageUtility.GetCurrentPrefabStage();
                if (stage != null)
                {
                    // Prefab mode
                    EditorSceneManager.MarkSceneDirty(stage.scene);
                }
                else
                {
                    // Normal scene
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
            }
            else if (methodResult != null && target is MonoBehaviour behaviour)
            {
                behaviour.StartCoroutine(methodResult);
            }
        }

        //EditorGUI.EndDisabledGroup();
    }

    private static GUIStyle GetHeaderGUIStyle()
    {
        GUIStyle style = new GUIStyle(EditorStyles.centeredGreyMiniLabel);
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.UpperCenter;

        return style;
    }

}
#endif