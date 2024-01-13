using UnityEngine;

public class ButtonManager : MonoBehaviour {

    [SerializeField] private GameObject YouSureQuitView;
    [SerializeField] private GameObject BackgroundPanel;
    [SerializeField] private GameObject FeedbackPanel;
    [SerializeField] private GameObject mainCharacter;

    private void Start() {
        BackgroundPanel.SetActive(false);
        YouSureQuitView.SetActive(false);
        FeedbackPanel.SetActive(false);
    }

    public void OnSaveClick() { GameManager.SaveGame(); }

    public void OnQuitGameClick() {
        BackgroundPanel.SetActive(true);
        YouSureQuitView.SetActive(true);
    }

    public void OnFeedbackButtonClick() {
        BackgroundPanel.SetActive(true);
        FeedbackPanel.SetActive(true);
    }

    public void OnGoBackClick() {
        BackgroundPanel.SetActive(false);
        YouSureQuitView.SetActive(false);
        FeedbackPanel.SetActive(false);
    }

    public void OnReallyQuitGameClick() {
        mainCharacter.GetComponent<OpenInventoryScript>().OpenInventoryBody("close");
        GameManager.GoToMainMenu();
    }
}
