using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    [Header("------------ Audio Source -------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    

    [Header("------------ Audio Clip ---------------")]
    public AudioClip ReadyBackGround;
    public AudioClip StartBackGround;
    public AudioClip StolenBackGround;
    public AudioClip Death;
    public AudioClip CheckPoint;
    public AudioClip PortalIn;
    public AudioClip PortalOut;
    public AudioClip Click;
    public AudioClip[] RandomMoving;
    public AudioClip[] RandomSword;

    private string currentSceneName;

    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        if (currentSceneName == "StartScene")
        {
            musicSource.clip = StartBackGround;
            musicSource.Play();
        }
    }

    public void PlayMusic(string SceneName)
    {
        if (SceneName == "StolenScene")
        {
            musicSource.clip = StolenBackGround;
            musicSource.Play();
        }
        else if (currentSceneName == "ReadyScene")
        {
            musicSource.clip = ReadyBackGround;
            musicSource.Play();
        }
    }

    public void StopMusic(string pastSceneName)
    {
        if (pastSceneName == "StolenScene")
        {
            musicSource.clip = StolenBackGround;
            musicSource.Stop();
        }
        else if (pastSceneName == "ReadyScene")
        {
            musicSource.clip = ReadyBackGround;
            musicSource.Stop();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void RandomSoundness()
    {
        SFXSource.clip = RandomMoving[Random.Range(0, RandomMoving.Length)];
        SFXSource.Play();
    }

    public void RandomSwordness()
    {
        SFXSource.clip = RandomSword[Random.Range(0, RandomSword.Length)];
        SFXSource.Play();
    }

}
