using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : ScriptableObject {
    
    [SerializeField] private string _name;
    [SerializeField] private int energyDeveloped;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private string description;
    
}