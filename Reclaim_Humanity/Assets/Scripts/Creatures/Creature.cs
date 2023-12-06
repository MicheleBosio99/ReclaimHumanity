using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature
{
    public CreatureBase Base { get; set; }
    public int Level { get; set; }
    
    public int HP { get; set; }
    
    public List<Move> Moves { get; set; }

    public Creature(CreatureBase cbase, int clevel)
    {
        Base = cbase;
        Level = clevel;
        HP = MaxHp;
        
        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves) 
        {
            if (move.Level <= Level)
            {
                Moves.Add(new Move(move.Base));
            }

            if (Moves.Count >= 4)
            {
                break;
            }
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }
    
    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }
    
    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpecialAttack * Level) / 100f) + 5; }
    }
    
    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpecialDefense * Level) / 100f) + 5; }
    }
    
    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }
    
    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }

    public DamageDetails TakeDamage(Move move, Creature attacker)
    {
        float critical = 1f;
        if (UnityEngine.Random.value * 100f <= 6.25f)
        {
            critical = 2f;
        }
        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * 
                     TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);

        var damageDetails = new DamageDetails()
        {
            TypeEffectiveness = type,
            Critical = critical,
            Fainted = false
        };
        
        float modifiers = UnityEngine.Random.Range(0.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            damageDetails.Fainted = true;
        }

        return damageDetails;
    }

    public Move GetRandomMove()
    {
        int r = UnityEngine.Random.Range(0, Moves.Count);
        return Moves[r];
    }
}

public class DamageDetails
{
    public bool Fainted { get; set; }
    public float Critical { get; set; }
    public float TypeEffectiveness { get; set; }
}
