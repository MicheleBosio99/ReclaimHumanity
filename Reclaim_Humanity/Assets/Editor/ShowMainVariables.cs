using UnityEngine;
using UnityEditor;

public class ShowMainVariables : EditorWindow {
    
    private InventoryItemsSO inventory;
    
    [MenuItem("Window/Debug Main Variables")]
    public static void ShowWindow() {
        EditorWindow.GetWindow(typeof(ShowMainVariables));
    }

    private void OnGUI() {
        GUILayout.Label("Current Time Scale: " + Time.timeScale.ToString());
    }
}
