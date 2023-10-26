using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySpheres : MonoBehaviour {

    [SerializeField] private LabEnergySO labEnergy;
    [SerializeField] private int sphereNumber;
    private Animator animator;
    private float min;
    private float max;
    
    // Start is called before the first frame update
    void Awake() {
        animator = gameObject.GetComponent<Animator>();
        min = (sphereNumber - 1) * 100.0f;
        max = sphereNumber * 100.0f;
    }

    private void Update() {
        int animationToPlay;
        if (labEnergy.totalEnergy >= max) { animationToPlay = 4; }
        else if (labEnergy.totalEnergy < min) { animationToPlay = 0; }
        else { animationToPlay = (int) Math.Floor((labEnergy.totalEnergy - min) / 25); }

        animator.SetInteger("AnimationToPlay", animationToPlay);
    }
}
