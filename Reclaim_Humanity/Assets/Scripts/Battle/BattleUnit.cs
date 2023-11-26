using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] private bool isFriend;

    public Creature Creature { get; set; }

    public void SetUp(CreatureBase creatureBase, int level)
    {
        gameObject.SetActive(true);
        Creature = new Creature(creatureBase, level);
        if (isFriend)
        {
            GetComponent<Image>().sprite = Creature.Base.SpriteL;
        }
        else
        {
            GetComponent<Image>().sprite = Creature.Base.SpriteR;
        }

    }
}