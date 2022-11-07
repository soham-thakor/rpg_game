using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        PlayerFootstep,
        SwordSlash,
        PlayerDamaged,
        PlayerDeath,
        KnightDamaged,
        KnightDeath,
        DialogueSound
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundObj = new GameObject("Sound");
        AudioSource audioSrc = soundObj.AddComponent<AudioSource>();
        audioSrc.PlayOneShot(GetAudioClip(sound));
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundAssets.SoundClip soundClip in SoundAssets.i.soundClips)
        {
            if (soundClip.sound == sound) return soundClip.audioClip;
        }
        // Log if error occurs when trying to fetch sound
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
