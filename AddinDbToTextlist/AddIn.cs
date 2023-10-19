//using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.HW;
using Siemens.Engineering.SW;
using Siemens.Engineering;
using Siemens.Engineering.Hmi;
using System.Windows.Forms;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW.Tags;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Xml;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using AddinDbToTextlist.Helpers;
using static TiaHelperLibrary.TiaHelper;
using System.Xml.Serialization;
using AddinDbToTextlist.Models;
using TiaXmlGenerator;
using TiaXmlGenerator.Models;
using System.Globalization;
using DeepL.Model;
using DeepL;
using Siemens.Engineering.Hmi.TextGraphicList;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.Remoting.Messaging;
using TiaXmlGenerator.Helpers;
using Language = Siemens.Engineering.Language;
using TextBox = System.Windows.Forms.TextBox;
using Button = System.Windows.Forms.Button;

namespace AddinDbToTextlist
{
    public class AddIn : ContextMenuAddIn
    {
        /// <summary>
        ///The global TIA Portal Object 
        ///<para>It will be used in the TIA Add-In.</para>
        /// </summary>
        TiaPortal _tiaportal;
        bool onlyValidValues = false;
        bool keyDuplicated = false;

        /// <summary>
        /// The display name of the Add-In.
        /// </summary>
        private const string s_DisplayNameOfAddIn = "Test";

        /// <summary>
        /// The constructor of the AddIn.
        /// Creates an object of the class AddIn
        /// Called from AddInProvider, when the first
        /// right-click is performed in TIA
        /// Motherclass' constructor of ContextMenuAddin
        /// will be executed, too. 
        /// </summary>
        /// <param name="tiaportal">
        /// Represents the actual used TIA Portal process.
        /// </param>
        public AddIn(TiaPortal tiaportal) : base(s_DisplayNameOfAddIn)
        {
            /*
             * The acutal TIA Portal process is saved in the 
             * global TIA Portal variable _tiaportal
             * tiaportal comes as input Parameter from the
             * AddInProvider
            */
            _tiaportal = tiaportal;
        }

        /// <summary>
        /// The method is supplemented to include the Add-In
        /// in the Context Menu of TIA Portal.
        /// Called when a right-click is performed in TIA
        /// and a mouse-over is performed on the name of the Add-In.
        /// </summary>
        /// <typeparam name="addInRootSubmenu">
        /// The Add-In will be displayed in 
        /// the Context Menu of TIA Portal.
        /// </typeparam>
        /// <example>
        /// ActionItems like Buttons/Checkboxes/Radiobuttons
        /// are possible. In this example, only Buttons will be created 
        /// which will start the Add-In program code.
        /// </example>
        protected override void BuildContextMenuItems(ContextMenuAddInRoot
            addInRootSubmenu)
        {
            /* Method addInRootSubmenu.Items.AddActionItem
             * Will Create a Pushbutton with the text 'Start Add-In Code'
             * 1st input parameter of AddActionItem is the text of the 
             *          button
             * 2nd input parameter of AddActionItem is the clickDelegate, 
             *          which will be executed in case the button 'Start 
             *          Add-In Code' will be clicked/pressed.
             * 3rd input parameter of AddActionItem is the 
             *          updateStatusDelegate, which will be executed in 
             *          case there is a mouseover the button 'Start 
             *          Add-In Code'.
             * in <placeholder> the type of AddActionItem will be 
             *          specified, because AddActionItem is generic 
             * AddActionItem<DeviceItem> will create a button that will be 
             *          displayed if a rightclick on a DeviceItem will be 
             *          performed in TIA Portal
             * AddActionItem<Project> will create a button that will be 
             *          displayed if a rightclick on the project name 
             *          will be performed in TIA Portal
            */
            /*addInRootSubmenu.Items.AddActionItem<Project>(
                "Test", OnDoSomething, OnCanSomething);*/

            /*addInRootSubmenu.Items.AddActionItem<DeviceItem>(
                "Test", OnDoSomething, OnCanSomething);*/

            addInRootSubmenu.Items.AddActionItem<PlcBlock>(
                "Test", OnDoSomething, OnCanSomething);

            /*addInRootSubmenu.Items.AddActionItem<PlcBlockGroup>(
                "Test", OnDoSomething, OnCanSomething);*/
        }

