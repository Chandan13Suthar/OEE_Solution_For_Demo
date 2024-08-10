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
using Store = FTOptix.Store;
using System.Linq.Expressions;
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.MicroController;
#endregion

public class OpportunityLossDataLogic : BaseNetLogic
{


    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var owner = (OpportunityLossData)LogicObject.Owner;
        toVariable = owner.ToVariable;
        fromVariable = owner.FromVariable;
        diffVariable = owner.DiffVariable;
        gbuttonVariable = owner.GButtonVariable;
        standardspeedVariable = owner.StandardSpeedVariable;
        actualproductionVariable = owner.ActualProductionVariable;
        standardproductionVariable = owner.StandardProductionVariable;
        actualspeedVariable = owner.ActualSpeedVariable;
        stvsacVariable = owner.StVsAcVariable;
        runtimeVariable = owner.RunTimeVariable;
        downtimeVariable = owner.DownTimeVariable;
        idletimeVariable = owner.IdleTimeVariable;
        setuptimeVariable = owner.SetupTimeVariable;
        priceVariable = owner.PriceVariable;
        actualspeedAvgVariable = owner.ActualSpeedAvgVariable;
        standardspeedAvgVariable = owner.StandardSpeedAvgVariable;
        downtimeLossVariable = owner.DownTimeLossVariable;
        idletimeLossVariable = owner.IdleTimeLossVariable; 
        speedLossVariable = owner.SpeedLossVariable;
        conveyorVariable = owner.ConveyorVariable;




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
        bool gbutton = gbuttonVariable.Value;
        int standardspeed = standardspeedVariable.Value;
        int actualspeed = actualspeedVariable.Value;
        float stvsac = stvsacVariable.Value;
        int standardproduction = standardproductionVariable.Value;
        int runtime = runtimeVariable.Value;
        int downtime = downtimeVariable.Value;
        int idletime = idletimeVariable.Value; 
        int setuptime = setuptimeVariable.Value;
        int actualspeedAvg = actualspeedAvgVariable.Value;
        int standardspeedAvg = standardspeedAvgVariable.Value;
        int downtimeloss = downtimeLossVariable.Value;
        int idletimeloss = idletimeLossVariable.Value;
        int speedloss = speedLossVariable.Value;
        int conveyor = conveyorVariable.Value;
        int price = priceVariable.Value;
        
        
        var project = FTOptix.HMIProject.Project.Current;
        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore3 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore4 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore5 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore6 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore7 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore8 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
       // var myStore9 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");


        object[,] resultSet1;
        string[] header1;
        object[,] resultSet2;
        string[] header2;
        object[,] resultSet3;
        string[] header3;
        object[,] resultSet4;
        string[] header4;
        object[,] resultSet5;
        string[] header5;
        object[,] resultSet6;
        string[] header6;
        object[,] resultSet7;
        string[] header7;
        object[,] resultSet8;
        string[] header8;
      //  object[,] resultSet9;
      //  string[] header9;



