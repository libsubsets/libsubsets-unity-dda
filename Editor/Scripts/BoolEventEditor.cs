﻿using Subsets.Dda;
using UnityEditor;
using UnityEngine;

namespace Subsets.Dda.Editor
{
    [CustomEditor(typeof(BoolEvent), editorForChildClasses: true)]
    public class BoolEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            BoolEvent e = target as BoolEvent;
            if (GUILayout.Button("Raise"))
            {
                e.Raise(e.Variable);
            }
        }
    }
}