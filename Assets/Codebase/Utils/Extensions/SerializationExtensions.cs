using UnityEngine;

namespace Assets.Codebase.Utils.Extensions
{
    public static class SerializationExtensions
    {
        public static T ToDeserealized<T>(this string json)
        => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj)
        => JsonUtility.ToJson(obj);
    }
}
