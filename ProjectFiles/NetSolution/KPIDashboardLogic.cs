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
using System.Linq.Expressions;
using System.Collections.Generic;
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.MicroController;

#endregion

public class KPIDashboardLogic : BaseNetLogic
{
    

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var owner = (KPIDashboard1)LogicObject.Owner;
        standardproductionVariable = owner.StandardProductionVariable;
        actualproductionVariable = owner.ActualProductionVariable;
        stoppageVariable = owner.StoppageVariable;
        wastageVariable = owner.WastageVariable;
        standardspeedVariable = owner.StandardSpeedVariable;
        actualspeedVariable = owner.ActualSpeedVariable;
        runtimeVariable = owner.RunTimeVariable;
        downtimeVariable = owner.DownTimeVariable;
        conveyorbeltVariable = owner.ConveyorBeltVariable;
        driveoverloadVariable = owner.DriveOverloadVariable;
        labelingissueVariable = owner.LabelingIssueVariable;
        rawmaterialVariable = owner.RawMaterialVariable;
        paperzamVariable = owner.PaperZamVariable;  
        motorzamVariable = owner.MotorZamVariable;
        performanceVariable = owner.PerformanceVariable;
        availabilityVariable = owner.AvailabilityVariable;
        qualityVariable = owner.QualityVariable;
        oeeVariable = owner.OEEVariable;
        idletimeVariable = owner.IdleTimeVariable;
        setuptimeVariable = owner.SetupTimeVariable;
        avssVariable = owner.AvsSVariable;
        downtimelossVariable = owner.DowntimeLossVariable;
        idletimelossVariable = owner.IdletimeLossVariable;
        setuptimelossVariable = owner.SetuptimeLossVariable;
        lowspeedlossVariable = owner.LowspeedLossVariable;
        totallossVariable = owner.TotalLossVariable;
        mttrVariable = owner.MTTRVariable;
        mtbfVariable = owner.MTBFVariable;
        standardproductionkVariable = owner.StandardProductionKVariable;

        gbuttonVariable = owner.GBUTTONVariable;
        todateVariable = owner.ToDateVariable;
        fromdateVariable = owner.FromDateVariable;
        totaltimeVariable = owner.TotalTimeVariable;


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
        int standardproduction = standardproductionVariable.Value;
        int actualproduction = actualproductionVariable.Value;
        int stoppage = stoppageVariable.Value;
        int wastage = wastageVariable.Value;
        int standardspeed = standardspeedVariable.Value;
        int actualspeed = actualspeedVariable.Value;
        int runtime = runtimeVariable.Value;
        int downtime = downtimeVariable.Value;
        int conveyorbelt = conveyorbeltVariable.Value;
        int driveoverload = driveoverloadVariable.Value;
        int labelingissue = labelingissueVariable.Value;
        int rawmaterial = rawmaterialVariable.Value;
        int paperzam = paperzamVariable.Value;
        int motorzam = motorzamVariable.Value;
        int performance = performanceVariable.Value;
        int availability = availabilityVariable.Value;
        int quality = qualityVariable.Value;
        int oee = oeeVariable.Value;
        int idletime = idletimeVariable.Value;
        int setuptime = setuptimeVariable.Value;
        float avss = avssVariable.Value;
        int downtimeloss = downtimelossVariable.Value;
        int idletimeloss = idletimelossVariable.Value;
        int setuptimeloss = setuptimelossVariable.Value;
        int lowspeedloss = lowspeedlossVariable.Value;
        int totalloss = totallossVariable.Value;
        int mttr = mttrVariable.Value;
        int mtbf = mtbfVariable.Value;
        DateTime todate = todateVariable.Value;
        DateTime fromdate = fromdateVariable.Value;
        bool gbutton = gbuttonVariable.Value;
        float stproduction = standardproductionkVariable.Value;
        int totaltime = totaltimeVariable.Value;


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
        var myStore10 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore11 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore12 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore13 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore14 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore15 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore16 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore17 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore18 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore19 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore20 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");




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
        object[,] resultSet10;
        string[] header10;
        object[,] resultSet11;
        string[] header11;
        object[,] resultSet12;
        string[] header12;
        object[,] resultSet13;
        string[] header13;
        object[,] resultSet14;
        string[] header14;
        object[,] resultSet15;
        string[] header15;
        object[,] resultSet16;
        string[] header16;
        object[,] resultSet17;
        string[] header17;
        object[,] resultSet18;
        string[] header18;
        object[,] resultSet19;
        string[] header19;
        object[,] resultSet20;
        string[] header20;


