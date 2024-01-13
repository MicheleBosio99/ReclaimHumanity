using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/Create new creature")]
public class CreatureBase : ScriptableObject
{
    [FormerlySerializedAs("name")] [SerializeField] private string creatureName;

    [TextArea] [SerializeField] private string description;

    [SerializeField] private Sprite spriteL;
    [SerializeField] private Sprite spriteR;

    [SerializeField] private RuntimeAnimatorController animatorBattle;

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

    public string CreatureName
    {
        get { return creatureName; }
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
    
    public RuntimeAnimatorController AnimatorBattle
    {
        get { return animatorBattle; }
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
    Grass,
    Electric
}

public class TypeChart
{
    static float[][] chart =
    {
        //                    NOR   FIR   WAT   GRS   ELE
        /*NOR*/ new float[] { 1f,   1f,   1f,   1f,   1f   },
        /*FIR*/ new float[] { 1f,   0.5f, 0.5f, 2f,   1f   },
        /*WAT*/ new float[] { 1f,   2f,   0.5f, 0.5f, 1f   },
        /*GRS*/ new float[] { 1f,   0.5f, 2f,   0.5f, 1f   },
        /*ELE*/ new float[] { 1f,   1f,   2f,   0.5f, 0.5f }
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
