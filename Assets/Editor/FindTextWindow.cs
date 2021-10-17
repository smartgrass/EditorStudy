using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using XiaoCao;


public class FindTextWindow : XiaoCaoWindow
{
    public string findStr = "";


    //菜单工具
    [MenuItem("Tools/FindText")]
    static void Open()
    {
        OpenWindow<FindTextWindow>();
    }

    [Button]
    private void FindText()
    {
        if (findStr.IsEmpty())
            return;

        var texts = GameObject.FindObjectsOfType<Text>();

        foreach (var item in texts)
        {
            if (item.text.Contains(findStr) && item.gameObject.activeInHierarchy)
            {
                item.gameObject.SelectSelf();
            }
        }

    }


}

