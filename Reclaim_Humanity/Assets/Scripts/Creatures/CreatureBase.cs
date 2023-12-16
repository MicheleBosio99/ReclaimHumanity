using System;
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

    [SerializeField] private CreatureType type1;
    [SerializeField] private CreatureType type2;
    
    // Base stats
    [SerializeField] private int maxHp;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private int specialAttack;
    [SerializeField] private int specialDefense;
    [SerializeField] private int speed;

    [SerializeField] private List<LearnableMove> learnableMoves;
    
    [SerializeField] private List<ItemsSO> droppableObjects;

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
    
    public CreatureType Type1
    {
        get { return type1; }
    }
    
    public CreatureType Type2
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

    public List<ItemsSO> DroppableObjects {
        get { return droppableObjects; }
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

public enum CreatureType
{
    None,
    Normal,
    Fire,
    Water,
    Grass
}

public class TypeChart
{
    static float[][] chart =
    {
        //                    NOR   FIR   WAT   GRS
        /*NOR*/ new float[] { 1f,   1f,   1f,   1f   },
        /*FIR*/ new float[] { 1f,   0.5f, 0.5f, 2f   },
        /*WAT*/ new float[] { 1f,   2f,   0.5f, 0.5f },
        /*GRS*/ new float[] { 1f,   0.5f, 2f,   0.5f }
    };

    public static float GetEffectiveness(CreatureType attackType, CreatureType defenseType)
    {
        if (attackType == CreatureType.None || defenseType == CreatureType.None)
        {
            return 1;
        }

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}
