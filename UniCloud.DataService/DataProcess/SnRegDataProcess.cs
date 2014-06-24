#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/24 11:41:08
// 文件名：SnRegDataProcess
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/24 11:41:08
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

#endregion

namespace UniCloud.DataService.DataProcess
{
    /// <summary>
    ///     到寿监控数据处理，必须在FlightLog、SnReg、PnReg数据同步完成之后计算。
    /// </summary>
    public class SnRegDataProcess
    {
        private readonly PartBCUnitOfWork _unitOfWork;

        public SnRegDataProcess(PartBCUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 计算到寿日期
        /// </summary>
        public void ProcessLifeMointor()
        {
            //1、获取需要计算的寿控件SnReg\SnHistory
            var snRegs = _unitOfWork.CreateSet<SnReg>().Where(s => s.IsLife == true).ToList();
            if (snRegs.Count == 0) return;
            snRegs.ForEach(p =>
            {
                //2、遍历SnReg,如果最近一条SnHistory的TSN2及CSN2为空，则需要计算最近一条SnHistory与上一段历史之间这段时间的飞行小时和飞行循环数据。
                if (p.IsLifeCst == false)
                {
                    var snHistories = _unitOfWork.CreateSet<SnHistory>().Where(sh => sh.SnRegId == p.Id && (sh.TSN != sh.TSN2 || sh.CSN != sh.CSN2)).ToList();
                    //2.1、如果此SnReg不计算在库存放的寿命，则TSN2=TSN，CSN2=CSN
                    if (snHistories.Count != 0)
                    {
                        snHistories.ForEach(sh =>
                        {
                            if (sh.TSN != sh.TSN2) sh.SetTSN2(sh.TSN);
                            if (sh.CSN != sh.CSN2) sh.SetCSN2(sh.CSN);
                        });
                    }
                }
                else //需要计算在库时间对寿命的影响
                {
                    var snHistories = _unitOfWork.CreateSet<SnHistory>().Where(sh => sh.SnRegId == p.Id).ToList();
                    //2.2、如果此SnReg要计算在库存放的寿命，首先要判断这段历史SnReg的状态，然后计算这段时间的在库寿命
                    if (snHistories.Count != 0)
                    {
                        snHistories.ForEach(sh =>
                        {
                            //TODO：需要做处理
                        });
                    }
                }

                if (p.Status == SnStatus.装机 || (p.Status == SnStatus.在库 && p.IsLifeCst))//只有装机的件或者在库且需要计算在库寿命的件才需要更新寿命预测
                {
                    //3、在SnReg的TSN\CSN及TSN2\CSN2数据完整之后，计算到寿日期，首先取出SnReg的维修控制组数据
                    var pnReg = GetPnReg(p.PnRegId);
                    if (pnReg != null)
                    {
                        var item = GetItem(pnReg.Id);
                        if (item != null)
                        {
                            //3.1取出SnReg的项维修控制组和件控制组，判断是否有重复的，
                            var itemMaintainCtrls =
                                _unitOfWork.CreateSet<ItemMaintainCtrl>().Where(i => i.ItemId == item.Id).ToList();
                            var pnMaintainCtrls =
                                _unitOfWork.CreateSet<PnMaintainCtrl>().Where(pn => pn.Id == pnReg.Id).ToList();
                            itemMaintainCtrls.ForEach(imc =>
                            {
                                if (imc.MaintainWorkId != null &&
                                    pnMaintainCtrls.Any(pmc => pmc.MaintainWorkId == imc.MaintainWorkId))
                                {
                                    itemMaintainCtrls.Remove(imc); //对于同一个维修工作的控制组，若在部件控制组里有定义，采用部件带的控制组
                                }
                            });
                            //将项和部件维修控制组进行合并之后组成新的控制组集合
                            var maintainCtrls = new List<MaintainCtrl>();
                            itemMaintainCtrls.ForEach(maintainCtrls.Add);
                            pnMaintainCtrls.ForEach(maintainCtrls.Add);
                            //3.2针对每个控制组的控制策略和控制明细，计算出到剩余的使用小时和使用循环
                            if (maintainCtrls.Count != 0)
                            {
                                maintainCtrls.ForEach(mc =>
                                {
                                    //根据每个SnReg对应的MaintainCtrl计算到寿监控 TODO

                                });
                            }
                        }
                    }
                }
            });
        }

        private PnReg GetPnReg(int pnRegId)
        {
            return _unitOfWork.CreateSet<PnReg>().FirstOrDefault(pn => pn.Id == pnRegId);
        }

        private Item GetItem(int pnRegId)
        {
            var installCtrl = _unitOfWork.CreateSet<InstallController>().FirstOrDefault(l => l.PnRegId == pnRegId);
            if (installCtrl != null)
            {
                var item = _unitOfWork.CreateSet<Item>().FirstOrDefault(p => p.Id == installCtrl.ItemId);
                return item;
            }
            return null;
        }

        /// <summary>
        /// 由维修控制组计算
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="mc"></param>
        /// <returns></returns>
        private LifeMonitor CreateLifeMonitor(SnReg sn, MaintainCtrl mc)
        {
            string workDescription="";
            if (mc.MaintainWork != null)
                workDescription = mc.MaintainWork.WorkCode;
            else workDescription = mc.Description;
            if (!string.IsNullOrWhiteSpace(workDescription))
            {
                var lifeMointor =
                    _unitOfWork.CreateSet<LifeMonitor>().FirstOrDefault(lm => lm.WorkDescription == workDescription);
                if (lifeMointor != null)
                {
                    //TODO 需要完善
                }
            }
            return null;
        }
    }

}
