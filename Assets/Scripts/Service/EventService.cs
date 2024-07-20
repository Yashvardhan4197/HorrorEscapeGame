using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventService 
{
    private EventService instance;
    public EventService Instance { get
        {
            if(instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    public EventController LightSwitchToggled;
    public EventService()
    {
        LightSwitchToggled = new EventController();
    }
}
