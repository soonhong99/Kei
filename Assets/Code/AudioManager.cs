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
        if (currentSceneName == "ReadyScene")
        {
            musicSource.clip = ReadyBackGround;
            musicSource.Play();
        }
        else if (currentSceneName == "StartScene")
        {
            musicSource.clip = StartBackGround;
            musicSource.Play();
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
