using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager<AudioManager>
{
    [Serializable]
    public class AudioClips
    {
        public string name;
        public AudioClip clip;
    }

    [Header("BGM")]
    public AudioSource bgmSource;
    public List<AudioClips> bgmClips;

    [Header("SFX")]
    public AudioSource sfxSource;
    public List<AudioClips> sfxClips;

    [Header("Loop SFX")]
    public AudioSource loopSfxSource;
    public List<AudioClips> loopSfxClips;

    private Dictionary<string, AudioClip> _bgmDict = new();
    private Dictionary<string, AudioClip> _sfxDict = new();
    private Dictionary<string, AudioClip> _loopSfxDict = new();

    private void Awake()
    {
        SetDoNotDestroyOnLoad();
    }
    
    private void Start()
    {
        foreach (var clip in bgmClips)
        {
            if (clip != null && clip.clip != null)
            {
                _bgmDict[clip.name] = clip.clip;
            }
        }
        
        foreach (var clip in sfxClips)
        {
            if (clip != null && clip.clip != null)
            {
                _sfxDict[clip.name] = clip.clip;
            }
        }

        foreach (var clip in loopSfxClips)
        {
            if (clip != null && clip.clip != null)
            {
                _loopSfxDict[clip.name] = clip.clip;
            }
        }
    }

    public void PlayBGM(string name, bool loop = true, float fadeTime = 1f)
    {
        if (!_bgmDict.TryGetValue(name, out var clip)) return;

        StartCoroutine(FadeIn(bgmSource, clip, fadeTime, loop));
    }

    public void StopBGM(float fadeTime = 1f)
    {
        StartCoroutine(FadeOut(bgmSource, fadeTime));
    }

    public void PlayLoopSFX(string name, bool loop = true)
    {
        if (!_loopSfxDict.TryGetValue(name, out var clip)) return;
        loopSfxSource.clip = clip;
        loopSfxSource.loop = loop;
        loopSfxSource.volume = 1.0f;
        loopSfxSource.Play();
    }

    public void StopLoopSFX()
    {
        loopSfxSource.Stop();
    }

    public void PlaySFX(string name, float volume = 1f)
    {
        if (_sfxDict.TryGetValue(name, out var clip))
            sfxSource.PlayOneShot(clip, volume);
    }

    private IEnumerator FadeIn(AudioSource source, AudioClip clip, float time, bool loop)
    {
        source.clip = clip;
        source.loop = loop;
        source.volume = 0;
        source.Play();

        float elapsed = 0f;
        while (elapsed < time)
        {
            source.volume = Mathf.Lerp(0, 1, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        source.volume = 1;
    }

    private IEnumerator FadeOut(AudioSource source, float time)
    {
        float startVolume = source.volume;
        float elapsed = 0f;

        while (elapsed < time)
        {
            source.volume = Mathf.Lerp(startVolume, 0, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        source.Stop();
        source.volume = 1;
    }
}
