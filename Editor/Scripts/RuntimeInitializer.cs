using System;
using System.Collections.Generic;
using System.Linq;
using Subsets.Dda;
using UnityEditor;
using UnityEngine;

namespace Subsets.Dda.Editor
{
    
    
    public static class RuntimeInitializer
    {
        [InitializeOnLoadMethod]
        public static void RegisterChanged()
        {
            EditorApplication.playModeStateChanged -= OnStateChanged; 
            EditorApplication.playModeStateChanged += OnStateChanged; 
        }
        
        static void OnStateChanged(PlayModeStateChange change)
        {
            HashSet<IRuntimeInitialize> instances = RuntimeInstances.GetInstances();
            Debug.Log(String.Format("RuntimeInitializer::OnStateChanged: EnterPlayMode : {0}, Options:{1}, StateChanged: {2}, Instances count: {3}",
                EditorSettings.enterPlayModeOptionsEnabled, EditorSettings.enterPlayModeOptions.ToString(), change, instances.Count));
            if (EditorSettings.enterPlayModeOptionsEnabled &&
                EditorSettings.enterPlayModeOptions.HasFlag(EnterPlayModeOptions.DisableDomainReload))
            {
                if (change == PlayModeStateChange.ExitingEditMode)
                {
                    RuntimeInitializerSettings.Transit(RuntimeInitializerState.Initializing);
                    Initialize(); 
                }
                else if (change == PlayModeStateChange.EnteredPlayMode)
                {
                    //This event is synchronized with the editor application's update loop,
                    //it may occur after the game's update loop has already executed one or more times
                    RaiseRuntimeInitialize(); 
                    RuntimeInitializerSettings.Transit(RuntimeInitializerState.Initialized);
                }
                else if (change == PlayModeStateChange.ExitingPlayMode)
                {
                    RuntimeInitializerSettings.Transit(RuntimeInitializerState.Disable);
                }
            }
        }

        public static void Initialize()
        {
            HashSet<IRuntimeInitialize> instances = RuntimeInstances.GetInstances();
            Debug.Log(String.Format("RuntimeInitializer::Initialize: Instance count: {0}", instances.Count));
            foreach (IRuntimeInitialize instance in instances)
            {
                instance.RuntimeFinalize();
                instance.RuntimeInitialize();
            }
        }

        public static void RaiseRuntimeInitialize()
        {
            HashSet<IRuntimeInitialize> instances = RuntimeInstances.GetInstances();
            Debug.Log(String.Format("RuntimeInitializer::RaiseRuntimeInitialize Instance count: {0}", instances.Count));
            foreach (IRuntimeInitialize instance in instances)
            {
                instance.RaiseRuntimeInitializeEvent();
            }
        }
    }
}