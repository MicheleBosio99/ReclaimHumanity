using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreatePersistentFolders {
    
    private static CreatePersistentFolders instance;

    public static CreatePersistentFolders GetInstance() { return instance ??= new CreatePersistentFolders(); }

    private CreatePersistentFolders() { }
    
    public void GeneratePersistentFolder(string _path) {
        var path = Path.Combine(Application.persistentDataPath, _path);
        if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
    }
}
