#region Using directives
using FTOptix.NetLogic;
using System;
using UAManagedCore;
using FTOptix.UI;
using FTOptix.EventLogger;
using FTOptix.Store;
using FTOptix.SQLiteStore;
using FTOptix.AuditSigning;
using FTOptix.Alarm;
using FTOptix.RAEtherNetIP;
using FTOptix.CommunicationDriver;
using FTOptix.Modbus;
using FTOptix.DataLogger;
using FTOptix.Recipe;
using FTOptix.Report;
using FTOptix.WebUI;
using FTOptix.ODBCStore;
using System.Buffers;
using FTOptix.MicroController;
#endregion

public class DateLogic : BaseNetLogic

{
    
    public override void Start()
    {
        periodicTask = new PeriodicTask(UpdateDateTime, 1000, LogicObject);
        periodicTask.Start();
    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
    }

    private void UpdateDateTime()
    {
        
        
        var dateVar = LogicObject.GetVariable("Date");
        dateVar.Value = DateTime.Today.ToString("yyyy-MM-dd 00:00:00");
        var startdate = LogicObject.GetVariable("startDate");
        startdate.Value = dateVar.Value;

        var tomorrowDateVar = LogicObject.GetVariable("TomorrowDate");
        tomorrowDateVar.Value = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd 00:00:00"); ;// Current date + 1 day
        var enddate = LogicObject.GetVariable("endDate");
        enddate.Value = tomorrowDateVar.Value;

        var yesterdayDateVar = LogicObject.GetVariable("yesterdayDate");
        yesterdayDateVar.Value = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"); ; // Current date - 1 day, time set to midnight (00:00:01)

    }

    private PeriodicTask periodicTask;
}
