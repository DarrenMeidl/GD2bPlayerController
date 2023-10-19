using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour   //This will be a singleton, only 1 version exists & can't be destroyed
{
    
    public static AudioManager instance;    //Same exact variable (not copy) that can be called across all scripts
    
    public AudioClip jumpClip;
    public AudioClip dashClip;
    public AudioClip footstepSound;
    public AudioClip attackSound;
    public AudioClip backgroundMusic;
    
    public AudioSource soundEffectSource;
    public AudioSource backgroundMusicSource;

    void Awake()
    {
        if(instance = null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
            return;
        }
    

    soundEffectSource = gameObject.AddComponent<AudioSource>();
    backgroundMusicSource = gameObject.AddComponent<AudioSource>();

    backgroundMusicSource.clip = backgroundMusic;
    backgroundMusicSource.loop = true;
    backgroundMusicSource.Play();
    }


    public void PlayJumpSound(){
        soundEffectSource.PlayOneShot(jumpClip);
    }
    public void PlayDashSound(){
        soundEffectSource.PlayOneShot(dashClip);
    }
    public void PlayFootstepSound(){
        soundEffectSource.PlayOneShot(footstepSound);
    }
    public void PlayAttackSound(){
        soundEffectSource.PlayOneShot(attackSound);
    }
    public void PlayBackgroundMusic(){
        if(!backgroundMusicSource.isPlaying){
            backgroundMusicSource.Play();
        }
    }
    
    public void PauseBackgroundMusic(){
        backgroundMusicSource.Pause();
    }
    public void StopBackgroundMusic(){
        backgroundMusicSource.Stop();
    }
    public void SetBackgroundMusicVolume(float volume){
        backgroundMusicSource.volume = volume;
    }

}
