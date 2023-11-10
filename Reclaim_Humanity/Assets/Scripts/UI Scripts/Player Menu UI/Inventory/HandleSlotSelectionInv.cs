using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandleSlotSelectionInv : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    
    private Vector3 normalSlotScale;
    private Color normalSlotColor;
    [SerializeField] private Vector3 selectedSlotScale = new Vector3(1.4f, 1.4f, 1.0f);
    [SerializeField] private Color selectedSlotColor = Color.white;
    
    
    private Image slotImage;
    public Tuple<HandleItemsInInventoryInv, int> handlerWithIndex;

    private bool isSlotSpecial;

    private void Start() {
        slotImage = gameObject.GetComponent<Image>();

        normalSlotColor = slotImage.color;
        normalSlotScale = gameObject.transform.localScale;
    }

    public void EnterHover() {
        handlerWithIndex.Item1.SetCurrentSelectedSlotByIndex(handlerWithIndex.Item2, isSlotSpecial);
        handlerWithIndex.Item1.ShowDescriptionSelectedObject(handlerWithIndex.Item2, isSlotSpecial);
        gameObject.transform.localScale = selectedSlotScale;
        slotImage.color = selectedSlotColor;
    }

    public void ExitHover() {
        handlerWithIndex.Item1.UnShowDescriptionSelectedObject();
        gameObject.transform.localScale = normalSlotScale;
        slotImage.color = normalSlotColor;
    }

    public void OnPointerEnter(PointerEventData eventData) { EnterHover(); }

    public void OnPointerExit(PointerEventData eventData) { ExitHover(); }

    public void SaveHandler(HandleItemsInInventoryInv slotsHandler, int index, bool isSpecial) {
        isSlotSpecial = isSpecial;
        handlerWithIndex = new Tuple<HandleItemsInInventoryInv, int>(slotsHandler, index);
    }
}



// public void OnPointerClick(PointerEventData eventData) {
//     return; // NOT USED IN INVENTORY
//     //isSelected = !isSelected;
//     //if (isSelected) { ShowSelected(); }
//     //else { ShowUnselected(); }
//     
//     //handlerWithIndex.Item1.SlotIsSelected(handlerWithIndex.Item2, isSlotSpecial);
// }


// public void DeselectSlot() { isSelected = false; ShowUnselected(); }
