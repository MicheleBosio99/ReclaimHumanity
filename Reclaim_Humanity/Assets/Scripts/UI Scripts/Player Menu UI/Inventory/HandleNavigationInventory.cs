using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleNavigationInventory : MonoBehaviour, GetNavInputInterface {

    // [SerializeField] private 
    
    public void GetNavigationInput(Vector2 movement) {
        Debug.Log("In inventory: " + movement.ToString());
    }


}
