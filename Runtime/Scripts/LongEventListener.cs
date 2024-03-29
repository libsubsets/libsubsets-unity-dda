﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Subsets.Dda
{
    public enum LongCompare
    {
        Equal,
        IsNot,
        Updated
    }
    
    [Serializable]
    public class LongCondition
    {
        public LongCompare Compare;
        public int Value;
    }
    
    public class LongEventListener : BaseEventListener<long>
    {
        public ResponseConditionOperator ConditionOperator;
       
        public List<LongCondition> Conditions;
        protected  override bool CheckCompareCondition()
        {
            LongEvent e = Event as LongEvent;
            if (e)
            {
                ConditionCompareResult result = new ConditionCompareResult();
                foreach (LongCondition condition in Conditions)
                {
                    if (condition.Compare == LongCompare.Equal)
                    {
                        result.Add(e.Variable == condition.Value);
                    }
                    else if (condition.Compare == LongCompare.IsNot)
                    {
                        result.Add(e.Variable != condition.Value);
                    }
                    else if (condition.Compare == LongCompare.Updated)
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