        /// <summary>
        /// The method contains the program code of the TIA Add-In.
        /// Called when the button 'Start Add-In Code' will be pressed.
        /// </summary>
        /// <typeparam name="menuSelectionProvider">
        /// here, the same type as in addInRootSubmenu.Items.AddActionItem
        /// must be used -> here it is <DeviceItem>
        /// </typeparam>
        private void OnDoSomething(MenuSelectionProvider<Project>
            menuSelectionProvider)
        {
            try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex) 
            { 
                LogToFile.Save(ex); 
            }
        }

        private void OnDoSomething(MenuSelectionProvider<DeviceItem>
            menuSelectionProvider)
        {
            try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                LogToFile.Save(ex);
            }
        }

        private void OnDoSomething(MenuSelectionProvider<PlcBlock>
            menuSelectionProvider)
        {
            try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                LogToFile.Save(ex);
            }
        }

        private void OnDoSomething(MenuSelectionProvider<PlcBlockGroup>
            menuSelectionProvider)
        {
            try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                LogToFile.Save(ex);
            }
        }

        /// <summary>
        /// Called when there is a mousover the button at a DeviceItem.
        /// It will be used to enable the button.
        /// </summary>
        /// <typeparam name="menuSelectionProvider">
        /// here, the same type as in addInRootSubmenu.Items.AddActionItem
        /// must be used -> here it is <DeviceItem>
        /// </typeparam>
        private MenuStatus OnCanSomething(MenuSelectionProvider<Project> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Disabled;
        }

        private MenuStatus OnCanSomething(MenuSelectionProvider<PlcBlock> menuSelectionProvider)
        {
            //enable the button
            PlcBlock selection = (PlcBlock)menuSelectionProvider.GetSelection().FirstOrDefault();


            if (selection.ProgrammingLanguage is ProgrammingLanguage.DB)
            {
                return MenuStatus.Enabled;
            }

            return MenuStatus.Disabled;
        }

        private MenuStatus OnCanSomething(MenuSelectionProvider<DeviceItem> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Disabled;
        }

        private MenuStatus OnCanSomething(MenuSelectionProvider<PlcBlockGroup> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Disabled;
        }


        /// <summary>
        /// This method will be invoked by the TIA Add-In Tester. The return value of this
        /// method will be provided in the Click- and UpdateDelegate's MenuSelectionProvider
        /// </summary>
        /// <param name="label">Label of the context menu item</param>
        /// <returns>Objects to provide for the MenuSelectionProvider</returns>
        public IEnumerable<IEngineeringObject> GetSelection(string label)
        {
            // go to project settings -> Debug -> command line arguments
            // specify which context menu item to test at --item
            PlcSoftware software = GetPlcSoftware(_tiaportal);

            switch (label)
            {
                case "Test":
                    /*var myPlc = project
                         .Devices[1]
                        .DeviceItems.FirstOrDefault(plc => plc.Name.Length > 0)
                        .GetService<SoftwareContainer>().Software as PlcSoftware;
                    // return the program blocks folder*/
                    PlcBlockGroup blockGroup = GetGroupByBlockName(software.BlockGroup, "DB_StatusDef");


                    return blockGroup.Blocks.Where<PlcBlock>(b => b.ProgrammingLanguage == ProgrammingLanguage.DB);

                default:
                    return Enumerable.Empty<IEngineeringObject>();
            }
        }



        private static bool CheckCancellation(ExclusiveAccess accesWindow)
        {
            if (accesWindow.IsCancellationRequested)
            {
                accesWindow.Dispose();
                return true;
            }
            return false;
        }


        public void DbToTextlist(MenuSelectionProvider menuSelectionProvider)
        {           
            PlcSoftware software = GetPlcSoftware(menuSelectionProvider);
            List<CultureInfo> projectLanguages = new List<CultureInfo>();
            foreach(Language language in _tiaportal.Projects.FirstOrDefault().LanguageSettings.ActiveLanguages)
            {
                projectLanguages.Add(language.Culture);
            }
            

            PlcBlock block = menuSelectionProvider.GetSelection<PlcBlock>().FirstOrDefault();

            // File to export
            FileInfo xmlFilePath = new FileInfo(Path.GetTempFileName() + ".xml");
            block.Export(xmlFilePath, ExportOptions.WithDefaults);


            // Dictionary with statuses - only one description per status number
            string xmlData = File.ReadAllText(xmlFilePath.FullName);

            XmlSerializer serializer = new XmlSerializer(typeof(Document));
            string text = string.Empty;
            string prefix = string.Empty;

            TextListGen textListGen = new TextListGen("textlist", projectLanguages.Count);


            #region Window
            /// 
            /// Window with text
            ///
            Form window = new Form();
            window.Width = 800;
            window.Height = 600;
            window.Text = "DB to Textlist";


            /// Option checkbox
            CheckBox checkBox = new CheckBox();
            checkBox.Checked = true;
            checkBox.Text = "Przepisz tylko zmienne int";
            checkBox.Width = 250;
            checkBox.Left = 10;
            checkBox.Margin = new Padding(10);


            /// Textlist name input
            TextBox textInput = new TextBox();
            textInput.Enabled = true;
            textInput.MaxLength = 30;
            textInput.Top = 30;
            textInput.Left = 60;
            textInput.Width = 200;
            textInput.Margin = new Padding(10);


            Label labelName = new Label();
            labelName.Enabled = false;
            labelName.Text = "Nazwa:";
            labelName.Top = 30;
            labelName.Left = 10;
            labelName.Width = 50;


            /// Button
            Button btnOk = new Button();
            btnOk.Text = "OK";
            btnOk.Left = 90;
            btnOk.Top = 60;
            btnOk.Width = 80;
            btnOk.Click += (sender, e) =>
            {
                textListGen.Name = textInput.Text;
                onlyValidValues = checkBox.Checked;
                window.Close();
            };

            /// Window add controls and show
            window.Controls.Add(checkBox);
            window.Controls.Add(labelName);
            window.Controls.Add(textInput);
            window.Controls.Add(btnOk);
            window.ShowDialog();
            #endregion

            ExclusiveAccess tiaMessage = _tiaportal.ExclusiveAccess("Kopiowanie zawartości DB...");

            // Include Structure name?

            using (StringReader reader = new StringReader(xmlData))
            {
                Document document = (Document)serializer.Deserialize(reader);
                

                foreach (SectionsSectionMember member in document.SWBlocksGlobalDB.AttributeList.Interface.Sections.Section.Member)
                {
                    prefix = string.Empty;
                    text += member.Name + "\t" + member.Datatype + "\t" + member.StartValue + "\n";

                    AddEntry(member, member.Name, ref textListGen);

                    if (member.Member != null)
                    {
                        prefix = member.Name + " - ";
                        MemberRecurrence(member.Member, ref text, ref prefix, ref textListGen);
                    }

                    if (member.Subelement != null)
                    {
                        prefix = member.Name;
                        MemberRecurrence(member.Subelement, ref text, ref prefix, ref textListGen);
                    }

                    if (member.Sections != null)
                    {
                        prefix = member.Name + " - ";
                        MemberRecurrence(member.Sections.Section.Member, ref text, ref prefix, ref textListGen);
                    }
                }
            }

            text = "";

            foreach (TextlistEntry entry in textListGen.Entries)
            {
                text += entry.Number + "\t" + entry.Texts[0] + "\n";
            }


            /* //debug window
            

            RichTextBox textBox = new RichTextBox();
            textBox.Text = text;
            textBox.Dock = DockStyle.Fill;

            Button btn = new Button();
            btn.Text = "OK";
            btn.Dock = DockStyle.Bottom;
            btn.Click += (sender, e) => { window.Close(); };

            window.Controls.Add(btn);
            window.Controls.Add(textBox);
            window.AcceptButton = btn;
            window.ShowDialog();*/

            #region Textlist generate
            /// 
            /// TEXTLIST
            /// 

            // Add comment
            foreach (CultureInfo culture in projectLanguages)
            {
                // todo comment translation?
                textListGen.Comment.Add("Auto generated");
            }

            // Sort textlist by text number
            textListGen.Entries = textListGen.Entries.OrderBy(e => e.Number).ToList();


            // Adding entries to text list


            /// 
            /// HEADER
            /// 
            string xmlContant = "";
            string tempContant = XmlHelper.TextlistHeader.Contant;
            int id = 0;
            tempContant = XmlHelper.InsertIds(tempContant, ref id);

            List<string> comments = new List<string>();
            foreach (CultureInfo cultureInfo in projectLanguages)
            {
                comments.Add("Auto generated");
            }

            tempContant = XmlHelper.InsertName(tempContant, textListGen.Name);
            tempContant += XmlHelper.AddTextlistComment(projectLanguages, comments, ref id);
            tempContant = XmlHelper.InsertIds(tempContant, ref id);
            xmlContant += tempContant;


            /// 
            /// ENTRIES
            /// 
            int entryCounter = 0;
            foreach (TextlistEntry entry in textListGen.Entries)
            {
                //tempContant = XmlHelper.tplTextlistEntry.Contant;
                tempContant = XmlHelper.AddTextlistEntry(entry, projectLanguages, ref id);
                tempContant = XmlHelper.InsertNumber(tempContant, entry.Number);
                tempContant = XmlHelper.InsertIds(tempContant, ref id);
                //tempContant = XmlHelper.InsertIds

                xmlContant += tempContant;

                tiaMessage.Text = $"Generowanie tekstów {entryCounter++} / {textListGen.Entries.Count}";
            }



            /// 
            /// FOOTER
            /// 
            FileInfo newXmlFilePath = new FileInfo(Path.GetTempFileName() + ".xml");
            xmlContant += XmlHelper.TextlistFooter.Contant;

            File.WriteAllText(newXmlFilePath.FullName, xmlContant);
            //Console.WriteLine(xmlContant);

            tiaMessage.Dispose();

            HmiTarget hmiSoftware = GetHmiTarget(_tiaportal);

            TextListComposition textListComposition = hmiSoftware.TextLists;
            IList<TextList> importedTextLists = textListComposition.Import(new FileInfo(newXmlFilePath.FullName), ImportOptions.Override);

            #endregion


            
        }


        private void MemberRecurrence(SectionsSectionMember[] members, ref string membersText, ref string prefix, ref TextListGen textListGen)
        {
            foreach (SectionsSectionMember member in members)
            {
                membersText += prefix + member.Name + "\t" + member.Datatype + "\t" + member.StartValue + "\n";
                string subprefix = prefix;

                AddEntry(member, prefix + member.Name, ref textListGen);

                if (member.Member != null)
                {

                    subprefix += member.Name + " - ";
                    MemberRecurrence(member.Member, ref membersText, ref subprefix, ref textListGen);
                }

                if (member.Subelement != null)
                {
                    subprefix += member.Name;
                    MemberRecurrence(member.Subelement, ref membersText, ref subprefix, ref textListGen);
                }

                if (member.Sections != null)
                {
                    subprefix += member.Name + " - ";
                    MemberRecurrence(member.Sections.Section.Member, ref membersText, ref subprefix, ref textListGen);
                }
            }
        }


        private void MemberRecurrence(SectionsSectionMemberSubelement[] members, ref string membersText, ref string prefix, ref TextListGen textListGen)
        {
            
            foreach (SectionsSectionMemberSubelement subElement in members)
            {
                membersText += prefix + "[" + subElement.Path + "]" + "\t" + subElement.StartValue + "\n";
            }
        }


        private void AddEntry(SectionsSectionMember member, string text, ref TextListGen textListGen)
        {
            if (onlyValidValues)
            {
                if (!CheckIfInt(member.Datatype)) return;
                if (textListGen.Entries.Any(e => e.Number == member.StartValue)) keyDuplicated = true;
                else
                {
                    TextlistEntry newEntry = new TextlistEntry(textListGen.CulturesNumber);
                    newEntry.Texts = newEntry.Texts.Select(t => text).ToList();
                    newEntry.Number = member.StartValue;
                    textListGen.Entries.Add(newEntry);
                }
            }
            else
            {
                if (textListGen.Entries.Any(e => e.Number == member.StartValue)) keyDuplicated = true;
                TextlistEntry newEntry = new TextlistEntry(textListGen.CulturesNumber);
                newEntry.Texts = newEntry.Texts.Select(t => text).ToList();
                newEntry.Number = member.StartValue;
                textListGen.Entries.Add(newEntry);
            }
        }


        private bool CheckIfInt(string datatype)
        {
            switch (datatype)
            {
                case "Int":
                    return true;

                case "UInt":
                    return true;

                case "SInt":
                    return true;

                case "USInt":
                    return true;

                case "LInt":
                    return true;

                case "ULInt":
                    return true;

                case "DInt":
                    return true;

                case "UDInt":
                    return true;

                case "Byte":
                    return true;

                case "Word":
                    return true;

                case "DWord":
                    return true;

                default:
                    return false;
            }
        }
    }
}