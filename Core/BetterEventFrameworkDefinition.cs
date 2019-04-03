using System;
using Betterfly.BetterCode;

namespace Betterfly.BetterEventFramework
{
    public interface IEventBase{}
    
    public delegate void ExecuteEventAction<TEventUnit>(ref TEventUnit eventUnit) where TEventUnit : struct, IEventUnit;

    public enum EventUnitState
    {
        Continue,
        Break,
        Return,
    }
    
    public interface IEventUnit : IEventBase
    {
        EventUnitState UnitState { get; set; }
    }
    
    public interface ISimpleEventUnit : IEventUnit{}

    public interface IReturnEventUnit<TResult> : IEventUnit
    {
        TResult Result { get; set; }
    }
    
    public interface IEventUnitManager : IToInfo, IIsLog
    {
        void RegisterActionCall<TEventUnit>(ExecuteEventAction<TEventUnit> executeEventAction, byte priority = byte.MaxValue) where TEventUnit : struct, IEventUnit;

        void UnregisterActionCall<TEventUnit>(ExecuteEventAction<TEventUnit> executeEventAction,
            byte priority = byte.MaxValue) where TEventUnit : struct, IEventUnit;

        void CallAction<TEventUnit>(TEventUnit eventUnit) where TEventUnit : struct, IEventUnit;
    }
    
    public interface IEventManager : IEventUnitManager{}
}