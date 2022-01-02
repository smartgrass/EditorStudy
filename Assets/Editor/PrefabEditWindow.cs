using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using XiaoCao;


public class PrefabEditWindow : XiaoCaoWindow
{
    public GameObject prefabRoot;
    public GameObject root;




    //菜单工具
    [MenuItem("Tools/PrefabEditWindow")]
    static void Open()
    {
        OpenWindow<PrefabEditWindow>();
    }

    [Button("查找预制体")]
    private void FindPrefab()
    {
        //找到预制体的路径
        var res = PrefabUtility.GetNearestPrefabInstanceRoot(root);
        res.SelectSelf();
        Debug.Log(res);
    }

    [Button]
    private void FindPrefab2()
    {
        var res = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(root);
        Debug.Log(res);
    }


    [Button]
    private void FindPrefab3()
    {
        var res = PrefabUtility.GetOutermostPrefabInstanceRoot(root);
        Debug.Log(res);

    }

    [MenuItem("GameObject/SearchRoot", priority = 0)]
    private static void Search(MenuCommand menuCommand)
    {
        Debug.Log(menuCommand.context);
    }



}

