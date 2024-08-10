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
using Store = FTOptix.Store;
using System.Text.RegularExpressions;
using FTOptix.SQLiteStore;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Xml;
using System.Text;
using System.Linq.Expressions;
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.MicroController;
#endregion

public class DownAnalysisDataLogic : BaseNetLogic
{

    public override void Start()
    {
        var owner = (DownAnalysisData)LogicObject.Owner;
        totaltimeVariable = owner.TotalTimeVariable;
        ideltimeVariable = owner.IdelTimeVariable;
        occupiedtimeVariable = owner.OccupiedTimeVariable;
        lunchVariable = owner.LunchVariable;
        runningVariable = owner.RunningVariable;
        plannedruntimeVariable = owner.PlannedRuntimeVariable;
        actualruntimeVariable = owner.ActualRuntimeVariable;
        conveyorbeltVariable = owner.ConveyorBeltVariable;
        driveoverloadVariable = owner.DriveOverloadVariable;
        labellingissueVariable = owner.LabellingIssueVariable;
        rawmaterialVariable = owner.RawMaterialVariable;
        paperzamVariable = owner.PaperZamVariable;
        motorzamVariable = owner.MotorZamVariable;
        totaloperationaltime = owner.TotalOperationalTimeVariable;
        toVariable = owner.ToVariable;
        fromVariable = owner.FromVariable;
        setuptimeVariable = owner.SetupTimeVariable;




        periodicTask = new PeriodicTask(IncrementDecrementTask, 60, LogicObject);
        periodicTask.Start();

    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
        runningVariable.Value = false;
    }


