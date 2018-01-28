using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource soundEffect;

    [SerializeField]
    private AudioClip mainTheme;
    [SerializeField]
    private AudioClip mainTheme2;
    [SerializeField]
    private AudioClip menuButtonPressedSound;
    [SerializeField]
    private AudioClip menuButtonHoverSound;
    [SerializeField]
    private AudioClip launchButtonPressedSound;
    [SerializeField]
    private AudioClip ingameButtonPressedSound;
    [SerializeField]
    private AudioClip shipLaunchedSound;
    [SerializeField]
    private AudioClip paperMovedSound;
    [SerializeField]
    private AudioClip printerSound;

    void Awake () {
        DontDestroyOnLoad( this.gameObject );
        PlayMainTheme();
    }

    public void PlayMainTheme()
    {
        music.clip = mainTheme;
        music.loop = true;
        music.Play();
    }

    public void PlayMainTheme2 () {
        music.clip = mainTheme2;
        music.loop = true;
        music.Play();
    }

    public void PlayMenuButtonPressedSound()
    {
        soundEffect.PlayOneShot(menuButtonPressedSound);
    }

    public void PlayMenuButtonHoverSound () {
        soundEffect.PlayOneShot( menuButtonHoverSound );
    }

    public void PlayLaunchButtonPressedSound()
    {
        soundEffect.PlayOneShot(launchButtonPressedSound);
    }

    public void PlayIngameButtonPressedSound()
    {
        soundEffect.PlayOneShot(ingameButtonPressedSound);
    }

    public void PlayShipLaunchedSound()
    {
        soundEffect.PlayOneShot(shipLaunchedSound);
    }

    public void PlayPaperMovedSound()
    {
        soundEffect.PlayOneShot(paperMovedSound);
    }

    public void PlayPrinterSound()
    {
        soundEffect.PlayOneShot(printerSound);
    }

    public void StopMusic()
    {
        music.Stop();
    }
}
