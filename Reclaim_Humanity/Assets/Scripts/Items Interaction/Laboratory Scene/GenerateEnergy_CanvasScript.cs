using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnergyCanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject inventory;
    private bool stopPlayer;

    private void Start() {
        gameObject.SetActive(false);
    }
}
