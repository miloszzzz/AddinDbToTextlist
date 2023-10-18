using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AddinDbToTextlist.Models
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

        private DocumentSWBlocksGlobalDB sWBlocksGlobalDBField;

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
        [System.Xml.Serialization.XmlElementAttribute("SW.Blocks.GlobalDB")]
        public DocumentSWBlocksGlobalDB SWBlocksGlobalDB
        {
            get
            {
                return this.sWBlocksGlobalDBField;
            }
            set
            {
                this.sWBlocksGlobalDBField = value;
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
    public partial class DocumentSWBlocksGlobalDB
    {

        private DocumentSWBlocksGlobalDBAttributeList attributeListField;

        private DocumentSWBlocksGlobalDBMultilingualText[] objectListField;

        private byte idField;

        /// <remarks/>
        public DocumentSWBlocksGlobalDBAttributeList AttributeList
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
        public DocumentSWBlocksGlobalDBMultilingualText[] ObjectList
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
    public partial class DocumentSWBlocksGlobalDBAttributeList
    {

        private bool autoNumberField;

        private bool dBAccessibleFromOPCUAField;

        private object headerAuthorField;

        private object headerFamilyField;

        private object headerNameField;

        private decimal headerVersionField;

        private DocumentSWBlocksGlobalDBAttributeListInterface interfaceField;

        private bool isOnlyStoredInLoadMemoryField;

        private bool isRetainMemResEnabledField;

        private bool isWriteProtectedInASField;

        private string memoryLayoutField;

        private byte memoryReserveField;

        private string nameField;

        private byte numberField;

        private string programmingLanguageField;

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
        public bool DBAccessibleFromOPCUA
        {
            get
            {
                return this.dBAccessibleFromOPCUAField;
            }
            set
            {
                this.dBAccessibleFromOPCUAField = value;
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
        public DocumentSWBlocksGlobalDBAttributeListInterface Interface
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
        public bool IsOnlyStoredInLoadMemory
        {
            get
            {
                return this.isOnlyStoredInLoadMemoryField;
            }
            set
            {
                this.isOnlyStoredInLoadMemoryField = value;
            }
        }

        /// <remarks/>
        public bool IsRetainMemResEnabled
        {
            get
            {
                return this.isRetainMemResEnabledField;
            }
            set
            {
                this.isRetainMemResEnabledField = value;
            }
        }

        /// <remarks/>
        public bool IsWriteProtectedInAS
        {
            get
            {
                return this.isWriteProtectedInASField;
            }
            set
            {
                this.isWriteProtectedInASField = value;
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
        public byte MemoryReserve
        {
            get
            {
                return this.memoryReserveField;
            }
            set
            {
                this.memoryReserveField = value;
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
        public byte Number
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksGlobalDBAttributeListInterface
    {

        private Sections sectionsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4")]
        public Sections Sections
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
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4", IsNullable = false)]
    public partial class Sections
    {

        private SectionsSection sectionField;

        /// <remarks/>
        public SectionsSection Section
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

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4")]
    public partial class SectionsSection
    {

        private SectionsSectionMember[] memberField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Member")]
        public SectionsSectionMember[] Member
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

        private SectionsSectionMemberBooleanAttribute[] attributeListField;

        private short startValueField;

        private bool startValueFieldSpecified;

        private SectionsSectionMember[] memberField;

        private string nameField;

        private string datatypeField;

        private string remanenceField;

        private string accessibilityField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("BooleanAttribute", IsNullable = false)]
        public SectionsSectionMemberBooleanAttribute[] AttributeList
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
        public short StartValue
        {
            get
            {
                return this.startValueField;
            }
            set
            {
                this.startValueField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StartValueSpecified
        {
            get
            {
                return this.startValueFieldSpecified;
            }
            set
            {
                this.startValueFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Member")]
        public SectionsSectionMember[] Member
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
        public string Remanence
        {
            get
            {
                return this.remanenceField;
            }
            set
            {
                this.remanenceField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4")]
    public partial class SectionsSectionMemberBooleanAttribute
    {

        private string nameField;

        private bool systemDefinedField;

        private bool valueField;

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
        public bool SystemDefined
        {
            get
            {
                return this.systemDefinedField;
            }
            set
            {
                this.systemDefinedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public bool Value
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4")]
    public partial class SectionsSectionMemberMember
    {

        private SectionsSectionMemberMemberBooleanAttribute[] attributeListField;

        private ushort startValueField;

        private string nameField;

        private string datatypeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("BooleanAttribute", IsNullable = false)]
        public SectionsSectionMemberMemberBooleanAttribute[] AttributeList
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
        public ushort StartValue
        {
            get
            {
                return this.startValueField;
            }
            set
            {
                this.startValueField = value;
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
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.siemens.com/automation/Openness/SW/Interface/v4")]
    public partial class SectionsSectionMemberMemberBooleanAttribute
    {

        private string nameField;

        private bool systemDefinedField;

        private bool valueField;

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
        public bool SystemDefined
        {
            get
            {
                return this.systemDefinedField;
            }
            set
            {
                this.systemDefinedField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public bool Value
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class DocumentSWBlocksGlobalDBMultilingualText
    {

        private DocumentSWBlocksGlobalDBMultilingualTextMultilingualTextItem[] objectListField;

        private byte idField;

        private string compositionNameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("MultilingualTextItem", IsNullable = false)]
        public DocumentSWBlocksGlobalDBMultilingualTextMultilingualTextItem[] ObjectList
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
    public partial class DocumentSWBlocksGlobalDBMultilingualTextMultilingualTextItem
    {

        private DocumentSWBlocksGlobalDBMultilingualTextMultilingualTextItemAttributeList attributeListField;

        private byte idField;

        private string compositionNameField;

        /// <remarks/>
        public DocumentSWBlocksGlobalDBMultilingualTextMultilingualTextItemAttributeList AttributeList
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
    public partial class DocumentSWBlocksGlobalDBMultilingualTextMultilingualTextItemAttributeList
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



}
