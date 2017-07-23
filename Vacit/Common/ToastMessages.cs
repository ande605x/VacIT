using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Vacit.Common
{
    public class ToastMessages
    {

        public DateTime standardScheduledTimeBefore1 { get; set; }
        public DateTime standardScheduledTimeBefore2 { get; set; }
        public DateTime standardScheduledTimeBefore3 { get; set; }
        public DateTime scheduleTestTime { get; set; }




        public static void CreateToastMessage(string line1, string line2, string line3, string imageFilename, DateTime vaccineTime)
        {
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText04);

            XmlNodeList toastStringElements = toastXml.GetElementsByTagName("text");
            toastStringElements[0].AppendChild(toastXml.CreateTextNode(line1));
            toastStringElements[1].AppendChild(toastXml.CreateTextNode(line2));
            toastStringElements[2].AppendChild(toastXml.CreateTextNode(line3));

            String imagePath = "file:///" + imageFilename;// Path.GetFullPath(imageFilename);
            XmlNodeList toastImageElements = toastXml.GetElementsByTagName("image");
            toastImageElements[0].Attributes[1].NodeValue = imagePath;

          

            // FOR TESTING ONLY 7 SECONDS:
            DateTime scheduleTestTime = DateTime.Now.AddSeconds(7);
            ScheduledToastNotification scheduledToastTest = new ScheduledToastNotification(toastXml, scheduleTestTime);
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToastTest);

            // NORMAL SCHEDULING:
            // DateTime scheduledTime1 = DateTime.Now.AddSeconds(7);//vaccineTime - standardScheduledTimeBefore1;
            // ScheduledToastNotification scheduledToast1 = new ScheduledToastNotification(toastXml, scheduledTime1);
            // ToastNotificationManager.CreateToastNotifier().AddToSchedule(scheduledToast1);
        }


    }
}
