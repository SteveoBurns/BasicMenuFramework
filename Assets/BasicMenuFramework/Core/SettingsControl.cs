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
        public Dropdown resolutionDropdown;
        
        private Resolution[] allowedRes;

        private void Awake()
        {
            Resolutions();
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
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
        }

        /// <summary>
        /// Pauses time by setting timescale to 0.
        /// </summary>
        public void Pause() => Time.timeScale = 0;
        
        /// <summary>
        /// Unpauses time by setting timescale to 1.
        /// </summary>
        public void UnPause() => Time.timeScale = 1;
        
        /// <summary>
        /// Quits from both the Play Mode in the Unity Editor and the Built Application.
        /// </summary>
        public void ExitGame()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        }

        /// <summary>
        /// Loads the scene of the passed scene name
        /// </summary>
        /// <param name="_sceneName">Scene name to load</param>
        public void LoadScene(string _sceneName) => SceneManager.LoadScene(_sceneName);

        
    }
}