using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BasicMenuFramework.Core
{
    /// <summary>
    /// The MenuControl class handles all the UI functionality for scene management, pause and quit.
    /// </summary>
    public class MenuControl : MonoBehaviour
    {
        [Header("Menu Buttons"),Tooltip("The quit button within the scene")]
        [SerializeField] private Button quitButton;
        [Tooltip("The pause button within the scene")]
        [SerializeField] private Button pauseButton;

        public bool showPopUp = false;
        [SerializeField] private GameObject popUp;
        
        public List<SceneLoadButton> sceneLoadButtons = new List<SceneLoadButton>();
        
        private bool isPaused = false;
        
        /// <summary>
        /// This contains the data needed for the Scene Load Buttons list to function.
        /// </summary>
        [Serializable]
        public class SceneLoadButton
        {
            [Header("Button and Scene to load"), Tooltip("Assign a button")]
            public Button button;
            [Tooltip("Assign the scene for the button to load when clicked")]
            public SceneAsset sceneToLoad;

            public void LoadScene()
            {
                SceneManager.LoadScene(sceneToLoad.name);
            }
        }

 

        // Start is called before the first frame update
        void Start()
        {
            // Adding listners to the UI buttons.
            quitButton.onClick.AddListener(QuitGame);
            pauseButton.onClick.AddListener(Pause);
            
            // Asigning the load scene method to each button in sceneLoadButtons.
            foreach(SceneLoadButton _scene in sceneLoadButtons)
            {
                _scene.button.onClick.AddListener(_scene.LoadScene);
            }
        }

        
        /// <summary>
        /// Handles Pause functionality for the UI button.
        /// </summary>
        public void Pause()
        {
            if(!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                if(showPopUp)
                    popUp.SetActive(true);
                
            }
            else if(isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
                if(showPopUp)
                    popUp.SetActive(false);
            }
        }
        
        /// <summary>
        /// Quits from both the Play Mode in the Unity Editor and the Built Application.
        /// </summary>
        public void QuitGame()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }

    }
}