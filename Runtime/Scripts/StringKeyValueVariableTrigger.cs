﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace Subsets.Dda
{
    public class StringKeyValueVariableTrigger : MonoBehaviour
    {
        [Serializable]
        public class StringCondition
        {
            public String Id;
            public StringKeyValueVariable Variable;
            public StringCompare Compare;
            public string Value;
            public bool Trigger = true;
        }

        public ResponseConditionOperator ConditionOperator;
        public List<StringCondition> Conditions = new List<StringCondition>();
        public UnityEvent Listeners;

        public void Awake()
        {
            foreach (StringCondition condition in Conditions)
            {
                if (condition.Trigger)
                {
                    condition.Variable.Find(condition.Id).PropertyChanged += OnExecute;
                }
                
            }
        }

        public void OnDestroy()
        {
            foreach (StringCondition condition in Conditions)
            {
                if (condition.Trigger)
                {
                    condition.Variable.PropertyChanged -= OnExecute;
                }
            }
        }

        private void OnExecute(object sender, PropertyChangedEventArgs args)
        {
            Execute();
        }

        private void OnEnable()
        {
        }

        private void Execute()
        {
            if (enabled)
            {
                ConditionCompareResult result = new ConditionCompareResult();
                foreach (StringCondition condition in Conditions)
                {
                    if (condition.Compare == StringCompare.Equal)
                    {
                        result.Add(condition.Variable.Find(condition.Id).Value.Equals(condition.Value));
                    }
                    else if (condition.Compare == StringCompare.Contains)
                    {
                        result.Add(condition.Variable.Find(condition.Id).Value.Contains(condition.Value));
                    }
                    else if (condition.Compare == StringCompare.IsNot)
                    {
                        result.Add(!condition.Variable.Find(condition.Id).Value.Equals(condition.Value));
                    }
                    else if (condition.Compare == StringCompare.Updated)
                    {
                        result.Add(true);
                    }
                }

                if (result.CheckConditionOperator(ConditionOperator))
                {
                    Listeners?.Invoke();
                }
            }
        }
    }
}