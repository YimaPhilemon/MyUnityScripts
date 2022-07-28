using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AlarmManager : MonoBehaviour
{
      [Serializable]
    public class MyAlarms
    {
        public List<string> AlarmName = new List<string>();
        public List<string> AlarmDesign = new List<string>(); 
        public List<int> AlarmHour = new List<int>();
        public List<int> AlarmMinutes = new List<int>();
        public List<int> AlarmSet = new List<int>();
        //public bool set;

    }
    public MyAlarms alarms;

      [Serializable]
    public class SnoozeDuration
    {
        public float snoozetimeduration;
        public GameObject checker15;
        public GameObject checker30;
        public GameObject checker45;

    }
    public SnoozeDuration SD;

    public int hours;
    public int minutes;
    public int seconds;
    string design;
    public string h;
    public int HH;
    public int alarmHr;
    public int alarmMm;
    public Text alarmHrText;
    public Text alarmMmText;
    public string alarmDesign;
    public GameObject checkerPM;
    public GameObject checkerAM;
    public GameObject Alarm;
    public GameObject Home;
    public GameObject Settings;
    public GameObject SetAlarmp;
    public GameObject Timep;
    public GameObject MyAlarm;

    public Text AlarmNameField;

    public Text AN;
    public Text AT;
    public GameObject content;
    public GameObject alarmSave;
    public int indexSave;
    public int Savelistcount;
    public int AlarmedSaved;
    public int DeleteIndex;
    //public Text AH;
    //public Text AM;

    public float countdown;
    public float snoozetime;
    public bool snoozee; 
    public int indexCheck;


    
    void Awake ()
    {
        if(Application.isEditor)
        {
            Application.runInBackground = true;
        }
		StartCoroutine("SetAlarmText");

		// alarms.AlarmHour = PlayerPrefs.GetInt("AlarmHour");
		// alarms.AlarmMinutes = PlayerPrefs.GetInt("AlarmMinutes");
		// alarms.AlarmDesign = PlayerPrefs.GetString("AlarmDesign");
		//alarms.AlarmName = PlayerPrefs.GetString("AlarmName");      
	}

    // Start is called before the first frame update
    void Start()
    {
        LoadAlarm();
        countdown = snoozetime = 900.0f;
		//alarmHr = hours;
		//alarmMm = minutes;
		//alarmDesign = design;
		StartCoroutine("SetAlarmText");
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        System.DateTime time = System.DateTime.Now;

        h = time.ToString("hh");
        hours = System.Convert.ToInt32(h);
        minutes = time.Minute;
        seconds = time.Second;
        design = time.ToString("tt");
        HH = time.Hour;
        alarmHrText.text = alarmHr.ToString("D2");
        alarmMmText.text = alarmMm.ToString("D2");
        //AN.text = alarms.AlarmName;
        //AD.text = alarms.AlarmDesign;
        //AH.text = alarms.AlarmHour.ToString();
        //AM.text = alarms.AlarmMinutes.ToString("D2");


        
        CheckAlarm();
		SnoozeManager();


		PlayerPrefs.SetInt("AlarmSaved", Savelistcount);
        
    }

    
    public void AddHr()
    {
        alarmHr++;
        if(alarmHr > 12)
        {
            alarmHr = 1;
        }
            
    }

     public void SubHr()
    {
        alarmHr--;

        if(alarmHr < 1)
        {
            alarmHr = 12;
        }
            
    }

    public void AddMm()
    {
        alarmMm++;
        if(alarmMm > 59)
        {
            alarmMm = 0;
        }
            
    }

     public void SubMm()
    {
        alarmMm--;
        if(alarmMm < 0)
        {
            alarmMm = 59;
        }
   
    }

    public void DesignOperation1()
    {
        if(alarmDesign == ("AM"))
        {
            alarmDesign = ("PM");
            checkerAM.SetActive(false);
            checkerPM.SetActive(true);
        }
    }

    public void DesignOperation2()
    {
        if(alarmDesign == ("PM"))
        {
            alarmDesign = ("AM");
            checkerAM.SetActive(true);
            checkerPM.SetActive(false);
        }
    }

    public void SetAlarm()
    {
        /*alarms.AlarmHour = alarmHr;
        alarms.AlarmMinutes = alarmMm;
        alarms.AlarmDesign  = alarmDesign;
        alarms.AlarmName = AlarmNameField.text.ToString();
        
        PlayerPrefs.SetInt("AlarmHour", alarms.AlarmHour);
        PlayerPrefs.SetInt("AlarmMinutes", alarms.AlarmMinutes);
        PlayerPrefs.SetString("AlarmDesign", alarms.AlarmDesign);
        PlayerPrefs.SetString("AlarmName", alarms.AlarmName); */       
        alarms.AlarmHour.Add(alarmHr);
        alarms.AlarmMinutes.Add(alarmMm);
        alarms.AlarmDesign.Add(alarmDesign); 
        alarms.AlarmName.Add(AlarmNameField.text);
        alarms.AlarmSet.Add(1);
        //alarms.set = true;
        
        for(int i = AlarmedSaved; i < alarms.AlarmName.Count && i < alarms.AlarmHour.Count && i < alarms.AlarmMinutes.Count && i < alarms.AlarmDesign.Count && i < alarms.AlarmSet.Count; i++)
        {
            
            PlayerPrefs.SetString("AlarmNames" + i, alarms.AlarmName[i]);
            PlayerPrefs.SetString("AlarmDesigns" + i, alarms.AlarmDesign[i]);
            PlayerPrefs.SetInt("AlarmMinutes" + i, alarms.AlarmMinutes[i]);
            PlayerPrefs.SetInt("AlarmHours" + i, alarms.AlarmHour[i]);
            PlayerPrefs.SetInt("AlarmSet" + i, alarms.AlarmSet[i]);

            //DeleteAlarm(i);
            indexSave = i;
            

        }

            AN.text = alarms.AlarmName[indexSave];
            AT.text = alarms.AlarmHour[indexSave].ToString("D2") + ":" + alarms.AlarmMinutes[indexSave].ToString("D2") + alarms.AlarmDesign[indexSave];

            var copy = Instantiate(alarmSave);
            copy.transform.SetParent(content.transform);
            copy.transform.localPosition = Vector3.zero;
            AlarmedSaved++;
            DeleteIndex++;
        
        PlayerPrefs.SetInt("CountAlarmName", alarms.AlarmName.Count);
        PlayerPrefs.SetInt("CountAlarmMinutes", alarms.AlarmMinutes.Count);
        PlayerPrefs.SetInt("CountAlarmHour", alarms.AlarmHour.Count);
        PlayerPrefs.SetInt("CountAlarmDesign", alarms.AlarmDesign.Count);
        PlayerPrefs.SetInt("CountSet", alarms.AlarmSet.Count);
        
        PlayerPrefs.SetInt("AlarmSaved", AlarmedSaved);
        PlayerPrefs.Save();

    }

    public void LoadAlarm()
    {
        Savelistcount = PlayerPrefs.GetInt("CountAlarmName");

        for(int i = 0; i < Savelistcount; i++)
        {
            string alarmN = PlayerPrefs.GetString("AlarmNames" + i);
            string alarmD = PlayerPrefs.GetString("AlarmDesigns" + i);
            int alarmM = PlayerPrefs.GetInt("AlarmMinutes" + i);
            int alarmH = PlayerPrefs.GetInt("AlarmHours" + i);
            int alarmS = PlayerPrefs.GetInt("AlarmSet" + i);
            alarms.AlarmHour.Add(alarmH);
            alarms.AlarmMinutes.Add(alarmM);
            alarms.AlarmDesign.Add(alarmD); 
            alarms.AlarmName.Add(alarmN);
            alarms.AlarmSet.Add(alarmS);
            AN.text = alarmN;
            AT.text = alarmH.ToString("D2") + ":" + alarmM.ToString("D2") + alarmD;

            var copy = Instantiate(alarmSave);
            copy.transform.SetParent(content.transform);
            copy.transform.localPosition = Vector3.zero;
            DeleteIndex = i;
            indexSave = i;
        }
        AlarmedSaved = PlayerPrefs.GetInt("AlarmSaved");
    }
    
   

    void CheckAlarm()
    {
        for (int i = 0; i < alarms.AlarmDesign.Count && i < alarms.AlarmHour.Count && i < alarms.AlarmMinutes.Count; i++)
        {
            
            
              if(design.Equals(alarms.AlarmDesign[i]))
        {
            if(hours == alarms.AlarmHour[i] && minutes == alarms.AlarmMinutes[i])
            {
               if(alarms.AlarmSet[i] == 1)
               {
                     Alarm.SetActive(true);
                    Home.SetActive(false);
                    Settings.SetActive(false);
                     MyAlarm.SetActive(false);
                    SetAlarmp.SetActive(false);
                    Timep.SetActive(false);
                    alarms.AlarmSet[i] = 0;
                    indexCheck = i;
                }
           
            }
        }
        
        }
        /*if(design.Equals(alarms.AlarmDesign[i]))
        {
            if(hours == alarms.AlarmHour & minutes == alarms.AlarmMinutes)
            {
               
                     Alarm.SetActive(true);
                    Home.SetActive(false);
                    Settings.SetActive(false);
                     MyAlarm.SetActive(false);
                    SetAlarmp.SetActive(false);
                    Timep.SetActive(false);
                
           
            }
        }*/
    }

    public void Dismissbt()
    {
       alarms.AlarmSet[indexCheck] = 0;
       //alarms.set = false;
    }

    public void Snooze()
    {
       snoozee = true;
    }

    public void Snooze15()
    {
        SD.snoozetimeduration = 900.0f;
        countdown = snoozetime = SD.snoozetimeduration;
        SD.checker15.SetActive(true);
        SD.checker30.SetActive(false);
        SD.checker45.SetActive(false);
        
    }

    public void Snooze30()
    {
        SD.snoozetimeduration = 1800.0f;
        countdown = snoozetime = SD.snoozetimeduration;
        SD.checker15.SetActive(false);
        SD.checker30.SetActive(true);
        SD.checker45.SetActive(false);
        
    }

    public void Snooze45()
    {
        SD.snoozetimeduration = 2700.0f;
        countdown = snoozetime = SD.snoozetimeduration;
        SD.checker15.SetActive(false);
        SD.checker30.SetActive(false);
        SD.checker45.SetActive(true);
        
    }

    IEnumerator SetAlarmText()
    {
        yield return new WaitForSeconds(5);
        alarmHr = hours;
        alarmMm = minutes;
        alarmDesign = design;

    
    }

	void SnoozeManager()
	{
		if (snoozee == true)
		{
			if (countdown > 0)
			{
				countdown -= Time.deltaTime;
				//alarms.set = false;
				alarms.AlarmSet[indexCheck] = 0;
				Alarm.SetActive(false);
				Home.SetActive(true);
			}

			if (countdown < 0)
			{
				Alarm.SetActive(true);
				Home.SetActive(false);
				Settings.SetActive(false);
				MyAlarm.SetActive(false);
				SetAlarmp.SetActive(false);
				Timep.SetActive(false);

				//alarms.set = true;
				alarms.AlarmSet[indexCheck] = 1;
				snoozee = false;
				countdown = snoozetime;
			}
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if(pause == true)
		{
			CheckAlarm();
			SnoozeManager();
		}
	}
    
}
