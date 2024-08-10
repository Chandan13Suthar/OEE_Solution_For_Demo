#region Using directives
using System;
using System.Threading;
using UAManagedCore;
using FTOptix.ODBCStore;
using FTOptix.RAEtherNetIP;
using FTOptix.CommunicationDriver;
using System.Xml.Schema;
using System.Linq.Expressions;
using Store = FTOptix.Store;
using FTOptix.DataLogger;
using FTOptix.HMIProject;
using FTOptix.NetLogic;
using FTOptix.NativeUI;
using FTOptix.UI;
using FTOptix.CoreBase;
using FTOptix.Store;
using FTOptix.Report;
using FTOptix.Retentivity;
using FTOptix.Core;
using System.Xml;
using System.Text;
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.MicroController;
#endregion

public class OEEcalculationSimulatorLogic : BaseNetLogic
{

    public override void Start()
    {
        var owner = (OEEcalculationSimulator)LogicObject.Owner;
        availabilityVariable = owner.AvailabilityVariable;
        qualityVariable = owner.QualityVariable;
        performanceVariable = owner.PerformanceVariable;
        oeeVariable = owner.OEEVariable;
        isRunningVariable = owner.InRunningVariable;
        actualSpeedVariable = owner.ActualSpeedVariable;
        standardSpeedVariable = owner.StandardSpeedVariable;
        targetProductionVariable = owner.TargetProductionVariable;
        currentSpeedProductionVariable = owner.CurrentSpeedProductionVariable;
        totaltimeVariable = owner.TotaltimeVariable;
        runtimeVariable = owner.RuntimeVariable;
        avgspeedproductionVariable = owner.AvgSpeedProductionVariable;
        avgspeedVariable = owner.AvgSpeedVariable;
       



        periodicTask = new PeriodicTask(IncrementDecrementTask, 60, LogicObject);
        periodicTask.Start();

        isRunningVariable.Value = true;

    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
        
        isRunningVariable.Value = false;
    }
   

    public void IncrementDecrementTask()
    {
        float oee = oeeVariable.Value;
        float availability = availabilityVariable.Value;
        float performance = performanceVariable.Value;
        float quality = qualityVariable.Value;
        bool isrunning = isRunningVariable.Value;
        int actualspeed = actualSpeedVariable.Value;
        int standardspeed = standardSpeedVariable.Value;
        int targetproduction = targetProductionVariable.Value; 
        int currentspeedproduction = currentSpeedProductionVariable.Value;
        int totaltime = totaltimeVariable.Value;
        int runtime = runtimeVariable.Value;
        int avgspeed = avgspeedVariable.Value;
        int avgspeedproduction = avgspeedproductionVariable.Value;
       

        if (isrunning == true)
        {
            oee = (availability * performance * quality) / 10000;
            targetproduction = standardspeed * (totaltime - runtime);
            currentspeedproduction = actualspeed * (totaltime - runtime);
            avgspeedproduction =  (totaltime - runtime) * avgspeed;
           


        }

        oeeVariable.Value = oee;
        targetProductionVariable.Value = targetproduction;
        currentSpeedProductionVariable.Value = currentspeedproduction;
        avgspeedproductionVariable.Value = avgspeedproduction;
        

    }



    private IUAVariable oeeVariable;
    private IUAVariable availabilityVariable;
    private IUAVariable qualityVariable;
    private IUAVariable performanceVariable;
    private IUAVariable isRunningVariable;
    private IUAVariable actualSpeedVariable;
    private IUAVariable standardSpeedVariable;
    private IUAVariable targetProductionVariable;
    private IUAVariable currentSpeedProductionVariable;
    private IUAVariable totaltimeVariable;
    private IUAVariable runtimeVariable;
    private IUAVariable avgspeedproductionVariable;
    private IUAVariable avgspeedVariable;
    private PeriodicTask periodicTask;
    
    
    

}



