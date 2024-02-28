using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using System.Runtime.InteropServices;

namespace MirraMetrika
{
    public class MirraAnalytics : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern void AddMetriksEvent(string eventID);

        [DllImport("__Internal")]
        private static extern void AddMetriksEventObject(string eventID, string eventObject);

        public static void SendEvent(string eventID)
        {
            if (!Application.isEditor)
                AddMetriksEvent(eventID);
            else
                Debug.Log($"Analytics Event Send: {eventID}");
        }

        public static void SendEventObject(string eventID, object eventObject)
        {
            if (!Application.isEditor)
                AddMetriksEventObject(eventID, JsonUtility.ToJson(eventObject));
            else
                Debug.Log($"Analytics Event Object Send: {eventID}\n Object: {JsonUtility.ToJson(eventObject)}");
        }

        public static void SendEventObject(string eventID, Dictionary<string, object> eventProperty)
        {
            string quote = "\"";
            string parameters = string.Empty;

            for(int i = 0; i < eventProperty.Count; i++)
            {
                var item = eventProperty.ElementAt(i);
                string addQuote = string.Empty;

                if (item.Value is string)
                    addQuote = quote;

                parameters += $"{quote}{item.Key}{quote}: {addQuote}{item.Value}{addQuote}";
                if (i < eventProperty.Count - 1)
                    parameters = parameters.Insert(parameters.Length, ",");
            }

            parameters = parameters.Insert(0, "{");
            parameters = parameters.Insert(parameters.Length, "}");

            if (!Application.isEditor)
                AddMetriksEventObject(eventID, parameters);
            else
                Debug.Log($"Analytics Event Object Send: {eventID}\n Object: {parameters}");
        }
    }
}
