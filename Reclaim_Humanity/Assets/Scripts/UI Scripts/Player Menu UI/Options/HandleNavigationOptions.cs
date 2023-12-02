using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleNavigationOptions : MonoBehaviour, GetNavInputInterface {

    public void GetNavigationInput(Vector2 movement) {
        Debug.Log("In options: " + movement.ToString());
    }
}
