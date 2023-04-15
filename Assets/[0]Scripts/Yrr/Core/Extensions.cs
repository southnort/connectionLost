using System.Collections.Generic;
using System.Globalization;
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


        public static string ToIntString(this float value)
        {
            return ((int)value).ToString();
        }

        public static string ToDotString(this float value)
        {
            var str = value.ToString(CultureInfo.InvariantCulture);
            return str.Replace(",", ".");
        }

        public static string ToShortMoneyString(this float value)
        {
            string[] prefix = { string.Empty, "K", "M", "B" };
            var absolute = Mathf.Abs(value);
            int add;
            if (absolute < 1)
            {
                add = (int)Mathf.Floor(Mathf.Floor(Mathf.Log10(absolute)) / 3);
            }
            else
            {
                add = (int)(Mathf.Floor(Mathf.Log10(absolute)) / 3);
            }

            var shortNumber = value / Mathf.Pow(10, add * 3);

            return $"{shortNumber:0.#}{prefix[add]}";
        }

        public static string ToShortTimeString(this float timeValue)
        {
            var time = (int)timeValue + 1;

            var seconds = time % 60f;
            var minutes = time / 60;
            var hours = minutes / 60;
            minutes = minutes % 60;

            if (hours > 0) return hours.ToString("00") + "h ";
            if (minutes > 0) return minutes.ToString("00") + "m ";
            return seconds.ToString("00") + "s";
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