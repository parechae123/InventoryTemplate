using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTon <T> where T : SingleTon<T>,new()
{
    private static T instance;
    public static T GetInstance 
    { 
        get 
        {
            if (instance == null)
            {
                instance = new T();
                instance.Init();
            }
            return instance;
        } 
    }
    protected virtual void Init()
    {

    }
}
