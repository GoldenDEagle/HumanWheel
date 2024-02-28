using System;
using System.Collections.Generic;

namespace Assets.Codebase.Utils.CustomTypes
{
    [Serializable]
    public class Sublist<T>
    {
        public string Name = "ListName";
        public List<T> List = new List<T>();
    }
}
