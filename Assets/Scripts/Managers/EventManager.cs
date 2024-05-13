using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, Action<Dictionary<string, object>>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager Inst {
        get {
            if (!eventManager) {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager) {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else {
                    eventManager.Init();

                    //  Sets this to not be destroyed when reloading scene
                    DontDestroyOnLoad(eventManager);
                }
            }
            return eventManager;
        }
    }

    void Init() {
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
        }
    }

    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener) {
        Action<Dictionary<string, object>> thisEvent;

        if (Inst.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent += listener;
            Inst.eventDictionary[eventName] = thisEvent;
        }
        else {
            thisEvent += listener;
            Inst.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<Dictionary<string, object>> listener) {
        if (eventManager == null) return;
        Action<Dictionary<string, object>> thisEvent;
        if (Inst.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent -= listener;
            Inst.eventDictionary[eventName] = thisEvent;
        }
    }

    /// <summary>
    /// Usage: For Quests:: EventManager.TriggerObjective(Objective.[typeNotifier] + [relevant name], [relevant obj/num]);
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="message"></param>
    public static void TriggerEvent(string eventName, Dictionary<string, object> message) {
        Action<Dictionary<string, object>> thisEvent = null;
        if (Inst.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke(message);
        }
    }

    public static void TriggerEvent(string eventName, string message, object obj) {
        Action<Dictionary<string, object>> thisEvent = null;
        if (Inst.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent.Invoke(new Dictionary<string, object> { { message, obj } });
        }
    }

    public static void TriggerObjective(string identifier, object val) {
        TriggerEvent(Objective.StringNotifier, identifier, val);
	}
}