#region StandardUsing
using System;
using System.Linq.Expressions;
using System.Text;
using UAManagedCore;
using FTOptix.EventLogger;
using FTOptix.Alarm;
using FTOptix.MicroController;
using FTOptix.UI;
using Store = FTOptix.Store;
#endregion

public class CalculateAvgSpeedLogic : FTOptix.NetLogic.BaseNetLogic
{
    public override void Start()
    {
        // Insert code to be executed when the user-defined logic is started
        var owner = (CalulateAvgSpeed)LogicObject.Owner;
        avgspeedVariable = owner.AvgSpeedVariable;
        buttonVariable = owner.ButtonVariable;
        avgstandardspeedVariable = owner.AvgStandardSpeedVariable;
        standardproductionVariable = owner.StandardProductionVariable;
        runtimeVariable = owner.RuntimeVariable;
        currentspeedVariable = owner.CurrentSpeedVariable;
        averagespeedproductionVariable = owner.AverageSpeedProductionVariable;
        currentspeedproductionVariable = owner.CurrentSpeedProductionVariable;
        totaltimeVariable = owner.TotalTimeVariable;
        actualproductionVariable = owner.ActualProductionVariable;
        targetproductionVariable = owner.TargetProductionVariable;
        ideltimeVariable = owner.IdleTimeVariable;
        performanceVariable = owner.PerformanceVariable;
        availabilityVariable = owner.AvailabilityVariable;
        qualityVariable = owner.QualityVariable;
        oeeVariable = owner.OEEVariable;
        totalproductionVariable = owner.TotalProductionVariable;

        periodicTask = new PeriodicTask(IncrementDecrementTask, 1000, LogicObject);
        periodicTask.Start();
      
    }
    
    public override void Stop()
    {
        // Insert code to be executed when the user-defined logic is stopped
        periodicTask.Dispose();
        periodicTask = null;

    }
    [FTOptix.NetLogic.ExportMethod]
    public void StartMethod()
    {
        periodicTask.Start();

    }

    [FTOptix.NetLogic.ExportMethod]
    public void StopMethod()
    {
        periodicTask.Cancel();

    }

    private void IncrementDecrementTask()
    {
        
       
       

        var project = FTOptix.HMIProject.Project.Current;
        var myStore = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore1 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");
        var myStore2 = project.GetObject("DataStores").Get<Store.Store>("ODBCDatabase");

        object[,] resultSet;
        string[] header;
        object[,] resultSet1;
        string[] header1;
        object[,] resultSet2;
        string[] header2;


        int avgspeed = avgspeedVariable.Value;
        int avgstandardspeed = avgstandardspeedVariable.Value;
        int standardproduction = standardproductionVariable.Value;
        int runtime = runtimeVariable.Value;
        int currentspeed = currentspeedVariable.Value;
        int currentspeedproduction = currentspeedproductionVariable.Value;
        int averagespeedproduction = averagespeedproductionVariable.Value; 
        int totaltime = totaltimeVariable.Value;
        int actualproduction = actualproductionVariable.Value;
        int targetproduction = targetproductionVariable.Value;
        int idletime = ideltimeVariable.Value;
        float performance = performanceVariable.Value;
        float availability = availabilityVariable.Value;
        float quality = qualityVariable.Value;
        float oee = oeeVariable.Value;
        int totalproduction = totalproductionVariable.Value;

        if (buttonVariable.Value == false)
        {
            string TodayDate = DateTime.Now.ToString("dd-MMM-yyyy");
            string query = $"SELECT AVG(ActualSpeed) FROM OEERawDataLogger WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore.Query(query, out header, out resultSet);

            string query1 = $"SELECT AVG(StandardSpeed) FROM OEERawDataLogger WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore1.Query(query1, out header1, out resultSet1);

            string query2 = $"SELECT MAX(TotalProduction) FROM OEERawDataLogger WHERE LocalTimeStamp BETWEEN '" + TodayDate + " 00:00:00' AND '" + TodayDate + " 23:59:58'";
            myStore2.Query(query2, out header2, out resultSet2);


            var rowCount = resultSet != null ? resultSet.GetLength(0) : 0;
            var columnCount = header != null ? header.Length : 0;
                if (rowCount > 0 && columnCount > 0)
                {
                    var column1 = Convert.ToInt32(resultSet[0, 0]);
                    avgspeed = column1;

                }
            var rowCount1 = resultSet1 != null ? resultSet1.GetLength(0) : 0;
            var columnCount1 = header1 != null ? header1.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet1[0, 0]);
                avgstandardspeed = column1;

            }
            var rowCount2 = resultSet2 != null ? resultSet2.GetLength(0) : 0;
            var columnCount2 = header2 != null ? header2.Length : 0;
            if (rowCount1 > 0 && columnCount1 > 0)
            {
                var column1 = Convert.ToInt32(resultSet2[0, 0]);
                totalproduction = column1;

            }



            standardproduction = actualproduction + (avgstandardspeed * (1440 - totaltime));
            currentspeedproduction = actualproduction + (currentspeed * (1440 - totaltime));
            averagespeedproduction = actualproduction + (avgspeed * (1440 - totaltime));
            targetproduction = (avgstandardspeed * (1440 - idletime ));
            performance = ((totalproduction *100 ) / runtime) / avgstandardspeed;
            quality = (actualproduction * 100 ) / totalproduction;
            availability = (runtime * 100 ) / totaltime;
            oee = (performance * quality * availability) / 10000;

            avgspeedVariable.Value = avgspeed;
            avgstandardspeedVariable.Value = avgstandardspeed;
            standardproductionVariable.Value = standardproduction;
            currentspeedproductionVariable.Value = currentspeedproduction;
            averagespeedproductionVariable.Value = averagespeedproduction;
            targetproductionVariable.Value = targetproduction;
            performanceVariable.Value = performance;
            qualityVariable.Value = quality;
            availabilityVariable.Value = availability;
            oeeVariable.Value = oee;


        }
    }
    private IUAVariable buttonVariable;
    private IUAVariable avgstandardspeedVariable;
    private IUAVariable avgspeedVariable;
    private IUAVariable standardproductionVariable;
    private IUAVariable runtimeVariable;
    private IUAVariable currentspeedVariable;
    private IUAVariable averagespeedproductionVariable;
    private IUAVariable currentspeedproductionVariable;
    private IUAVariable totaltimeVariable;
    private IUAVariable actualproductionVariable;
    private IUAVariable targetproductionVariable;
    private IUAVariable ideltimeVariable;
    private IUAVariable performanceVariable;
    private IUAVariable availabilityVariable;
    private IUAVariable qualityVariable;
    private IUAVariable oeeVariable;
    private IUAVariable totalproductionVariable;
    private PeriodicTask periodicTask;

}











