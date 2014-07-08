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
                    AcReg = x[0].Trim(),
                    Msn = x[1].Trim(),
                    FlightNo = x[2].Trim(),
                    FlightDate = DateTime.ParseExact(x[3].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Departure = x[4].Trim(),
                    Arrival = x[5].Trim(),
                    E1Dep = !string.IsNullOrWhiteSpace(x[6]) ? decimal.Parse(x[6].Trim()) : 0M,
                    E1Arr = !string.IsNullOrWhiteSpace(x[7]) ? decimal.Parse(x[7].Trim()) : 0M,
                    E2Dep = !string.IsNullOrWhiteSpace(x[8]) ? decimal.Parse(x[8].Trim()) : 0M,
                    E2Arr = !string.IsNullOrWhiteSpace(x[9]) ? decimal.Parse(x[9].Trim()) : 0M,
                    ApuDep = !string.IsNullOrWhiteSpace(x[10]) ? decimal.Parse(x[10].Trim()) : 0M,
                    ApuArr = !string.IsNullOrWhiteSpace(x[11]) ? decimal.Parse(x[11].Trim()) : 0M,
                    FH = decimal.Parse(x[12])
                };
            fls.ToList()
                .ForEach(
                    fl =>
                    {
                        var flightLog = FlightLogFactory.CreateFlightLog(fl.AcReg, fl.Msn, fl.FlightNo, fl.FlightDate,
                            fl.Departure, fl.Arrival);
                        flightLog.SetOil(fl.E1Dep, fl.E1Arr, fl.E2Dep, fl.E2Arr, 0, 0, 0, 0, fl.ApuDep, fl.ApuArr);
                        flightLog.SetFlightHour(fl.FH);
                        flRep.Add(flightLog);
                    });

            // Act
            flRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }
    }
}