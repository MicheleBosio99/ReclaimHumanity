using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySpheres : MonoBehaviour {

    [SerializeField] private GameObject labEnergySOSetter;
    [SerializeField] private int sphereNumber;
    
    private Animator animator;
    private float min;
    private float max;
    private float maxEnergy;
    private float maxEnergyPerSphere;
    private LabEnergySOSetter energySoSetter;
    
    // Start is called before the first frame update
    void Awake() {
        energySoSetter = labEnergySOSetter.GetComponent<LabEnergySOSetter>();
        maxEnergy = energySoSetter.GetMaxEnergy();
        maxEnergyPerSphere = maxEnergy / 5.0f;
        animator = gameObject.GetComponent<Animator>();
        min = (sphereNumber - 1) * maxEnergyPerSphere;
        max = sphereNumber * maxEnergyPerSphere;
    }

    private void Update() {
        int animationToPlay;
        var level = maxEnergyPerSphere / 4.0f;
        if (energySoSetter.GetCurrentEnergy() >= max) { animationToPlay = 4; }
        else if (energySoSetter.GetCurrentEnergy() < min) { animationToPlay = 0; }
        else { animationToPlay = (int) Math.Floor((energySoSetter.GetCurrentEnergy() - min) / level); } // TODO CHANGE

        animator.SetInteger("AnimationToPlay", animationToPlay);
    }
}
