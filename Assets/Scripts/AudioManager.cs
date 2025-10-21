using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("BGM")]
    public AudioSource bgmSource;
    public List<AudioClip> bgmClips;

    [Header("SFX")]
    public AudioSource sfxSource;
    public List<AudioClip> sfxClips;

    [Header("Loop SFX")]
    public AudioSource loopSfxSource;
    public List<AudioClip> loopSfxClips;

    private Dictionary<string, AudioClip> _bgmDict = new();
    private Dictionary<string, AudioClip> _sfxDict = new();
    private Dictionary<string, AudioClip> _loopSfxDict = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (var clip in bgmClips)
            _bgmDict[clip.name] = clip;

        foreach (var clip in sfxClips)
            _sfxDict[clip.name] = clip;

        foreach (var clip in loopSfxClips)
            _loopSfxDict[clip.name] = clip;
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
