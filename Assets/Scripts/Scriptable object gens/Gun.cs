using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Kawaiisun.SimpleHostile{
    [CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
    public class Gun : ScriptableObject
    {
        public string name;
        public GameObject prefab;
        public float firerate;
    }
}
