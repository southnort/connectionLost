using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Yrr.Core
{
    public static class Extensions
    {
        public static void ClearChildren(this Transform transform)
        {
            var count = transform.childCount;
            if (count <= 0) return;
            for (var i = count - 1; i >= 0; i--)
            {
                var child = transform.GetChild(i);
                child.SetParent(null);
                Object.Destroy(child.gameObject);
            }
        }


        public static Vector2 GetRandomCoordinatesAroundPoint(this Vector2 originalPoint, float radius,
               bool pointOnRadiusLine = false)
        {
            float angle = Random.Range(0, 360);
            var lenght = pointOnRadiusLine ? radius :
                (Random.Range(0, radius));

            var x = Mathf.Cos(angle * Mathf.Deg2Rad) * lenght;
            var y = Mathf.Sin(angle * Mathf.Deg2Rad) * lenght;


            var result = new Vector2(originalPoint.x + x, originalPoint.y + y);


            return result;
        }


        public static T GetRandomItem<T>(this List<T> list)
        {
            return list.Count < 1 ? default : list[Random.Range(0, list.Count)];
        }

        public static T GetRandomItem<T>(this T[] list)
        {
            return list.Length < 1 ? default : list[Random.Range(0, list.Length)];
        }

        public static T GetLast<T>(this List<T> list)
        {
            return list.Count < 1 ? default : list[^1];
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Random.Range(0, n);
                list.Swap(k, n);
            }
        }

        public static void Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        }
    }


#if UNITY_EDITOR
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
#endif
}