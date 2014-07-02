#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 10:15:59
// 文件名：CAACAircraftTypeData
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 10:15:59
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using UniCloud.Domain.UberModel.Aggregates.CAACAircraftTypeAgg;
using UniCloud.Infrastructure.Data.UberModel.InitialData.InitialBase;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
   public class CAACAircraftTypeData: InitialDataBase
    {
       public CAACAircraftTypeData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        ///     初始化机型相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var aircraftTypes = new List<CAACAircraftType>
            {

                // 250座以上客机
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("AB65EE49-D110-40F1-B3CE-52CADB0C6B81"), "A380",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("53871626-C2FE-4B15-9E78-752AA4620ED6"), "A350",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("638F6F24-226B-4B58-8280-EEDDB305B951"), "B747-400P",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("D1DC0C25-769B-4CBB-9EB8-6E22AFA7FD85"), "B747-400C",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("EA12EB5F-C5F9-43C4-82B3-2FFEC3E598A7"), "B747-8",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("63616869-C36E-4DD6-A1C2-4AD9E2B8F403"), "B777-200A",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("CA7C4ECA-170D-4894-B6E9-8CB7F0AA59D9"), "B777-200B",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("2D722386-5B3F-4547-B41E-F5020DE70B30"), "B777-300",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("20719661-A03E-41D3-865E-C183C1B9E512"), "B777-300ER",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("24BADE01-E624-4C84-B8B1-388FECC9D773"), "B767-300",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("09219721-7F63-4790-AD85-75BB62D298FE"), "B767-300ER",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("6FA3FF32-AF8B-4CD3-B973-275ADB536CBF"), "B787-8",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("BBB22D28-A9CD-415A-BF0D-F49B68972DC7"), "B787-9",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("416734C4-9720-4777-B273-E46F0AED5DEA"), "A340-300",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("3632F0D9-6F47-4A80-AF54-7A37D18FC3AB"), "A340-600",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("8070D8B1-FA34-4854-9D8D-9250564CEA61"), "A300-600",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("5C690CB2-2D33-4006-858B-0BE610E9CB47"), "A330-200",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("F8BFBD1E-F2AC-40F2-9C29-A09ECB360DA2"), "A330-300",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("B962079E-C968-46E4-99A8-24771F5C79CD")),

                // 100-200座客机
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("1826BD57-733C-4679-95C8-7DFFEFAB38F4"), "B757-200",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("A0F3A212-C6C9-48B2-998D-47E052CDA3C1"), "B737-300",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("95D515CD-4DDD-4AC1-AA0C-8D83AD8339F2"), "B737-400",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("E8E95A6F-26BA-4EA4-A7D6-360FA6DE1CCD"), "B737-700",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("8571E7E1-387A-4570-9158-A1DFB656CA9F"), "B737-800",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("A8018768-C88A-4CDC-9297-4C4996E970FB"), "B737-900",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("5103CE46-9301-46A8-8D6A-054B8C6025C1"), "B737-8",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("967CC88E-C664-4555-A147-DE17F5A697EE"), "A319",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("EF5DD798-C16D-47CD-A588-ABD257A6B6B6"), "A320",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("562DCC8F-9577-4956-86E2-F3A3A0A3EEA2"), "A321",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("A4552254-2AF2-43C6-9284-83D1B3D8A7E0"), "A320NEO",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("799EA50A-2AAF-479D-B297-7A14271599E8"), "C919",Guid.Parse("BC67BB37-3C52-4C6A-96FD-5EF8E88D19EA"),Guid.Parse("E2629307-6770-4F2E-B104-224BF79D3DF1")),

                // 100座以下客机
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("4EB297C7-DCBC-4EB1-9956-590D4E514351"), "CRJ200",Guid.Parse("EC88B5C8-A882-4724-8B0C-ED1CE44DE736"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("55F5D629-959A-4DC9-A653-730140832BAF"), "CRJ700",Guid.Parse("EC88B5C8-A882-4724-8B0C-ED1CE44DE736"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("59346863-901F-4C3B-8FD4-3DDF5C824E9D"), "CRJ900",Guid.Parse("EC88B5C8-A882-4724-8B0C-ED1CE44DE736"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("EA47622C-2096-4A53-8460-B54B91916459"), "C-100",Guid.Parse("338CE613-E742-4A4A-85AC-7744A4D38F47"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("7BBE194A-15E8-4F3A-A4CA-3DADC609DF1A"), "C-300",Guid.Parse("338CE613-E742-4A4A-85AC-7744A4D38F47"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("D33F2C06-C8EC-45B9-A6FC-9C0E0090B699"), "DO328",Guid.Parse("338CE613-E742-4A4A-85AC-7744A4D38F47"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("3E03BCC5-0AAE-4A20-BD9B-F0FE34B2491F"), "ERJ145",Guid.Parse("2A2B2E9B-ED8F-4D00-9D12-CF264012206B"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("B21BF122-2B66-447C-A9FD-B68FA377A931"), "EMB190",Guid.Parse("2A2B2E9B-ED8F-4D00-9D12-CF264012206B"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("992AC72E-BB26-4672-A6DC-DFC667048F76"), "MA60",Guid.Parse("AF10EF52-0CD3-4A4F-8AEB-03979F1B62A3"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("C3520CBA-5429-4F5A-A824-B09ADF11E686"), "MA600",Guid.Parse("AF10EF52-0CD3-4A4F-8AEB-03979F1B62A3"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("BE6EACB6-F873-4540-9926-0C843282F567"), "ARJ21",Guid.Parse("BC67BB37-3C52-4C6A-96FD-5EF8E88D19EA"),Guid.Parse("5BA59D84-632C-4635-B7F0-B6B7D7FACC2B")),

                // 大型货机
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("6D46FF0A-5282-4B47-8E48-5A04605FBDEF"), "B777F",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("34E4C7B3-6A30-4BEC-AC91-334310839EAC")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("83A17593-BDB8-42E0-8617-D82495F807E8"), "B747-200F",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("34E4C7B3-6A30-4BEC-AC91-334310839EAC")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("66B1397E-8A1C-4DB5-9DFE-03E99A284218"), "B747-400F",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("34E4C7B3-6A30-4BEC-AC91-334310839EAC")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("5EDF08E9-2921-4F9E-89FE-82ADC14547E1"), "A300F",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("34E4C7B3-6A30-4BEC-AC91-334310839EAC")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("6BCFE715-07C2-4E7E-AAFF-F77A80D21603"), "A330F",Guid.Parse("9F14444A-228D-4681-9B33-835AB10B608C"),Guid.Parse("34E4C7B3-6A30-4BEC-AC91-334310839EAC")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("9E686699-D573-4D58-977D-C3AB236F542A"), "MD11F",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("34E4C7B3-6A30-4BEC-AC91-334310839EAC")),

                // 中型货机
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("FF0FCFA1-A88E-45BF-8B01-5427950ED36B"), "B757-200F",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("984C4402-6FE7-42AC-894D-1C60624B2B09")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("AAB69123-F6CB-4FFD-804A-09538237D96A"), "TU204",Guid.Parse("40A9F375-A798-4B0C-A004-CCD890FDD658"),Guid.Parse("984C4402-6FE7-42AC-894D-1C60624B2B09")),
                CAACAircraftTypeFactory.CreateAircraftType(Guid.Parse("0B6163E7-2969-4E38-A5DA-42DF5200918E"), "B737F",Guid.Parse("DC520EC0-8A90-4B4E-A982-E321D379380A"),Guid.Parse("984C4402-6FE7-42AC-894D-1C60624B2B09")),
            };
            aircraftTypes.ForEach(p => Context.CaacAircraftTypes.Add(p));

        }
    }
}
