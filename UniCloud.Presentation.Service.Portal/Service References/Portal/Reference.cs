//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34014
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 原始文件名:
// 生成日期: 2014-7-3 0:09:11
namespace UniCloud.Presentation.Service.Portal.Portal
{
    
    /// <summary>
    /// 架构中不存在 PortalData 的注释。
    /// </summary>
    public partial class PortalData : global::System.Data.Services.Client.DataServiceContext
    {
        /// <summary>
        /// 初始化新的 PortalData 对象。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public PortalData(global::System.Uri serviceRoot) : 
                base(serviceRoot, global::System.Data.Services.Common.DataServiceProtocolVersion.V3)
        {
            this.ResolveName = new global::System.Func<global::System.Type, string>(this.ResolveNameFromType);
            this.ResolveType = new global::System.Func<string, global::System.Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
            this.Format.LoadServiceModel = GeneratedEdmModel.GetInstance;
        }
        partial void OnContextCreated();
        /// <summary>
        /// 因为在 Visual Studio 中为此服务引用配置的
        /// 命名空间与在服务器架构中指示的命名空间不同，所以
        /// 使用类型映射器在这两者之间进行映射。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected global::System.Type ResolveTypeFromName(string typeName)
        {
            global::System.Type resolvedType = this.DefaultResolveType(typeName, "UniCloud.Application.PortalBC.DTO", "UniCloud.Presentation.Service.Portal.Portal");
            if ((resolvedType != null))
            {
                return resolvedType;
            }
            return null;
        }
        /// <summary>
        /// 因为在 Visual Studio 中为此服务引用配置的
        /// 命名空间与在服务器架构中指示的命名空间不同，所以
        /// 使用类型映射器在这两者之间进行映射。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected string ResolveNameFromType(global::System.Type clientType)
        {
            if (clientType.Namespace.Equals("UniCloud.Presentation.Service.Portal.Portal", global::System.StringComparison.Ordinal))
            {
                return string.Concat("UniCloud.Application.PortalBC.DTO.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// 架构中不存在 AircraftSeries 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<AircraftSeriesDTO> AircraftSeries
        {
            get
            {
                if ((this._AircraftSeries == null))
                {
                    this._AircraftSeries = base.CreateQuery<AircraftSeriesDTO>("AircraftSeries");
                }
                return this._AircraftSeries;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<AircraftSeriesDTO> _AircraftSeries;
        /// <summary>
        /// 架构中不存在 AircraftSeries 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToAircraftSeries(AircraftSeriesDTO aircraftSeriesDTO)
        {
            base.AddObject("AircraftSeries", aircraftSeriesDTO);
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private abstract class GeneratedEdmModel
        {
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel ParsedModel = LoadModelFromString();
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private const string ModelPart0 = @"<edmx:Edmx Version=""1.0"" xmlns:edmx=""http://schemas.microsoft.com/ado/2007/06/edmx""><edmx:DataServices m:DataServiceVersion=""1.0"" m:MaxDataServiceVersion=""3.0"" xmlns:m=""http://schemas.microsoft.com/ado/2007/08/dataservices/metadata""><Schema Namespace=""UniCloud.Application.PortalBC.DTO"" xmlns=""http://schemas.microsoft.com/ado/2006/04/edm""><EntityType Name=""AircraftSeriesDTO""><Key><PropertyRef Name=""Id"" /></Key><Property Name=""Id"" Type=""Edm.Guid"" Nullable=""false"" /><Property Name=""Name"" Type=""Edm.String"" /></EntityType></Schema><Schema Namespace=""UniCloud.DistributedServices.Portal"" xmlns=""http://schemas.microsoft.com/ado/2006/04/edm""><EntityContainer Name=""PortalData"" m:IsDefaultEntityContainer=""true""><EntitySet Name=""AircraftSeries"" EntityType=""UniCloud.Application.PortalBC.DTO.AircraftSeriesDTO"" /></EntityContainer></Schema></edmx:DataServices></edmx:Edmx>";
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static string GetConcatenatedEdmxString()
            {
                return string.Concat(ModelPart0);
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            public static global::Microsoft.Data.Edm.IEdmModel GetInstance()
            {
                return ParsedModel;
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel LoadModelFromString()
            {
                string edmxToParse = GetConcatenatedEdmxString();
                global::System.Xml.XmlReader reader = CreateXmlReader(edmxToParse);
                try
                {
                    return global::Microsoft.Data.Edm.Csdl.EdmxReader.Parse(reader);
                }
                finally
                {
                    ((global::System.IDisposable)(reader)).Dispose();
                }
            }
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::System.Xml.XmlReader CreateXmlReader(string edmxToParse)
            {
                return global::System.Xml.XmlReader.Create(new global::System.IO.StringReader(edmxToParse));
            }
        }
    }
    /// <summary>
    /// 架构中不存在 UniCloud.Application.PortalBC.DTO.AircraftSeriesDTO 的注释。
    /// </summary>
    /// <KeyProperties>
    /// Id
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("AircraftSeries")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Id")]
    public partial class AircraftSeriesDTO : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 创建新的 AircraftSeriesDTO 对象。
        /// </summary>
        /// <param name="ID">Id 的初始值。</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static AircraftSeriesDTO CreateAircraftSeriesDTO(global::System.Guid ID)
        {
            AircraftSeriesDTO aircraftSeriesDTO = new AircraftSeriesDTO();
            aircraftSeriesDTO.Id = ID;
            return aircraftSeriesDTO;
        }
        /// <summary>
        /// 架构中不存在属性 Id 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Guid Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this.OnIdChanging(value);
                this._Id = value;
                this.OnIdChanged();
                this.OnPropertyChanged("Id");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
        /// <summary>
        /// 架构中不存在属性 Name 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.OnNameChanging(value);
                this._Name = value;
                this.OnNameChanged();
                this.OnPropertyChanged("Name");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Name;
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
