﻿using System;
using UnityEngine;

namespace Subsets.Dda
{
    public class CommandBinder : MonoBehaviour
    {
        public String SourceEvent;
        public void Awake()
        {
            UnityEngine.EventSystems.EventTrigger trigger = GetComponent<UnityEngine.EventSystems.EventTrigger>();
        }

        public void Raise()
        {
        }
    }
}