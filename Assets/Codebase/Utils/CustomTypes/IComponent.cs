using UnityEngine;

namespace Assets.Codebase.Utils.CustomTypes
{
    public interface IComponent
    {
        Transform transform { get; }
        GameObject gameObject { get; }
    }
}
