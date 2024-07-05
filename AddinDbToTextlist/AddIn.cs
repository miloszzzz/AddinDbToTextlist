//using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.HW;
using Siemens.Engineering.SW;
using Siemens.Engineering;
using Siemens.Engineering.Hmi;
using Siemens.Engineering.SW.Blocks;
using System.Collections.Generic;
using System.Linq;
using System;
using static TiaHelperLibrary.TiaHelper;
using AddinDbToTextlist.Helpers;
using System.Globalization;
using AddinDbToTextlist.Functions;


//$(ProjectDir)\AfterCompilation.bat $(ProjectDir) C:\Net\TiaAddInTester\bin

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
        //Transl transl = new Transl("1afb7c20-b3c3-c8f0-de26-ca48e5b045ac:fx");
        //bool translate = false;
        CultureInfo selectedLanguage = new CultureInfo("en-US");
        public List<CultureInfo> projectLanguages = new List<CultureInfo>(10);
        public ExclusiveAccess tiaMessage;

        /// <summary>
        /// The display name of the Add-In.
        /// </summary>
        private const string s_DisplayNameOfAddIn = "Textlists";

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
            addInRootSubmenu.Items.AddActionItem<Project>(
                "Db to textlist - DB", DbDoSomething, DbCanSomething);

            addInRootSubmenu.Items.AddActionItem<DeviceItem>(
                "Db to textlist - DB", DbDoSomething, DbCanSomething);

            addInRootSubmenu.Items.AddActionItem<PlcBlock>(
                "Db to textlist", DbDoSomething, DbCanSomething);

            addInRootSubmenu.Items.AddActionItem<PlcBlockGroup>(
                "Db to textlist - DB", DbDoSomething, DbCanSomething);
            
            addInRootSubmenu.Items.AddActionItem<Project>(
                "Screens to textlist - Screen", ScreenDoSomething, ScreenCanSomething);

            addInRootSubmenu.Items.AddActionItem<DeviceItem>(
                "Screens to textlist - Screen", ScreenDoSomething, ScreenCanSomething);

            addInRootSubmenu.Items.AddActionItem<PlcBlock>(
                "Screens to textlist - Screen", ScreenDoSomething, ScreenCanSomething);

            addInRootSubmenu.Items.AddActionItem<PlcBlockGroup>(
                "Screens to textlist - Screen", ScreenDoSomething, ScreenCanSomething);
            
            addInRootSubmenu.Items.AddActionItem<Siemens.Engineering.Hmi.Screen.Screen>(
                "Screens to textlist", ScreenDoSomething, ScreenCanSomething);
        }

        #region DoSomething

        /// <summary>
        /// The method contains the program code of the TIA Add-In.
        /// Called when the button 'Start Add-In Code' will be pressed.
        /// </summary>
        /// <typeparam name="menuSelectionProvider">
        /// here, the same type as in addInRootSubmenu.Items.AddActionItem
        /// must be used -> here it is <DeviceItem>
        /// </typeparam>
        private void DbDoSomething(MenuSelectionProvider<Project>
            menuSelectionProvider)
        {
            /*try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex) 
            { 
                LogToFile.Save(ex); 
            }*/
        }

        private void DbDoSomething(MenuSelectionProvider<DeviceItem>
            menuSelectionProvider)
        {
            /*try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                LogToFile.Save(ex);
            }*/
        }

        private void DbDoSomething(MenuSelectionProvider<PlcBlock>
            menuSelectionProvider)
        {
            try
            {
                Db dbAddin = new Db(_tiaportal, projectLanguages);
                dbAddin.DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Addin canceled") _tiaportal.Dispose();
                else
                {
                    LogToFile.Save(ex);
                    _tiaportal.Dispose();
                }
            }
        }

        private void DbDoSomething(MenuSelectionProvider<PlcBlockGroup>
            menuSelectionProvider)
        {
            /*try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                LogToFile.Save(ex);
            }*/
        }

        private void ScreenDoSomething(MenuSelectionProvider<Project>
            menuSelectionProvider)
        {
            /*try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex) 
            { 
                LogToFile.Save(ex); 
            }*/
        }

        private void ScreenDoSomething(MenuSelectionProvider<DeviceItem>
            menuSelectionProvider)
        {
            /*try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                LogToFile.Save(ex);
            }*/
        }

        private void ScreenDoSomething(MenuSelectionProvider<PlcBlock>
            menuSelectionProvider)
        {
            
        }

        private void ScreenDoSomething(MenuSelectionProvider<PlcBlockGroup>
            menuSelectionProvider)
        {
            /*try
            {
                DbToTextlist(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                LogToFile.Save(ex);
            }*/
        }

        private void ScreenDoSomething(MenuSelectionProvider<Siemens.Engineering.Hmi.Screen.Screen>
            menuSelectionProvider)
        {
            try
            {
                ScreenAddin screenAddin = new ScreenAddin(_tiaportal, projectLanguages);
                screenAddin.ScreensToTextlist(menuSelectionProvider);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Addin canceled") _tiaportal.Dispose();
                else
                {
                    LogToFile.Save(ex);
                    _tiaportal.Dispose();
                }
            }
        }

        #endregion


        #region CanSomething
        /// <summary>
        /// Called when there is a mousover the button at a DeviceItem.
        /// It will be used to enable the button.
        /// </summary>
        /// <typeparam name="menuSelectionProvider">
        /// here, the same type as in addInRootSubmenu.Items.AddActionItem
        /// must be used -> here it is <DeviceItem>
        /// </typeparam>
        private MenuStatus DbCanSomething(MenuSelectionProvider<Project> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Disabled;
        }

        private MenuStatus DbCanSomething(MenuSelectionProvider<PlcBlock> menuSelectionProvider)
        {
            //enable the button
            PlcBlock selection = menuSelectionProvider.GetSelection<PlcBlock>().FirstOrDefault();


            if (selection.ProgrammingLanguage is ProgrammingLanguage.DB)
            {
                return MenuStatus.Enabled;
            }

            return MenuStatus.Disabled;
        }

        private MenuStatus DbCanSomething(MenuSelectionProvider<DeviceItem> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Disabled;
        }

        private MenuStatus DbCanSomething(MenuSelectionProvider<PlcBlockGroup> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Disabled;
        }


        private MenuStatus ScreenCanSomething(MenuSelectionProvider<Project> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Disabled;
        }

        private MenuStatus ScreenCanSomething(MenuSelectionProvider<PlcBlock> menuSelectionProvider)
        {
            //enable the button
            /*PlcBlock selection = menuSelectionProvider.GetSelection<PlcBlock>().FirstOrDefault();


            if (selection.ProgrammingLanguage is ProgrammingLanguage.DB)
            {
                return MenuStatus.Enabled;
            }*/

            return MenuStatus.Disabled;
        }

        private MenuStatus ScreenCanSomething(MenuSelectionProvider<DeviceItem> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Disabled;
        }

        private MenuStatus ScreenCanSomething(MenuSelectionProvider<PlcBlockGroup> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Disabled;
        }

        private MenuStatus ScreenCanSomething(MenuSelectionProvider<Siemens.Engineering.Hmi.Screen.Screen> menuSelectionProvider)
        {
            //enable the button
            return MenuStatus.Enabled;
        }

        #endregion

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
                case "Db to textlist":
                    /*var myPlc = project
                         .Devices[1]
                        .DeviceItems.FirstOrDefault(plc => plc.Name.Length > 0)
                        .GetService<SoftwareContainer>().Software as PlcSoftware;
                    // return the program blocks folder*/
                    PlcBlockGroup blockGroup = GetGroupByBlockName(software.BlockGroup, "DB_StatusDef");
                    return blockGroup.Blocks.Where<PlcBlock>(b => b.ProgrammingLanguage == ProgrammingLanguage.DB);

                case "Screens to textlist":
                    HmiTarget target = GetHmiTarget(_tiaportal);
                    return target.ScreenFolder.Screens;

                default:
                    return Enumerable.Empty<IEngineeringObject>();
            }
        }



        public bool CheckCancellation()
        {
            if (tiaMessage.IsCancellationRequested)
            {
                throw new Exception("Addin canceled");
            }
            return false;
        }


    }
}