using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleNavigationStatistics : MonoBehaviour, GetNavInputInterface {

    public void GetNavigationInput(Vector2 movement) {
        Debug.Log("In statistics: " + movement.ToString());
    }
    
}
