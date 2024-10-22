using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
   public static SoundFXManager instance; 
   [SerializeField] private AudioSource movementFXObject;
   [SerializeField] private AudioSource soundFXObject;
   
   


   private void Awake()
   {
      if (instance == null)
      {
         instance = this;
      }
   }

   public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
   {
      //spawn in gameObject
      AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
      
      //assingn audioclip
      audioSource.clip = audioClip;
      
      //assign volume
      audioSource.volume = volume;
      
      //play sound
      audioSource.Play();
      
      //get lenght of FX sound clip
      float clipLenght = audioSource.clip.length;
      
      //destroy object when sound terminate
      Destroy(audioSource.gameObject, clipLenght);
   }
   
   public void PlayMovementFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
   {
      //spawn in gameObject
      AudioSource audioSource = Instantiate(movementFXObject, spawnTransform.position, Quaternion.identity);
      
      //assingn audioclip
      audioSource.clip = audioClip;
      
      //assign volume
      audioSource.volume = volume;
      
      //play sound
      audioSource.Play();
      
      //get lenght of FX sound clip
      float clipLenght = audioSource.clip.length;
      
      //destroy object when sound terminate
      Destroy(audioSource.gameObject, clipLenght);
   }
}