        if (gbutton == true)
        {
            string new123 = toVar.ToString("dd-MMM-yyyy");
            string new321 = fromVar.ToString("dd-MMM-yyyy");
            string query = $"SELECT SUM(StandardSpeed) FROM OEEDataLogging WHERE LocalTimeStamp BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 00:00:00'";
            string query1 = $"SELECT SUM(ActualSpeed) FROM OEEDataLogging WHERE LocalTimeStamp BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 00:00:00'";
            string query2 = $"SELECT MAX(RunTime) FROM TimeLogging WHERE LocalTimeStamp BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 00:00:00'";
            string query3 = $"SELECT MAX(DownTime) FROM TimeLogging WHERE LocalTimeStamp BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 00:00:00'";
            string query4 = $"SELECT MAX(IdleTime) FROM TimeLogging WHERE LocalTimeStamp BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 00:00:00'";
            string query5 = $"SELECT MAX(SetupTime) FROM TimeLogging WHERE LocalTimeStamp BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 00:00:00'";
            string query6 = $"SELECT AVG(ActualSpeed) FROM OEEDataLogging WHERE LocalTimeStamp BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 00:00:00'";
            string query7 = $"SELECT AVG(StandardSpeed) FROM OEEDataLogging WHERE LocalTimeStamp BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 00:00:00'";
            //string query8 = $"SELECT SUM(Max_Value) FROM (SELECT MAX(ConveyorBelt) AS Max_Value FROM DailyLogging WHERE LocalTimestamp >= '" + new321 + " 00:00:00' AND LocalTimestamp <= '" + new123 + " 23:59:59' GROUP BY CAST(LocalTimestamp AS DATE))";


            myStore1.Query(query, out header1, out resultSet1);
            myStore2.Query(query1, out header2, out resultSet2);
            myStore3.Query(query2, out header3, out resultSet3);
            myStore4.Query(query3, out header4, out resultSet4);
            myStore5.Query(query4, out header5, out resultSet5);
            myStore6.Query(query5, out header6, out resultSet6);
            myStore7.Query(query6, out header7, out resultSet7);
            myStore8.Query(query7, out header8, out resultSet8);
           // myStore9.Query(query8, out header9, out resultSet9);

            var rowCount = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount = header1 != null ? header1.Length : 0;
            if (rowCount > 0 && columnCount > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var standardSpeed = column1;
                standardspeed = standardSpeed;
            }



            var rowCount1 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount1 = header2 != null ? header2.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet2[0, 0]);
                var actualSpeed = column1;
                actualspeed = actualSpeed;
            }

            var rowCount2 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
            var columnCount2 = header3 != null ? header3.Length : 0;
            if (rowCount2 > 0 && columnCount2 > 0)
            {
                var column1 = Convert.ToInt32(resultSet3[0, 0]);
                var runTime = column1;
                runtime = runTime;
            }
            var rowCount3 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
            var columnCount3 = header4 != null ? header4.Length : 0;
            if (rowCount3 > 0 && columnCount3 > 0)
            {
                var column1 = Convert.ToInt32(resultSet4[0, 0]);
                var downTime = column1;
                downtime = downTime;
            }
            var rowCount4 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
            var columnCount4 = header5 != null ? header5.Length : 0;
            if (rowCount4 > 0 && columnCount4 > 0)
            {
                var column1 = Convert.ToInt32(resultSet5[0, 0]);
                var idleTime = column1;
                idletime = idleTime;
            }
            var rowCount5 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
            var columnCount5 = header6 != null ? header6.Length : 0;
            if (rowCount5 > 0 && columnCount5 > 0)
            {
                var column1 = Convert.ToInt32(resultSet6[0, 0]);
                var setupTime = column1;
                setuptime = setupTime;
            }
            var rowCount6 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
            var columnCount6 = header7 != null ? header7.Length : 0;
            if (rowCount6 > 0 && columnCount6 > 0)
            {
                var column1 = Convert.ToInt32(resultSet7[0, 0]);
                var actualspeedavg = column1;
                actualspeedAvg = actualspeedavg;
            }
            var rowCount7 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
            var columnCount7 = header8 != null ? header8.Length : 0;
            if (rowCount7 > 0 && columnCount7 > 0)
            {
                var column1 = Convert.ToInt32(resultSet8[0, 0]);
                var standardspeedavg = column1;
                standardspeedAvg = standardspeedavg;
            }
            /*
            var rowCount8 = resultSet9 != null ? resultSet9.GetLength(0) : 0;
            var columnCount8 = header9 != null ? header9.Length : 0;
            if (rowCount > 0 && columnCount > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var conveyour_belt = column1;
                conveyor = conveyour_belt;
            }

            */


            var result = toVar - fromVar;
            diff = Convert.ToInt32(result.TotalMinutes);   // difference of two DateTime for calculation
            standardproduction = standardspeed;
            var sproduction = standardproduction;
            var aproduction = actualspeed;
            float stvsac1 = (aproduction * 100);
            float stvsac2 = stvsac1 / sproduction;
            stvsac = stvsac2;
            var dloss = (downtime * standardspeedAvg * price) / 60;
            downtimeloss = dloss;

            var iloss = (idletime * standardspeedAvg * price) / 60;
            idletimeloss = iloss;

            var speedlosS = ((standardspeedAvg - actualspeedAvg) * diff * price) / 60;
            speedloss = speedlosS;

        }
        diffVariable.Value = diff;
        standardproductionVariable.Value = standardproduction;
        actualspeedVariable.Value = actualspeed;
        stvsacVariable.Value = stvsac;
        runtimeVariable.Value = runtime;
        downtimeVariable.Value = downtime;  
        idletimeVariable.Value = idletime;
        setuptimeVariable.Value = setuptime;
        speedLossVariable.Value = speedloss;
        idletimeLossVariable.Value = idletimeloss;
        downtimeLossVariable.Value = downtimeloss;
        conveyorVariable.Value = conveyor;
      

    }
    private IUAVariable toVariable;
    private IUAVariable fromVariable;
    private IUAVariable diffVariable;
    private IUAVariable gbuttonVariable;
    private IUAVariable actualspeedVariable;
    private IUAVariable stvsacVariable;
    private IUAVariable runtimeVariable;
    private IUAVariable downtimeVariable;
    private IUAVariable idletimeVariable;
    private IUAVariable setuptimeVariable;
    private IUAVariable priceVariable;
    private IUAVariable actualspeedAvgVariable;
    private IUAVariable standardspeedAvgVariable;
    private IUAVariable downtimeLossVariable;
    private IUAVariable idletimeLossVariable;
    private IUAVariable speedLossVariable;
    private IUAVariable conveyorVariable;
    private IUAVariable standardspeedVariable;
    private IUAVariable actualproductionVariable;
    private IUAVariable standardproductionVariable;
    private PeriodicTask periodicTask;
}




