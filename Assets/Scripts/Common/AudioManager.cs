using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音效管理类
/// </summary>
public class AudioManager : MonoBehaviour
{
    public const int MusicUI = 0;
    public const int MusicGame = 1;
        
    public const int SoundButtonClick = 0;
    public const int SoundGameWin = 1;
    public const int SoundGameLose = 2;
    public const int SoundTi = 3;
    public const int SoundIn = 4;
    public const int SoundHit = 5;
    public const int SoundHit1 = 6;
	public const int SoundReward = 7;
    public const int SoundItem1 = 8;
    public const int SoundItem0 = 9;
    public const int SoundHit2 = 10;
    public const int SoundHit3 = 11;
    public const int SoundHit4 = 12;
    public const int SoundBoom = 13;
    public const int SoundCoin = 14;
    public const int SoundDie = 15;

    public AudioSource audioSourceMusic;
    public AudioSource audioSourceSound;
    public AudioClip[] audioClipsMusic;
    public AudioClip[] audioClipsOne;
      
   
    public void PlayMusic(int id)
    {
        if (GameController.GetInstance().soundState == 0)
        {
            return;
        }
        audioSourceMusic.volume = 1;
        audioSourceMusic.clip = audioClipsMusic[id];
        audioSourceMusic.Play();
    }

    public void PlaySound(int id)
    {
        if (GameController.GetInstance().soundState == 0)
        {
            return;
        }
        
        audioSourceSound.PlayOneShot(audioClipsOne[id]);
    }


    public void StopMusic(int id)
    {
        if (GameController.GetInstance().soundState == 0)
        {
            return;
        }
        audioSourceMusic.volume = 0;
    }
    private void Update()
    {
        if (GameController.GetInstance().soundState == 0)
        {
            if (audioSourceMusic.isPlaying)
                audioSourceMusic.Stop();
        }
        else
        {
            if (audioSourceMusic.isPlaying==false)
                audioSourceMusic.Play();
        }
    }
}
