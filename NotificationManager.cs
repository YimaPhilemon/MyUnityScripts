using UnityEngine;
using System;
#if UNITY_ANDROID
using Unity.Notifications.Android;
using UnityEngine.Android;
#elif UNITY_IOS
using Unity.Notifications.iOS;
#endif

public class NotificationManager : MonoBehaviour
{
	private void Start()
	{
#if UNITY_ANDROID
		if (!Permission.HasUserAuthorizedPermission("android.permission.POST_NOTIFICATIONS"))
		{
			Permission.RequestUserPermission("android.permission.POST_NOTIFICATIONS");
		}

		var channel = new AndroidNotificationChannel()
		{
			Id = "channel_id",
			Name = "Default Channel",
			Importance = Importance.Default,
			Description = "Generic notifications",
		};

		AndroidNotificationCenter.RegisterNotificationChannel(channel);

		SendNotification("Test Notification", "This is a test notification", 10);
#endif
	}

	public void SendNotification(string title, string message, float time)
	{
#if UNITY_ANDROID
		var AndriodNotification = new AndroidNotification()
		{
			Title = title,
			Text = message,
			SmallIcon = "icon",
			LargeIcon = "logo",
			FireTime = DateTime.Now.AddMinutes(time),
		};

		AndroidNotificationCenter.SendNotification(AndriodNotification, "channel_id");

#elif UNITY_IOS
		var timeTrigger = new iOSNotificationTimeIntervalTrigger()
        {
            TimeInterval = TimeSpan.FromMinutes(time),
            Repeats = false
        };

        var notification = new iOSNotification()
        {
            Identifier = "_notification_01",
            Title = title,
            Body = message,
            Subtitle = "Test Subtitle",
            ShowInForeground = true,
            ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
            CategoryIdentifier = "category_id",
            ThreadIdentifier = "thread_id",
            Trigger = timeTrigger,
        };

        iOSNotificationCenter.ScheduleNotification(notification);	 
#endif
	}
}
