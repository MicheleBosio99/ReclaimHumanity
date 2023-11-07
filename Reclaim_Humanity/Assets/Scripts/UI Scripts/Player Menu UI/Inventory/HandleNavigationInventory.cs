using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Vector2 = UnityEngine.Vector2;

public class HandleNavigationInventory : MonoBehaviour {

    [SerializeField] private GameObject InventoryPanel;
    private HandleSlotSelection selectionHandler;
    private HandleItemsInInventory inventoryHandler;
    
    private SlotNavigationGraph slotGraph;

    private void Awake() {
        inventoryHandler = InventoryPanel.GetComponent<HandleItemsInInventory>();

        var ordinarySlots = inventoryHandler.OrdinaryInventorySlotsUI;
        var specialSlots = inventoryHandler.SpecialInventorySlotsUI;

        List<GameObject> slotsOrdered = new List<GameObject>();
        
        // Creates list with all slots
        for (var i = 1; i <= 15; i++) {
            if (i % 5 == 0) { slotsOrdered.Add(ordinarySlots[i - 1]); slotsOrdered.Add(specialSlots[(i - 1) / 5]); }
            else { slotsOrdered.Add(ordinarySlots[i - 1]); }
        }
        
        slotGraph = new SlotNavigationGraph();
        slotGraph.AddNodes(slotsOrdered, 3, 6);
    }
    
    private void ChangeSelectedSlot(Vector2 movement) {
        inventoryHandler.CurrentSelectedSlot.GetComponent<HandleSlotSelection>().ExitHover();
        inventoryHandler.CurrentSelectedSlot = slotGraph.GetNeighborWithDirection(inventoryHandler.CurrentSelectedSlot, movement);
        inventoryHandler.CurrentSelectedSlot.GetComponent<HandleSlotSelection>().EnterHover();
    }

    private Queue<Vector2> movementQueue = new Queue<Vector2>();

    public void GetNavigationInput(Vector2 movement) { movementQueue.Enqueue(movement); }

    private void Update() {
        if (movementQueue.TryDequeue(out var movement)) {
            if (movement.x == 0.0f || movement.y == 0.0f) { ChangeSelectedSlot(movement); }
        }
    }
}

public class SlotNavigationNode {

    private Dictionary<Vector2, GameObject> inventorySlotNeighbors = new();

    public Dictionary<Vector2, GameObject> InventorySlotNeighbors {
        get => inventorySlotNeighbors;
        set => inventorySlotNeighbors = value;
    }

    // Assertion
    public void AddNeighbor(Vector2 direction, GameObject slot) { Assert.IsTrue(inventorySlotNeighbors.TryAdd(direction, slot)); }

    public GameObject GetNeighbor(Vector2 direction) { return inventorySlotNeighbors[direction]; }

    public override string ToString() {
        return $"Up: {inventorySlotNeighbors[Vector2.up]}, Down: {inventorySlotNeighbors[Vector2.down]}," +
               $"Left: {inventorySlotNeighbors[Vector2.left]}, Right: {inventorySlotNeighbors[Vector2.right]};\n";
    }
}

public class SlotNavigationGraph {

    private Dictionary<GameObject, SlotNavigationNode> inventorySlotGraph = new();

    public void AddNodes(List<GameObject> slotList, int numOfRows, int numOfColumns) {

        for(var i = 0; i < numOfRows; i ++) {
            for (var j = 0; j < numOfColumns; j ++) {

                var index = j + i * numOfColumns;
                if (!inventorySlotGraph.ContainsKey(slotList[index])) {

                    var slotNN = new SlotNavigationNode();
                    var k = i; var h = j;

                    // Add Slot Above
                    if (k == 0) { k = numOfRows - 1; }
                    else { k --; }
                    var slot = slotList[h + k * numOfColumns];
                    slotNN.AddNeighbor(Vector2.up, slot);
                    
                    k = i; h = j;
                    
                    // Add Slot Below
                    if (k == numOfRows - 1) { k = 0; }
                    else { k ++; }
                    slot = slotList[h + k * numOfColumns];
                    slotNN.AddNeighbor(Vector2.down, slot);
                    
                    k = i; h = j;
                    
                    // Add Slot Left
                    if (h == 0) { h = numOfColumns - 1; }
                    else { h --; }
                    slot = slotList[h + k * numOfColumns];
                    slotNN.AddNeighbor(Vector2.left, slot);
                    
                    k = i; h = j;
                    
                    // Add Slot Right
                    if (h == numOfColumns - 1) { h = 0; }
                    else { h ++; }
                    slot = slotList[h + k * numOfColumns];
                    slotNN.AddNeighbor(Vector2.right, slot);

                    inventorySlotGraph[slotList[index]] = slotNN;
                }
            }
        }
    }

    public GameObject GetNeighborWithDirection(GameObject slot, Vector2 direction) { return inventorySlotGraph[slot].GetNeighbor(direction); }
    
    
}
