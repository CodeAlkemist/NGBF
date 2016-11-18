using UnityEngine;
using System.Collections;

namespace NGBF
{
    public class IOHandler
    {
        public T OpenFile<T>()
        {
            return default(T);
        }
        public bool SaveFile<T>(T Data, string location)
        {
            return false;
        }
    }
}
