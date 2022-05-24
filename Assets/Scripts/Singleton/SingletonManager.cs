using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Controller;
using Unity.Collections;
using UnityEngine;

namespace Singleton
{
    public class SingletonManager : MonoBehaviour, ISingleton, ICleanup
    {
        private HashSet<GameObject> awoken;
        public HashSet<GameObject> Awoken => awoken ?? (awoken = new HashSet<GameObject>());

        [SerializeField, ReadOnly] private List<string> types;

        public void UpdateTypeList()
        {
            types = awoken.Select(type => type.name).ToList();
        }


        public void Cleanup()
        {
            types.Clear();
            awoken.Clear();
            Singleton<SingletonManager>.IsInited = false;
            foreach (var single in awoken)
            {
                Destroy(single);
            }
        }
    }
}