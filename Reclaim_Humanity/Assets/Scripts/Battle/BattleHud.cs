using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private HPBar hpBar;

    public void SetData(Creature creature)
    {
        gameObject.SetActive(true);
        nameText.text = creature.Base.Name;
        levelText.text = "Lvl " + creature.Level;
        hpBar.SetHP((float) creature.HP / creature.MaxHp);
    }
    
}
