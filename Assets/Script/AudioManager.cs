using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource bgmSource;

    [Header("SFX")]
    [SerializeField] private List<SFXEntry> sfxList;

    [Header("BGM Playlist")]
    [SerializeField] private List<AudioClip> bgmPlaylist;
    [SerializeField] private bool shuffle = false;

    private Dictionary<SFXType, AudioClip> sfxDict;
    private int currentBgmIndex = 0;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        sfxDict = new Dictionary<SFXType, AudioClip>();
        foreach (var entry in sfxList)
        {
            sfxDict[entry.type] = entry.clip;
        }
    }

    void Start()
    {
        if (bgmPlaylist.Count > 0)
            PlayCurrentBGM();
    }

    void Update()
    {
        if (!bgmSource.isPlaying && bgmPlaylist.Count > 0)
        {
            PlayNextBGM();
        }
    }

    public void PlaySFX(SFXType type)
    {
        if (!sfxDict.ContainsKey(type)) return;
        sfxSource.PlayOneShot(sfxDict[type]);
    }

    void PlayCurrentBGM()
    {
        bgmSource.clip = bgmPlaylist[currentBgmIndex];
        bgmSource.loop = false;
        bgmSource.Play();
    }

    void PlayNextBGM()
    {
        if (shuffle)
        {
            currentBgmIndex = Random.Range(0, bgmPlaylist.Count);
        }
        else
        {
            currentBgmIndex++;
            if (currentBgmIndex >= bgmPlaylist.Count)
                currentBgmIndex = 0;
        }

        PlayCurrentBGM();
    }

    public void SetPlaylist(List<AudioClip> newPlaylist, bool shufflePlaylist = false)
    {
        if (newPlaylist == null || newPlaylist.Count == 0) return;

        bgmPlaylist = newPlaylist;
        shuffle = shufflePlaylist;
        currentBgmIndex = 0;

        PlayCurrentBGM();
    }
}

[System.Serializable]
public class SFXEntry
{
    public SFXType type;
    public AudioClip clip;
}

