using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buddy : MonoBehaviour
{
    [SerializeField] public GameObject buddy;
    
    void Start()
    {
        print(buddy.name);
        print(GameManager.buddy1);
        if (buddy.name == "Buddy")
        {
            if (GameManager.buddy1)
            {
                gameObject.SetActive(false);
            }
        }
        else if (buddy.name == "Buddy2")
        {
            if (GameManager.buddy2)
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
