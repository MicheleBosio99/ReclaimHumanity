using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleNavigationMap : MonoBehaviour, GetNavInputInterface {

    public void GetNavigationInput(Vector2 movement) {
        Debug.Log("In map: " + movement.ToString());
    }
   
}
