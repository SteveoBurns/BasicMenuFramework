using BasicMenuFramework.Core;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace BasicMenuFramework.Editor.Core
{
    [CustomEditor(typeof(MenuControl))]
    public class MenuControlEditor : UnityEditor.Editor
    {
        private MenuControl menuControl;

        private SerializedProperty quitButtonProperty;
        private SerializedProperty pauseButtonProperty;
        private SerializedProperty sceneLoadButtonsProperty;

        private void OnEnable()
        {
            menuControl = target as MenuControl;

            quitButtonProperty = serializedObject.FindProperty("quitButton");
            pauseButtonProperty = serializedObject.FindProperty("pauseButton");
            sceneLoadButtonsProperty = serializedObject.FindProperty("sceneLoadButtons");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(quitButtonProperty);
                EditorGUILayout.PropertyField(pauseButtonProperty);
            }
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.Space();
            
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.PropertyField(sceneLoadButtonsProperty);
            }
            EditorGUILayout.EndVertical();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}