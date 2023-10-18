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



namespace AddinDbToTextlist
{
    public class AddIn : ContextMenuAddIn
    {
        /// <summary>
        ///The global TIA Portal Object 
        ///<para>It will be used in the TIA Add-In.</para>
        /// </summary>
        TiaPortal _tiaportal;

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

            PlcBlock block = menuSelectionProvider.GetSelection<PlcBlock>().FirstOrDefault();

            // File to export
            FileInfo xmlFilePath = new FileInfo(Path.GetTempFileName() + ".xml");
            block.Export(xmlFilePath, ExportOptions.WithDefaults);


            // Dictionary with statuses - only one description per status number
            string xmlData = File.ReadAllText(xmlFilePath.FullName);

            XmlSerializer serializer = new XmlSerializer(typeof(Document));

            // Include Structure name?

            using (StringReader reader = new StringReader(xmlData))
            {
                Document document = (Document)serializer.Deserialize(reader);
                string members = string.Empty;
                string prefix = string.Empty;

                foreach (SectionsSectionMember member in document.SWBlocksGlobalDB.AttributeList.Interface.Sections.Section.Member)
                {
                    if (member.Datatype == "Struct") prefix = member.Name + " - ";
                    else members += prefix + member.Name + "\t" + member.Datatype + "\t" + member.StartValue + "\n";

                    if (member.Member != null) MemberRecurrence(member.Member, ref members, ref prefix);
                }

                Form window = new Form();
                window.Width = 800;
                window.Height = 600;

                RichTextBox textBox = new RichTextBox();
                textBox.Text = members;
                textBox.Dock = DockStyle.Fill;
                
                Button btn = new Button();
                btn.Text = "OK";
                btn.Dock = DockStyle.Bottom;
                btn.Click += (sender, e) => { window.Close(); };

                window.Controls.Add(btn);
                window.Controls.Add(textBox);
                window.AcceptButton = btn;
                window.ShowDialog();
            }

            HmiTarget hmiSoftware = GetHmiTarget(_tiaportal);
        }


        private void MemberRecurrence(SectionsSectionMember[] members, ref string membersText, ref string prefix)
        {
            foreach (SectionsSectionMember member in members)
            {
                if (member.Datatype == "Struct") prefix += member.Name + " - ";
                else membersText += member.Name + "\t" + member.Datatype + "\t" + member.StartValue + "\n";

                if (member.Member != null) MemberRecurrence(member.Member, ref membersText, ref prefix);
            }
        }
    }
}