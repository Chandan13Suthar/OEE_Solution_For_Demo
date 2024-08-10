#region Using directives
using System;
using UAManagedCore;

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
using System.Xml;
using System.Text;
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.MicroController;
using Store = FTOptix.Store;
#endregion

public class TimeDiffObjectLogic : BaseNetLogic
{
 

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var owner = (TimeDiffObject)LogicObject.Owner;
        toVariable = owner.ToVariable;
        fromVariable = owner.FromVariable;
        diffVariable = owner.DiffVariable;
        gbuttonVariable = owner.GButtonVariable;
        actualspeedVariable = owner.ActualSpeedVariable;
        standardspeedVariable = owner.StandardSpeedVariable;
        actualproductionVariable = owner.ActualProductionVariable;
        standardproductionVariable = owner.StandardProductionVariable;



        periodicTask = new PeriodicTask(IncrementDecrementTask, 60, LogicObject);
        periodicTask.Start();

    }
    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        periodicTask.Dispose();
        periodicTask = null;
    }
    private void IncrementDecrementTask()
    {
        DateTime toVar = toVariable.Value;
        DateTime fromVar = fromVariable.Value;
        int diff = diffVariable.Value;
        Boolean gbutton = gbuttonVariable.Value;



        if (gbuttonVariable.Value == true)
        {

            var result = toVar - fromVar;
            diff = Convert.ToInt32(result.TotalMinutes);   // difference of two DateTime for calculation




            diffVariable.Value = diff;
        }
    }
    private IUAVariable toVariable;
    private IUAVariable fromVariable;
    private IUAVariable diffVariable;
    private IUAVariable gbuttonVariable;
    private IUAVariable actualspeedVariable;
    private IUAVariable standardspeedVariable;
    private IUAVariable actualproductionVariable;
    private IUAVariable standardproductionVariable;
    private PeriodicTask periodicTask;
}

    
    

