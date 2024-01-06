using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private HPBar hpBar;
    
    [SerializeField] private bool isFriend;
    private Image image;
    private Vector3 originalPos;

    private Creature _creature;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
    }

    public void SetData(Creature creature)
    {
        _creature = creature;
        
        gameObject.SetActive(true);
        nameText.text = creature.Base.Name;
        levelText.text = "Lvl " + creature.Level;
        hpBar.SetHP((float) creature.HP / creature.MaxHp);
        
        PlayEnterAnimation();
    }
    
    public void PlayEnterAnimation()
    {
        if (isFriend)
        {
            image.transform.localPosition = new Vector3(-800f, originalPos.y);
        }
        else
        {
            image.transform.localPosition = new Vector3(800f, originalPos.y);
        }

        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public IEnumerator UpdateHP()
    {
        yield return hpBar.SetHPSmooth((float)_creature.HP / _creature.MaxHp);
    }
}
