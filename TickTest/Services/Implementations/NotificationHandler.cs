﻿using LightForms;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;

namespace TickTest.Services
{
    public class NotificationHandler : INotificationHandler
    {
        public void Init()
        {
            CrossContainer.Instance.Create<ILocalNotification>().NotificationReceived += App_NotificationReceived;
        }

        private void App_NotificationReceived(object sender, Dictionary<string, string> data)
        {
            try
            {
                if (!int.TryParse(data[LocalNotificationConstants.IdKey], out int id)) return;
                if (!int.TryParse(data[LocalNotificationConstants.ActionKey], out int idaction)) return;
                CrossContainer.Instance.Create<ILocalNotification>().Clear(id);
                var action = new EnumFactory<NotificationAction, INotificationAction>(ServiceHandler.ServicesNameSpace, "Action")
                    .Resolve((NotificationAction)idaction)
                    .Open();
            }
            catch (Exception ex) { Crashes.TrackError(ex); }
        }
    }
}