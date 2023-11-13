using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RecipesSelectionHandler : MonoBehaviour {
    
    [SerializeField] private InventoryItemsSO inventoryItemsSo;
    private List<InventoryItem> itemsInInventory;
    
    [SerializeField] private GameObject slotItemReq1;
    [SerializeField] private GameObject slotItemReq2;
    [SerializeField] private GameObject slotItemResult;
    
    private RecipesItemSlotHandler handlerSlotItemReq1;
    private RecipesItemSlotHandler handlerSlotItemReq2;
    private RecipesItemSlotHandler handlerSlotItemResult;
    
    private Recipe activeRecipe;
    
    private int activeRecipeQuantity;
    
    private bool recipeIsActive;
    
    private int requiredQuantityItem1;
    private int requiredQuantityItem2;
    private int quantityItemResult;
    
    private InventoryItem itemReq1Inventory;
    private InventoryItem itemReq2Inventory;
    private InventoryItem itemResultedInventory;
    
    private Sprite itemResultSprite;
    

    private void Start() {
        handlerSlotItemReq1 = slotItemReq1.GetComponent<RecipesItemSlotHandler>();
        handlerSlotItemReq2 = slotItemReq2.GetComponent<RecipesItemSlotHandler>();
        handlerSlotItemResult = slotItemResult.GetComponent<RecipesItemSlotHandler>();
        
        itemsInInventory = inventoryItemsSo.OrdinaryItemsInInventory;
        
        activeRecipeQuantity = 0;
        recipeIsActive = false;
        
        requiredQuantityItem1 = 0;
        requiredQuantityItem2 = 0;
        quantityItemResult = 0;
    }

    public void RecipeButtonClicked(Recipe recipe) { activeRecipe = recipe; activeRecipeQuantity = 1; UpdateRecipeSlots(); }

    private void UpdateRecipeSlots() {
        if (activeRecipe == null) { ResetRecipeSlots(); return; }

        var itemReq1 = new Item(activeRecipe.item1ID, activeRecipe.item1Name, activeRecipe.item1QuantityReq);
        var itemReq2 = new Item(activeRecipe.item2ID, activeRecipe.item2Name, activeRecipe.item2QuantityReq);
        var itemResult = new Item(activeRecipe.itemResultID, activeRecipe.itemResultName, activeRecipe.itemResultQuantity);
        
        itemReq1.itemSprite = Resources.Load<Sprite>($"ItemsSprites/{itemReq1.itemID}");
        itemReq2.itemSprite = Resources.Load<Sprite>($"ItemsSprites/{itemReq2.itemID}");
        itemResultSprite = itemResult.itemSprite = Resources.Load<Sprite>($"ItemsSprites/{itemResult.itemID}");
        
        requiredQuantityItem1 = itemReq1.itemQuantity * activeRecipeQuantity;
        requiredQuantityItem2 = itemReq2.itemQuantity * activeRecipeQuantity;
        quantityItemResult = itemResult.itemQuantity * activeRecipeQuantity;
        
        // Check if itemReq1 and itemReq2 are available in inventory with right amounts;
        // ItemReq1:
        itemReq1Inventory = inventoryItemsSo.SearchOrdinaryItemByID(itemReq1.itemID);
        var itemReq1Enough = itemReq1Inventory.ItemQuantity >= requiredQuantityItem1;
        // ItemReq2:
        itemReq2Inventory = inventoryItemsSo.SearchOrdinaryItemByID(itemReq2.itemID);
        var itemReq2Enough = itemReq2Inventory.ItemQuantity >= requiredQuantityItem2;
        
        // Set sprites, 40 % visible if not possible to craft them;
        handlerSlotItemReq1.SetImageWithTransparency(itemReq1.itemSprite, itemReq1Enough ? 1.0f : 0.4f);
        handlerSlotItemReq2.SetImageWithTransparency(itemReq2.itemSprite, itemReq2Enough ? 1.0f : 0.4f);
        handlerSlotItemResult.SetImageWithTransparency(itemResult.itemSprite, 
            itemReq1Enough & itemReq2Enough ? 1.0f : 0.4f);
        
        // Set quantities required;
        handlerSlotItemReq1.SetQuantityText(requiredQuantityItem1);
        handlerSlotItemReq2.SetQuantityText(requiredQuantityItem2);
        handlerSlotItemResult.SetQuantityText(quantityItemResult);
        
        recipeIsActive =  itemReq1Enough & itemReq2Enough;
    }

    private void ResetRecipeSlots() {
        activeRecipeQuantity = 0;
        recipeIsActive = false;
        
        requiredQuantityItem1 = 0;
        requiredQuantityItem2 = 0;
        quantityItemResult = 0;
        
        itemReq1Inventory = InventoryItem.GetEmptyItem();
        itemReq2Inventory = InventoryItem.GetEmptyItem();
        
        handlerSlotItemReq1.SetImageWithTransparency(null, 0.0f);
        handlerSlotItemReq2.SetImageWithTransparency(null, 0.0f);
        handlerSlotItemResult.SetImageWithTransparency(null, 0.0f);
        
        handlerSlotItemReq1.SetQuantityText(0);
        handlerSlotItemReq2.SetQuantityText(0);
        handlerSlotItemResult.SetQuantityText(0);
    }


    public void OnPlusQuantityClick() {
        activeRecipeQuantity = activeRecipeQuantity == 0 ? 0 : activeRecipeQuantity ++;
        UpdateRecipeSlots();
    }

    public void OnMinusQuantityClick() {
        activeRecipeQuantity = activeRecipeQuantity <= 0 ? 0 : (activeRecipeQuantity == 1 ? 1 : activeRecipeQuantity --);
        UpdateRecipeSlots();
    }

    public void OnCraftButtonClick() {
        if (!recipeIsActive) { return; }

        if (inventoryItemsSo.OrdinaryItemsInInventory.Count == inventoryItemsSo.MaxOrdinarySlots) {
            Debug.Log("Handle Message Full Inventory");
            return;
        }

        itemReq1Inventory.ItemQuantity -= requiredQuantityItem1;
        Assert.IsTrue(itemReq1Inventory.ItemQuantity >= 0);
        // Check for itemQuantity in inventorySlotSO == 0, because there is need to handle delete of objects with 0 quantity;
        itemReq2Inventory.ItemQuantity -= requiredQuantityItem2;
        Assert.IsTrue(itemReq2Inventory.ItemQuantity >= 0);
        
        itemResultedInventory = inventoryItemsSo.SearchOrdinaryItemByID(activeRecipe.itemResultID);
        if (itemResultedInventory.ItemQuantity > 0) { itemResultedInventory.ItemQuantity += quantityItemResult; }
        else {
            // inventoryItemsSo.AddOrdinaryItemToInventory(new InventoryItem(activeRecipe.itemResultID, itemResultSprite,
            //     quantityItemResult, "", 0.0f, false));
            // ERRORRRRRRR TODO: make it so that it gets from resources a scriptable object with all the information about the object already coded inside;
        }

    }


    private class Item {
        public string itemID;
        public string itemName;
        public int itemQuantity;
        
        public Sprite itemSprite;

        public Item(string itemID, string itemName, int itemQuantity) {
            this.itemID = itemID;
            this.itemName = itemName;
            this.itemQuantity = itemQuantity;
        }
    }
}
