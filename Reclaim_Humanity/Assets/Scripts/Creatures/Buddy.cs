using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buddy : MonoBehaviour
{
    [SerializeField] public GameObject buddy;
    
    void Start()
    {
        if (buddy.name == "Buddy")
        {
            if (GameManager.buddy1)
            { 
                Destroy(gameObject);
            }
        }
        else if (buddy.name == "Buddy2")
        {
            if (GameManager.buddy2)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
