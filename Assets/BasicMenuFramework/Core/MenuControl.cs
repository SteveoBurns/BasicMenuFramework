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
    public class MenuControl : MonoBehaviour
    {
        [Header("Menu Buttons"),Tooltip("The quit button within the scene")]
        [SerializeField] private Button quitButton;
        [Tooltip("The pause button within the scene")]
        [SerializeField] private Button pauseButton;
        
        public List<SceneLoadButton> sceneLoadButtons = new List<SceneLoadButton>();
        
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

 
        private bool isPaused = false;

        // Start is called before the first frame update
        void Start()
        {
            quitButton.onClick.AddListener(QuitGame);
            pauseButton.onClick.AddListener(Pause);
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
            }
            else if(isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
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

        /// <summary>
        /// Loads the scene of the passed scene name
        /// </summary>
        /// <param name="_sceneName">Scene name to load</param>
        public void LoadScene(string _sceneName) => SceneManager.LoadScene(_sceneName);
    }
}