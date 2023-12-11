using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RecipesSelectionHandler : MonoBehaviour {
    
    private List<GameObject> recipesButtons;
    
    [SerializeField] private InventoryItemsSO inventoryItemsSo;
    [SerializeField] private GameObject notEnoughSpacePanel;
    
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
    
    private ItemsSO item1SO;
    private ItemsSO item2SO;
    private ItemsSO itemResSO;
    

    private void Start() {
        handlerSlotItemReq1 = slotItemReq1.GetComponent<RecipesItemSlotHandler>();
        handlerSlotItemReq2 = slotItemReq2.GetComponent<RecipesItemSlotHandler>();
        handlerSlotItemResult = slotItemResult.GetComponent<RecipesItemSlotHandler>();
        
        activeRecipeQuantity = 0;
        recipeIsActive = false;
        
        requiredQuantityItem1 = 0;
        requiredQuantityItem2 = 0;
        quantityItemResult = 0;
        
        notEnoughSpacePanel.SetActive(false);
    }

    public void RecipeButtonClicked(Recipe recipe) { activeRecipe = recipe; activeRecipeQuantity = 1; UpdateRecipeSlots(); }

    private void UpdateRecipeSlots() {
        if (activeRecipe == null) { ResetRecipeSlots(); return; }
        
        item1SO = Resources.Load<ItemsSO>($"Items/{activeRecipe.item1ID}");
        item2SO = Resources.Load<ItemsSO>($"Items/{activeRecipe.item2ID}");
        itemResSO = Resources.Load<ItemsSO>($"Items/{activeRecipe.itemResultID}");
        
        requiredQuantityItem1 = activeRecipe.item1QuantityReq * activeRecipeQuantity;
        requiredQuantityItem2 = activeRecipe.item2QuantityReq * activeRecipeQuantity;
        quantityItemResult = activeRecipe.itemResultQuantity * activeRecipeQuantity;
        
        // Check if itemReq1 and itemReq2 are available in inventory with right amounts;
        // ItemReq1:
        itemReq1Inventory = inventoryItemsSo.SearchOrdinaryItemByID(item1SO.ItemID);
        var itemReq1Enough = itemReq1Inventory.ItemQuantity >= requiredQuantityItem1;
        // ItemReq2:
        itemReq2Inventory = inventoryItemsSo.SearchOrdinaryItemByID(item2SO.ItemID);
        var itemReq2Enough = itemReq2Inventory.ItemQuantity >= requiredQuantityItem2;
        
        // Set sprites, 40 % visible if not possible to craft them;
        handlerSlotItemReq1.SetImageWithTransparency(item1SO.ItemSprite, itemReq1Enough ? 1.0f : 0.4f);
        handlerSlotItemReq2.SetImageWithTransparency(item2SO.ItemSprite, itemReq2Enough ? 1.0f : 0.4f);
        handlerSlotItemResult.SetImageWithTransparency(itemResSO.ItemSprite,
            itemReq1Enough & itemReq2Enough ? 1.0f : 0.4f);
        
        // Set quantities required;
        handlerSlotItemReq1.SetQuantityText(requiredQuantityItem1);
        handlerSlotItemReq2.SetQuantityText(requiredQuantityItem2);
        handlerSlotItemResult.SetQuantityText(quantityItemResult);
        
        recipeIsActive =  itemReq1Enough & itemReq2Enough;
    }

    public void ResetRecipeSlots() {
        if (handlerSlotItemReq1 == null || handlerSlotItemReq2 == null || handlerSlotItemResult == null) { return; }

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
        activeRecipeQuantity = activeRecipeQuantity == 0 ? 0 : activeRecipeQuantity + 1;
        UpdateRecipeSlots();
    }

    public void OnMinusQuantityClick() {
        activeRecipeQuantity = activeRecipeQuantity <= 0 ? 0 : (activeRecipeQuantity == 1 ? 1 : activeRecipeQuantity - 1);
        UpdateRecipeSlots();
    }

    public void OnCraftButtonClick() {
        if (!recipeIsActive) { return; }

        if (inventoryItemsSo.OrdinaryItemsInInventory.Count == inventoryItemsSo.MaxOrdinarySlots) {
            StartCoroutine(ShowNotEnoughSpacePanel()); return;
        }

        try {
            inventoryItemsSo.ChangeQuantityOrdinaryItem(itemReq1Inventory, requiredQuantityItem1);
            inventoryItemsSo.ChangeQuantityOrdinaryItem(itemReq2Inventory, requiredQuantityItem2);
        }
        catch (NotEnoughItemsInInventoryException e) { Debug.Log(e); return; }
        

        itemResultedInventory = inventoryItemsSo.SearchOrdinaryItemByID(activeRecipe.itemResultID);
        if (itemResultedInventory.ItemQuantity > 0) { itemResultedInventory.ItemQuantity += quantityItemResult; }
        else {
            var invItem = itemResSO.ToInventoryItem(quantityItemResult);
            inventoryItemsSo.AddOrdinaryItemToInventory(invItem);
        }
        
        ResetRecipeSlots();
    }

    private IEnumerator ShowNotEnoughSpacePanel() {
        notEnoughSpacePanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        notEnoughSpacePanel.SetActive(false);
    }
}
