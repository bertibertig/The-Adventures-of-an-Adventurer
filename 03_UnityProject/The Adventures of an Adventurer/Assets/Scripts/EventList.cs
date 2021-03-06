﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventList : MonoBehaviour {

    public class Event
    {
        private int id;
        private string eventName;
        private bool hasHappened;
        private string description;

        public int ID { get { return this.id; } set { this.id = value; } }
        public string EventName { get { return this.eventName; } set { this.eventName = value; } }
        public bool HasHappened { get { return this.hasHappened; } set { this.hasHappened = value; } }
        public string Description { get { return this.description; } set { this.description = value; } }
    }

    private List<Event> events;

    public List<Event> GetEvents { get { return this.events; } }

    public float PreviousLevel { get; set; }
    public Vector3 PlayerPositionForNewLevel { get; set; }

    void Start()
    {
        events = new List<Event>();
        PreviousLevel = 0;
    }
	// Use this for initialization
	void Awake () {
        if (GameObject.FindGameObjectsWithTag("EventList").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("EventList")[1]);
        }
	}

    public void AddEvent(string eventName, bool happened = true, string description = "")
    {
        bool eventAlreadyContained = false;
        foreach (Event e in events)
        {
            if (e.EventName == eventName)
                eventAlreadyContained = true;
            
        }
        if (!eventAlreadyContained)
        {
            int index = events.Count;
            events.Add(new Event());
            events[index].ID = index;
            events[index].EventName = eventName;
            events[index].HasHappened = happened;
            events[index].Description = description;
        }
    }

    public Event GetEvent(int id)
    {
        if (id >= events.Count)
            return null;
        return events[id];
    }

    public Event GetEvent(string eventName)
    {
        if (events != null)
        {
            foreach (Event e in events)
            {
                if (e.EventName == eventName)
                    return e;

            }
        }
        return null;
    }

    public int GetEventID(string eventName)
    {
        foreach (Event e in events)
        {
            if (e.EventName == eventName)
                return e.ID;

        }
        return 0;
    }

    public void ListAllEvents()
    {
        StartCoroutine("ListAllEventsCoroutine");
    }

    private IEnumerator ListAllEventsCoroutine()
    {
        foreach(Event e in events)
        {
            print("Name: " + e.EventName + " Happened: " + e.HasHappened + " Description: " + e.Description);
            yield return null;
        }
    }
}
