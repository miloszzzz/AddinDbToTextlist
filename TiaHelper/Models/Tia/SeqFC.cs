using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiaHelperLibrary.Models.Tia
{
    // UWAGA: Wygenerowany kod może wymagać co najmniej programu .NET Framework 4.5 lub .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class Document
    {

        private DocumentEngineering engineeringField;

        private DocumentDocumentInfo documentInfoField;

        private DocumentSWBlocksFC sWBlocksFCField;

        /// <remarks/>
        public DocumentEngineering Engineering
        {
            get
            {
                return this.engineeringField;
            }
            set
            {
                this.engineeringField = value;
            }
        }

        /// <remarks/>
        public DocumentDocumentInfo DocumentInfo
        {
            get
            {
                return this.documentInfoField;
            }
            set
            {
                this.documentInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SW.Blocks.FC")]
        public DocumentSWBlocksFC SWBlocksFC
        {
            get
            {
                return this.sWBlocksFCField;
            }
            set
            {
                this.sWBlocksFCField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentEngineering
    {

        private string versionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentDocumentInfo
    {

        private System.DateTime createdField;

        private string exportSettingField;

        private DocumentDocumentInfoInstalledProducts installedProductsField;

        /// <remarks/>
        public System.DateTime Created
        {
            get
            {
                return this.createdField;
            }
            set
            {
                this.createdField = value;
            }
        }

        /// <remarks/>
        public string ExportSetting
        {
            get
            {
                return this.exportSettingField;
            }
            set
            {
                this.exportSettingField = value;
            }
        }

        /// <remarks/>
        public DocumentDocumentInfoInstalledProducts InstalledProducts
        {
            get
            {
                return this.installedProductsField;
            }
            set
            {
                this.installedProductsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentDocumentInfoInstalledProducts
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("OptionPackage", typeof(DocumentDocumentInfoInstalledProductsOptionPackage))]
        [System.Xml.Serialization.XmlElementAttribute("Product", typeof(DocumentDocumentInfoInstalledProductsProduct))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentDocumentInfoInstalledProductsOptionPackage
    {

        private string displayNameField;

        private string displayVersionField;

        /// <remarks/>
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
            }
        }

        /// <remarks/>
        public string DisplayVersion
        {
            get
            {
                return this.displayVersionField;
            }
            set
            {
                this.displayVersionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentDocumentInfoInstalledProductsProduct
    {

        private string displayNameField;

        private string displayVersionField;

        /// <remarks/>
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
            }
        }

        /// <remarks/>
        public string DisplayVersion
        {
            get
            {
                return this.displayVersionField;
            }
            set
            {
                this.displayVersionField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFC
    {

        private DocumentSWBlocksFCAttributeList attributeListField;

        private DocumentSWBlocksFCObjectList objectListField;

        private byte idField;

        /// <remarks/>
        public DocumentSWBlocksFCAttributeList AttributeList
        {
            get
            {
                return this.attributeListField;
            }
            set
            {
                this.attributeListField = value;
            }
        }

        /// <remarks/>
        public DocumentSWBlocksFCObjectList ObjectList
        {
            get
            {
                return this.objectListField;
            }
            set
            {
                this.objectListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCAttributeList
    {

        private bool autoNumberField;

        private object headerAuthorField;

        private object headerFamilyField;

        private object headerNameField;

        private decimal headerVersionField;

        private DocumentSWBlocksFCAttributeListInterface interfaceField;

        private bool isIECCheckEnabledField;

        private string memoryLayoutField;

        private string nameField;

        private ushort numberField;

        private string programmingLanguageField;

        private bool setENOAutomaticallyField;

        private object uDABlockPropertiesField;

        private bool uDAEnableTagReadbackField;

        /// <remarks/>
        public bool AutoNumber
        {
            get
            {
                return this.autoNumberField;
            }
            set
            {
                this.autoNumberField = value;
            }
        }

        /// <remarks/>
        public object HeaderAuthor
        {
            get
            {
                return this.headerAuthorField;
            }
            set
            {
                this.headerAuthorField = value;
            }
        }

        /// <remarks/>
        public object HeaderFamily
        {
            get
            {
                return this.headerFamilyField;
            }
            set
            {
                this.headerFamilyField = value;
            }
        }

        /// <remarks/>
        public object HeaderName
        {
            get
            {
                return this.headerNameField;
            }
            set
            {
                this.headerNameField = value;
            }
        }

        /// <remarks/>
        public decimal HeaderVersion
        {
            get
            {
                return this.headerVersionField;
            }
            set
            {
                this.headerVersionField = value;
            }
        }

        /// <remarks/>
        public DocumentSWBlocksFCAttributeListInterface Interface
        {
            get
            {
                return this.interfaceField;
            }
            set
            {
                this.interfaceField = value;
            }
        }

        /// <remarks/>
        public bool IsIECCheckEnabled
        {
            get
            {
                return this.isIECCheckEnabledField;
            }
            set
            {
                this.isIECCheckEnabledField = value;
            }
        }

        /// <remarks/>
        public string MemoryLayout
        {
            get
            {
                return this.memoryLayoutField;
            }
            set
            {
                this.memoryLayoutField = value;
            }
        }

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public ushort Number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }

        /// <remarks/>
        public string ProgrammingLanguage
        {
            get
            {
                return this.programmingLanguageField;
            }
            set
            {
                this.programmingLanguageField = value;
            }
        }

        /// <remarks/>
        public bool SetENOAutomatically
        {
            get
            {
                return this.setENOAutomaticallyField;
            }
            set
            {
                this.setENOAutomaticallyField = value;
            }
        }

        /// <remarks/>
        public object UDABlockProperties
        {
            get
            {
                return this.uDABlockPropertiesField;
            }
            set
            {
                this.uDABlockPropertiesField = value;
            }
        }

        /// <remarks/>
        public bool UDAEnableTagReadback
        {
            get
            {
                return this.uDAEnableTagReadbackField;
            }
            set
            {
                this.uDAEnableTagReadbackField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCAttributeListInterface
    {

        private SectionsSection[] sectionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4")]
        [System.Xml.Serialization.XmlArrayItemAttribute("Section", IsNullable = false)]
        public SectionsSection[] Sections
        {
            get
            {
                return this.sectionsField;
            }
            set
            {
                this.sectionsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4")]
    public partial class SectionsSection
    {

        private SectionsSectionMember memberField;

        private string nameField;

        /// <remarks/>
        public SectionsSectionMember Member
        {
            get
            {
                return this.memberField;
            }
            set
            {
                this.memberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4")]
    public partial class SectionsSectionMember
    {

        private string nameField;

        private string datatypeField;

        private string accessibilityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Datatype
        {
            get
            {
                return this.datatypeField;
            }
            set
            {
                this.datatypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Accessibility
        {
            get
            {
                return this.accessibilityField;
            }
            set
            {
                this.accessibilityField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectList
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MultilingualText", typeof(DocumentSWBlocksFCObjectListMultilingualText))]
        [System.Xml.Serialization.XmlElementAttribute("SW.Blocks.CompileUnit", typeof(DocumentSWBlocksFCObjectListSWBlocksCompileUnit))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectListMultilingualText
    {

        private DocumentSWBlocksFCObjectListMultilingualTextMultilingualTextItem[] objectListField;

        private string idField;

        private string compositionNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextItem", IsNullable = false)]
        public DocumentSWBlocksFCObjectListMultilingualTextMultilingualTextItem[] ObjectList
        {
            get
            {
                return this.objectListField;
            }
            set
            {
                this.objectListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CompositionName
        {
            get
            {
                return this.compositionNameField;
            }
            set
            {
                this.compositionNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectListMultilingualTextMultilingualTextItem
    {

        private DocumentSWBlocksFCObjectListMultilingualTextMultilingualTextItemAttributeList attributeListField;

        private string idField;

        private string compositionNameField;

        /// <remarks/>
        public DocumentSWBlocksFCObjectListMultilingualTextMultilingualTextItemAttributeList AttributeList
        {
            get
            {
                return this.attributeListField;
            }
            set
            {
                this.attributeListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CompositionName
        {
            get
            {
                return this.compositionNameField;
            }
            set
            {
                this.compositionNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectListMultilingualTextMultilingualTextItemAttributeList
    {

        private string cultureField;

        private object textField;

        /// <remarks/>
        public string Culture
        {
            get
            {
                return this.cultureField;
            }
            set
            {
                this.cultureField = value;
            }
        }

        /// <remarks/>
        public object Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectListSWBlocksCompileUnit
    {

        private DocumentSWBlocksFCObjectListSWBlocksCompileUnitAttributeList attributeListField;

        private DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualText[] objectListField;

        private string idField;

        private string compositionNameField;

        /// <remarks/>
        public DocumentSWBlocksFCObjectListSWBlocksCompileUnitAttributeList AttributeList
        {
            get
            {
                return this.attributeListField;
            }
            set
            {
                this.attributeListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualText", IsNullable = false)]
        public DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualText[] ObjectList
        {
            get
            {
                return this.objectListField;
            }
            set
            {
                this.objectListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CompositionName
        {
            get
            {
                return this.compositionNameField;
            }
            set
            {
                this.compositionNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectListSWBlocksCompileUnitAttributeList
    {

        private DocumentSWBlocksFCObjectListSWBlocksCompileUnitAttributeListNetworkSource networkSourceField;

        private string programmingLanguageField;

        /// <remarks/>
        public DocumentSWBlocksFCObjectListSWBlocksCompileUnitAttributeListNetworkSource NetworkSource
        {
            get
            {
                return this.networkSourceField;
            }
            set
            {
                this.networkSourceField = value;
            }
        }

        /// <remarks/>
        public string ProgrammingLanguage
        {
            get
            {
                return this.programmingLanguageField;
            }
            set
            {
                this.programmingLanguageField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectListSWBlocksCompileUnitAttributeListNetworkSource
    {

        private FlgNet flgNetField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
        public FlgNet FlgNet
        {
            get
            {
                return this.flgNetField;
            }
            set
            {
                this.flgNetField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4", IsNullable = false)]
    public partial class FlgNet
    {

        private FlgNetParts partsField;

        private FlgNetWire[] wiresField;

        /// <remarks/>
        public FlgNetParts Parts
        {
            get
            {
                return this.partsField;
            }
            set
            {
                this.partsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Wire", IsNullable = false)]
        public FlgNetWire[] Wires
        {
            get
            {
                return this.wiresField;
            }
            set
            {
                this.wiresField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetParts
    {

        private object[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Access", typeof(FlgNetPartsAccess))]
        [System.Xml.Serialization.XmlElementAttribute("Call", typeof(FlgNetPartsCall))]
        [System.Xml.Serialization.XmlElementAttribute("Part", typeof(FlgNetPartsPart))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsAccess
    {

        private FlgNetPartsAccessConstant constantField;

        private FlgNetPartsAccessComponent[] symbolField;

        private string scopeField;

        private byte uIdField;

        /// <remarks/>
        public FlgNetPartsAccessConstant Constant
        {
            get
            {
                return this.constantField;
            }
            set
            {
                this.constantField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Component", IsNullable = false)]
        public FlgNetPartsAccessComponent[] Symbol
        {
            get
            {
                return this.symbolField;
            }
            set
            {
                this.symbolField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Scope
        {
            get
            {
                return this.scopeField;
            }
            set
            {
                this.scopeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte UId
        {
            get
            {
                return this.uIdField;
            }
            set
            {
                this.uIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsAccessConstant
    {

        private string constantTypeField;

        private string constantValueField;

        private string nameField;

        /// <remarks/>
        public string ConstantType
        {
            get
            {
                return this.constantTypeField;
            }
            set
            {
                this.constantTypeField = value;
            }
        }

        /// <remarks/>
        public string ConstantValue
        {
            get
            {
                return this.constantValueField;
            }
            set
            {
                this.constantValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsAccessComponent
    {

        private FlgNetPartsAccessComponentAccess accessField;

        private string nameField;

        private string accessModifierField;

        /// <remarks/>
        public FlgNetPartsAccessComponentAccess Access
        {
            get
            {
                return this.accessField;
            }
            set
            {
                this.accessField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AccessModifier
        {
            get
            {
                return this.accessModifierField;
            }
            set
            {
                this.accessModifierField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsAccessComponentAccess
    {

        private FlgNetPartsAccessComponentAccessConstant constantField;

        private string scopeField;

        /// <remarks/>
        public FlgNetPartsAccessComponentAccessConstant Constant
        {
            get
            {
                return this.constantField;
            }
            set
            {
                this.constantField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Scope
        {
            get
            {
                return this.scopeField;
            }
            set
            {
                this.scopeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsAccessComponentAccessConstant
    {

        private string constantTypeField;

        private ushort constantValueField;

        /// <remarks/>
        public string ConstantType
        {
            get
            {
                return this.constantTypeField;
            }
            set
            {
                this.constantTypeField = value;
            }
        }

        /// <remarks/>
        public ushort ConstantValue
        {
            get
            {
                return this.constantValueField;
            }
            set
            {
                this.constantValueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsCall
    {

        private FlgNetPartsCallCallInfo callInfoField;

        private byte uIdField;

        /// <remarks/>
        public FlgNetPartsCallCallInfo CallInfo
        {
            get
            {
                return this.callInfoField;
            }
            set
            {
                this.callInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte UId
        {
            get
            {
                return this.uIdField;
            }
            set
            {
                this.uIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsCallCallInfo
    {

        private FlgNetPartsCallCallInfoParameter[] parameterField;

        private string nameField;

        private string blockTypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Parameter")]
        public FlgNetPartsCallCallInfoParameter[] Parameter
        {
            get
            {
                return this.parameterField;
            }
            set
            {
                this.parameterField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BlockType
        {
            get
            {
                return this.blockTypeField;
            }
            set
            {
                this.blockTypeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsCallCallInfoParameter
    {

        private string nameField;

        private string sectionField;

        private string typeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Section
        {
            get
            {
                return this.sectionField;
            }
            set
            {
                this.sectionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsPart
    {

        private FlgNetPartsPartInstance instanceField;

        private FlgNetPartsPartTemplateValue templateValueField;

        private FlgNetPartsPartNegated negatedField;

        private string nameField;

        private byte uIdField;

        private decimal versionField;

        private bool versionFieldSpecified;

        private bool disabledENOField;

        private bool disabledENOFieldSpecified;

        /// <remarks/>
        public FlgNetPartsPartInstance Instance
        {
            get
            {
                return this.instanceField;
            }
            set
            {
                this.instanceField = value;
            }
        }

        /// <remarks/>
        public FlgNetPartsPartTemplateValue TemplateValue
        {
            get
            {
                return this.templateValueField;
            }
            set
            {
                this.templateValueField = value;
            }
        }

        /// <remarks/>
        public FlgNetPartsPartNegated Negated
        {
            get
            {
                return this.negatedField;
            }
            set
            {
                this.negatedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte UId
        {
            get
            {
                return this.uIdField;
            }
            set
            {
                this.uIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VersionSpecified
        {
            get
            {
                return this.versionFieldSpecified;
            }
            set
            {
                this.versionFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool DisabledENO
        {
            get
            {
                return this.disabledENOField;
            }
            set
            {
                this.disabledENOField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DisabledENOSpecified
        {
            get
            {
                return this.disabledENOFieldSpecified;
            }
            set
            {
                this.disabledENOFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsPartInstance
    {

        private FlgNetPartsPartInstanceComponent[] componentField;

        private string scopeField;

        private byte uIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Component")]
        public FlgNetPartsPartInstanceComponent[] Component
        {
            get
            {
                return this.componentField;
            }
            set
            {
                this.componentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Scope
        {
            get
            {
                return this.scopeField;
            }
            set
            {
                this.scopeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte UId
        {
            get
            {
                return this.uIdField;
            }
            set
            {
                this.uIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsPartInstanceComponent
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsPartTemplateValue
    {

        private string nameField;

        private string typeField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetPartsPartNegated
    {

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetWire
    {

        private object[] itemsField;

        private byte uIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("IdentCon", typeof(FlgNetWireIdentCon))]
        [System.Xml.Serialization.XmlElementAttribute("NameCon", typeof(FlgNetWireNameCon))]
        [System.Xml.Serialization.XmlElementAttribute("OpenCon", typeof(FlgNetWireOpenCon))]
        [System.Xml.Serialization.XmlElementAttribute("Powerrail", typeof(object))]
        public object[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte UId
        {
            get
            {
                return this.uIdField;
            }
            set
            {
                this.uIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetWireIdentCon
    {

        private byte uIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte UId
        {
            get
            {
                return this.uIdField;
            }
            set
            {
                this.uIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetWireNameCon
    {

        private byte uIdField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte UId
        {
            get
            {
                return this.uIdField;
            }
            set
            {
                this.uIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/NetworkSource/FlgNet/v4")]
    public partial class FlgNetWireOpenCon
    {

        private byte uIdField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte UId
        {
            get
            {
                return this.uIdField;
            }
            set
            {
                this.uIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualText
    {

        private DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualTextMultilingualTextItem[] objectListField;

        private string idField;

        private string compositionNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextItem", IsNullable = false)]
        public DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualTextMultilingualTextItem[] ObjectList
        {
            get
            {
                return this.objectListField;
            }
            set
            {
                this.objectListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CompositionName
        {
            get
            {
                return this.compositionNameField;
            }
            set
            {
                this.compositionNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualTextMultilingualTextItem
    {

        private DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualTextMultilingualTextItemAttributeList attributeListField;

        private string idField;

        private string compositionNameField;

        /// <remarks/>
        public DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualTextMultilingualTextItemAttributeList AttributeList
        {
            get
            {
                return this.attributeListField;
            }
            set
            {
                this.attributeListField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string CompositionName
        {
            get
            {
                return this.compositionNameField;
            }
            set
            {
                this.compositionNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualTextMultilingualTextItemAttributeList
    {

        private string cultureField;

        private string textField;

        /// <remarks/>
        public string Culture
        {
            get
            {
                return this.cultureField;
            }
            set
            {
                this.cultureField = value;
            }
        }

        /// <remarks/>
        public string Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4", IsNullable = false)]
    public partial class Sections
    {

        private SectionsSection[] sectionField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Section")]
        public SectionsSection[] Section
        {
            get
            {
                return this.sectionField;
            }
            set
            {
                this.sectionField = value;
            }
        }
    }


}
