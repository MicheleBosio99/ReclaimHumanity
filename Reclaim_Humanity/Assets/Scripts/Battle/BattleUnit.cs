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

    private Image image;
    private Vector3 originalPos;
    
    public Vector3 OriginalPos
    {
        get { return originalPos; }
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        originalPos = image.transform.localPosition;
    }

    public void SetUp(CreatureBase creatureBase, int level, int HP=1000)
    {
        gameObject.SetActive(true);
        Creature = new Creature(creatureBase, level, HP);
        if (isFriend)
        {
            image.sprite = Creature.Base.SpriteL;
        }
        else
        {
            image.sprite = Creature.Base.SpriteR;
        }

        if (Creature.Base.Type1 == CreatureType.Fire)
        {
            GetComponent<Image>().color = Color.red;
        }
        
        if (Creature.Base.Type1 == CreatureType.Grass)
        {
            GetComponent<Image>().color = Color.green;
        }
        
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
    
    public void FlashOnHit()
    {
        // Start the coroutine to perform the flash animation
        StartCoroutine(FlashAnimation());
    }
    
    public void FlashOnHit2()
    {
        // Start the coroutine to perform the flash animation
        StartCoroutine(FlashAnimation2());
    }

    // Coroutine to perform the flash animation
    IEnumerator FlashAnimation()
    {
        Color originalColor = GetComponent<Image>().color;
        GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Image>().color = originalColor;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Image>().color = originalColor;
    }
    
    IEnumerator FlashAnimation2()
    {
        Color originalColor = GetComponent<Image>().color;
        GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Image>().color = originalColor;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Image>().color = originalColor;
    }
}