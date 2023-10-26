using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySpheres : MonoBehaviour {

    [SerializeField] private LabEnergySO labEnergy;
    [SerializeField] private int sphereNumber;
    
    private Animator animator;
    
    // Start is called before the first frame update
    void Awake() {
        animator = gameObject.GetComponent<Animator>();
        int animationToPlay;

        float min = (sphereNumber - 1) * 100.0f;
        float max = sphereNumber * 100.0f;

        if (labEnergy.totalEnergy >= max) { animationToPlay = 4; }
        else if (labEnergy.totalEnergy < min) { animationToPlay = 0; }
        else { animationToPlay = (int) Math.Floor((labEnergy.totalEnergy - min) / 25); }

        Debug.Log("AnimToPlay sphere " + sphereNumber + ": " + animationToPlay);

        animator.SetInteger("AnimationToPlay", animationToPlay);
    }
}
