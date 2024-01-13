using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] private bool isFriend;

    public Creature Creature { get; set; }

    private SpriteRenderer image;
    private Vector3 originalPos;
    private Animator animator;
    
    public Vector3 OriginalPos
    {
        get { return originalPos; }
    }

    private void Awake()
    {
        image = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        originalPos = image.transform.localPosition;
    }

    public void SetUp(CreatureBase creatureBase, int level, int HP=1000)
    {
        gameObject.SetActive(true);
        Creature = new Creature(creatureBase, level, HP);

        if (Creature.Base.SpecialDefense == 19)  // identify wolves
        {
            transform.localScale = new Vector3(80f, 80f, 80f);
            transform.localPosition += new Vector3(0f, 10f);
            originalPos = image.transform.localPosition;
        }

        animator.runtimeAnimatorController = Creature.Base.AnimatorBattle;
        if (isFriend)
        {
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Speed", 0);
        }
        /*if (isFriend)
        {
            image.sprite = Creature.Base.SpriteL;
        }
        else
        {
            image.sprite = Creature.Base.SpriteR;
        }*/
        
        PlayEnterAnimation();
    }

    public void PlayEnterAnimation()
    {
        if (isFriend)
        {
            image.transform.localPosition = new Vector3(-500f, originalPos.y);
        }
        else
        {
            image.transform.localPosition = new Vector3(500f, originalPos.y);
        }

        image.transform.DOLocalMoveX(originalPos.x, 1f);
    }

    public void PlayAttackAnimation(Vector3 otherPos)
    {
        var sequence = DOTween.Sequence();
        StartCoroutine(AttackAnimation());
        if (isFriend)
        {
            sequence.Append(image.transform.DOLocalMove(
                new Vector3(otherPos.x - 100f, otherPos.y), 0.3f));
        }
        else
        {
            sequence.Append(image.transform.DOLocalMove(
                new Vector3(otherPos.x + 100f, otherPos.y), 0.3f));
        }
        
        sequence.Append(image.transform.DOLocalMove(
            new Vector3(originalPos.x, originalPos.y), 0.3f));
    }

    IEnumerator AttackAnimation()
    {
        animator.SetFloat("Attack", 1);
        yield return new WaitForSeconds(1f);
        animator.SetFloat("Attack", 0);
    }
}