        if (gbutton == true)
        {
               
            string new123 = todate.ToString("dd-MMM-yyyy");
            string new321 = fromdate.ToString("dd-MMM-yyyy");
            string query1 = $"SELECT SUM(StandardProduction) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query2 = $"SELECT SUM(ActualProduction) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query3 = $"SELECT SUM(Stoppage) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00   ' AND '" + new123 + " 23:59:59'";
            string query4 = $"SELECT SUM(Wastage) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query5 = $"SELECT AVG(StandardSpeed) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query6 = $"SELECT AVG(ActualSpeed) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query7 = $"SELECT SUM(RunTime) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query8 = $"SELECT SUM(DownTime) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query9 = $"SELECT SUM(ConveyorBelt) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query10 = $"SELECT SUM(DriveOverload) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query11 = $"SELECT SUM(LabellingIssue) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query12 = $"SELECT SUM(RawMaterial) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query13 = $"SELECT SUM(MotorZam) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query14 = $"SELECT SUM(PaperZam) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query15 = $"SELECT AVG(Performance) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query16 = $"SELECT AVG(Availability) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query17 = $"SELECT AVG(Quality) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query18 = $"SELECT AVG(OEE) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query19 = $"SELECT SUM(IdleTime) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query20 = $"SELECT SUM(SetupTime) FROM DataLogger12 WHERE DailyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";



            myStore1.Query(query1, out header1, out resultSet1);
            myStore2.Query(query2, out header2, out resultSet2);
            myStore3.Query(query3, out header3, out resultSet3);
            myStore4.Query(query4, out header4, out resultSet4);
            myStore5.Query(query5, out header5, out resultSet5);
            myStore6.Query(query6, out header6, out resultSet6);
            myStore7.Query(query7, out header7, out resultSet7);
            myStore8.Query(query8, out header8, out resultSet8);
            myStore9.Query(query9, out header9, out resultSet9);
            myStore10.Query(query10, out header10, out resultSet10);
            myStore11.Query(query11, out header11, out resultSet11);
            myStore12.Query(query12, out header12, out resultSet12);
            myStore13.Query(query13, out header13, out resultSet13);
            myStore14.Query(query14, out header14, out resultSet14);
            myStore15.Query(query15, out header15, out resultSet15);
            myStore16.Query(query16, out header16, out resultSet16);
            myStore17.Query(query17, out header17, out resultSet17);
            myStore18.Query(query18, out header18, out resultSet18);
            myStore19.Query(query19, out header19, out resultSet19);
            myStore20.Query(query20, out header20, out resultSet20);

            

            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var Standardproduction = column1;
                standardproduction = Standardproduction;
            }

            
            var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount2 = header2 != null ? header2.Length : 0;
            if (rowCount2 > 0 && columnCount2 > 0)
            {
                var column1 = Convert.ToInt32(resultSet2[0, 0]);
                var Actualproduction = column1;
                actualproduction = Actualproduction;
            }


            var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
            var columnCount3 = header3 != null ? header3.Length : 0;
            if (rowCount3 > 0 && columnCount3 > 0)
            {
                var column1 = Convert.ToInt32(resultSet3[0, 0]);
                var stoppagE = column1;
                stoppage = stoppagE;
            }


            var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
            var columnCount4 = header4 != null ? header4.Length : 0;
            if (rowCount4 > 0 && columnCount4 > 0)
            {
                var column1 = Convert.ToInt32(resultSet4[0, 0]);
                var WastagE = column1;
                wastage = WastagE;
            }

            var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
            var columnCount5 = header5 != null ? header5.Length : 0;
            if (rowCount5 > 0 && columnCount5 > 0)
            {
                var column1 = Convert.ToInt32(resultSet5[0, 0]);
                var Standardspeed = column1;
                standardspeed = Standardspeed;
            }


            var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
            var columnCount6 = header6 != null ? header6.Length : 0;
            if (rowCount6 > 0 && columnCount6 > 0)
            {
                var column1 = Convert.ToInt32(resultSet6[0, 0]);
                var Actualspeed = column1;
                actualspeed = Actualspeed;
            }


            var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
            var columnCount7 = header7 != null ? header7.Length : 0;
            if (rowCount7 > 0 && columnCount7 > 0)
            {
                var column1 = Convert.ToInt32(resultSet7[0, 0]);
                var Runtime = column1;
                runtime = Runtime;
            }

