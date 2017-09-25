//using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Msit115BestOne.Models;
using Msit115BestOne.Areas.Admin.Models;

namespace Msit115BestOne.Areas.Admin.Service
{
    public class LogService
    {
        public void LogInfo(string AdminName, string SaveData, string Data_Action, string Orignal_Page)
        {
            string ActionName = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("action");
            string ControllersName = HttpContext.Current.Request.RequestContext.RouteData.GetRequiredString("controller");

            AdminLog adminLog = new AdminLog()
            {
                CreateDateTime = DateTime.Now,
                AdminName = AdminName,
                SaveData = SaveData,
                Data_Action = Data_Action,
                Orignal_Page = Orignal_Page,
                ActionName = ActionName,
                ControllersName = ControllersName
            };

            AdminGR<AdminLog> log = new AdminGR<AdminLog>();
            log.LogCreate(adminLog);

            /*
             CREATE TABLE [dbo].[AdminLog]
             (
	             [LogId] [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	             [CreateDateTime] [datetime] NOT NULL,
                 [AdminName] [nvarchar](max) NOT NULL,
                 [SaveData] [nvarchar](max) NOT NULL,
                 [Data_Action] [nvarchar](50) NOT NULL,
                 [Orignal_Page] [nvarchar](50) NOT NULL,
	             [ActionName] [nvarchar](50) NOT NULL,
	             [ControllersName] [nvarchar](50) NOT NULL
             )
             */

            //Logger LogWriter = LogManager.GetCurrentClassLogger();
            //LogWriter.Info("");


            //string LogStr = "Result = " + ResultBool + "; SaveData = " + SaveString +
            //       "; Data_Action = " + actions.ToString() +
            //       "; Orignal_Page = " + pages.ToString() +
            //       "; Statement = " + Statement +
            //       "; Message = " + Message +
            //       "; ControllersName = " + _controllerName +
            //       "; ActionName = " + _actionName;

            //Logger LogWriter = LogManager.GetCurrentClassLogger();
            //LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, "", "Hi");

            //theEvent.Properties["result"] = "true";
            //theEvent.Properties["savedata"] = "SaveString";
            //theEvent.Properties["data_Action"] = "actions.ToString()";
            //theEvent.Properties["Orignal_Page"] = "pages.ToString()";

            //theEvent.Properties["Statement"] = "Statement";
            //theEvent.Properties["ControllersName"] = "_controllerName";
            //theEvent.Properties["ActionName"] = "_actionName";

            //theEvent.Properties["ResultAllStr"] = "LogStr"; //Only For File 

            //LogWriter.Log(theEvent);
        }


        //public void LogCURDInfo(bool ResultBool, List<string> SaveData,
        //    Data_Action actions, Orignal_Page pages,
        //    string Statement, string Message,
        //    string Controllers, string ActionName)
        //{
        //    //string OriginalString = ReturnString(OriginalData);

        //    Logger LogWriter = NLog.LogManager.GetCurrentClassLogger();
        //    LogWriter.Info(" Result = " + ResultBool + "; SaveData = " + SaveString +
        //        //"; OriginalData = "+
        //        //OriginalString + 
        //        "; Data_Action = " + actions.ToString() +
        //        "; Orignal_Page = " + pages.ToString() +
        //        "; Statement = " + Statement +
        //        "; Message = " + Message +
        //        "; ControllersName = " + Controllers +
        //        "; ActionName = " + ActionName);
        //}
    }
}