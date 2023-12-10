using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DeleteResourcesPersistentFolder : MonoBehaviour {
    
    [SerializeField] private bool doIt;
    private void Awake() {
        var path = Path.Combine(Application.persistentDataPath, "Resources");
        if (doIt && Directory.Exists(path)) { Directory.Delete(path, true); }
    }
}
