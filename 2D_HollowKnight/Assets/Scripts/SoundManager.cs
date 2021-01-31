using UnityEngine.Audio;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("音源管理")]
    public AudioMixer mixer;

    public void VolumeBGM(float v){

        mixer.SetFloat("Vol_BGM",v);
    }
    public void VolumeSFX(float v){

        mixer.SetFloat("Vol_SFX",v);
    }
}