            var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
            var columnCount8 = header8 != null ? header8.Length : 0;
            if (rowCount8 > 0 && columnCount8 > 0)
            {
                var column1 = Convert.ToInt32(resultSet8[0, 0]);
                var Downtime = column1;
                downtime = Downtime;
            }

            var rowCount9 = resultSet9 != null ? resultSet9.GetLength(0) : 0;
            var columnCount9 = header9 != null ? header9.Length : 0;
            if (rowCount9 > 0 && columnCount9 > 0)
            {
                var column1 = Convert.ToInt32(resultSet9[0, 0]);
                var Conveyorbelt = column1;
                conveyorbelt = Conveyorbelt;
            }

            var rowCount10 = resultSet10 != null ? resultSet10.GetLength(0) : 0;
            var columnCount10 = header10 != null ? header10.Length : 0;
            if (rowCount10 > 0 && columnCount10 > 0)
            {
                var column1 = Convert.ToInt32(resultSet10[0, 0]);
                var Driveoverload = column1;
                driveoverload = Driveoverload;
            }

            var rowCount11 = resultSet11 != null ? resultSet11.GetLength(0) : 0;
            var columnCount11 = header11 != null ? header11.Length : 0;
            if (rowCount11 > 0 && columnCount11 > 0)
            {
                var column1 = Convert.ToInt32(resultSet11[0, 0]);
                var Labellingissue = column1;
                labelingissue = Labellingissue;
            }

            var rowCount12 = resultSet12 != null ? resultSet12.GetLength(0) : 0;
            var columnCount12 = header12 != null ? header12.Length : 0;
            if (rowCount12 > 0 && columnCount12 > 0)
            {
                var column1 = Convert.ToInt32(resultSet12[0, 0]);
                var Rawmaterial = column1;
                rawmaterial = Rawmaterial;
            }

            var rowCount13 = resultSet13 != null ? resultSet13.GetLength(0) : 0;
            var columnCount13 = header13 != null ? header13.Length : 0;
            if (rowCount13 > 0 && columnCount13 > 0)
            {
                var column1 = Convert.ToInt32(resultSet13[0, 0]);
                var Motorzam = column1;
                motorzam = Motorzam;
            }

            var rowCount14 = resultSet14 != null ? resultSet14.GetLength(0) : 0;
            var columnCount14 = header14 != null ? header14.Length : 0;
            if (rowCount14 > 0 && columnCount14 > 0)
            {
                var column1 = Convert.ToInt32(resultSet14[0, 0]);
                var Paperzam = column1;
                paperzam = Paperzam;
            }

            var rowCount15 = resultSet15 != null ? resultSet15.GetLength(0) : 0;
            var columnCount15 = header15 != null ? header15.Length : 0;
            if (rowCount15 > 0 && columnCount15 > 0)
            {
                var column1 = Convert.ToInt32(resultSet15[0, 0]);
                var PerformancE = column1;
                performance = PerformancE;
            }

            var rowCount16 = resultSet16 != null ? resultSet16.GetLength(0) : 0;
            var columnCount16 = header16 != null ? header16.Length : 0;
            if (rowCount16 > 0 && columnCount16 > 0)
            {
                var column1 = Convert.ToInt32(resultSet16[0, 0]);
                var AvailabilitY = column1;
                availability = AvailabilitY;
            }

            var rowCount17 = resultSet17 != null ? resultSet17.GetLength(0) : 0;
            var columnCount17 = header17 != null ? header17.Length : 0;
            if (rowCount17 > 0 && columnCount17 > 0)
            {
                var column1 = Convert.ToInt32(resultSet17[0, 0]);
                var QualitY = column1;
                quality = QualitY;
            }

            var rowCount18 = resultSet18 != null ? resultSet18.GetLength(0) : 0;
            var columnCount18 = header18 != null ? header18.Length : 0;
            if (rowCount18 > 0 && columnCount18 > 0)
            {
                var column1 = Convert.ToInt32(resultSet18[0, 0]);
                var Oee = column1;
                oee = Oee;
            }


            var rowCount19 = resultSet19 != null ? resultSet19.GetLength(0) : 0;
            var columnCount19 = header19 != null ? header19.Length : 0;
            if (rowCount19 > 0 && columnCount19 > 0)
            {
                var column1 = Convert.ToInt32(resultSet19[0, 0]);
                var Idletime = column1;
                idletime = Idletime;
            }


