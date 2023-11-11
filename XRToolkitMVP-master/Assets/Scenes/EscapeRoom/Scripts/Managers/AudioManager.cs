using DilmerGames.Core.Singletons;
using System;
using System.Collections;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Background Music Tracks")]
    [SerializeField]
    private AudioClip[] tracks;
    private AudioSource audioSource;
    [Header("Events")]
    public Action onCurrentTrackEnded;

    public void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShuffleWhenItStopsPlaying());
        ShuffleAndPlay();
    }

    public void ShuffleAndPlay(GameState gameState = GameState.Playing)
    {
        if (tracks.Length > 0)
        {
            audioSource.clip = tracks[UnityEngine.Random.Range(0, tracks.Length - 1)];
            audioSource.Play();
        }
    }

    private IEnumerator ShuffleWhenItStopsPlaying()
    {
        while (true)
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            ShuffleAndPlay();
            onCurrentTrackEnded?.Invoke();
        }
    }
}
