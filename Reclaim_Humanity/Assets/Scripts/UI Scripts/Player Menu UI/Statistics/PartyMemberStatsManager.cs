using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberStatsManager : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image memberImage;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject radarChart;
    [SerializeField] private Button minorHealButton;
    [SerializeField] private Button majorHealButton;
    
    [SerializeField] private Image minorHealImage;
    [SerializeField] private Image majorHealImage;
    
    [SerializeField] private InventoryItemsSO inventoryItemsSO;
    
    private HealthBarBehaviour healthBarBehaviour;
    private PentagonalChartBehaviour pentagonalChartBehaviour;
    
    private int indexInParty;

    private void Awake() {
        healthBarBehaviour = healthBar.GetComponent<HealthBarBehaviour>();
        pentagonalChartBehaviour = radarChart.GetComponent<PentagonalChartBehaviour>();
    }

    public void SetStatsParameters(int index) {
        indexInParty = index;

        var member = GameManager.party[index];
        nameText.text = $"{member.CreatureName.ToUpper()}";
        memberImage.sprite = member.SpriteL;
        var c = memberImage.color;
        c.a = 1.0f;
        memberImage.color = c;

        healthBarBehaviour.SetIndexInParty(index);
        pentagonalChartBehaviour.SetRadarChart(index);

        SetButtonInteractability();
    }

    private void SetButtonInteractability() {
        var minorHealActive = inventoryItemsSO.SearchOrdinaryItemByID("00_GoodMushroom").ItemID != "";
        minorHealButton.interactable = minorHealActive;
        var c = minorHealImage.color; c.a = minorHealActive ? 1.0f : 0.6f; minorHealImage.color = c;
        
        var majorHealActive = inventoryItemsSO.SearchOrdinaryItemByID("06_ElectronicScrap").ItemID != "";
        majorHealButton.interactable = majorHealActive;
        c = majorHealImage.color; c.a = majorHealActive ? 1.0f : 0.6f; majorHealImage.color = c;
    }

    public void OnMinorHealButtonClick() {
        if (GameManager.GetPartyMemberHp(indexInParty) >= GameManager.GetPartyMemberMaxHp(indexInParty)) { return; }

        inventoryItemsSO.RemoveTotOrdinaryItem(inventoryItemsSO.SearchOrdinaryItemByID("00_GoodMushroom"), 1);
        GameManager.HealPartyMemberByIndex(indexInParty, GameManager.bonusMultiplier.minorHealingAmount);
        ReSetParametersForAll();
    }
    
    public void OnMajorHealButtonClick() {
        if (GameManager.GetPartyMemberHp(indexInParty) >= GameManager.GetPartyMemberMaxHp(indexInParty)) { return; }
        
        inventoryItemsSO.RemoveTotOrdinaryItem(inventoryItemsSO.SearchOrdinaryItemByID("06_ElectronicScrap"), 1);
        GameManager.HealPartyMemberByIndex(indexInParty, GameManager.bonusMultiplier.majorHealingAmount);
        ReSetParametersForAll();
    }
    
    private void ReSetParametersForAll() {
        foreach (Transform child in gameObject.transform.parent.transform) {
            Debug.Log(child.name); 
            child.GetComponentInChildren<PartyMemberStatsManager>().SetButtonInteractability();
        }
        healthBarBehaviour.ShowHealth();
    }
}
