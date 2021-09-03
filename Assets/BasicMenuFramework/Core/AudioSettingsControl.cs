using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace BasicMenuFramework.Core
{
    public class AudioSettingsControl : MonoBehaviour
    {
        [Header("Audio Mixer Variables")]
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private string masterGroupExposedParam;
        [SerializeField] private string musicGroupExposedParam;
        [SerializeField] private string sfxGroupExposedParam;

        [Header("Volume Sliders")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
    
        // Start is called before the first frame update
        void Start()
        {
            LoadVolumePlayerPrefs();

            masterVolumeSlider.onValueChanged.AddListener(MasterSlider);
            musicVolumeSlider.onValueChanged.AddListener(MusicSlider);
            sfxVolumeSlider.onValueChanged.AddListener(SFXSlider);
        }

    
    #region Volume Sliders
    
        /// <summary>
        /// Loads all the saved values for settings from playerprefs.
        /// </summary>
        public void LoadVolumePlayerPrefs()
        {
            if(PlayerPrefs.HasKey("MusicVolume"))
            {
                float volume = PlayerPrefs.GetFloat("MusicVolume");
                musicVolumeSlider.value = volume;
                MusicSlider(volume);
            }

            if(PlayerPrefs.HasKey("SFXVolume"))
            {
                float volume = PlayerPrefs.GetFloat("SFXVolume");
                sfxVolumeSlider.value = volume;
                SFXSlider(volume);
            }
        
            if(PlayerPrefs.HasKey("MasterVolume"))
            {
                float volume = PlayerPrefs.GetFloat("MasterVolume");
                masterVolumeSlider.value = volume;
                MasterSlider(volume);
            }
        }
    
     
    
        /// <summary>
        /// Function for the Master Volume slider. Saves value to PlayerPrefs.
        /// </summary>
        /// <param name="_volume">Float passed in from the slider</param>
        public void MasterSlider(float _volume)
        {
            PlayerPrefs.SetFloat("MasterVolume", _volume);
            _volume = VolumeRemap(_volume);
            audioMixer.SetFloat(masterGroupExposedParam, _volume);        
        }
    
        /// <summary>
        /// Function for the Master Volume slider. Saves value to PlayerPrefs.
        /// </summary>
        /// <param name="_volume">Float passed in from the slider</param>
        public void MusicSlider(float _volume)
        {
            PlayerPrefs.SetFloat("MusicVolume", _volume);
            _volume = VolumeRemap(_volume);
            audioMixer.SetFloat(musicGroupExposedParam, _volume);        
        }

        /// <summary>
        /// Function for the SFX Volume slider. Saves value to PlayerPrefs.
        /// </summary>
        /// <param name="_volume">Float passed in from the slider</param>
        public void SFXSlider(float _volume)
        {
            PlayerPrefs.SetFloat("SFXVolume", _volume);
            _volume = VolumeRemap(_volume);
            audioMixer.SetFloat(sfxGroupExposedParam, _volume);
        }

        /// <summary>
        /// Remaps the passed in float from the slider(value 0-1) and outputs to audio scale
        /// </summary>
        /// <param name="value">Passed in slider value</param>
        /// <returns>Remapped float value to pass into the audio mixer</returns>
        public float VolumeRemap(float value)
        {
            return -40 + (value - 0) * (20 - -40) / (1 - 0);
        }
    #endregion
    
    }
}