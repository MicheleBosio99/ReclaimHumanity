using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/Create new creature")]
public class CreatureBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea] [SerializeField] private string description;

    [SerializeField] private Sprite spriteL;
    [SerializeField] private Sprite spriteR;

    [SerializeField] private Type type1;
    [SerializeField] private Type type2;
    
    // Base stats
    [SerializeField] private int maxHp;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private int specialAttack;
    [SerializeField] private int specialDefense;
    [SerializeField] private int speed;

    [SerializeField] private List<LearnableMove> learnableMoves;

    public string Name
    {
        get { return name; }
    }
    
    public string Description
    {
        get { return description; }
    }
    
    public Sprite SpriteL
    {
        get { return spriteL; }
    }
    
    public Sprite SpriteR
    {
        get { return spriteR; }
    }
    
    public Type Type1
    {
        get { return type1; }
    }
    
    public Type Type2
    {
        get { return type2; }
    }
    
    public int MaxHp
    {
        get { return maxHp; }
    }
    
    public int Attack
    {
        get { return attack; }
    }
    
    public int Defense
    {
        get { return defense; }
    }
    
    public int SpecialAttack
    {
        get { return specialAttack; }
    }
    
    public int SpecialDefense
    {
        get { return specialDefense; }
    }
    
    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] private MoveBase moveBase;
    [SerializeField] private int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

public enum Type
{
    Normal,
    Fire,
    Water
}
