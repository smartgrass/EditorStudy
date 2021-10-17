using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace XiaoCao
{
    public static class XiaocaoEditorTool
    {
        public static void SelectSelf(this  UnityEngine.Object self)
        {
            Selection.activeObject = self;
        }

        public static Object[] ToObjectArray(this IEnumerable<UnityEngine.Object> list)
        {
            return list.ToArray();
        } 
    }
}
