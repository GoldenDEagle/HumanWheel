using System;
using TMPro;
using UniRx;

namespace Assets.Codebase.Utils.Extensions
{
    public static class UnityUIExtensions
    {
        public static IDisposable SubscribeToTMPText(this IObservable<string> source, TMP_Text text)
        {
            return source.SubscribeWithState(text, (x, t) => t.text = x);
        }

        public static IDisposable SubscribeToTMPText<T>(this IObservable<T> source, TMP_Text text)
        {
            return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
        }
    }
}
