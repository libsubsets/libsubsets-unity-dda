﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Subsets.Dda
{
    public enum StringCompare
    {
        Equal,
        Contains,
        IsNot,
        Updated,
    }
   
    [Serializable]
    public class StringCondition
    {
        public StringCompare Compare;
        public string Value;
    }
    
    public class StringEventListener : BaseEventListener<string>
    {
        public ResponseConditionOperator ConditionOperator;
        public List<StringCondition> Conditions = new List<StringCondition>();
        
        protected  override bool CheckCompareCondition()
        {
            StringEvent e = Event as StringEvent;
            if (e)
            {
                ConditionCompareResult result = new ConditionCompareResult();
                foreach (StringCondition condition in Conditions)
                {
                    if (condition.Compare == StringCompare.Equal)
                    {
                        result.Add(e.Variable.Equals(condition.Value));
                    }
                    else if (condition.Compare == StringCompare.Contains)
                    {
                        result.Add(e.Variable.Contains(condition.Value));
                    }
                    else if (condition.Compare == StringCompare.IsNot)
                    {
                        result.Add(!e.Variable.Equals(condition.Value));
                    }    
                    else if (condition.Compare == StringCompare.Updated)
                    {
                        result.Add(true);
                    }
                }

                return result.CheckConditionOperator(ConditionOperator);
            }
            throw new Exception("Event type is wrong:" + Event.ToString());
        }
    }
}