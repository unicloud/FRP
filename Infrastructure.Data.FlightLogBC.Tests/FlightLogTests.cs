#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：14:29
// 方案：FRP
// 项目：Infrastructure.Data.FlightLogBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.FlightLogBC.Tests
{
    [TestClass]
    public class FlightLogTests
    {
        [TestMethod]
        public void CreateFlightLogTest()
        {
            // Arrange
            var flRep = UniContainer.Resolve<IFlightLogRepository>();

            var dlg = new OpenFileDialog
            {
                Filter = @"CSV文件（.csv）|*.csv"
            };
            var file = dlg.ShowDialog();

            if (file != DialogResult.OK) return;

            var fileName = dlg.FileName;
            var fls = from fl in File.ReadAllLines(fileName).Skip(1)
                let x = fl.Split(',')
                select new
                {
                    FlightNo = x[1].Trim(),
                    LogNo = x[2].Trim(),
                    LegNo = x[3].Trim(),
                    AcReg = x[4].Trim(),
                    Msn = x[5].Trim(),
                    FlightDate = DateTime.Parse(x[7].Trim()),
                    BlockOn = x[8].Trim(),
                    TakeOff = x[9].Trim(),
                    Landing = x[10].Trim(),
                    BlockStop = x[11].Trim(),
                    TotalFH = decimal.Parse(x[12].Trim()),
                    TotalBH = decimal.Parse(x[13].Trim()),
                    FlightHours = decimal.Parse(x[14].Trim()),
                    BlockHours = decimal.Parse(x[15].Trim()),
                    TotalCycle = int.Parse(x[16].Trim()),
                    Cycle = int.Parse(x[17].Trim()),
                    Departure = x[18].Trim(),
                    Arrival = x[19].Trim(),
                    ToGoNum = int.Parse(x[20].Trim()),
                    ApuCycle = int.Parse(x[21].Trim()),
                    ApuMM = int.Parse(x[22].Trim()),
                    E1Dep = !string.IsNullOrWhiteSpace(x[23]) ? decimal.Parse(x[23].Trim()) : 0M,
                    E1Arr = !string.IsNullOrWhiteSpace(x[24]) ? decimal.Parse(x[24].Trim()) : 0M,
                    E2Dep = !string.IsNullOrWhiteSpace(x[25]) ? decimal.Parse(x[25].Trim()) : 0M,
                    E2Arr = !string.IsNullOrWhiteSpace(x[26]) ? decimal.Parse(x[26].Trim()) : 0M,
                    E3Dep = !string.IsNullOrWhiteSpace(x[27]) ? decimal.Parse(x[27].Trim()) : 0M,
                    E3Arr = !string.IsNullOrWhiteSpace(x[28]) ? decimal.Parse(x[28].Trim()) : 0M,
                    E4Dep = !string.IsNullOrWhiteSpace(x[29]) ? decimal.Parse(x[29].Trim()) : 0M,
                    E4Arr = !string.IsNullOrWhiteSpace(x[30]) ? decimal.Parse(x[30].Trim()) : 0M,
                    ApuDep = !string.IsNullOrWhiteSpace(x[31]) ? decimal.Parse(x[31].Trim()) : 0M,
                    ApuArr = !string.IsNullOrWhiteSpace(x[32]) ? decimal.Parse(x[32].Trim()) : 0M,
                };

            fls.ToList()
                .ForEach(
                    fl =>
                    {
                        var flightLog = FlightLogFactory.CreateFlightLog(fl.AcReg, fl.Msn, fl.FlightNo, fl.FlightDate,
                            fl.Departure, fl.Arrival);
                        flightLog.SetOil(fl.E1Dep, fl.E1Arr, fl.E2Dep, fl.E2Arr, fl.E3Dep, fl.E3Arr, fl.E4Dep, fl.E4Arr,
                            fl.ApuDep, fl.ApuArr);
                        flightLog.SetFlightTime(fl.BlockOn, fl.TakeOff, fl.Landing, fl.BlockStop);
                        flightLog.SetFlightConsume(fl.FlightHours, fl.BlockHours, fl.TotalFH, fl.TotalBH, fl.TotalCycle,
                            fl.Cycle, fl.ToGoNum, fl.ApuCycle, fl.ApuMM);
                        flRep.Add(flightLog);
                    });

            // Act
            flRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }
    }
}