            var rowCount20 = resultSet20 != null ? resultSet20.GetLength(0) : 0;
            var columnCount20 = header20 != null ? header20.Length : 0;
            if (rowCount20 > 0 && columnCount20 > 0)
            {
                var column1 = Convert.ToInt32(resultSet20[0, 0]);
                var setupTime = column1;
                setuptime = setupTime;
            }


           // var sproduction = standardspeed * runtime;
            float Avss = (actualproduction * 100) / standardproduction;
            var downtimelosS = (downtime * standardspeed);
            var idletimelosS = (idletime * standardspeed);
            var setuptimelosS = (setuptime * standardspeed);
            var lowspeedlosS = (standardspeed - actualspeed) * runtime;
            var totallosS = (downtimeloss + idletimeloss + setuptimeloss + lowspeedloss);
            var mttR = downtime / stoppage;
            var mtbF = runtime / stoppage;
            float stpr = actualproduction / 1000;
            var tt = runtime + downtime + idletime + setuptime;

            
          //  standardproduction = sproduction;
            avss = Avss;
            downtimeloss = downtimelosS;
            idletimeloss = idletimelosS;
            setuptimeloss = setuptimelosS;
            lowspeedloss = lowspeedlosS;
            totalloss = totallosS;
            mttr = mttR;
            mtbf = mtbF;
            stproduction = stpr;
            totaltime = tt;


            standardproductionVariable.Value = standardproduction;
            actualproductionVariable.Value = actualproduction;
            stoppageVariable.Value = stoppage;
            wastageVariable.Value = wastage;
            standardspeedVariable.Value = standardspeed;
            actualspeedVariable.Value = actualspeed;
            runtimeVariable.Value = runtime;
            downtimeVariable.Value = downtime;
            conveyorbeltVariable.Value = conveyorbelt;
            driveoverloadVariable.Value = driveoverload;
            labelingissueVariable.Value = labelingissue;
            rawmaterialVariable.Value = rawmaterial;
            paperzamVariable.Value = paperzam;
            motorzamVariable.Value = motorzam;
            performanceVariable.Value = performance;
            availabilityVariable.Value = availability;
            qualityVariable.Value = quality;
            oeeVariable.Value = oee;
            idletimeVariable.Value = idletime;
            setuptimeVariable.Value = setuptime;
            avssVariable.Value = avss;
            downtimelossVariable.Value = downtimeloss;
            idletimelossVariable.Value = idletimeloss;
            setuptimelossVariable.Value = setuptimeloss;
            lowspeedlossVariable.Value = lowspeedloss;
            totallossVariable.Value = totalloss;
            mttrVariable.Value = mttr;
            mtbfVariable.Value = mtbf;
            standardproductionkVariable.Value = stproduction;
            totaltimeVariable.Value = totaltime;


        }
    }
    private IUAVariable standardproductionVariable;
    private IUAVariable actualproductionVariable;
    private IUAVariable stoppageVariable;
    private IUAVariable availabilityVariable;
    private IUAVariable qualityVariable;
    private IUAVariable oeeVariable;
    private IUAVariable idletimeVariable;
    private IUAVariable setuptimeVariable;
    private IUAVariable avssVariable;
    private IUAVariable downtimelossVariable;
    private IUAVariable idletimelossVariable;
    private IUAVariable setuptimelossVariable;
    private IUAVariable lowspeedlossVariable;
    private IUAVariable totallossVariable;
    private IUAVariable mttrVariable;
    private IUAVariable mtbfVariable;
    private IUAVariable standardproductionkVariable;
    private IUAVariable gbuttonVariable;
    private IUAVariable todateVariable;
    private IUAVariable fromdateVariable;
    private IUAVariable totaltimeVariable;
    private IUAVariable downtimeVariable;
    private IUAVariable conveyorbeltVariable;
    private IUAVariable driveoverloadVariable;
    private IUAVariable paperzamVariable;
    private IUAVariable motorzamVariable;
    private IUAVariable performanceVariable;
    private IUAVariable labelingissueVariable;
    private IUAVariable rawmaterialVariable;
    private IUAVariable wastageVariable;
    private IUAVariable standardspeedVariable;
    private IUAVariable actualspeedVariable;
    private IUAVariable runtimeVariable;
    private PeriodicTask periodicTask;
}
