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
// 生成日期: 2014-7-3 0:02:07
namespace UniCloud.Presentation.Service.CommonService.Common
{
    
    /// <summary>
    /// 架构中不存在 CommonServiceData 的注释。
    /// </summary>
    public partial class CommonServiceData : global::System.Data.Services.Client.DataServiceContext
    {
        /// <summary>
        /// 初始化新的 CommonServiceData 对象。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public CommonServiceData(global::System.Uri serviceRoot) : 
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
            global::System.Type resolvedType = this.DefaultResolveType(typeName, "UniCloud.Application.CommonServiceBC.DTO", "UniCloud.Presentation.Service.CommonService.Common");
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
            if (clientType.Namespace.Equals("UniCloud.Presentation.Service.CommonService.Common", global::System.StringComparison.Ordinal))
            {
                return string.Concat("UniCloud.Application.CommonServiceBC.DTO.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// 架构中不存在 Documents 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<DocumentDTO> Documents
        {
            get
            {
                if ((this._Documents == null))
                {
                    this._Documents = base.CreateQuery<DocumentDTO>("Documents");
                }
                return this._Documents;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<DocumentDTO> _Documents;
        /// <summary>
        /// 架构中不存在 DocumentTypes 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<DocumentTypeDTO> DocumentTypes
        {
            get
            {
                if ((this._DocumentTypes == null))
                {
                    this._DocumentTypes = base.CreateQuery<DocumentTypeDTO>("DocumentTypes");
                }
                return this._DocumentTypes;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<DocumentTypeDTO> _DocumentTypes;
        /// <summary>
        /// 架构中不存在 Documents 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToDocuments(DocumentDTO documentDTO)
        {
            base.AddObject("Documents", documentDTO);
        }
        /// <summary>
        /// 架构中不存在 DocumentTypes 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToDocumentTypes(DocumentTypeDTO documentTypeDTO)
        {
            base.AddObject("DocumentTypes", documentTypeDTO);
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private abstract class GeneratedEdmModel
        {
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private static global::Microsoft.Data.Edm.IEdmModel ParsedModel = LoadModelFromString();
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
            private const string ModelPart0 = "<edmx:Edmx Version=\"1.0\" xmlns:edmx=\"http://schemas.microsoft.com/ado/2007/06/edm" +
                "x\"><edmx:DataServices m:DataServiceVersion=\"1.0\" m:MaxDataServiceVersion=\"3.0\" x" +
                "mlns:m=\"http://schemas.microsoft.com/ado/2007/08/dataservices/metadata\"><Schema " +
                "Namespace=\"UniCloud.Application.CommonServiceBC.DTO\" xmlns=\"http://schemas.micro" +
                "soft.com/ado/2007/05/edm\"><EntityType Name=\"DocumentDTO\"><Key><PropertyRef Name=" +
                "\"DocumentId\" /></Key><Property Name=\"DocumentId\" Type=\"Edm.Guid\" Nullable=\"false" +
                "\" /><Property Name=\"Name\" Type=\"Edm.String\" /><Property Name=\"Extension\" Type=\"E" +
                "dm.String\" /><Property Name=\"FileStorage\" Type=\"Edm.Binary\" /><Property Name=\"Fi" +
                "leContent\" Type=\"Edm.String\" /><Property Name=\"DocumentTypeId\" Type=\"Edm.Int32\" " +
                "Nullable=\"false\" /><Property Name=\"Abstract\" Type=\"Edm.String\" /><Property Name=" +
                "\"Note\" Type=\"Edm.String\" /><Property Name=\"Uploader\" Type=\"Edm.String\" /><Proper" +
                "ty Name=\"IsValid\" Type=\"Edm.Boolean\" Nullable=\"false\" /><Property Name=\"CreateTi" +
                "me\" Type=\"Edm.DateTime\" Nullable=\"false\" /><Property Name=\"UpdateTime\" Type=\"Edm" +
                ".DateTime\" Nullable=\"false\" /></EntityType><EntityType Name=\"DocumentTypeDTO\"><K" +
                "ey><PropertyRef Name=\"DocumentTypeId\" /></Key><Property Name=\"DocumentTypeId\" Ty" +
                "pe=\"Edm.Int32\" Nullable=\"false\" /><Property Name=\"IsChecked\" Type=\"Edm.Boolean\" " +
                "Nullable=\"false\" /><Property Name=\"Name\" Type=\"Edm.String\" /><Property Name=\"Des" +
                "cription\" Type=\"Edm.String\" /></EntityType></Schema><Schema Namespace=\"UniCloud." +
                "DistributedServices.CommonService\" xmlns=\"http://schemas.microsoft.com/ado/2007/" +
                "05/edm\"><EntityContainer Name=\"CommonServiceData\" m:IsDefaultEntityContainer=\"tr" +
                "ue\"><EntitySet Name=\"Documents\" EntityType=\"UniCloud.Application.CommonServiceBC" +
                ".DTO.DocumentDTO\" /><EntitySet Name=\"DocumentTypes\" EntityType=\"UniCloud.Applica" +
                "tion.CommonServiceBC.DTO.DocumentTypeDTO\" /><FunctionImport Name=\"SearchDocument" +
                "\" ReturnType=\"Collection(UniCloud.Application.CommonServiceBC.DTO.DocumentDTO)\" " +
                "EntitySet=\"Documents\" m:HttpMethod=\"GET\"><Parameter Name=\"keyword\" Type=\"Edm.Str" +
                "ing\" /><Parameter Name=\"documentType\" Type=\"Edm.String\" /></FunctionImport><Func" +
                "tionImport Name=\"GetSingleDocument\" ReturnType=\"UniCloud.Application.CommonServi" +
                "ceBC.DTO.DocumentDTO\" EntitySet=\"Documents\" m:HttpMethod=\"GET\"><Parameter Name=\"" +
                "documentId\" Type=\"Edm.String\" /></FunctionImport></EntityContainer></Schema></ed" +
                "mx:DataServices></edmx:Edmx>";
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
    /// 架构中不存在 UniCloud.Application.CommonServiceBC.DTO.DocumentDTO 的注释。
    /// </summary>
    /// <KeyProperties>
    /// DocumentId
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("Documents")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("DocumentId")]
    public partial class DocumentDTO : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 创建新的 DocumentDTO 对象。
        /// </summary>
        /// <param name="documentId">DocumentId 的初始值。</param>
        /// <param name="documentTypeId">DocumentTypeId 的初始值。</param>
        /// <param name="isValid">IsValid 的初始值。</param>
        /// <param name="createTime">CreateTime 的初始值。</param>
        /// <param name="updateTime">UpdateTime 的初始值。</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static DocumentDTO CreateDocumentDTO(global::System.Guid documentId, int documentTypeId, bool isValid, global::System.DateTime createTime, global::System.DateTime updateTime)
        {
            DocumentDTO documentDTO = new DocumentDTO();
            documentDTO.DocumentId = documentId;
            documentDTO.DocumentTypeId = documentTypeId;
            documentDTO.IsValid = isValid;
            documentDTO.CreateTime = createTime;
            documentDTO.UpdateTime = updateTime;
            return documentDTO;
        }
        /// <summary>
        /// 架构中不存在属性 DocumentId 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Guid DocumentId
        {
            get
            {
                return this._DocumentId;
            }
            set
            {
                this.OnDocumentIdChanging(value);
                this._DocumentId = value;
                this.OnDocumentIdChanged();
                this.OnPropertyChanged("DocumentId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Guid _DocumentId;
        partial void OnDocumentIdChanging(global::System.Guid value);
        partial void OnDocumentIdChanged();
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
        /// <summary>
        /// 架构中不存在属性 Extension 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Extension
        {
            get
            {
                return this._Extension;
            }
            set
            {
                this.OnExtensionChanging(value);
                this._Extension = value;
                this.OnExtensionChanged();
                this.OnPropertyChanged("Extension");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Extension;
        partial void OnExtensionChanging(string value);
        partial void OnExtensionChanged();
        /// <summary>
        /// 架构中不存在属性 FileStorage 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public byte[] FileStorage
        {
            get
            {
                if ((this._FileStorage != null))
                {
                    return ((byte[])(this._FileStorage.Clone()));
                }
                else
                {
                    return null;
                }
            }
            set
            {
                this.OnFileStorageChanging(value);
                this._FileStorage = value;
                this.OnFileStorageChanged();
                this.OnPropertyChanged("FileStorage");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private byte[] _FileStorage;
        partial void OnFileStorageChanging(byte[] value);
        partial void OnFileStorageChanged();
        /// <summary>
        /// 架构中不存在属性 FileContent 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string FileContent
        {
            get
            {
                return this._FileContent;
            }
            set
            {
                this.OnFileContentChanging(value);
                this._FileContent = value;
                this.OnFileContentChanged();
                this.OnPropertyChanged("FileContent");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _FileContent;
        partial void OnFileContentChanging(string value);
        partial void OnFileContentChanged();
        /// <summary>
        /// 架构中不存在属性 DocumentTypeId 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int DocumentTypeId
        {
            get
            {
                return this._DocumentTypeId;
            }
            set
            {
                this.OnDocumentTypeIdChanging(value);
                this._DocumentTypeId = value;
                this.OnDocumentTypeIdChanged();
                this.OnPropertyChanged("DocumentTypeId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _DocumentTypeId;
        partial void OnDocumentTypeIdChanging(int value);
        partial void OnDocumentTypeIdChanged();
        /// <summary>
        /// 架构中不存在属性 Abstract 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Abstract
        {
            get
            {
                return this._Abstract;
            }
            set
            {
                this.OnAbstractChanging(value);
                this._Abstract = value;
                this.OnAbstractChanged();
                this.OnPropertyChanged("Abstract");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Abstract;
        partial void OnAbstractChanging(string value);
        partial void OnAbstractChanged();
        /// <summary>
        /// 架构中不存在属性 Note 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Note
        {
            get
            {
                return this._Note;
            }
            set
            {
                this.OnNoteChanging(value);
                this._Note = value;
                this.OnNoteChanged();
                this.OnPropertyChanged("Note");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Note;
        partial void OnNoteChanging(string value);
        partial void OnNoteChanged();
        /// <summary>
        /// 架构中不存在属性 Uploader 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Uploader
        {
            get
            {
                return this._Uploader;
            }
            set
            {
                this.OnUploaderChanging(value);
                this._Uploader = value;
                this.OnUploaderChanged();
                this.OnPropertyChanged("Uploader");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Uploader;
        partial void OnUploaderChanging(string value);
        partial void OnUploaderChanged();
        /// <summary>
        /// 架构中不存在属性 IsValid 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this.OnIsValidChanging(value);
                this._IsValid = value;
                this.OnIsValidChanged();
                this.OnPropertyChanged("IsValid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private bool _IsValid;
        partial void OnIsValidChanging(bool value);
        partial void OnIsValidChanged();
        /// <summary>
        /// 架构中不存在属性 CreateTime 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.DateTime CreateTime
        {
            get
            {
                return this._CreateTime;
            }
            set
            {
                this.OnCreateTimeChanging(value);
                this._CreateTime = value;
                this.OnCreateTimeChanged();
                this.OnPropertyChanged("CreateTime");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.DateTime _CreateTime;
        partial void OnCreateTimeChanging(global::System.DateTime value);
        partial void OnCreateTimeChanged();
        /// <summary>
        /// 架构中不存在属性 UpdateTime 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.DateTime UpdateTime
        {
            get
            {
                return this._UpdateTime;
            }
            set
            {
                this.OnUpdateTimeChanging(value);
                this._UpdateTime = value;
                this.OnUpdateTimeChanged();
                this.OnPropertyChanged("UpdateTime");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.DateTime _UpdateTime;
        partial void OnUpdateTimeChanging(global::System.DateTime value);
        partial void OnUpdateTimeChanged();
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
    /// <summary>
    /// 架构中不存在 UniCloud.Application.CommonServiceBC.DTO.DocumentTypeDTO 的注释。
    /// </summary>
    /// <KeyProperties>
    /// DocumentTypeId
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("DocumentTypes")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("DocumentTypeId")]
    public partial class DocumentTypeDTO : global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 创建新的 DocumentTypeDTO 对象。
        /// </summary>
        /// <param name="documentTypeId">DocumentTypeId 的初始值。</param>
        /// <param name="isChecked">IsChecked 的初始值。</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static DocumentTypeDTO CreateDocumentTypeDTO(int documentTypeId, bool isChecked)
        {
            DocumentTypeDTO documentTypeDTO = new DocumentTypeDTO();
            documentTypeDTO.DocumentTypeId = documentTypeId;
            documentTypeDTO.IsChecked = isChecked;
            return documentTypeDTO;
        }
        /// <summary>
        /// 架构中不存在属性 DocumentTypeId 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int DocumentTypeId
        {
            get
            {
                return this._DocumentTypeId;
            }
            set
            {
                this.OnDocumentTypeIdChanging(value);
                this._DocumentTypeId = value;
                this.OnDocumentTypeIdChanged();
                this.OnPropertyChanged("DocumentTypeId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _DocumentTypeId;
        partial void OnDocumentTypeIdChanging(int value);
        partial void OnDocumentTypeIdChanged();
        /// <summary>
        /// 架构中不存在属性 IsChecked 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public bool IsChecked
        {
            get
            {
                return this._IsChecked;
            }
            set
            {
                this.OnIsCheckedChanging(value);
                this._IsChecked = value;
                this.OnIsCheckedChanged();
                this.OnPropertyChanged("IsChecked");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private bool _IsChecked;
        partial void OnIsCheckedChanging(bool value);
        partial void OnIsCheckedChanged();
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
        /// <summary>
        /// 架构中不存在属性 Description 的注释。
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this.OnDescriptionChanging(value);
                this._Description = value;
                this.OnDescriptionChanged();
                this.OnPropertyChanged("Description");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Description;
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
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
