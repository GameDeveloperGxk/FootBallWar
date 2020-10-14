using UnityEngine;
using System.Collections;

public class Tools {

    private static Tools instance = null ;

    public static Tools GetInstance()
    {
        if( instance == null)
        {
            instance = new Tools();
        }
        return instance;
    }

    private Tools()
    {

    }

    /// <summary>
    /// 移除对象的全部子对象
    /// </summary>
    /// <param name="parent"></param>
    public static void RemoveAllChildren(GameObject parent)
    {
        Transform transform;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            transform = parent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }

}
