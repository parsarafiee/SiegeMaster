using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using General;
using System;
using System.Reflection;
using Managers;
public class Helper
{

    // GetClosetInRange : variable 
    // type : the object that you want to get 
    // correntTransfrom : get the closest object to this transform 
    // range  : the range to get the closest 
    //Give Bonus Point to Charles :)  parsa : he deals damage with yasuo less than yummi 

    public static Transform GetClosetInRange(Type type, Transform correntTransfrom, float range) // 
    {
        Transform newtransform = null;
        //using reflextion to find the manager and get the method 
        var manager = type.GetProperty("Instance", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)?.GetValue(null);

        MethodInfo info = type.GetMethod("GetClosest");
        if (info == null)
        {
            Debug.Log("we couldnt found the Method in manager ");
            return null;
        }
        newtransform = (Transform)info.Invoke(manager, new object[] { correntTransfrom, range });
        if (newtransform == null)
        {
            return null;
        }
        return newtransform;

    }
}
