using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
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
        DialogueSound,
        PlaceWaterBomb,
        WaterBombExplode,
        SpeedUpBoost,
        FireBite,
        NormalChestOpen,
        BossChestOpen,
        GoblinDamaged,
        GoblinDeath,
        Fireball,
        CulpritFound,
        PickupKey,
        UnlockDoor
    }

    private static Dictionary<Sound, float> soundTimers;

    public static void Initialize()
    {
        soundTimers = new Dictionary<Sound, float>();
        soundTimers[Sound.PlayerFootstep] = 0;
    }

    public static void PlaySound(Sound sound)
    {
        GameObject soundObj = new GameObject("Sound");
        AudioSource audioSrc = soundObj.AddComponent<AudioSource>();
        audioSrc.outputAudioMixerGroup = GetSoundGroup(sound);
        audioSrc.PlayOneShot(GetAudioClip(sound));
        Destroy(soundObj, 5f);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(SoundAssets.SoundClip soundClip in SoundAssets.i.soundClips)
        {
            if (soundClip.sound == sound) return soundClip.audioClip;
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

    private static AudioMixerGroup GetSoundGroup(Sound sound)
    {
        foreach(SoundAssets.SoundClip soundClip in SoundAssets.i.soundClips)
        {
            if (soundClip.sound == sound){
                if(soundClip.group){
                    return soundClip.group;
                }
                Debug.LogError("Sound group for " + sound + " not found!");
                return null;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
