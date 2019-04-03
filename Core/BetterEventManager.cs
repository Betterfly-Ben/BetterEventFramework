using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Betterfly.BetterCode;
using UnityEngine;

namespace Betterfly.BetterEventFramework
{
    public class BetterEventManager : IEventManager,IDeepCloneable<Dictionary<Type,SortedDictionary<byte,Delegate>>>
    {
        private static Dictionary<Type,SortedDictionary<byte,Delegate>> _eventActions = new Dictionary<Type, SortedDictionary<byte, Delegate>>();

        public void RegisterActionCall<TEventUnit>(ExecuteEventAction<TEventUnit> executeEventAction, byte priority = byte.MaxValue) where TEventUnit : struct, IEventUnit
        {
            if (executeEventAction == null)
            {
                return;
            }
			
            Type eventType = typeof(TEventUnit);
            if (_eventActions.ContainsKey(eventType) == false)
            {
                _eventActions.Add(eventType,new SortedDictionary<byte, Delegate>());
            }

            if (_eventActions[eventType].ContainsKey(priority) == false)
            {
                _eventActions[eventType].Add(priority,executeEventAction);
            }
            else
            {
                ExecuteEventAction<TEventUnit> actions = _eventActions[eventType][priority] as ExecuteEventAction<TEventUnit>;
                actions += executeEventAction;
                _eventActions[eventType][priority] = actions;
            }
			

            #region log

            if (IsLog)
            {
                ExecuteEventAction<TEventUnit> logActions = _eventActions[eventType][priority] as ExecuteEventAction<TEventUnit>;
                if (logActions == null)
                {
                    Debug.Log($"Register event action {eventType.Name} {priority} null actions");
                }
                else
                {
                    string logMsg = logActions.GetInvocationList().Select(l => l == null ? $"Null link" : $"{l.Target} -> {l.Method}").JoinString();
                    Debug.Log($"Register event action {eventType.Name} {priority} [{logActions.GetInvocationList().Length}] {logMsg}");
                }
            }

            #endregion
        }

        public void UnregisterActionCall<TEventUnit>(ExecuteEventAction<TEventUnit> executeEventAction, byte priority = byte.MaxValue) where TEventUnit : struct, IEventUnit
        {
            if (executeEventAction == null)
            {
                return;
            }
			
            Type eventType = typeof(TEventUnit);
            if (_eventActions.ContainsKey(eventType) == false)
            {
                return;
            }

            if (_eventActions[eventType].ContainsKey(priority) == false)
            {
                return;
            }

            ExecuteEventAction<TEventUnit> actions = _eventActions[eventType][priority] as ExecuteEventAction<TEventUnit>;
            if (actions != null)
            {
                actions -= executeEventAction;
                _eventActions[eventType][priority] = actions;
            }

            #region log

            if (IsLog)
            {
                if (actions == null)
                {
                    Debug.Log($"Unregister event action {eventType.Name} {priority} null actions");
                }
                else
                {
                    string logMsg = actions.GetInvocationList().Select(l => l == null ? $"Null link" : $"{l.Target} -> {l.Method}").JoinString();
                    Debug.Log($"Unregister event action {eventType.Name} {priority} [{actions.GetInvocationList().Length}] {logMsg}");
                }
            }

            #endregion
			
            if (actions == null)
            {
                _eventActions[eventType].Remove(priority);
            }

            if (_eventActions[eventType].IsNullOrEmpty())
            {
                _eventActions.Remove(eventType);
            }
        }

        public void CallAction<TEventUnit>(TEventUnit eventUnit) where TEventUnit : struct, IEventUnit
        {
            Type eventType = typeof(TEventUnit);
            if (_eventActions.ContainsKey(eventType) == false)
            {
                return;
            }

            var dic = _eventActions[eventType];
            foreach (var delKvs in dic)
            {
                // Break or Return
                if (eventUnit.UnitState != EventUnitState.Continue)
                {
                    break;
                }
                Delegate[] targetActions = delKvs.Value.GetInvocationList();
                if (targetActions.IsNullOrEmpty())
                {
                    continue;
                }

                for (int i = 0;i < targetActions.Length;++i)
                {
                    ExecuteEventAction<TEventUnit> call = targetActions[i] as ExecuteEventAction<TEventUnit>;
                    
                    // Break or Return
                    if (eventUnit.UnitState != EventUnitState.Continue)
                    {
                        break;
                    }

                    #region log

                    if (IsLog)
                    {
                        if (call == null)
                        {
                            Debug.LogError($"Call event action {eventType.Name} {delKvs.Key} [{i}] is null!");
                        }
                        else
                        {
                            Debug.Log($"Call event action {eventType.Name} {delKvs.Key} [{i}] {call.Target} -> {call.Method}");
                        }
                    }

                    #endregion
                    
                    call?.Invoke(ref eventUnit);
                    
                    // Break or Return
                    if (eventUnit.UnitState != EventUnitState.Continue)
                    {
                        break;
                    }
                }
            }
        }
        
        
        public string ToInfo()
        {
            if (_eventActions.IsNullOrEmpty())
            {
                return "Null register";
            }

            Dictionary<Type, SortedDictionary<byte, Delegate>> cloneDic = DeepClone();
            StringBuilder stringBuilder = new StringBuilder("Event register:\n");
            foreach (var eventKvs in cloneDic)
            {
                stringBuilder.AppendLine($"{eventKvs.Key}:");
                if (eventKvs.Value.IsNullOrEmpty())
                {
                    stringBuilder.AppendLine($"    Null data");
                    continue;
                }

                foreach (var kvs in cloneDic[eventKvs.Key])
                {
                    stringBuilder.AppendLine($"    {kvs.Key}:");
                    var dels = kvs.Value;
                    if (dels == null)
                    {
                        stringBuilder.AppendLine($"        Null data");
                        continue;
                    }

                    var l = dels.GetInvocationList();
                    for(int j = 0;j < l.Length;++j)
                    {
                        var del = l[j];
                        if (del == null)
                        {
                            stringBuilder.AppendLine($"        [{j}] Null data element");
                        }
                        else
                        {
                            stringBuilder.AppendLine($"        [{j}] {del.Target} -> {del.Method}");
                        }
                    }
                }
            }

            return stringBuilder.ToString();
        }

        public Dictionary<Type, SortedDictionary<byte, Delegate>> DeepClone()
        {
            if (_eventActions.IsNullOrEmpty())
            {
                return null;
            }
            Dictionary<Type,SortedDictionary<byte,Delegate>> result = new Dictionary<Type, SortedDictionary<byte, Delegate>>();
            foreach (var kvs in _eventActions)
            {
                result.Add(kvs.Key,new SortedDictionary<byte, Delegate>());
                SortedDictionary<byte, Delegate> dic = kvs.Value;
                foreach (var dels in dic)
                {
                    result[kvs.Key].Add(dels.Key,dels.Value);
                }
            }

            return result;
        }

        public bool IsLog { get; set; } = false;
    }
}