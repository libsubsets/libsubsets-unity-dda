﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Subsets.Dda;

namespace Subsets.Dda.Editor
{
    [CustomEditor(typeof(StringEventListener))]
    public class StringEventListenerEditor : UnityEditor.Editor
    {
        private SerializedProperty eventProperty;
        private SerializedProperty conditionsProperty;
        private SerializedProperty responseProperty;
        
        public void OnEnable()
        {
            eventProperty = serializedObject.FindProperty("Event");
            conditionsProperty = serializedObject.FindProperty("Conditions");
            responseProperty = serializedObject.FindProperty("Response");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            StringEventListener e = target as StringEventListener;

            EditorGUILayout.PropertyField(eventProperty, new GUIContent("Event") );

            EditorGUILayout.PropertyField(conditionsProperty, new GUIContent("Conditions") );
            if (conditionsProperty.arraySize > 1)
            {
                e.ConditionOperator = (ResponseConditionOperator)EditorGUILayout.EnumPopup("Condition Operator", e.ConditionOperator);
            }
            EditorGUILayout.PropertyField(responseProperty, new GUIContent("Response") );

            serializedObject.ApplyModifiedProperties();
        }
    }
}