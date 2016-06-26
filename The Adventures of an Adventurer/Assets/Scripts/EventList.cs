using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventList : MonoBehaviour {

    public class Event
    {
        private int id;
        private string eventName;
        private bool hasHappened;

        public int ID { get { return this.id; } set { this.id = value; } }
        public string EventName { get { return this.eventName; } set { this.eventName = value; } }
        public bool HasHappened { get { return this.hasHappened; } set { this.hasHappened = value; } }
    }

    private List<Event> events;

    public List<Event> GetEvents { get { return this.events; } }

    void Start()
    {
        events = new List<Event>();
    }
	// Use this for initialization
	void Awake () {
        if (GameObject.FindGameObjectsWithTag("EventList").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("EventList")[1]);
        }

	}

    public void AddEvent(string eventName, bool happened)
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
        }
    }

    public Event GetEvent(int id)
    {
        return events[id];
    }

    public Event GetEvent(string eventName)
    {
        foreach (Event e in events)
        {
            if (e.EventName == eventName)
                return e;

        }
        return null;
    }
}
