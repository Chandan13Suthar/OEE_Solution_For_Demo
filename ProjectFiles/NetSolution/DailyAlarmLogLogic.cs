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
using Store = FTOptix.Store;
#endregion

public class DailyAlarmLogLogic : BaseNetLogic
{
    

    public override void Start()
    {
        var owner = (DailyAlarmLog)LogicObject.Owner;
        conveyorbeltVariable = owner.ConveyorBeltVariable;
        driveoverloadVariable = owner.DriveOverloadVariable;
        labellingissueVariable = owner.LabellingIssueVariable;
        rawmaterialVariable = owner.RawMaterialVariable;
        paperzamVariable = owner.PaperZamVariable;
        motorzamVariable = owner.MotorZamVariable;
        runbuttonVariable = owner.RunButtonVariable;
        idletimeVariable = owner.IdletimeVariable;
        setuptimeVariable = owner.SetuptimeVariable;


        periodicTask = new PeriodicTask(IncrementDecrementTask, 60, LogicObject);
        periodicTask.Start();

    }

    public override void Stop()
    {
        periodicTask.Dispose();
        periodicTask = null;
    }

    public void IncrementDecrementTask()
    {

        int conveyor = conveyorbeltVariable.Value;
        int driveover = driveoverloadVariable.Value;
        int label = labellingissueVariable.Value;
        int rawm = rawmaterialVariable.Value;
        int paperz = paperzamVariable.Value;
        int motorz = motorzamVariable.Value;
        int idletime = idletimeVariable.Value;
        int setuptime = setuptimeVariable.Value;   




        var project = FTOptix.HMIProject.Project.Current;
        var myStore = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
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




        if (runbuttonVariable.Value == true)
        {
            string TodayDate = DateTime.Now.ToString("dd-MMM-yyyy");
            string query1 = $"SELECT MAX(ConveyorBelt) FROM DailyLogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore.Query(query1, out header1, out resultSet1);

            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                conveyor = column1;

            }

            string query2 = $"SELECT MAX(DriveOverLoad) FROM DailyLogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore.Query(query2, out header2, out resultSet2);

            var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount2 = header2 != null ? header2.Length : 0;
            if (rowCount2 > 0 && columnCount2 > 0)
            {
                var column2 = Convert.ToInt32(resultSet2[0, 0]);
                driveover = column2;

            }


            string query3 = $"SELECT MAX(LabellingIssue) FROM DailyLogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore.Query(query3, out header3, out resultSet3);

            var rowCount3 = resultSet3 != null ? resultSet3.GetLength(0) : 0;
            var columnCount3 = header3 != null ? header3.Length : 0;
            if (rowCount3 > 0 && columnCount3 > 0)
            {
                var column3 = Convert.ToInt32(resultSet3[0, 0]);
                label = column3;

            }

            string query4 = $"SELECT MAX(RawMaterial) FROM DailyLogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore.Query(query4, out header4, out resultSet4);

            var rowCount4 = resultSet4 != null ? resultSet4.GetLength(0) : 0;
            var columnCount4 = header4 != null ? header4.Length : 0;
            if (rowCount4 > 0 && columnCount4 > 0)
            {
                var column4 = Convert.ToInt32(resultSet4[0, 0]);
                rawm = column4;

            }


            string query5 = $"SELECT MAX(PaperZam) FROM DailyLogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore.Query(query5, out header5, out resultSet5);

            var rowCount5 = resultSet5 != null ? resultSet5.GetLength(0) : 0;
            var columnCount5 = header5 != null ? header5.Length : 0;
            if (rowCount5 > 0 && columnCount5 > 0)
            {
                var column5 = Convert.ToInt32(resultSet5[0, 0]);
                paperz = column5;

            }

            string query6 = $"SELECT MAX(MotorZam) FROM DailyLogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore.Query(query6, out header6, out resultSet6);

            var rowCount6 = resultSet6 != null ? resultSet6.GetLength(0) : 0;
            var columnCount6 = header6 != null ? header6.Length : 0;
            if (rowCount6 > 0 && columnCount6 > 0)
            {
                var column6 = Convert.ToInt32(resultSet6[0, 0]);
                motorz = column6;

            }

            string query7 = $"SELECT MAX(IdelTime) FROM DailyLogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore.Query(query7, out header7, out resultSet7);
            var rowCount7 = resultSet7 != null ? resultSet7.GetLength(0) : 0;
            var columnCount7 = header7 != null ? header7.Length : 0;
            if (rowCount7 > 0 && columnCount7 > 0)
            {
                var column7 = Convert.ToInt32(resultSet7[0, 0]);
                idletime = column7;

            }

            string query8 = $"SELECT MAX(SetupTime) FROM DailyLogging WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore.Query(query8, out header8, out resultSet8);
            var rowCount8 = resultSet8 != null ? resultSet8.GetLength(0) : 0;
            var columnCount8 = header8 != null ? header8.Length : 0;
            if (rowCount8 > 0 && columnCount8 > 0)
            {
                var column8 = Convert.ToInt32(resultSet8[0, 0]);
                setuptime = column8;

            }






            conveyorbeltVariable.Value = conveyor;
            driveoverloadVariable.Value = driveover;
            labellingissueVariable.Value = label;
            rawmaterialVariable.Value = rawm;
            paperzamVariable.Value = paperz;
            motorzamVariable.Value = motorz;
            setuptimeVariable.Value = setuptime;
            idletimeVariable.Value = idletime;
        }





    }



    private IUAVariable conveyorbeltVariable;
    private IUAVariable driveoverloadVariable;
    private IUAVariable labellingissueVariable;
    private IUAVariable rawmaterialVariable;
    private IUAVariable paperzamVariable;
    private IUAVariable motorzamVariable;
    private IUAVariable runbuttonVariable;
    private IUAVariable idletimeVariable;
    private IUAVariable setuptimeVariable;
    private PeriodicTask periodicTask;
}
