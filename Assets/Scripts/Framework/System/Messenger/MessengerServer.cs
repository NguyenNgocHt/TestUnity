using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;

namespace Framework
{
    public static class ServerMessenger
    {
        #region Internal variables

        public static Dictionary<GameServerEvent, Delegate> eventTable = new Dictionary<GameServerEvent, Delegate>() { 
                
        };

        //Message handlers (index) that should never be removed, regardless of calling Cleanup
        private static List<int> permanentMessages = new List<int>();

        #endregion

        #region Helper methods

        //Marks a certain message as permanent.
        public static void MarkAsPermanent(GameServerEvent gameServerEvent)
        {
            permanentMessages.Add((int)gameServerEvent);
        }

        public static void Cleanup()
        {
            foreach (var _event in eventTable)
            {
                if (!permanentMessages.Contains((int)_event.Key))
                    eventTable[_event.Key] = null;
            }
        }

        public static void PrintEventTable()
        {
            PDebug.Log("\t\t\t=== MESSENGER PrintEventTable ===");

            foreach (var _event in eventTable)
            {
                PDebug.Log("Event:{0}|{1}", _event.Key, _event);
            }

            PDebug.Log("\n");
        }

        #endregion

        #region Message logging and exception throwing

        static void OnListenerAdding(GameServerEvent _event, Delegate listenerBeingAdded)
        {
            Delegate d = eventTable[_event];
            if (d != null && d.GetType() != listenerBeingAdded.GetType())
            {
                PDebug.LogError("Attempting to add listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being added has type {2}", _event, d.GetType().Name, listenerBeingAdded.GetType().Name);
            }
        }

        static void OnListenerRemoving(GameServerEvent _event, Delegate listenerBeingRemoved)
        {
            Delegate d = eventTable[_event];

            if (d == null)
            {
                PDebug.LogWarning("Attempting to remove listener with for event type \"{0}\" but current listener is null.", _event);
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                PDebug.LogError("Attempting to remove listener with inconsistent signature for event type {0}. Current listeners have type {1} and listener being removed has type {2}", _event, d.GetType().Name, listenerBeingRemoved.GetType().Name);
            }
        }

        static void OnBroadcasting(int eventIndex, Delegate broadcastMessage)
        {
            PDebug.LogError("Broadcasting message \"{0}\" but listeners have a different signature than the broadcaster.", (int)eventIndex);
        }

        #endregion

        #region AddListener

        //No parameters
        static public void AddListener(GameServerEvent gameServerEvent, Callback handler, bool isPermanent = true)
        {
            if (eventTable.ContainsKey(gameServerEvent))
            {
                eventTable[gameServerEvent] = (Callback)eventTable[gameServerEvent] + handler;
            }
            else
            {
                eventTable.TryAdd(gameServerEvent, handler);
            }
            OnListenerAdding(gameServerEvent, handler);
            if (isPermanent)
            {
                permanentMessages.Add((int)gameServerEvent);
            }
        }

        //Single parameter
        static public void AddListener<T>(GameServerEvent gameServerEvent, Callback<T> handler, bool isPermanent = true)
        {
            int index = (int)gameServerEvent;

            if (eventTable.ContainsKey(gameServerEvent))
            {
                eventTable[gameServerEvent] = (Callback<T>)eventTable[gameServerEvent] + handler;
            }
            else
            {
                eventTable.TryAdd(gameServerEvent, handler);
            }
            OnListenerAdding(gameServerEvent, handler);
            if (isPermanent)
            {
                permanentMessages.Add(index);
            }
        }

        //Two parameters
        static public void AddListener<T, U>(GameServerEvent gameServerEvent, Callback<T, U> handler, bool isPermanent = true)
        {
            int index = (int)gameServerEvent;

            if (eventTable.ContainsKey(gameServerEvent))
            {
                eventTable[gameServerEvent] = (Callback<T, U>)eventTable[gameServerEvent] + handler;
            }
            else
            {
                eventTable.TryAdd(gameServerEvent, handler);
            }
            OnListenerAdding(gameServerEvent, handler);
            if (isPermanent)
            {
                permanentMessages.Add(index);
            }
        }

        //Three parameters
        static public void AddListener<T, U, V>(GameServerEvent gameServerEvent, Callback<T, U, V> handler, bool isPermanent = true)
        {
            int index = (int)gameServerEvent;

            if (eventTable.ContainsKey(gameServerEvent))
            {
                eventTable[gameServerEvent] = (Callback<T, U, V>)eventTable[gameServerEvent] + handler;
            }
            else
            {
                eventTable.TryAdd(gameServerEvent, handler);
            }
            OnListenerAdding(gameServerEvent, handler);
            if (isPermanent)
            {
                permanentMessages.Add(index);
            }
        }

        #endregion

        #region RemoveListener

        //No parameters
        static public void RemoveListener(GameServerEvent gameServerEvent, Callback handler)
        {
            int index = (int)gameServerEvent;

            OnListenerRemoving(gameServerEvent, handler);
            eventTable[gameServerEvent] = (Callback)eventTable[gameServerEvent] - handler;
        }

        //Single parameter
        static public void RemoveListener<T>(GameServerEvent gameServerEvent, Callback<T> handler)
        {
            int index = (int)gameServerEvent;

            OnListenerRemoving(gameServerEvent, handler);
            eventTable[gameServerEvent] = (Callback<T>)eventTable[gameServerEvent] - handler;
        }

        //Two parameters
        static public void RemoveListener<T, U>(GameServerEvent gameServerEvent, Callback<T, U> handler)
        {
            int index = (int)gameServerEvent;

            OnListenerRemoving(gameServerEvent, handler);
            eventTable[gameServerEvent] = (Callback<T, U>)eventTable[gameServerEvent] - handler;
        }

        //Three parameters
        static public void RemoveListener<T, U, V>(GameServerEvent gameServerEvent, Callback<T, U, V> handler)
        {
            int index = (int)gameServerEvent;

            OnListenerRemoving(gameServerEvent, handler);
            eventTable[gameServerEvent] = (Callback<T, U, V>)eventTable[gameServerEvent] - handler;
        }

        #endregion

        #region Broadcast

        //No parameters
        static public void Broadcast(GameServerEvent gameServerEvent)
        {
            if (eventTable[gameServerEvent] != null)
                ((Callback)eventTable[gameServerEvent])?.Invoke();
        }

        //Single parameter
        static public void Broadcast<T>(GameServerEvent gameServerEvent, T arg1)
        {
            int index = (int)gameServerEvent;

            if (eventTable[gameServerEvent] != null)
                ((Callback<T>)eventTable[gameServerEvent])?.Invoke(arg1);
        }

        //Two parameters
        static public void Broadcast<T, U>(GameServerEvent gameServerEvent, T arg1, U arg2)
        {
            int index = (int)gameServerEvent;

            if (eventTable[gameServerEvent] != null)
                ((Callback<T, U>)eventTable[gameServerEvent])?.Invoke(arg1, arg2);
        }

        //Three parameters
        static public void Broadcast<T, U, V>(GameServerEvent gameServerEvent, T arg1, U arg2, V arg3)
        {
            int index = (int)gameServerEvent;

            if (eventTable[gameServerEvent] != null)
                ((Callback<T, U, V>)eventTable[gameServerEvent])?.Invoke(arg1, arg2, arg3);
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