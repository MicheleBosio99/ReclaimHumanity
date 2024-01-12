using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ItemsScriptableObject", menuName = "ScriptableObjects/ItemsSO")]
public class ItemsSO : ScriptableObject {

    [SerializeField] private string itemID;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private float itemEnergyGenerated;
    [SerializeField] private string itemDescription;
    [SerializeField] private bool isItemSpecial;
    [SerializeField] private bool onCombat;
    [SerializeField] private string attributeBoosted;
    [SerializeField] private int boost;
    [SerializeField] private string itemEffect;

    public string ItemID {
        get => itemID;
        set => itemID = value;
    }

    public string ItemName {
        get => itemName;
        set => itemName = value;
    }

    public Sprite ItemSprite {
        get => itemSprite;
        set => itemSprite = value;
    }

    public float ItemEnergyGenerated {
        get => itemEnergyGenerated;
        set => itemEnergyGenerated = value;
    }

    public string ItemDescription {
        get => itemDescription;
        set => itemDescription = value;
    }
    
    public bool IsItemSpecial {
        get => isItemSpecial;
        set => isItemSpecial = value;
    }
    
    public bool OnCombat {
        get => onCombat;
        set => onCombat = value;
    }
    
    public string AttributeBoosted {
        get => attributeBoosted;
        set => attributeBoosted = value;
    }
    
    public int Boost {
        get => boost;
        set => boost = value;
    }
    
    public string ItemEffect {
        get => itemEffect;
        set => itemEffect = value;
    }

    public InventoryItem ToInventoryItem(int quantity) {
        return new InventoryItem(itemID, itemName, itemSprite, quantity, itemDescription, 
            itemEnergyGenerated, isItemSpecial, onCombat, attributeBoosted, boost, itemEffect);
    }
}
