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
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.MicroController;
#endregion

public class KPICalculationLogic : BaseNetLogic
{
    

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var owner = (KPICalculation)LogicObject.Owner;
        standardproductionVariable = owner.StandardProductionVariable;
        actualproductionVariable = owner.ActualProductionVariable;
        stoppageVariable = owner.StoppageVariable;
        wastageVariable = owner.WastageVariable;
        standardspeedVariable = owner.StandardSpeedVariable;
        actualspeedVariable = owner.ActualSpeedVariable;
        runtimeVariable = owner.RunTimeVariable;
        downtimeVariable = owner.DownTimeVariable;
        conveyorbeltVariable = owner.ConveyorBeltVariable;
        driveoverloadVaribale = owner.DriveOverloadVariable;
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


        gbuttonVariable = owner.GButtonVariable;
       


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
        int driveoverload = driveoverloadVaribale.Value;
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
        bool gbutton = gbuttonVariable.Value;
        



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
            string TodayDate = DateTime.Now.ToString("dd-MMM-yyyy");
            string query30 = $"SELECT SUM(StandardSpeed) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query31 = $"SELECT MAX(ActualProduction) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query32 = $"SELECT MAX(Stoppage) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query33 = $"SELECT MAX(Wastage) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query34 = $"SELECT MAX(StandardSpeed) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query35 = $"SELECT MAX(ActualSpeed) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query36 = $"SELECT MAX(RunTime) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query37 = $"SELECT MAX(Downtime) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query38 = $"SELECT MAX(ConveyorBelt) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query39 = $"SELECT MAX(DriveOverload) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query40 = $"SELECT MAX(LabellingIssue) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query41 = $"SELECT MAX(RawMaterial) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query42 = $"SELECT MAX(MotorZam) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query43 = $"SELECT MAX(PaperZam) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query44 = $"SELECT AVG(Performance) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query45 = $"SELECT AVG(Availability) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query46 = $"SELECT AVG(Quality) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query47 = $"SELECT AVG(OEE) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query48 = $"SELECT MAX(IdleTime) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";
            string query49 = $"SELECT MAX(SetupTime) FROM KPILogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:59'";



            myStore1.Query(query30, out header1, out resultSet1);
            myStore2.Query(query31, out header2, out resultSet2);
            myStore3.Query(query32, out header3, out resultSet3);
            myStore4.Query(query33, out header4, out resultSet4);
            myStore5.Query(query34, out header5, out resultSet5);
            myStore6.Query(query35, out header6, out resultSet6);
            myStore7.Query(query36, out header7, out resultSet7);
            myStore8.Query(query37, out header8, out resultSet8);
            myStore9.Query(query38, out header9, out resultSet9);
            myStore10.Query(query39, out header10, out resultSet10);
            myStore11.Query(query40, out header11, out resultSet11);
            myStore12.Query(query41, out header12, out resultSet12);
            myStore13.Query(query42, out header13, out resultSet13);
            myStore14.Query(query43, out header14, out resultSet14);
            myStore15.Query(query44, out header15, out resultSet15);
            myStore16.Query(query45, out header16, out resultSet16);
            myStore17.Query(query46, out header17, out resultSet17);
            myStore18.Query(query47, out header18, out resultSet18);
            myStore19.Query(query48, out header19, out resultSet19);
            myStore20.Query(query49, out header20, out resultSet20);



            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var Standardproduction = column1 / 60;
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
                var Setuptime = column1;
                setuptime = Setuptime;
            }

            standardproductionVariable.Value = standardproduction;
            actualproductionVariable.Value = actualproduction;
            stoppageVariable.Value = stoppage;
            wastageVariable.Value = wastage;
            standardspeedVariable.Value = standardspeed;
            actualspeedVariable.Value = actualspeed;
            runtimeVariable.Value = runtime;
            downtimeVariable.Value = downtime;
            conveyorbeltVariable.Value = conveyorbelt;
            driveoverloadVaribale.Value = driveoverload;
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
        }

    }



    private IUAVariable standardproductionVariable;
    private IUAVariable actualproductionVariable;
    private IUAVariable stoppageVariable;
    private IUAVariable wastageVariable;
    private IUAVariable standardspeedVariable;
    private IUAVariable actualspeedVariable;
    private IUAVariable runtimeVariable;
    private IUAVariable downtimeVariable;
    private IUAVariable conveyorbeltVariable;
    private IUAVariable driveoverloadVaribale;
    private IUAVariable labelingissueVariable;
    private IUAVariable rawmaterialVariable;
    private IUAVariable paperzamVariable;
    private IUAVariable motorzamVariable;
    private IUAVariable performanceVariable;
    private IUAVariable availabilityVariable;
    private IUAVariable qualityVariable;
    private IUAVariable oeeVariable;
    private IUAVariable idletimeVariable;
    private IUAVariable setuptimeVariable;
    private IUAVariable gbuttonVariable;
    private PeriodicTask periodicTask;
}
