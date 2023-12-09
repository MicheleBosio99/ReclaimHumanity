using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] private bool isFriend;

    public Creature Creature { get; set; }

    public void SetUp(CreatureBase creatureBase, int level, int HP=1000)
    {
        gameObject.SetActive(true);
        Creature = new Creature(creatureBase, level, HP);
        if (isFriend)
        {
            GetComponent<Image>().sprite = Creature.Base.SpriteL;
        }
        else
        {
            GetComponent<Image>().sprite = Creature.Base.SpriteR;
        }

        if (Creature.Base.Type1 == CreatureType.Fire)
        {
            GetComponent<Image>().color = Color.red;
        }
        
        if (Creature.Base.Type1 == CreatureType.Grass)
        {
            GetComponent<Image>().color = Color.green;
        }
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