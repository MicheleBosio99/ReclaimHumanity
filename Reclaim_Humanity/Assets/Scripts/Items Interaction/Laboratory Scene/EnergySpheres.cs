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
    private float maxEnergy;
    private float maxEnergyPerSphere;
    
    // Start is called before the first frame update
    void Awake() {
        maxEnergy = labEnergy.MaxEnergyLab;
        maxEnergyPerSphere = maxEnergy / 5.0f;
        animator = gameObject.GetComponent<Animator>();
        min = (sphereNumber - 1) * maxEnergyPerSphere;
        max = sphereNumber * maxEnergyPerSphere;
    }

    private void Update() {
        int animationToPlay;
        float level = maxEnergyPerSphere / 4.0f;
        if (labEnergy.CurrentEnergy >= max) { animationToPlay = 4; }
        else if (labEnergy.CurrentEnergy < min) { animationToPlay = 0; }
        else { animationToPlay = (int) Math.Floor((labEnergy.CurrentEnergy - min) / level); }

        animator.SetInteger("AnimationToPlay", animationToPlay);
    }
}
