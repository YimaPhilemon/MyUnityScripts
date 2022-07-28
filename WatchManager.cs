using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WatchManager : MonoBehaviour
{

     [Serializable]
    public class count
    {
        public float Sec;
        public float Hr;
        public float Min;
        public Text TextSec;
        public Text TextHr;
        public Text TextMin;
        public GameObject Stopbt;
        public GameObject Resetbt;
        public GameObject play;
		public GameObject stop;
		public GameObject buttons;
        public GameObject playbt;


    }
    public count CD;

    public  float countdownSec;
    public float countdownMin;
    public float countdownH;

    public float StopwatchSec;
    public float StopwatchMin;
    public float StopwatchH;

    public Text StopwatchSecText;
    public Text StopwatchMinText;

    public bool stopwatchchecker = false;
    public GameObject Stopbt;
    public GameObject Lapbt;
    public GameObject play;
    public GameObject stop;
	public Button Lap;

    public bool counterchecker = false;
    public GameObject TimerAlarm;
    public GameObject Timer;
    //public float timeElapseSec;
    //public float timeElapseMin;
    //public Text ElapeSec;
    //public Text ElapeMin;

    public GameObject lap;
    public GameObject content;

    int index = 0;
    public Text num;
	public AudioSource click;
	public AudioSource tick;
	public AudioSource Alert;
	

	private void Awake()
    {
        WatchChecker();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

		Stopwatch();
		Counter();

		
           
           
        

        
            StopwatchSecText.text = StopwatchSec.ToString("00.00");
        
        
            StopwatchMinText.text = StopwatchMin.ToString("00") + ":";
        

        

       
            CD.TextHr.text = CD.Hr.ToString("00");
        

        
            CD.TextMin.text = CD.Min.ToString("00");
        

       
            CD.TextSec.text = CD.Sec.ToString("00");
        

        

        if(counterchecker == true)
        {
            CD.Sec = countdownSec;
            CD.Min = countdownMin;
            CD.Hr = countdownH;

        }

        if(CD.Hr==0 && CD.Min==0 && CD.Sec==0)
        {
            CD.playbt.SetActive(false);
            CD.Stopbt.SetActive(false);
			CD.Resetbt.SetActive(false);
		}
		else
        {
            CD.playbt.SetActive(true);
            CD.Stopbt.SetActive(true);
			CD.Resetbt.SetActive(true);
		}

        if(counterchecker == true && countdownH <= 0 && countdownMin <= 0 && countdownSec <= 0)
        {
            TimerAlarm.SetActive(true);
            //Timer.SetActive(false);
			Alert.Play();
			CD.buttons.SetActive(false);
			CD.Hr = 0;
			CD.Min = 0;
			CD.Sec = 0;

			counterchecker = false;


			/* (timeElapseSec >= 0)
                {
                    timeElapseSec+=Time.deltaTime;
                   
                }
        
            
                    if(timeElapseSec >= 59)
                    {
                        timeElapseMin+=1f;
                        timeElapseSec=0f;
                    }	 */



		}
        
          /*  ElapeSec.text = timeElapseSec.ToString("00");
        

        if (timeElapseMin < 10) {
            ElapeMin.text = "0" + timeElapseMin.ToString("0");
        }

        else if (timeElapseMin > 9) 
        {
            ElapeMin.text = timeElapseMin.ToString("0");
        }*/

        num.text = StopwatchMin.ToString("00") + ":" + StopwatchSec.ToString("00.00");	
    }	  

    public void StartStopWatch()
    {
        	if(stopwatchchecker == false)
		{
			stopwatchchecker = true;
			stop.SetActive(false);
			play.SetActive(true);
			Lap.interactable = true;
			click.Play();
			//Stopbt.SetActive(false);
			//Lapbt.SetActive(true);
		}
            
            
    }

	public void StopStopwatch()
	{
		if (stopwatchchecker == true)
		{
			stopwatchchecker = false;
			stop.SetActive(true);
			play.SetActive(false);
			Lap.interactable = false;
			click.Play();
			//Stopbt.SetActive(true);
			//Lapbt.SetActive(false);
		}
	}

    public void StopWatchReset()
    {
        StopwatchH = 00.00F;
        StopwatchSec = 00.00F;
        StopwatchMin = 00.00F;
		stopwatchchecker = false;
		//Stopbt.SetActive(false);
		stop.SetActive(false);
		play.SetActive(false);
		Lap.interactable = false;
		click.Play();
		foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        index = 0;
    }

    public void AddHr()
    {
        CD.Hr++;
		tick.Play();
		if (CD.Hr > 99)
        {
            CD.Hr = 00;
        }
            
    }

     public void SubHr()
    {
        CD.Hr--;
		tick.Play();

		if (CD.Hr < 00)
        {
            CD.Hr = 99;
        }
            
    }

     public void AddMin()
    {
        CD.Min++;
		tick.Play();
		if (CD.Min > 59)
        {
            CD.Min = 00;
        }
            
    }

     public void SubMin()
    {
        CD.Min--;
		tick.Play();

		if (CD.Min < 00)
        {
            CD.Min = 59;
        }
            
    }

    public void AddSec()
    {
        CD.Sec++;
		tick.Play();
		if (CD.Sec > 59)
        {
            CD.Sec = 00;
        }
            
    }

     public void SubSec()
    {
        CD.Sec--;
		tick.Play();

        if(CD.Sec < 00)
        {
            CD.Sec = 59;
        }
            
    }

    public void StartCounter()
    {
		if(counterchecker ==  false)
		{
		   counterchecker = true;
            CD.play.SetActive(true);
            CD.stop.SetActive(false);
            CD.buttons.SetActive(false);
            CD.Stopbt.SetActive(true);
            countdownH = CD.Hr;
            countdownMin = CD.Min;
            countdownSec = CD.Sec;
			click.Play();
			//Alert.Play();
		}
            

            
            
        
    }

	public void StopCounter()
	{
		if (counterchecker == true)
		{
			counterchecker = false;
			CD.play.SetActive(false);
			CD.stop.SetActive(true);
			click.Play();
			//Alert.Stop();
		}
	}

    public void ResetCounter()
    {
		
			counterchecker = false;
			CD.buttons.SetActive(true);
			CD.Stopbt.SetActive(false);
			CD.play.SetActive(false);
			CD.stop.SetActive(false);
			CD.Hr = 0;
			CD.Min = 0;
			CD.Sec = 0;
			//timeElapseSec = 0;
			//timeElapseMin = 0;
		click.Play();
		//Alert.Stop();


	}

    public void WatchChecker()
    {
        float cs = countdownSec;
        float ch = countdownH;
        float cm = countdownMin;
    }

    public void AddLap()
    {
		if(stopwatchchecker == true)
		{
			var copy = Instantiate(lap);
			copy.transform.parent = content.transform;
			copy.transform.localPosition = Vector3.zero;

			copy.GetComponentInChildren<Text>().text = (index + 1).ToString();

			index++;
			click.Play();
			//copy.GameObject.Find("time").GetComponentInChildren<Text>().text= (index).ToString();
		}
       
        
    }

	public void Stopwatch()
	{
		if (stopwatchchecker == true)
		{
			if (StopwatchSec < 60f)
			{
				StopwatchSec += Time.deltaTime;

			}

			if (StopwatchSec > 59f)
			{
				StopwatchMin += 1f;
				StopwatchSec = 0f;
			}

			if (StopwatchMin > 59f)
			{
				StopwatchH += 1f;
				StopwatchMin = 0f;
			}


		}
	}

	public void Counter()
	{
		if (counterchecker == true)
		{
			if (countdownSec >= 0)
			{
				countdownSec -= Time.deltaTime;

			}

			if (countdownMin > 0)
			{
				if (countdownSec <= 0)
				{
					countdownMin -= 1f;
					countdownSec = 59f;
				}


			}

			if (countdownH > 0)
			{
				if (countdownMin <= 0)
				{
					countdownH -= 1f;
					countdownMin = 59f;
				}


			}

		}
	}

	public void DissmissCountdown()
	{
		counterchecker = false;
		CD.buttons.SetActive(true);
		CD.Stopbt.SetActive(false);
		CD.play.SetActive(false);
		CD.stop.SetActive(false);
		CD.Hr = 0;
		CD.Min = 0;
		CD.Sec = 0;
		//timeElapseSec = 0;
		//timeElapseMin = 0;
		click.Play();
		Alert.Stop();
		TimerAlarm.SetActive(false);
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause == true)
		{
			Stopwatch();
			Counter();
		}
	}
}
