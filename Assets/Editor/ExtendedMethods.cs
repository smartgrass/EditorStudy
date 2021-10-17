using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.ComponentModel;


public static class ExtendedMethods
{
    /*public static string GetEnumLabel(this Enum enumValue)
    {
        FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        DescriptionAttribute[] attrs =
            fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        return attrs.Length > 0 ? attrs[0].Description : enumValue.ToString();
    }*/
    public static string GetEnumLabel(this Enum enumValue)
    {
        FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        EnumLabelAttribute[] attrs =
            fieldInfo.GetCustomAttributes(typeof(EnumLabelAttribute), false) as EnumLabelAttribute[];

        return attrs.Length > 0 ? attrs[0].label : enumValue.ToString();
    }
}

