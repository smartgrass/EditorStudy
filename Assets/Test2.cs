using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public bool IsON;
    private string str = "44";
    public ClassA classA = new ClassA();

    public Object Object;
    void Update()
    {
        if (IsON) 
        {
            ForGameName();
        }
        else
        {
            ForGameName2();
        }
    }


    private void ForGameName()
    {
        for (int i = 0; i < 5000; i++)
        {
            //DoString();
            Debug.Log("yns  " + Object.name.GetHashCode());
        }
    }
    private void ForGameName2()
    {
        for (int i = 0; i < 5000; i++)
        {
            DoString(classA.name);

        }
        Debug.Log("yns  " + classA.name.GetHashCode());

    }
    private void DoString(string name)
    {

    }
}


public class ClassA
{
    public string str = "Canvas";



    public string name
    {
        get
        {
            return str;
        }
        set
        {
            str = value;
        }
    }
}