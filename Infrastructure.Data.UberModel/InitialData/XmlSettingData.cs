#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/7 17:30:18
// 文件名：XmlSettingData
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/7 17:30:18
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.UberModel.Aggregates.XmlSettingAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class XmlSettingData : InitialDataBase
    {
        private const string SettingContent = "<FleetColorSet><ColorSet><Type TypeName=\"航空公司\"><Item Name=\"国航\" Color=\"#FF339933\" /><Item Name=\"国货航\" Color=\"#FF00ABA9\" /><Item Name=\"深航\" Color=\"#FF996600\" /><Item Name=\"昆航\" Color=\"#FFF09609\" /><Item Name=\"大连航\" Color=\"#FFA200FF\" /><Item Name=\"东航\" Color=\"#FFFF7F50\" /><Item Name=\"上航\" Color=\"#FFFF0097\" /><Item Name=\"联航\" Color=\"#FF8CBF26\" /><Item Name=\"南航\" Color=\"#FF1BA1E2\" /><Item Name=\"重庆航\" Color=\"#FFE671B8\" /><Item Name=\"厦航\" Color=\"#FFA200FF\" /><Item Name=\"大新华\" Color=\"#FF339933\" /><Item Name=\"海航\" Color=\"#FFF09609\" /><Item Name=\"扬子江\" Color=\"#FFFF7F50\" /><Item Name=\"首都\" Color=\"#FF1BA1E2\" /><Item Name=\"天航\" Color=\"#FF00ABA9\" /><Item Name=\"祥鹏\" Color=\"#FFFF0097\" /><Item Name=\"西部\" Color=\"#FF996600\" /><Item Name=\"川航\" Color=\"#FF8CBF26\" /><Item Name=\"山航\" Color=\"#FFE671B8\" /><Item Name=\"华夏\" Color=\"#FF1BA1E2\" /><Item Name=\"河北航\" Color=\"#FF339933\" /><Item Name=\"成都航\" Color=\"#FFF09609\" /><Item Name=\"幸福航\" Color=\"#FFA200FF\" /><Item Name=\"西藏航\" Color=\"#FFFF7F50\" /><Item Name=\"邮航\" Color=\"#FF00ABA9\" /><Item Name=\"奥凯\" Color=\"#FFFF0097\" /><Item Name=\"春秋\" Color=\"#FF8CBF26\" /><Item Name=\"翡翠\" Color=\"#FF996600\" /><Item Name=\"东海\" Color=\"#FFE671B8\" /><Item Name=\"吉祥\" Color=\"#FF1BA1E2\" /><Item Name=\"银河\" Color=\"#FF339933\" /><Item Name=\"顺丰\" Color=\"#FFF09609\" /><Item Name=\"友和\" Color=\"#FFFF7F50\" /><Item Name=\"长龙\" Color=\"#FF00ABA9\" /><Item Name=\"中货航\" Color=\"#FFA200FF\" /></Type><Type TypeName=\"座级\"><Item Name=\"250座以上客机\" Color=\"#FF000100\" /><Item Name=\"100-200座客机\" Color=\"#FF1BA1E2\" /><Item Name=\"100座以下客机\" Color=\"#FFFF7F50\" /><Item Name=\"大型货机\" Color=\"#FFF09609\" /><Item Name=\"中型货机\" Color=\"#FFA200FF\" /></Type><Type TypeName=\"机型\"><Item Name=\"A380\" Color=\"#FF339933\" /><Item Name=\"B747-400P\" Color=\"#FF1BA1E2\" /><Item Name=\"B747-400C\" Color=\"#FFF09609\" /><Item Name=\"B747-8\" Color=\"#FFA200FF\" /><Item Name=\"B777-200A\" Color=\"#FFFF7F50\" /><Item Name=\"B777-200B\" Color=\"#FF8CBF26\" /><Item Name=\"B777-300\" Color=\"#FF8CBF26\" /><Item Name=\"B777-300ER\" Color=\"#FFFF0097\" /><Item Name=\"B767-300\" Color=\"#FF996600\" /><Item Name=\"B767-300ER\" Color=\"#FF996600\" /><Item Name=\"B787-8\" Color=\"#FF339933\" /><Item Name=\"B787-9\" Color=\"#FFF09609\" /><Item Name=\"A340-300\" Color=\"#FF00ABA9\" /><Item Name=\"A340-600\" Color=\"#FF8CBF26\" /><Item Name=\"A300-600\" Color=\"#FFE671B8\" /><Item Name=\"A330-200\" Color=\"#FF1BA1E2\" /><Item Name=\"A330-300\" Color=\"#FF339933\" /><Item Name=\"B757-200\" Color=\"#FFFF0097\" /><Item Name=\"B737-300\" Color=\"#FF339933\" /><Item Name=\"B737-400\" Color=\"#FF00ABA9\" /><Item Name=\"B737-700\" Color=\"#FF8CBF26\" /><Item Name=\"B737-800\" Color=\"#FF1BA1E2\" /><Item Name=\"B737-900\" Color=\"#FF996600\" /><Item Name=\"B737-8\" Color=\"#FFE671B8\" /><Item Name=\"A319\" Color=\"#FFF09609\" /><Item Name=\"A320\" Color=\"#FFFF7F50\" /><Item Name=\"A321\" Color=\"#FF996600\" /><Item Name=\"A320NEO\" Color=\"#FFF09609\" /><Item Name=\"C919\" Color=\"#FF996600\" /><Item Name=\"CRJ200\" Color=\"#FFE671B8\" /><Item Name=\"CRJ700\" Color=\"#FFFF7F50\" /><Item Name=\"CRJ900\" Color=\"#FFA200FF\" /><Item Name=\"DO328\" Color=\"#FF1BA1E2\" /><Item Name=\"ERJ145\" Color=\"#FF00ABA9\" /><Item Name=\"EMB190\" Color=\"#FFFF0097\" /><Item Name=\"MA60\" Color=\"#FFFF0097\" /><Item Name=\"MA600\" Color=\"#FF8CBF26\" /><Item Name=\"ARJ21\" Color=\"#FFE671B8\" /><Item Name=\"B777F\" Color=\"#FFA200FF\" /><Item Name=\"B747-200F\" Color=\"#FFE671B8\" /><Item Name=\"B747-400F\" Color=\"#FF1BA1E2\" /><Item Name=\"A300F\" Color=\"#FF339933\" /><Item Name=\"MD11F\" Color=\"#FFF09609\" /><Item Name=\"B757-200F\" Color=\"#FFFF7F50\" /><Item Name=\"B737-300QC\" Color=\"#FFA200FF\" /><Item Name=\"B737F\" Color=\"#FF00ABA9\" /><Item Name=\"C-100\" Color=\"#FF00ABA9\" /><Item Name=\"C-300\" Color=\"#FFFF0097\" /><Item Name=\"A330\" Color=\"#FFA200FF\" /></Type><Type TypeName=\"引进方式\"><Item Name=\"购买\" Color=\"#FF1BA1E2\" /><Item Name=\"融资租赁\" Color=\"#FFFF7F50\" /><Item Name=\"经营租赁\" Color=\"#FF339933\" /><Item Name=\"湿租\" Color=\"#FFF09609\" /><Item Name=\"经营租赁续租\" Color=\"#FFA200FF\" /><Item Name=\"湿租续租\" Color=\"#FF00ABA9\" /></Type><Type TypeName=\"机龄\"><Item Name=\"0至2年之间\" Color=\"#FF1BA1E2\" /><Item Name=\"2至5年之间\" Color=\"#FF339933\" /><Item Name=\"5至7年之间\" Color=\"#FFF09609\" /><Item Name=\"7至50年之间\" Color=\"#FFFF7F50\" /></Type><Type TypeName=\"运力变化\"><Item Name=\"飞机数（子）\" Color=\"#FF00ABA9\" /><Item Name=\"座位数（子）\" Color=\"#FF339933\" /><Item Name=\"商载量（子）\" Color=\"#FFFF0097\" /><Item Name=\"飞机数\" Color=\"#FF8CBF26\" /><Item Name=\"座位数\" Color=\"#FF1BA1E2\" /><Item Name=\"商载量\" Color=\"#FFF09609\" /></Type></ColorSet></FleetColorSet>";

        public XmlSettingData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            var xmlSettings = new List<XmlSetting>
                             {
                                 XmlSettingFactory.CreateXmlSetting(Guid.Parse("8A60B054-092A-42B0-97FE-EA257350D9AB"),"颜色配置",SettingContent )
                             };
            xmlSettings.ForEach(p => Context.XmlSettings.Add(p));
        }
    }
}
