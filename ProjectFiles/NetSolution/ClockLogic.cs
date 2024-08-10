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
using FTOptix.MicroController;
#endregion

public class ClockLogic : BaseNetLogic
{
    public override void Start()
    {
        periodicTask = new PeriodicTask(UpdateTime, 1000, LogicObject);
        periodicTask.Start();
    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
    }

    private void UpdateTime()
    {
        var timeVar = LogicObject.GetVariable("Time");
        timeVar.Value = DateTime.Now;

        var timeoldVar = LogicObject.GetVariable("TimeOld");
        timeoldVar.Value = DateTime.Now.AddDays(-1);

        var timenewVar = LogicObject.GetVariable("TimeNew");
        timenewVar.Value = DateTime.Now.AddDays(+1);
    }

    private PeriodicTask periodicTask;
}
