﻿using UnityEditor;
using UnityEngine;
using LineUp;

namespace LineUp_Editor
{
    [CustomEditor(typeof(GameplayManager))]
    public class GameplayManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameplayManager script = (GameplayManager)target;
            if (GUILayout.Button("Generate new instructions"))
            {
                script.CreateNewConfiguration();
            }
        }
    }
}