    public void IncrementDecrementTask()
    {
        int totalt = totaltimeVariable.Value;
        int idelt = ideltimeVariable.Value;
        int occupiedt = occupiedtimeVariable.Value;
        int l = lunchVariable.Value;
        bool run = runningVariable.Value;
        int plannedrun = plannedruntimeVariable.Value;
        int actualrun = actualruntimeVariable.Value;
        int conveyor = conveyorbeltVariable.Value;
        int driveover = driveoverloadVariable.Value;
        int label = labellingissueVariable.Value;
        int rawm = rawmaterialVariable.Value;
        int paperz = paperzamVariable.Value;
        int motorz = motorzamVariable.Value;   
        int tot = totaloperationaltime.Value;
        DateTime toVar = toVariable.Value;
        DateTime fromVar = fromVariable.Value;
        int setuptime = setuptimeVariable.Value;



        var project = FTOptix.HMIProject.Project.Current;
        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore3 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore4 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore5 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore6 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore7 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore8 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore9 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");


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
        object[,] resultSet9;
        string[] header9;




        if (run == true)
        {
            
         //   string new123 = toVar.ToString("dd-MMM-yyyy");
        //    string new321 = fromVar.ToString("dd-MMM-yyyy");                                                          // '" + new321 + " 00:00:00' AND '" + new123 + " 00:00:00'";
       //     string query1 = $"SELECT SUM(Max_Value) FROM (SELECT MAX(ConveyorBelt) AS Max_Value FROM DailyLogging WHERE DailyTime >= '" + new321 + " 00:00:00' AND DailyTime <= '" + new123 + " 00:00:00' GROUP BY CAST(DailyTime AS DATE)) AS Max_Values";

     //       myStore1.Query(query1, out header1, out resultSet1);

            string new123 = toVar.ToString("dd-MMM-yyyy");
            string new321 = fromVar.ToString("dd-MMM-yyyy");
            string query10 = $"SELECT SUM(ConveyorBelt) FROM DataLogger11 WHERE DailyTime BETWEEN '" + new321 + " 00:00:01' AND '" + new123 + " 00:00:00'";
            string query11 = $"SELECT SUM(DriveOverLoad) FROM DataLogger11 WHERE DailyTime BETWEEN '" + new321 + " 00:00:01' AND '" + new123 + " 00:00:00'";
            string query12 = $"SELECT SUM(LabellingIssue) FROM DataLogger11 WHERE DailyTime BETWEEN '" + new321 + " 00:00:01' AND '" + new123 + " 00:00:00'";
            string query13 = $"SELECT SUM(RawMaterial) FROM DataLogger11 WHERE DailyTime BETWEEN '" + new321 + " 00:00:01' AND '" + new123 + " 00:00:00'";
            string query14 = $"SELECT SUM(PaperZam) FROM DataLogger11 WHERE DailyTime BETWEEN '" + new321 + " 00:00:01' AND '" + new123 + " 00:00:00'";
            string query15 = $"SELECT SUM(MotorZam) FROM DataLogger11 WHERE DailyTime BETWEEN '" + new321 + " 00:00:01' AND '" + new123 + " 00:00:00'";
            string query16 = $"SELECT SUM(Ideltime) FROM DataLogger11 WHERE DailyTime BETWEEN '" + new321 + " 00:00:01' AND '" + new123 + " 00:00:00'";
            string query17 = $"SELECT SUM(Setuptime) FROM DataLogger11 WHERE DailyTime BETWEEN '" + new321 + " 00:00:01' AND '" + new123 + " 00:00:00'";
            string query18 = $"SELECT SUM(Lunch) FROM DataLogger11 WHERE DailyTime BETWEEN '" + new321 + " 00:00:01' AND '" + new123 + " 00:00:00'";


            myStore1.Query(query10, out header1, out resultSet1);
            myStore2.Query(query11, out header2, out resultSet2);
            myStore3.Query(query12, out header3, out resultSet3);
            myStore4.Query(query13, out header4, out resultSet4);
            myStore5.Query(query14, out header5, out resultSet5);
            myStore6.Query(query15, out header6, out resultSet6);
            myStore7.Query(query16, out header7, out resultSet7);
            myStore8.Query(query17, out header8, out resultSet8);
            myStore9.Query(query18, out header9, out resultSet9);

            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var conveyour_belt = column1;
                conveyor = conveyour_belt;
            }


            var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount2 = header2 != null ? header2.Length : 0;
            if (rowCount2 > 0 && columnCount2 > 0)
            {
                var column1 = Convert.ToInt32(resultSet2[0, 0]);
                var driver = column1;
                driveover = driver;
            }


            var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
            var columnCount3 = header3 != null ? header3.Length : 0;
            if (rowCount3 > 0 && columnCount3 > 0)
            {
                var column1 = Convert.ToInt32(resultSet3[0, 0]);
                var labelling = column1;
                label = labelling;
            }


            var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
            var columnCount4 = header4 != null ? header4.Length : 0;
            if (rowCount4 > 0 && columnCount4 > 0)
            {
                var column1 = Convert.ToInt32(resultSet4[0, 0]);
                var rawmaterials = column1;
                rawm = rawmaterials;
            }


            var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
            var columnCount5 = header5 != null ? header5.Length : 0;
            if (rowCount5 > 0 && columnCount5 > 0)
            {
                var column1 = Convert.ToInt32(resultSet5[0, 0]);
                var paperzams = column1;
                paperz = paperzams;
            }


            var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
            var columnCount6 = header6 != null ? header6.Length : 0;
            if (rowCount6 > 0 && columnCount6 > 0)
            {
                var column1 = Convert.ToInt32(resultSet6[0, 0]);
                var motorzams = column1;
                motorz = motorzams;
            }


            var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
            var columnCount7 = header7 != null ? header7.Length : 0;
            if (rowCount7 > 0 && columnCount7 > 0)
            {
                var column1 = Convert.ToInt32(resultSet7[0, 0]);
                var idleTime = column1;
                idelt = idleTime;
            }


            var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
            var columnCount8 = header8 != null ? header8.Length : 0;
            if (rowCount8 > 0 && columnCount8 > 0)
            {
                var column1 = Convert.ToInt32(resultSet8[0, 0]);
                var setupTime = column1;
                setuptime = setupTime;
            }
            var rowCount9 = resultSet9 != null ? resultSet9.GetLength(0) : 0;
            var columnCount9 = header9 != null ? header9.Length : 0;
            if (rowCount9 > 0 && columnCount9 > 0)
            {
                var column1 = Convert.ToInt32(resultSet9[0, 0]);
                var luncH = column1;
                l = luncH;
            }





            var occupiedT = totalt - idelt;
            occupiedt = occupiedT;
//            l = (totalt * 1) / 1440;
            var plannedruN = occupiedt - l;
            plannedrun = plannedruN;
            actualrun = plannedrun - setuptime;
            tot = actualrun - (conveyor + driveover + label + rawm + paperz + motorz);


          //  throw new Exception(plannedruntimeVariable.Value);


            //throw new Exception(conveyor.ToString()); 

        }

        occupiedtimeVariable.Value = occupiedt;
        actualruntimeVariable.Value = actualrun;
        totaloperationaltime.Value = tot;
        lunchVariable.Value = l;
        conveyorbeltVariable.Value = conveyor;
        labellingissueVariable.Value = label;
        driveoverloadVariable.Value = driveover;
        rawmaterialVariable.Value = rawm;
        paperzamVariable.Value = paperz;
        motorzamVariable.Value = motorz;
        setuptimeVariable.Value = setuptime;
        plannedruntimeVariable.Value = plannedrun;
        ideltimeVariable.Value = idelt;
       

    }
    /*
    private DataTable ExecuteQuery(string query10)
    {
        throw new NotImplementedException();
    }
    */

    private IUAVariable runningVariable;
    private IUAVariable totaltimeVariable;
    private IUAVariable ideltimeVariable;
    private IUAVariable occupiedtimeVariable;
    private IUAVariable lunchVariable;
    private IUAVariable plannedruntimeVariable;
    private IUAVariable actualruntimeVariable;
    private IUAVariable conveyorbeltVariable;
    private IUAVariable driveoverloadVariable;
    private IUAVariable labellingissueVariable;
    private IUAVariable rawmaterialVariable;
    private IUAVariable paperzamVariable;
    private IUAVariable motorzamVariable;
    private IUAVariable totaloperationaltime;
    private IUAVariable toVariable;
    private IUAVariable fromVariable;
    private IUAVariable setuptimeVariable;
    private PeriodicTask periodicTask;
   

}
