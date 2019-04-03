namespace Betterfly.BetterEventFramework
{
    public static class BetterEventFactory
    {
        private static IEventManager BetterEventManagerCache = new BetterEventManager();

        public static IEventManager CurrentEventManager => BetterEventManagerCache;

        #region Static method

        public static void AddCall<TEventUnit>(ExecuteEventAction<TEventUnit> executeEventAction,
            byte priority = byte.MaxValue) where TEventUnit : struct, IEventUnit
        {
            CurrentEventManager.RegisterActionCall(executeEventAction,priority);
        }

        public static void RemoveCall<TEventUnit>(ExecuteEventAction<TEventUnit> executeEventAction,
            byte priority = byte.MaxValue) where TEventUnit : struct, IEventUnit
        {
            CurrentEventManager.UnregisterActionCall(executeEventAction,priority);
        }

        public static void BroadcastEvent<TEventUnit>(TEventUnit eventUnit) where TEventUnit : struct, IEventUnit
        {
            CurrentEventManager.CallAction(eventUnit);
        }

        public static string ToInfo()
        {
            return CurrentEventManager.ToInfo();
        }

        public static bool IsLog
        {
            get => CurrentEventManager.IsLog;
            set => CurrentEventManager.IsLog = value;
        }

        #endregion
    }
}