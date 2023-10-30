using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItems : ScriptableObject {
   private Dictionary<string, int> itemsInInventory = new Dictionary<string, int>();
   
   public Dictionary<string, int> GetItemsInInventory () { return itemsInInventory; }

   public void AddItemToInventory(string item) {
      itemsInInventory.TryGetValue(item, out int count);
      itemsInInventory[item] = count + 1;
   }

   public List<string> GetItemsAsList() { return itemsInInventory.Keys.ToList(); }

}
