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
        DialogueSound,
        PlaceWaterBomb,
        WaterBombExplode,
        SpeedUpBoost,
        FireBite,
        NormalChestOpen,
        BossChestOpen,
        GoblinDamaged,
        GoblinDeath
    }

    private static Dictionary<Sound, float> soundTimers;

    public static void Initialize()
    {
        soundTimers = new Dictionary<Sound, float>();
        soundTimers[Sound.PlayerFootstep] = 0;
    }

    // PlaySound function for 3D sounds
    /*public static void PlaySound(Sound sound, Vector3 position)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundObj = new GameObject("Sound");
            soundObj.transform.position = position;
            AudioSource audioSrc = soundObj.AddComponent<AudioSource>();
            audioSrc.clip = GetAudioClip(sound);
            audioSrc.Play();
        }
    }*/

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundObj = new GameObject("Sound");
            AudioSource audioSrc = soundObj.AddComponent<AudioSource>();
            audioSrc.PlayOneShot(GetAudioClip(sound));
            Destroy(soundObj, 5f);
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch(sound)
        {
            default:
                return true;
            // to prevent footsteps from constantly overlapping (error currently)
            case Sound.PlayerFootstep:
                /*if (soundTimers.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimers[sound];
                    float playerMoveTimerMax = .05f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimers[sound] = Time.time;
                        return true;
                    } else {*/
                        return false;
                    /*}
                } else {
                    return true;
                }*/
        }
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
