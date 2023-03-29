using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SoundAssets : MonoBehaviour
{
    private static SoundAssets _i;

    public static SoundAssets i
    {
        get {
            if (_i == null) _i = Instantiate(Resources.Load<SoundAssets>("SoundAssets"));
            return _i;
        }
    }

    public SoundClip[] soundClips;

    [System.Serializable]
    public class SoundClip
    {
        public SoundManager.Sound sound;
        public AudioMixerGroup group;
        public AudioClip audioClip;
    }
}
