using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    public Slider audioSlider;
    [SerializeField]
    private AudioMixer Mixer;

    [SerializeField]
    private AudioSource AudioSource;

    [SerializeField]
    private AudioMixMode MixMode;

    // Start is called before the first frame update
    void Start()
    {
        Mixer.SetFloat("Volume", Mathf.Log10(PlayerPrefs.GetFloat("Volume", 1) * 20));
        audioSlider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeSlider(float Value)
    {
        switch(MixMode)
        {
            case AudioMixMode.LinearAudioSourceVolume:
                AudioSource.volume = Value;
                break;
            case AudioMixMode.LinearMixerVolume:
                Mixer.SetFloat("Volume", (-80 + Value * 100));
                break;
            case AudioMixMode.LogorithmicMixerVolume:
                Mixer.SetFloat("Volume", Mathf.Log10(Value) * 20);
                break;
        }

        PlayerPrefs.SetFloat("Volume", Value);
        PlayerPrefs.Save();
    }

    public enum AudioMixMode
    {
        LinearAudioSourceVolume,
        LinearMixerVolume,
        LogorithmicMixerVolume
    }
}
