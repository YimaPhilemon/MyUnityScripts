using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SavedAlarm : MonoBehaviour
{
    [Serializable]
    public class MyAlarms
    {
        public string AlarmName;
        public string AlarmDesign;
        public int AlarmHour;
        public int AlarmMinutes;

    }
    public MyAlarms alarms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
