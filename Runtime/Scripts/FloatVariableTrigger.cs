using System;
using System.Collections.Generic;
using System.ComponentModel;
using Subsets.Dda;
using UnityEngine;
using UnityEngine.Events;

namespace Subsets.Dda
{
    public class FloatVariableTrigger : MonoBehaviour 
    {
        public FloatVariable Variable;
        public bool CompareWhenChanged = false;
        public float CompareEpsilon = 0.01f;
        public ResponseConditionOperator ConditionOperator;
       
        public List<FloatCondition> Conditions;
        public UnityEvent<FloatVariable> Listeners;
        
        private void Start()
        {
            Variable.PropertyChanged += OnExecute;
        }

        private void Destroy()
        {
            Variable.PropertyChanged -= OnExecute;
        }
        
        private void OnExecute(object sender, PropertyChangedEventArgs args)
        {
            if (CompareWhenChanged)
            {
                if (IsEqual(Variable.Value, Variable.OldValue))
                {
                    return;
                }
            }
                
            ConditionCompareResult result = new ConditionCompareResult();
            foreach (FloatCondition condition in Conditions)
            {
                if (condition.Compare == FloatCompare.Equal)
                {
                    result.Add(IsEqual(Variable.Value, condition.Value));
                }
                else if (condition.Compare == FloatCompare.IsNot)
                {
                    result.Add(!IsEqual(Variable.Value, condition.Value));
                }
                else if (condition.Compare == FloatCompare.Updated)
                {
                    result.Add(true);
                }
            }
                
            if (result.CheckConditionOperator(ConditionOperator))
            {
                Listeners?.Invoke(Variable);
            }
        }
        
        private bool IsEqual(float a, float b)
        {
            return Math.Abs(a - b) < CompareEpsilon;
        }
    }
}