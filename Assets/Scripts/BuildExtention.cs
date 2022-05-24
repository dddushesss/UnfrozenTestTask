using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static partial class BuildExtention
{
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        var res = gameObject.GetComponent<T>();
        if (res == null)
        {
            res = gameObject.AddComponent<T>();
        }

        return res;
    }

}