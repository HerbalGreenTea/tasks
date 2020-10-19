using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures
{
    public class Common
    {
        //public static int Earlier(object[] v, int day, int month, int year)
        //{
        //    int vYear = (int)v[2];
        //    int vMonth = (int)v[1];
        //    int vDay = (int)v[0];
        //    if (vYear < year) return 1;
        //    if (vYear > year) return 0;
        //    if (vMonth < month) return 1;
        //    if (vMonth > month) return 0;
        //    if (vDay < day) return 1;
        //    return 0;
        //}

        public static int Earlier(Date dateDev, Date dateGlobal)
        {
            if (dateDev.Year < dateGlobal.Year) return 1;
            if (dateDev.Year > dateGlobal.Year) return 0;
            if (dateDev.Month < dateGlobal.Month) return 1;
            if (dateDev.Month > dateGlobal.Month) return 0;
            if (dateDev.Day < dateGlobal.Day) return 1;
            return 0;
        }
    }

    public class ReportMaker
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="failureTypes">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes, 
            int[] deviceId, 
            object[][] times,
            List<Dictionary<string, object>> devices)
        {

            //var problematicDevices = new HashSet<int>();
            //for (int i = 0; i < failureTypes.Length; i++)
            //    if (Common.IsFailureSerious(failureTypes[i])==1 && Common.Earlier(times[i], day, month, year)==1)
            //        problematicDevices.Add(deviceId[i]);

            //var result = new List<string>();
            //foreach (var device in devices)
            //    if (problematicDevices.Contains((int)device["DeviceId"]))
            //        result.Add(device["Name"] as string);
            ////////////////////////////////////////////////////////////////////////////////

            var date = new Date(year, month, day);
            var listDevices = new List<Device>();

            foreach (var device in devices)
            {
                var dev = new Device((int)device["DeviceId"], device["Name"] as string);
                listDevices.Add(dev);
            }

            for (int i = 0; i < failureTypes.Length; i++)
            {
                var fail = new Failure(new Date((int)times[i][2], (int)times[i][1], (int)times[i][0]), (Type)failureTypes[i]);

                foreach (var dev in listDevices)
                {
                    if (dev.DeviceID == deviceId[i])
                    {
                        dev.Failure = fail;
                        break;
                    }
                }
            }

            return FindDevicesFailedBeforeDate(listDevices, date);
        }

        public static List<string> FindDevicesFailedBeforeDate(List<Device> devices, Date date)
        {
            var problematicDevices = new List<string>();

            foreach(var dev in devices)
            {
                if (Failure.IsFailureSerious((int)dev.Failure.FailureType) == 1 && Common.Earlier(dev.Failure.TimeFailure, date) == 1)
                    problematicDevices.Add(dev.NameDevice);
            }

            return problematicDevices;
        }
    }

    public class Date
    {
        public readonly int Year;
        public readonly int Month;
        public readonly int Day;

        public Date(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }
    }

    public class Device
    {
        public readonly int DeviceID;
        public readonly string NameDevice;
        public Failure Failure { get; set; }

        public Device(int deviceID, string name)
        {
            DeviceID = deviceID;
            NameDevice = name;
        }
    }

    public class Failure
    {
        public readonly Date TimeFailure;
        public readonly Type FailureType;

        public Failure(Date date, Type failureType)
        {
            TimeFailure = date;
            FailureType = failureType;
        }

        public static int IsFailureSerious(int failureType)
        {
            if (failureType % 2 == 0) return 1;
            return 0;
        }
    }

    public enum Type
    {
        unexpectedShutdown = 0,
        shortNonResponding = 1,
        hardwareFailures = 2,
        connectionProblems = 3
    }
}
