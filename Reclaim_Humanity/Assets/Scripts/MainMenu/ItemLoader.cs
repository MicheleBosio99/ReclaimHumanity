using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLoader : MonoBehaviour
{
    public static ItemLoader instance;
    [SerializeField] public List<ItemsSO> ItemsSos;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(transform.root.gameObject);
            return; 
        }
    }
}
