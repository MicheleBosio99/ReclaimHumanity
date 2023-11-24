using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Creature/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea] 
    [SerializeField] private string description;

    [SerializeField] private Type type;
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
    
    public Type Type
    {
        get { return type; }
    }
    
    public int Accuracy
    {
        get { return accuracy; }
    }
    
    public int PP
    {
        get { return pp; }
    }

}
