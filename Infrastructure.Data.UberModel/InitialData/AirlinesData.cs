#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:49:04
// 文件名：AirlinesData
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.UberModel.Aggregates.AirlinesAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class AirlinesData : InitialDataBase
    {
        public AirlinesData(UberModelUnitOfWork context) : base(context)
        {
        }

        public override void InitialData()
        {
            var airlineses = new List<Airlines>
            {
                AirlinesFactory.CreateAirlines(Guid.Parse("119F5D5B-201D-4B65-BFB5-9B19A706A2EE"),"中国国际航空股份有限公司","国航","CA","CCA"),
                AirlinesFactory.CreateAirlines(Guid.Parse("204311DE-1F2B-4518-AE5F-6E1904BC413A"),"中国国际货运航空有限公司","国货航","CA","CAO"),
                AirlinesFactory.CreateAirlines(Guid.Parse("49A84FC4-0718-4C17-89F9-E7CCA762B1F2"),"深圳航空有限责任公司","深航","ZH","CSZ"),
                AirlinesFactory.CreateAirlines(Guid.Parse("D99FB662-E8F9-4712-A2EF-FF430CA552D4"),"昆明航空有限公司","昆航","KY","KNA"),
                AirlinesFactory.CreateAirlines(Guid.Parse("25D3DAC6-D1E4-4013-9CE0-FEFDB812EF5B"),"大连航空有限责任公司","大连航","CA","CCD"),
                AirlinesFactory.CreateAirlines(Guid.Parse("41051A60-0225-4A18-8691-E52249E48751"),"中国东方航空股份有限公司","东航","MU","CES"),
                AirlinesFactory.CreateAirlines(Guid.Parse("1F073BD8-727B-4B7E-BF92-0453E92F3EC9"),"上海航空有限公司","上航","FM","CSH"),
                AirlinesFactory.CreateAirlines(Guid.Parse("E9A6FAD3-BE81-4A5C-B3F8-E666A21118D0"),"中国联合航空有限公司","联航","KN","CUA"),
                AirlinesFactory.CreateAirlines(Guid.Parse("90B55F6D-0F9F-4B33-94B9-BC91E689AB99"),"中国货运航空有限公司","中货航","CK","CHY"),
                AirlinesFactory.CreateAirlines(Guid.Parse("BBC333AA-E00C-4D87-83F5-4AE9727B800D"),"中国南方航空股份有限公司","南航","CZ","CSN"),
                AirlinesFactory.CreateAirlines(Guid.Parse("FE4A8301-2080-4D47-BBA4-57994FFFBC06"),"重庆航空有限责任公司","重庆航","0Q","CQN"),
                AirlinesFactory.CreateAirlines(Guid.Parse("9DE9595C-911F-41AA-B1B0-F1CCA9D458FF"),"厦门航空有限公司","厦航","MF","CXA"),
                AirlinesFactory.CreateAirlines(Guid.Parse("F1AE6CE4-8CAE-4831-B1B6-403237AC11F1"),"大新华航空有限公司","大新华","CN","GDC"),
                AirlinesFactory.CreateAirlines(Guid.Parse("DB322284-5AF7-4948-832F-66C8551EBB70"),"海南航空股份有限公司","海航","HU","CHH"),
                AirlinesFactory.CreateAirlines(Guid.Parse("DF7522EB-1E2F-4760-A517-76E6CB56A016"),"扬子江快运航空有限公司","扬子江","Y8","YZR"),
                AirlinesFactory.CreateAirlines(Guid.Parse("A99AF511-B59B-464E-9CF3-FB02E31A84A2"),"首都航空股份有限公司","首都","JD","DER"),
                AirlinesFactory.CreateAirlines(Guid.Parse("94D4A39C-71DD-4BAF-82CF-7BFB1562051B"),"天津航空有限责任公司","天航","GS","GCR"),
                AirlinesFactory.CreateAirlines(Guid.Parse("A7153179-9FB1-43D3-A3E8-5759F426CC0C"),"云南祥鹏航空有限责任公司","祥鹏","8L","LKE"),
                AirlinesFactory.CreateAirlines(Guid.Parse("004B37C1-70B8-4071-98AB-0BB73C466D00"),"西部航空有限责任公司","西部","PN","CHB"),
                AirlinesFactory.CreateAirlines(Guid.Parse("1978ADFC-A2FD-40CC-9A26-6DEDB55C335F"),"四川航空股份有限公司","川航","3U","CSC"),
                AirlinesFactory.CreateAirlines(Guid.Parse("1A1CF03E-C2B9-47A9-B783-CD241A3D494F"),"山东航空股份有限公司","山航","SC","CDG"),
                AirlinesFactory.CreateAirlines(Guid.Parse("98D7C37E-BF02-4E2A-88A8-C2101EF4547B"),"华夏航空有限公司","华夏","G5","HXA"),
                AirlinesFactory.CreateAirlines(Guid.Parse("568919F3-0199-472C-9F7A-354BB8EFBFEE"),"河北航空有限公司","河北航","NS","DBH"),
                AirlinesFactory.CreateAirlines(Guid.Parse("80B1ABC4-0CB0-4E8D-9C86-F86866B20486"),"成都航空有限公司","成都航","EU","UEA"),
                AirlinesFactory.CreateAirlines(Guid.Parse("FF0A172F-AAB1-44FB-9261-09F16446B3C5"),"幸福航空有限公司","幸福航","JR","JOY"),
                AirlinesFactory.CreateAirlines(Guid.Parse("1376B72F-583D-40AB-B9E1-375098EB2A1A"),"西藏航空有限公司","西藏航","TV","TBA"),
                AirlinesFactory.CreateAirlines(Guid.Parse("C1DDFDC0-5337-4519-B8BD-A8D2B2226396"),"中国货运邮政航空有限责任公司","邮航","8Y","CYZ"),
                AirlinesFactory.CreateAirlines(Guid.Parse("2A11460A-A4BD-4718-B7BA-4A9145F240E6"),"奥凯航空有限公司","奥凯","BK","OKA"),
                AirlinesFactory.CreateAirlines(Guid.Parse("6C0A32F7-50C5-46D6-9D8B-E0D30C96CC8C"),"春秋航空有限公司","春秋","9C","CQH"),
                AirlinesFactory.CreateAirlines(Guid.Parse("F1C6EA69-C195-406A-B359-702563D149FE"),"翡翠国际货运航空有限责任公司","翡翠","JI","JAE"),
                AirlinesFactory.CreateAirlines(Guid.Parse("34BB8897-3860-4BDE-9B77-3924EF131544"),"深圳东海航空有限公司","东海","J5","EPA"),
                AirlinesFactory.CreateAirlines(Guid.Parse("09B24BF5-EFD9-44B0-93FD-8A4664F6F431"),"上海吉祥航空有限公司","吉祥","HO","DKH"),
                AirlinesFactory.CreateAirlines(Guid.Parse("D0D559B2-CF6E-413C-8F87-0A87F41BE073"),"银河国际货运航空有限公司","银河","GD","GSC"),
                AirlinesFactory.CreateAirlines(Guid.Parse("8BAF2578-CF2F-4386-998D-BD37FD031D8C"),"顺丰航空有限公司","顺丰","O3","CSS"),
                AirlinesFactory.CreateAirlines(Guid.Parse("DEC75B84-042C-4471-B1AC-C6F268E62F0F"),"友和道通航空有限公司","友和","UW","UTP"),
                AirlinesFactory.CreateAirlines(Guid.Parse("E4630DA1-4103-494B-8E7E-5F9C8B92AFC3"),"长龙国际货运航空有限公司","长龙","GJ","CDC"),
            };
            airlineses.ForEach(a => Context.Airlineses.AddOrUpdate(u => u.Id, a));
        }
    }
}
