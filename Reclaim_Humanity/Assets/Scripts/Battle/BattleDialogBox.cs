using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] private int lettersPerSecond;
    [SerializeField] private Color highlitedColor;
    
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private TextMeshProUGUI curiosityText;
    [SerializeField] private GameObject actionSelector;
    [SerializeField] private GameObject moveSelector;
    [SerializeField] private GameObject moveDetails;
    [SerializeField] private GameObject itemSelector;
    [SerializeField] private GameObject itemDetails;

    [SerializeField] private List<TextMeshProUGUI> actionTexts;
    [SerializeField] private List<TextMeshProUGUI> moveTexts;
    [SerializeField] private List<TextMeshProUGUI> itemTexts;
    
    
    [SerializeField] private TextMeshProUGUI moveDescription;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }
    
    public IEnumerator TypeCuriosity(string dialog)
    {
        curiosityText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            curiosityText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }
    
    public void EnableCuriosityText(bool enabled)
    {
        curiosityText.enabled = enabled;
    }
    
    public void EnableActionSelector(bool enabled)
    {
        actionSelector.SetActive(enabled);
    }
    
    public void EnableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
    }
    
    public void EnableItemSelector(bool enabled)
    {
        itemSelector.SetActive(enabled);
        itemDetails.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction)
    {
        for (int i = 0; i < actionTexts.Count; i++)
        {
            if (i == selectedAction)
            {
                actionTexts[i].color = highlitedColor;
            }
            else
            {
                actionTexts[i].color = Color.black;
            }
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i == selectedMove)
            {
                moveTexts[i].color = highlitedColor;
            }
            else
            {
                moveTexts[i].color = Color.black;
            }
        }

        moveDescription.text = move.Base.Description;
        typeText.text = move.Base.Type.ToString();
    }
    
    public void UpdateItemSelection(int selectedItem, InventoryItem item)
    {
        for (int i = 0; i < itemTexts.Count; i++)
        {
            if (i == selectedItem)
            {
                itemTexts[i].color = highlitedColor;
            }
            else
            {
                itemTexts[i].color = Color.black;
            }
        }

        itemDescription.text = item.Description;
    }

    public void SetMoveNames(List<Move> moves)
    {
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i < moves.Count)
            {
                moveTexts[i].text = moves[i].Base.Name;
            }
            else
            {
                moveTexts[i].text = "-";
            }
        }
    }
    
    public void SetItemNames(List<InventoryItem> items)
    {
        for (int i = 0; i < itemTexts.Count; i++)
        {
            if (i < items.Count)
            {
                itemTexts[i].text = items[i].ItemName + " (x" + items[i].ItemQuantity + ")";
            }
            else
            {
                itemTexts[i].text = "-";
            }
        }
    }
}
