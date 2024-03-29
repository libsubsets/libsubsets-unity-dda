﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace Subsets.Dda
{
    [Serializable]
    public enum RuntimeInitializerState
    {
        Disable,
        Initializing,
        Initialized,
    }
    public static class RuntimeInitializerSettings
    {
        public static UnityEvent<RuntimeInitializerState> StateChanged = new UnityEvent<RuntimeInitializerState>();
        
        public static RuntimeInitializerState State = RuntimeInitializerState.Disable;
       
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            Debug.Log("RuntimeInitializerSettings::Initialize: State: " + State);
            if (State == RuntimeInitializerState.Disable)
            {
                Transit(RuntimeInitializerState.Initialized);
            }
        }
        public static void Transit(RuntimeInitializerState transitState)
        {
            if (State == transitState)
                return;
            State = transitState;
            StateChanged?.Invoke(State);
        }
    }
}