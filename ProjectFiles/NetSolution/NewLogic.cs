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





public class NewLogic : BaseNetLogic

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


        var todaydateVar = LogicObject.GetVariable("TodayDate");
        todaydateVar.Value = DateTime.Today;

        
        var tomorrowdateVar = LogicObject.GetVariable("TomorrowDate");
        tomorrowdateVar.Value = DateTime.Today.AddDays(1);

    }

    private PeriodicTask periodicTask;
}
