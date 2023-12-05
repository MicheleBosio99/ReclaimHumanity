using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Move", menuName = "Creature/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea] 
    [SerializeField] private string description;

    [FormerlySerializedAs("type")] [SerializeField] private CreatureType type;
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int pp;

    public string Name
    {
        get { return name; }
    }
    
    public string Description
    {
        get { return description; }
    }
    
    public CreatureType Type
    {
        get { return type; }
    }
    
    public int Accuracy
    {
        get { return accuracy; }
    }
    
    public int Power
    {
        get { return power; }
    }
    
    public int PP
    {
        get { return pp; }
    }

}
