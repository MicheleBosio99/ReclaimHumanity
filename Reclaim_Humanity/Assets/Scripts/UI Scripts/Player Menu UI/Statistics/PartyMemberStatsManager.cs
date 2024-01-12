using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PartyMemberStatsManager : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image memberImage;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject radarChart;
    [SerializeField] private Button minorCureButton;
    [SerializeField] private Button majorCureButton;
    
    [SerializeField] private InventoryItemsSO inventoryItemsSO;
    
    private HealthBarBehaviour healthBarBehaviour;
    private PentagonalChartBehaviour pentagonalChartBehaviour;

    private void Awake() {
        healthBarBehaviour = healthBar.GetComponent<HealthBarBehaviour>();
        pentagonalChartBehaviour = radarChart.GetComponent<PentagonalChartBehaviour>();
    }

    public void SetStatsParameters(int index) {
        var member = GameManager.party[index];
        nameText.text = member.CreatureName.ToUpper();
        memberImage.sprite = member.SpriteL;
        var c = memberImage.color; c.a = 1.0f; memberImage.color = c;
        
        healthBarBehaviour.SetIndexInParty(index);
        pentagonalChartBehaviour.SetRadarChart(index);
        
        minorCureButton.interactable = inventoryItemsSO.SearchOrdinaryItemByID("00_GoodMushroom").ItemID != "";
        majorCureButton.interactable = inventoryItemsSO.SearchOrdinaryItemByID("06_ElectronicScrap").ItemID != "";
    }
}
