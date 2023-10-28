using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBatteryShock : MonoBehaviour {
    private Animator animator;
    [SerializeField] private float lightningPercent = 0.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (Random.Range(0.0f, 1.0f) < lightningPercent) { StartCoroutine(LightningAnimationToFalse()); }
    }

    private IEnumerator LightningAnimationToFalse() {
        animator.SetBool("PlayLightningAnimation", true);
        yield return new WaitForSeconds(0.22f);
        animator.SetBool("PlayLightningAnimation", false);
    }
}
