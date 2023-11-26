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

    private Creature _creature;

    public void SetData(Creature creature)
    {
        _creature = creature;
        
        gameObject.SetActive(true);
        nameText.text = creature.Base.Name;
        levelText.text = "Lvl " + creature.Level;
        hpBar.SetHP((float) creature.HP / creature.MaxHp);
    }

    public IEnumerator UpdateHP()
    {
        yield return hpBar.SetHPSmooth((float)_creature.HP / _creature.MaxHp);
    }
}
