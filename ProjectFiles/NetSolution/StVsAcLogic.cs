#region Using directives
using System;
using UAManagedCore;
using OpcUa = UAManagedCore.OpcUa;
using FTOptix.DataLogger;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.ODBCStore;
using FTOptix.Report;
using FTOptix.RAEtherNetIP;
using FTOptix.Retentivity;
using FTOptix.CommunicationDriver;
using FTOptix.Core;
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.MicroController;
#endregion

public class StVsAcLogic : BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started

        
       
      
        periodicTask = new PeriodicTask(IncrementDecrementTask, 60, LogicObject);
        periodicTask.Start();


    }

    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped

        periodicTask.Dispose();
        periodicTask = null;
    }
    public void IncrementDecrementTask()
    {

        var actualProductionVar = LogicObject.GetVariable("ActualProduction");
        var standardProductionVar = LogicObject.GetVariable("StandardProduction");
        var stvsACVar = LogicObject.GetVariable("StVsAc");
        var buttongVar = LogicObject.GetVariable("Buttong");
        float stvsac = stvsACVar.Value;
        int actualp = actualProductionVar.Value;
        int standardp = standardProductionVar.Value;
        bool buttonG = buttongVar.Value;


        if (buttonG == true) 
        { 
            stvsac = (actualp * 100) / standardp ;

        }
        stvsACVar.Value = stvsac;

    }
    private PeriodicTask periodicTask;
}
