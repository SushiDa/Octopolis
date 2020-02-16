using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFXSource : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> audioClips;
    private Dictionary<string, AudioClip> audioClipsMap = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach(var clip in audioClips)
        {
            audioClipsMap.Add(clip.name, clip);
        }
    }

    public void PlaySound(string name)
    {
        if(audioClipsMap.ContainsKey(name))
        {
            audioSource.PlayOneShot(audioClipsMap[name]);
        }
    }


}
