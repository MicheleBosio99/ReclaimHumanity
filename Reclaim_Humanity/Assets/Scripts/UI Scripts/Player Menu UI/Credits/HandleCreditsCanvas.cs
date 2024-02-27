using UnityEngine;

public class HandleCreditsCanvas : MonoBehaviour {
    
    [SerializeField] private GameObject buttonsPanel;
    
    public void OnReturnToGameButtonClick() { buttonsPanel.GetComponent<ButtonManager>().OnGoBackClick(); }
}