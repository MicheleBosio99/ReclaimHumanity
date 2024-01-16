using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class NumOfHumansLeft : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI numOfHumansText;
    [SerializeField] private string biome;
    [SerializeField] private int numOfHumansTotal;
    [SerializeField] private Color textColorIfAll;
    
    private int numOfHumansAlready;

    private void Start() {
        numOfHumansAlready = GameManager.humansTalkedTo.GetHumansByBiome(biome);
        numOfHumansText.text = $"{numOfHumansAlready}/{numOfHumansTotal}";
        if(numOfHumansAlready == numOfHumansTotal) { numOfHumansText.color = textColorIfAll; }
    }
    
    public void AddHumanTalkedTo() {
        numOfHumansAlready ++;
        numOfHumansAlready = Mathf.Min(numOfHumansAlready, numOfHumansTotal);
        
        numOfHumansText.text = $"{numOfHumansAlready}/{numOfHumansTotal}";
        
        if(numOfHumansAlready == numOfHumansTotal) { numOfHumansText.color = textColorIfAll; }
        GameManager.humansTalkedTo.SetHumansByBiome(biome, numOfHumansAlready);
    }
}