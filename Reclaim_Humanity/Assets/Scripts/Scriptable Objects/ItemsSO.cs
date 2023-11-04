using UnityEngine;

[CreateAssetMenu(fileName = "ItemsScriptableObject", menuName = "ScriptableObjects/ItemsSO")]
public class Items : ScriptableObject {

    [SerializeField] private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private int itemEnergy;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string itemDescription;
    [SerializeField] private int quantity;

    public int ItemID {
        get => itemID;
        set => itemID = value;
    }

    public string ItemName {
        get => itemName;
        set => itemName = value;
    }

    public int ItemEnergy {
        get => itemEnergy;
        set => itemEnergy = value;
    }

    public Sprite ItemSprite {
        get => itemSprite;
        set => itemSprite = value;
    }

    public string ItemDescription {
        get => itemDescription;
        set => itemDescription = value;
    }

    public int Quantity {
        get => quantity;
        set => quantity = value;
    }
}
