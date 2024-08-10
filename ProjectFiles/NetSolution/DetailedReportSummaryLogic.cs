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
using System.Collections;
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.MicroController;
#endregion

public class DetailedReportSummaryLogic : BaseNetLogic
{
    

    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started

        var owner = (DetailedReportSummary)LogicObject.Owner;
        totalproductionVariable = owner.TotalProductionVariable;
        totalruntimeVariable = owner.TotalRuntimeVariable;
        totaldowntimeVariable = owner.TotalDowntimeVariable;
        totalidletimeVariable = owner.TotalIdletimeVariable;
        totalsetuptimeVariable = owner.TotalSetuptimeVariable;
        totalwastageVariable = owner.TotalWastageVariable;
        performanceVariable = owner.PerformanceVariable;
        availabilityVariable = owner.AvailabilityVariable;
        qualityVariable = owner.QualityVariable;
        oeeVariable = owner.OEEVariable;
        fromdateVariable = owner.FromDateVariable;
        todateVariable = owner.ToDateVariable;
        rbuttonVariable = owner.RButtonVariable;

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
        int totalproduction = totalproductionVariable.Value;
        int totalruntime = totalruntimeVariable.Value;
        int totaldowntime = totaldowntimeVariable.Value;
        int totalidletime = totalidletimeVariable.Value;
        int totalsetuptime = totalsetuptimeVariable.Value;
        int totalwastage = totalwastageVariable.Value;
        int performance = performanceVariable.Value;
        int availability = availabilityVariable.Value;
        int quality = qualityVariable.Value;
        int oee = oeeVariable.Value;
        DateTime todate = todateVariable.Value;
        DateTime fromdate = fromdateVariable.Value;
        bool rbutton = rbuttonVariable.Value;




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


        if (rbutton == true)
        {

            string new123 = todate.ToString("dd-MMM-yyyy");
            string new321 = fromdate.ToString("dd-MMM-yyyy");
            string query1 = $"SELECT SUM(HourlyCount) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query2 = $"SELECT SUM(Runtime) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query3 = $"SELECT SUM(Downtime) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query4 = $"SELECT SUM(Idletime) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query5 = $"SELECT SUM(Setuptime) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query6 = $"SELECT SUM(Wastage) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query7 = $"SELECT AVG(AvailabilitY) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query8 = $"SELECT AVG(Performance) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query9 = $"SELECT AVG(Quality) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";
            string query10 = $"SELECT AVG(OEE) FROM DetailedProductionReportLogging WHERE HourlyTime BETWEEN '" + new321 + " 00:00:00' AND '" + new123 + " 23:59:59'";


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

            

            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                var Totalproduction = column1;
                totalproduction = Totalproduction;
            }
            

            var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount2 = header2 != null ? header2.Length : 0;
            if (rowCount2 > 0 && columnCount2 > 0)
            {
                var column1 = Convert.ToInt32(resultSet2[0, 0]);
                var Totalruntime = column1;
                totalruntime = Totalruntime;
            }


            var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
            var columnCount3 = header3 != null ? header3.Length : 0;
            if (rowCount3 > 0 && columnCount3 > 0)
            {
                var column1 = Convert.ToInt32(resultSet3[0, 0]);
                var Totaldowntime = column1;
                totaldowntime = Totaldowntime;
            }


            var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
            var columnCount4 = header4 != null ? header4.Length : 0;
            if (rowCount4 > 0 && columnCount4 > 0)
            {
                var column1 = Convert.ToInt32(resultSet4[0, 0]);
                var Totalidletime = column1;
                totalidletime = Totalidletime;
            }

            var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
            var columnCount5 = header5 != null ? header5.Length : 0;
            if (rowCount5 > 0 && columnCount5 > 0)
            {
                var column1 = Convert.ToInt32(resultSet5[0, 0]);
                var Totalsetuptime = column1;
                totalsetuptime = Totalsetuptime;
            }


            var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
            var columnCount6 = header6 != null ? header6.Length : 0;
            if (rowCount6 > 0 && columnCount6 > 0)
            {
                var column1 = Convert.ToInt32(resultSet6[0, 0]);
                var Totalwastage = column1;
                totalwastage = Totalwastage;
            }


            var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
            var columnCount7 = header7 != null ? header7.Length : 0;
            if (rowCount7 > 0 && columnCount7 > 0)
            {
                var column1 = Convert.ToInt32(resultSet7[0, 0]);
                var Availability = column1;
                availability = Availability;
            }

            var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
            var columnCount8 = header8 != null ? header8.Length : 0;
            if (rowCount8 > 0 && columnCount8 > 0)
            {
                var column1 = Convert.ToInt32(resultSet8[0, 0]);
                var Performance = column1;
                performance = Performance;
            }

            var rowCount9 = resultSet9 != null ? resultSet9.GetLength(0) : 0;
            var columnCount9 = header9 != null ? header9.Length : 0;
            if (rowCount9 > 0 && columnCount9 > 0)
            {
                var column1 = Convert.ToInt32(resultSet9[0, 0]);
                var Quality = column1;
                quality = Quality;
            }

            var rowCount10 = resultSet10 != null ? resultSet10.GetLength(0) : 0;
            var columnCount10 = header10 != null ? header10.Length : 0;
            if (rowCount10 > 0 && columnCount10 > 0)
            {
                var column1 = Convert.ToInt32(resultSet10[0, 0]);
                var Oee = column1;
                oee = Oee;
            }

            totalproductionVariable.Value = totalproduction;
            totalruntimeVariable.Value = totalruntime;
            totaldowntimeVariable.Value = totaldowntime;
            totalidletimeVariable.Value = totalidletime;
            totalsetuptimeVariable.Value = totalsetuptime;
            totalwastageVariable.Value = totalwastage;
            availabilityVariable.Value = availability;
            performanceVariable.Value = performance;
            qualityVariable.Value = quality;
            oeeVariable.Value = oee;

            

        }
    }

    private IUAVariable totalproductionVariable;
    private IUAVariable totalruntimeVariable;
    private IUAVariable totaldowntimeVariable;
    private IUAVariable totalidletimeVariable;
    private IUAVariable totalsetuptimeVariable;
    private IUAVariable totalwastageVariable;
    private IUAVariable performanceVariable;
    private IUAVariable availabilityVariable;
    private IUAVariable qualityVariable;
    private IUAVariable oeeVariable;
    private IUAVariable fromdateVariable;
    private IUAVariable todateVariable;
    private IUAVariable rbuttonVariable;
    private PeriodicTask periodicTask;
}
