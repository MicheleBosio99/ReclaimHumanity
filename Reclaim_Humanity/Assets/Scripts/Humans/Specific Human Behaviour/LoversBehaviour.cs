using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class LoversBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite loverBoyLeftSight;
    [SerializeField] private Sprite loverGirlRightSight;
    [SerializeField] private GameObject loverBoy;
    [SerializeField] private GameObject loverGirl;
    [SerializeField] private GameObject player;
    [SerializeField] private TextAsset humansJsonTextAssets;
    
    [SerializeField] private GameObject blackBackground;
    [SerializeField] private Image blackBackgroundImage;
    [SerializeField] private TextMeshProUGUI textBackground;
    
    [SerializeField] private Vector2 loverBoyNewPosition;
    [SerializeField] private Vector2 loverGirlNewPosition;
    
    private List<string> loverGirlChangedGeneralDialogue;
    private List<string> loverBoyChangedGeneralDialogue;

    private const string loverBoyID = "03_OF_LoverBoyHuman";
    private const string loverGirlID = "04_OF_LoverGirlHuman";

    private string persistentHumanPath;
    private string humansJson;
    private List<Human> humans;
    
    private bool doneAlready;

    private void Awake() {
        blackBackground.SetActive(false);
        
        persistentHumanPath = Path.Combine(Application.persistentDataPath, "Resources/Humans/humans_OF.json");
        CreatePersistentFolders.GetInstance().GeneratePersistentFolder(Path.Combine(Application.persistentDataPath, "Resources/Humans"));

        if (PlayerPrefs.HasKey("RomeoJulietLoversDone")) {
            doneAlready = true; // PlayerPrefs.GetInt("RomeoJulietLoversDone") == 1;
        }
    }

    private void Start() { StartCoroutine(!doneAlready ? CheckLoverStatus() : ChangeLovers(false)); }

    private IEnumerator CheckLoverStatus() {
        var loverBoyHandler = loverBoy.GetComponent<InteractionHumanHandler>();
        var loverGirlHandler = loverGirl.GetComponent<InteractionHumanHandler>();

        while (loverBoyHandler.Human == null || loverGirlHandler.Human == null) {
            yield return new WaitForSeconds(2.5f);

            var loverBoyChanged = loverBoyHandler.Human == null;
            var loverGirlChanged = loverGirlHandler.Human == null;

            if (!loverBoyChanged && !loverGirlChanged) continue;
            
            loverBoyHandler = loverBoy.GetComponent<InteractionHumanHandler>();
            loverGirlHandler = loverGirl.GetComponent<InteractionHumanHandler>();
        }

        while (!loverBoyHandler.Human.spokenTo || !loverGirlHandler.Human.spokenTo) { yield return new WaitForSeconds(1.5f); }

        if (player.GetComponent<OpenInventoryScript>().isActive) { player.GetComponent<OpenInventoryScript>().OpenCloseUIFunc(true); }
        
        StartCoroutine(ChangeLovers(true));
        doneAlready = true;
    }
    
    private const float fadeDuration = 2.0f;
    private const float holdDuration = 4.0f;

    private IEnumerator ChangeLovers(bool doFade) {
        PlayerPrefs.SetInt("RomeoJulietLoversDone", 1);
        ChangeDialogues();

        if (doFade) {
            blackBackground.SetActive(true);
            StartCoroutine(FadeToBlack(true));
            yield return new WaitForSeconds(2.0f);
        }
        
        loverGirl.GetComponent<SpriteRenderer>().sprite = loverGirlRightSight;
        loverBoy.GetComponent<SpriteRenderer>().sprite = loverBoyLeftSight;
        
        
        
        loverGirl.transform.position = loverGirlNewPosition;
        loverBoy.transform.position = loverBoyNewPosition;

        if (doFade) {
            ChangePlayerPosition();
            
            textBackground.text = "The love unleashed by this long divided couple is so strong\nit can even heal a little robot.\n<i>Your health has been completely restored</i>";
            yield return new WaitForSeconds(holdDuration);
            textBackground.text = "";
            StartCoroutine(FadeToBlack(false));
            blackBackground.SetActive(false);
        }
    }

    private void ChangePlayerPosition() { player.GetComponent<PlayerMovement>().ChangePlayerPosition(gameObject.transform.position); }

    private IEnumerator FadeToBlack(bool fade) {
        var startColor = blackBackgroundImage.color;
        var endColor = blackBackgroundImage.color;
        endColor.a = fade ? 1.0f : 0.0f;
        var a = fade ? 0 : 1; var b = 1 - a;

        var currentTime = 0f;
        while (currentTime < fadeDuration) {
            currentTime += Time.deltaTime;
            blackBackgroundImage.color = Color.Lerp(startColor, endColor, Mathf.Lerp(a, b, currentTime / fadeDuration));
            yield return null;
        }
        
        // blackBackgroundImage.color = originalColor;
    }

    private void ChangeDialogues() {
        if (!File.Exists(persistentHumanPath)) { File.WriteAllText(persistentHumanPath, humansJsonTextAssets.text); }
        humansJson = File.ReadAllText(persistentHumanPath);
        
        humans = new List<Human>();
        humans = JsonConvert.DeserializeObject<List<Human>>(humansJson);
        
        var loverBoyHuman = humans.Find(hum => hum.humanID == loverBoyID);
        var loverGirlHuman = humans.Find(hum => hum.humanID == loverGirlID);
        
        InitializeNewGeneralDialogues();
        
        loverBoyHuman.generalDialogue.phrases = loverBoyChangedGeneralDialogue;
        loverGirlHuman.generalDialogue.phrases = loverGirlChangedGeneralDialogue;
        
        var loverBoyHandler = loverBoy.GetComponent<InteractionHumanHandler>().Human;
        var loverGirlHandler = loverGirl.GetComponent<InteractionHumanHandler>().Human;
        
        if (loverBoyHandler != null) { loverBoyHandler.generalDialogue.phrases = loverBoyChangedGeneralDialogue; }
        if (loverGirlHandler != null) { loverGirlHandler.generalDialogue.phrases = loverGirlChangedGeneralDialogue; }
        
        File.WriteAllText(persistentHumanPath, JsonConvert.SerializeObject(humans, Formatting.Indented));
    }

    private void InitializeNewGeneralDialogues() {
        loverBoyChangedGeneralDialogue = new List<string> { "<i>She's so beautiful...</i>" };
        loverGirlChangedGeneralDialogue = new List<string> { "<i>I still love him so much <3</i>" };
    }
}
