using System.IO;
using UnityEngine;
using UnityEngine.UI;
using File = UnityEngine.Windows.File;

public class LoadGameButton : MonoBehaviour {
    
    private void Start() {
        // Disable Load Game if there is no game instance already played
        if(!File.Exists(Path.Combine(Application.persistentDataPath, "savegame.dat"))) {
            GetComponent<Button>().interactable = false;
        }
    }

    public void OnButtonClick() {
        GameManager.LoadGame();
    }
}
