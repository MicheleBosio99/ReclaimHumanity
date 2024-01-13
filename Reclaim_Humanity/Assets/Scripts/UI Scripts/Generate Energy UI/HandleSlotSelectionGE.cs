using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandleSlotSelectionGE : MonoBehaviour , IPointerClickHandler {
    
    private Image slotImage;
    public Tuple<HandleItemsInInventoryGE, int> handlerWithIndex;

    private bool isInventory;

    private void Start() { slotImage = gameObject.GetComponent<Image>(); }

    public void OnPointerClick(PointerEventData eventData) {
        var shift = eventData.button != PointerEventData.InputButton.Left;
        handlerWithIndex.Item1.SlotGotClicked(handlerWithIndex.Item2, isInventory, shift);
    }
    
    public void SaveHandler(HandleItemsInInventoryGE slotsHandler, int index, bool _isInventory) {
        isInventory = _isInventory;
        handlerWithIndex = new Tuple<HandleItemsInInventoryGE, int>(slotsHandler, index);
    }
}
