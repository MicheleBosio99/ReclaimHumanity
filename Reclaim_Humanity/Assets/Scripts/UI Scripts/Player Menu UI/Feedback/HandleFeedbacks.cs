using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;

public class HandleFeedbacks : MonoBehaviour {
    
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_InputField feedback;
    private const string URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSeMve_nO08T4yTeDCNKwI00niZFws5yOQaE8tHYIzmUaeLkhQ/formResponse";
    private const string questionEntry = "entry.1797354207";

    private void Start() {
        feedback.onSelect.AddListener(delegate {OnInputFieldSelect(); });
        feedback.onDeselect.AddListener(delegate {OnInputFieldDeselect(); });
    }

    public void SendFeedback() {
        if (feedback.text == "Write here your feedback. Don't leave personal informations. Thanks.") { return; }
        StartCoroutine(SendFeedbackCoroutine(feedback.text));
    }

    private IEnumerator SendFeedbackCoroutine(string feedbackText) {
        var form = new WWWForm();
        form.AddField(questionEntry, feedbackText);
        var www = UnityWebRequest.Post(URL, form);
        feedback.text = "";
        yield return www.SendWebRequest();
    }

    private void OnInputFieldSelect() { player.GetComponent<PlayerInput>().DeactivateInput(); }

    private void OnInputFieldDeselect() { player.GetComponent<PlayerInput>().ActivateInput(); }
}
