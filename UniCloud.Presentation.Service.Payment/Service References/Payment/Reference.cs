//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 原始文件名:
// 生成日期: 2013/12/13 22:11:21
namespace UniCloud.Presentation.Service.Payment.Payment
{
    
    /// <summary>
    /// 架构中不存在 PaymentData 的注释。
    /// </summary>
    public partial class PaymentData : global::System.Data.Services.Client.DataServiceContext
    {
        /// <summary>
        /// 初始化新的 PaymentData 对象。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public PaymentData(global::System.Uri serviceRoot) : 
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
            global::System.Type resolvedType = this.DefaultResolveType(typeName, "UniCloud.Application.PaymentBC.DTO", "UniCloud.Presentation.Service.Payment.Payment");
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
            if (clientType.Namespace.Equals("UniCloud.Presentation.Service.Payment.Payment", global::System.StringComparison.Ordinal))
            {
                return string.Concat("UniCloud.Application.PaymentBC.DTO.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// 架构中不存在 MaintainInvoices 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<MaintainInvoiceDTO> MaintainInvoices
        {
            get
            {
                if ((this._MaintainInvoices == null))
                {
                    this._MaintainInvoices = base.CreateQuery<MaintainInvoiceDTO>("MaintainInvoices");
                }
                return this._MaintainInvoices;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<MaintainInvoiceDTO> _MaintainInvoices;
        /// <summary>
        /// 架构中不存在 MaintainInvoices 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToMaintainInvoices(MaintainInvoiceDTO maintainInvoiceDTO)
        {
            base.AddObject("MaintainInvoices", maintainInvoiceDTO);
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private abstract class GeneratedEdmModel
        {
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel ParsedModel = LoadModelFromString();
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private const string ModelPart0 = @"<edmx:Edmx Version=""1.0"" xmlns:edmx=""http://schemas.microsoft.com/ado/2007/06/edmx""><edmx:DataServices m:DataServiceVersion=""1.0"" m:MaxDataServiceVersion=""3.0"" xmlns:m=""http://schemas.microsoft.com/ado/2007/08/dataservices/metadata""><Schema Namespace=""UniCloud.Application.PaymentBC.DTO"" xmlns=""http://schemas.microsoft.com/ado/2006/04/edm""><EntityType Name=""MaintainInvoiceDTO""><Key><PropertyRef Name=""MaintainInvoiceId"" /></Key><Property Name=""MaintainInvoiceId"" Type=""Edm.Int32"" Nullable=""false"" /></EntityType></Schema><Schema Namespace=""UniCloud.DistributedServices.Payment"" xmlns=""http://schemas.microsoft.com/ado/2006/04/edm""><EntityContainer Name=""PaymentData"" m:IsDefaultEntityContainer=""true""><EntitySet Name=""MaintainInvoices"" EntityType=""UniCloud.Application.PaymentBC.DTO.MaintainInvoiceDTO"" /></EntityContainer></Schema></edmx:DataServices></edmx:Edmx>";
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
    /// 架构中不存在 UniCloud.Application.PaymentBC.DTO.MaintainInvoiceDTO 的注释。
    /// </summary>
    /// <KeyProperties>
    /// MaintainInvoiceId
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("MaintainInvoices")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("MaintainInvoiceId")]
    public partial class MaintainInvoiceDTO : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 创建新的 MaintainInvoiceDTO 对象。
        /// </summary>
        /// <param name="maintainInvoiceId">MaintainInvoiceId 的初始值。</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static MaintainInvoiceDTO CreateMaintainInvoiceDTO(int maintainInvoiceId)
        {
            MaintainInvoiceDTO maintainInvoiceDTO = new MaintainInvoiceDTO();
            maintainInvoiceDTO.MaintainInvoiceId = maintainInvoiceId;
            return maintainInvoiceDTO;
        }
        /// <summary>
        /// 架构中不存在属性 MaintainInvoiceId 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int MaintainInvoiceId
        {
            get
            {
                return this._MaintainInvoiceId;
            }
            set
            {
                this.OnMaintainInvoiceIdChanging(value);
                this._MaintainInvoiceId = value;
                this.OnMaintainInvoiceIdChanged();
                this.OnPropertyChanged("MaintainInvoiceId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _MaintainInvoiceId;
        partial void OnMaintainInvoiceIdChanging(int value);
        partial void OnMaintainInvoiceIdChanged();
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
