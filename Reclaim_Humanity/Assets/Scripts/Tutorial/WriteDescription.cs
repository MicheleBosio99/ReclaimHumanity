using TMPro;
using UnityEngine;

public class WriteDescription : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextAsset descriptionTextFile;

    private void Start() { descriptionText.text = descriptionTextFile.text; }
}
