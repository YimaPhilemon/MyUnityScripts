using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DeleteAlarm : MonoBehaviour
{
   public AlarmManager AM;
   public int child;
   public int count;
   public Toggle toggle;
   public bool check;
   
   void Start()
   {
       
        toggle = transform.Find("Toggle").GetComponent<Toggle>();
           
            
         
       
       
   }
   void FixedUpdate()
   {
       
        child = transform.GetSiblingIndex();
        count = AM.alarms.AlarmName.Count;
         try
            {
                AM.alarms.AlarmSet[child] = PlayerPrefs.GetInt("AlarmSet" + child);
                 if(AM.alarms.AlarmSet[child] == 1)
                {
                    toggle.isOn = true;
                    check = true;
                }
                if(AM.alarms.AlarmSet[child] == 0)
                {
                    toggle.isOn = false;
                    check = false;
                }

            }
        catch(ArgumentOutOfRangeException ar)
            {
                Debug.Log("main button");
            }
    }

   public void Delete()
   {
       int delete = transform.GetSiblingIndex();
       DeleteA(delete);

        AM.alarms.AlarmHour.Clear();
        AM.alarms.AlarmMinutes.Clear();
        AM.alarms.AlarmDesign.Clear();
        AM.alarms.AlarmName.Clear();
        AM.alarms.AlarmSet.Clear();
        foreach (Transform child in AM.content.transform)
        {
            Destroy(child.gameObject);
        }
       AM.LoadAlarm();
       
   }

    void DeleteA(int index)
    {
        
               
          
        

       
             for (int i = index; i < count-1; i++)
          {
            AM.alarms.AlarmName[i] = AM.alarms.AlarmName[i+1];
            AM.alarms.AlarmDesign[i] = AM.alarms.AlarmDesign[i+1];
            AM.alarms.AlarmHour[i] = AM.alarms.AlarmHour[i+1];
            AM.alarms.AlarmMinutes[i] = AM.alarms.AlarmMinutes[i+1];
            AM.alarms.AlarmSet[i] = AM.alarms.AlarmSet[i+1];

            

            PlayerPrefs.SetString("AlarmNames" + i, AM.alarms.AlarmName[i+1]);
            PlayerPrefs.SetString("AlarmDesigns" + i, AM.alarms.AlarmDesign[i+1]);
            PlayerPrefs.SetInt("AlarmMinutes" + i, AM.alarms.AlarmMinutes[i+1]);
            PlayerPrefs.SetInt("AlarmHours" + i, AM.alarms.AlarmHour[i+1]);
            PlayerPrefs.SetInt("AlarmSet" + i, AM.alarms.AlarmSet[i+1]);

            
        }  
            count = count-1;

            /*int setindex = count-1;
            
            AM.alarms.AlarmName.RemoveAt(setindex);
            AM.alarms.AlarmMinutes.RemoveAt(setindex);
            AM.alarms.AlarmDesign.RemoveAt(setindex);
            AM.alarms.AlarmHour.RemoveAt(setindex);
            AM.alarms.AlarmSet.RemoveAt(setindex);

            PlayerPrefs.DeleteKey("AlarmNames" + setindex);
            PlayerPrefs.DeleteKey("AlarmDesigns" + setindex);
            PlayerPrefs.DeleteKey("AlarmMinutes" + setindex);
            PlayerPrefs.DeleteKey("AlarmHours" + setindex);
            PlayerPrefs.DeleteKey("AlarmSet" + setindex);*/

            PlayerPrefs.SetInt("CountAlarmName", count);
            
        
        

        Destroy(gameObject);
     
    }
    
    public void AlarmActive()
    {
        if(check == true)
        {
            AM.alarms.AlarmSet[child] = 0;
            toggle.isOn = false;
            PlayerPrefs.SetInt("AlarmSet" + child, AM.alarms.AlarmSet[child]);
            check = false;
        }else

        if(check == false)
        {
            AM.alarms.AlarmSet[child] = 1;
            toggle.isOn = true;
            PlayerPrefs.SetInt("AlarmSet" + child, AM.alarms.AlarmSet[child]);
            check = true;
        }
    }
}
