using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Framework
{
    public static class Messenger
    {
        #region Internal variables

        public static Delegate[] eventTable = new Delegate[Enum.GetValues(typeof(GameEvent)).Length];

        //Message handlers (index) that should never be removed, regardless of calling Cleanup
        private static List<int> permanentMessages = new List<int>();

        #endregion

        #region Helper methods

        //Marks a certain message as permanent.
        public static void MarkAsPermanent(GameEvent gameEvent)
        {
            permanentMessages.Add((int)gameEvent);
        }

        public static void Cleanup()
        {
            for (int i = 0; i < eventTable.Length; i++)
            {
                if (!permanentMessages.Contains(i))
                    eventTable[i] = null;
            }
        }

        public static void PrintEventTable()
        {
            PDebug.Log("\t\t\t=== MESSENGER PrintEventTable ===");

            for (int i = 0; i < eventTable.Length; i++)
            {
                PDebug.Log("Event:{0}|{1}", (GameEvent)i, eventTable[i]);
            }

            PDebug.Log("\n");
        }

        #endregion

        #region Message logging and exception throwing

        static void OnListenerAdding(int eventIndex, Delegate listenerBeingAdded)
        {
            Delegate d = eventTable[eventIndex];
            if (d != null && d.GetType() != listenerBeingAdded.GetType())
            {
                PDebug.LogError("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", (GameEvent)eventIndex, d.GetType().Name, listenerBeingAdded.GetType().Name);
            }
        }

        static void OnListenerRemoving(int eventIndex, Delegate listenerBeingRemoved)
        {
            Delegate d = eventTable[eventIndex];

            if (d == null)
            {
                PDebug.LogWarning("Attempting to remove listener with for event type \"{0}\" but current listener is null.", (GameEvent)eventIndex);
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                PDebug.LogError("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", (GameEvent)eventIndex, d.GetType().Name, listenerBeingRemoved.GetType().Name);
            }
        }

        static void OnBroadcasting(int eventIndex, Delegate broadcastMessage)
        {
            PDebug.LogError("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", (int)eventIndex);
        }

        #endregion

        #region AddListener

        //No parameters
        static public void AddListener(GameEvent gameEvent, Callback handler, bool isPermanent = true)
        {
            int index = (int)gameEvent;

            OnListenerAdding(index, handler);
            eventTable[index] = (Callback)eventTable[index] + handler;
            if (isPermanent)
            {
                permanentMessages.Add(index);
            }
        }

        //Single parameter
        static public void AddListener<T>(GameEvent gameEvent, Callback<T> handler, bool isPermanent = true)
        {
            int index = (int)gameEvent;

            OnListenerAdding(index, handler);
            eventTable[index] = (Callback<T>)eventTable[index] + handler;
            if (isPermanent)
            {
                permanentMessages.Add(index);
            }
        }

        //Two parameters
        static public void AddListener<T, U>(GameEvent gameEvent, Callback<T, U> handler, bool isPermanent = true)
        {
            int index = (int)gameEvent;

            OnListenerAdding(index, handler);
            eventTable[index] = (Callback<T, U>)eventTable[index] + handler;
            if (isPermanent)
            {
                permanentMessages.Add(index);
            }
        }

        //Three parameters
        static public void AddListener<T, U, V>(GameEvent gameEvent, Callback<T, U, V> handler, bool isPermanent = true)
        {
            int index = (int)gameEvent;

            OnListenerAdding(index, handler);
            eventTable[index] = (Callback<T, U, V>)eventTable[index] + handler;
            if (isPermanent)
            {
                permanentMessages.Add(index);
            }
        }

        #endregion

        #region RemoveListener

        //No parameters
        static public void RemoveListener(GameEvent gameEvent, Callback handler)
        {
            int index = (int)gameEvent;

            OnListenerRemoving(index, handler);
            eventTable[index] = (Callback)eventTable[index] - handler;
        }

        //Single parameter
        static public void RemoveListener<T>(GameEvent gameEvent, Callback<T> handler)
        {
            int index = (int)gameEvent;

            OnListenerRemoving(index, handler);
            eventTable[index] = (Callback<T>)eventTable[index] - handler;
        }

        //Two parameters
        static public void RemoveListener<T, U>(GameEvent gameEvent, Callback<T, U> handler)
        {
            int index = (int)gameEvent;

            OnListenerRemoving(index, handler);
            eventTable[index] = (Callback<T, U>)eventTable[index] - handler;
        }

        //Three parameters
        static public void RemoveListener<T, U, V>(GameEvent gameEvent, Callback<T, U, V> handler)
        {
            int index = (int)gameEvent;

            OnListenerRemoving(index, handler);
            eventTable[index] = (Callback<T, U, V>)eventTable[index] - handler;
        }

        #endregion

        #region Broadcast

        //No parameters
        static public void Broadcast(GameEvent gameEvent)
        {
            int index = (int)gameEvent;

            if (eventTable[index] != null)
                ((Callback)eventTable[index])?.Invoke();
        }

        //Single parameter
        static public void Broadcast<T>(GameEvent gameEvent, T arg1)
        {
            int index = (int)gameEvent;

            if (eventTable[index] != null)
                ((Callback<T>)eventTable[index])?.Invoke(arg1);
        }

        //Two parameters
        static public void Broadcast<T, U>(GameEvent gameEvent, T arg1, U arg2)
        {
            int index = (int)gameEvent;

            if (eventTable[index] != null)
                ((Callback<T, U>)eventTable[index])?.Invoke(arg1, arg2);
        }

        //Three parameters
        static public void Broadcast<T, U, V>(GameEvent gameEvent, T arg1, U arg2, V arg3)
        {
            int index = (int)gameEvent;

            if (eventTable[index] != null)
                ((Callback<T, U, V>)eventTable[index])?.Invoke(arg1, arg2, arg3);
        }

        #endregion

        #region Messenger behaviour

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void RuntimeInit()
        {
            SceneManager.sceneLoaded += SceneLoadedCallback;
        }

        static void SceneLoadedCallback(Scene scene, LoadSceneMode mode)
        {
            // Clear event table every time scene changed
            Cleanup();
        }

        #endregion
    }
}