﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Subsets.Message2 
{
    [Serializable]
    public struct Void
    {
        
    }
    
    [CreateAssetMenu]
    public class GameEvent : BaseEvent<Void>
    {
    }
}