using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BasicMenuFramework.Core
{
    public class SettingsControl : MonoBehaviour
    {
        [Header("Settings UI Elements")]
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private Dropdown qualityDropdown;
        [SerializeField] private Toggle fullscreenToggle;

        private Resolution[] allowedRes;
        

        private void Awake()
        {
            Resolutions();
            QualitySetup();
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
            qualityDropdown.onValueChanged.AddListener(Quality);
            
        }

        // Start is called before the first frame update
        void Start()
        {
            LoadPlayerPrefs();
        }
        
        /// <summary>
        /// Gets the screens resolutions, displays them in the dropdown, sets the default and saves the choice.
        /// </summary>
        private void Resolutions()
        {
            allowedRes = Screen.resolutions;
        
            List<string> resOptions = new List<string>();
            int currentResolution = 0;

            for (int i = 0; i < allowedRes.Length; i++)
            {
                string option = allowedRes[i].width + "x" + allowedRes[i].height;
                resOptions.Add(option);

                if(allowedRes[i].width == Screen.currentResolution.width && allowedRes[i].height == Screen.currentResolution.height)
                {
                    currentResolution = i;
                }
            }
	                      
            resolutionDropdown.AddOptions(resOptions);
            
            // Loads any settings saved in playerprefs, or sets it to the current screen resolution
            if (PlayerPrefs.HasKey("Resolution"))
            {
                int resIndex = PlayerPrefs.GetInt("Resolution");
                resolutionDropdown.value = resIndex;
                resolutionDropdown.RefreshShownValue();
                SetResolution(resIndex);
            }
            else
            {
                resolutionDropdown.value = currentResolution;
                resolutionDropdown.RefreshShownValue();
            }
        }
    
        /// <summary>
        /// Setting the chosen resolution from the dropdown on the UI
        /// </summary>
        public void SetResolution(int resIndex)
        {
            PlayerPrefs.SetInt("Resolution", resIndex);
            Resolution resolution = allowedRes[resIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        /// <summary>
        /// Setting up the Quality Options for the dropdown on the UI.
        /// </summary>
        private void QualitySetup()
        {
            List<string> qualities = new List<string>();
            string[] qualitySettings = QualitySettings.names;
            foreach(string _quality in qualitySettings)
            {
                qualities.Add(_quality);
            }
            qualityDropdown.AddOptions(qualities);
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();
        }
        
        /// <summary>
        /// Sets the graphics quality level
        /// </summary>
        /// <param name="_index"></param>
        public void Quality(int _index)
        {
            QualitySettings.SetQualityLevel(_index);
            PlayerPrefs.SetInt("Quality", _index);
        }

        /// <summary>
        /// Loads any saved player prefs
        /// </summary>
        public void LoadPlayerPrefs()
        {
            if(PlayerPrefs.HasKey("Resolution"))
            {
                int resIndex = PlayerPrefs.GetInt("Resolution");
                resolutionDropdown.value = resIndex;
                resolutionDropdown.RefreshShownValue();
                SetResolution(resIndex);
            }
            if (PlayerPrefs.HasKey("Quality"))
            {
                int quality = PlayerPrefs.GetInt("Quality");
                qualityDropdown.value = quality;
                Quality(quality);
            }
            if (PlayerPrefs.HasKey("Fullscreen"))
            {
                bool _fullscreen = true;
                int fullscreen = PlayerPrefs.GetInt("Fullscreen");
                if (fullscreen == 1)
                {
                    _fullscreen = true;
                    fullscreenToggle.isOn = true;
                }
                else if (fullscreen == 0)
                {
                    _fullscreen = false;
                    fullscreenToggle.isOn = false;
                }
                Fullscreen(_fullscreen);
            }
        }
        
        /// <summary>
        /// Turns fullscreen mode on and off
        /// </summary>    
        public void Fullscreen(bool _fullscreen)
        {
            PlayerPrefs.SetInt("Fullscreen", (_fullscreen ? 1 : 0));
            Screen.fullScreen = _fullscreen;
        }

        
        
        

        
    }
}