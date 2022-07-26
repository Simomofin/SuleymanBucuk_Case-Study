using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SceneData", menuName = "SceneData/Create Scene Data", order = 1)]
public class SceneData : ScriptableObject
{
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset; // to prevent data lose 
    }

    [System.Serializable]
    public class DataContainer
    {
        public string tag = "";
        public Vector3 position = Vector3.zero;
        public Quaternion rotation = Quaternion.identity;
    }
    public List<DataContainer> SceneObjects;
   
} // Class
