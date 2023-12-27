using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionButtonsBehaviour : MonoBehaviour {

    public void OnMenuButtonClick() { gameObject.GetComponent<ScrollAutomatically>().ContinueStartGame("MainMenu"); }

    public void OnContinueButtonClick() { gameObject.GetComponent<ScrollAutomatically>().ContinueStartGame("Laboratory"); }
}
