using System.Collections;
using TMPro;
using UnityEngine;

public class HandleFeedbackCanvas : MonoBehaviour {
    
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private TMP_InputField inputFieldFeedback;

    public void OnReturnToGameButtonClick() { buttonsPanel.GetComponent<ButtonManager>().OnGoBackClick(); }

    public void OnSendFeedbackButtonClick() {
        gameObject.GetComponent<HandleFeedbacks>().SendFeedback();
        buttonsPanel.GetComponent<ButtonManager>().OnGoBackClick();
    }
    
    
}
