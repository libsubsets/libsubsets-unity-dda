﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

namespace Subsets.Dda
{
    public class IntegerVariableTrigger : MonoBehaviour
    {
        public IntegerVariable Variable;
        public ResponseConditionOperator ConditionOperator;
       
        public List<IntegerCondition> Conditions;
        public UnityEvent<IntegerVariable> Listeners;
        public void Start()
        {
            Variable.PropertyChanged += OnExecute;
        }

        private void Destroy()
        {
            Variable.PropertyChanged -= OnExecute;
        }

        private void OnExecute(object sender, PropertyChangedEventArgs args)
        {
            ConditionCompareResult result = new ConditionCompareResult();
            foreach (IntegerCondition condition in Conditions)
            {
                if (condition.Compare == IntegerCompare.Equal)
                {
                    result.Add(Variable.Value == condition.Value);
                }
                else if (condition.Compare == IntegerCompare.IsNot)
                {
                    result.Add(Variable.Value != condition.Value);
                }
                else if (condition.Compare == IntegerCompare.Updated)
                {
                    result.Add(true); 
                }
            }

            if (result.CheckConditionOperator(ConditionOperator))
            {
                Listeners?.Invoke(Variable);
            }
        }
    }
}