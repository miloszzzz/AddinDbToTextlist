using AddinDbToTextlist.Models;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.Hmi.TextGraphicList;
using Siemens.Engineering.Hmi;
using Siemens.Engineering.Hmi.Screen;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.SW;
using Siemens.Engineering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TiaXmlGenerator.Models;
using TiaXmlGenerator;
using static TiaHelperLibrary.TiaHelper;

namespace AddinDbToTextlist.Functions
{
    public class Screens
    {
        TiaPortal _tiaportal;
        bool onlyValidValues = false;
        bool keyDuplicated = false;
        public List<CultureInfo> projectLanguages;
        public ExclusiveAccess tiaMessage;
        CultureInfo selectedLanguage = new CultureInfo("en-US");

        public Screens(TiaPortal tiaportal, List<CultureInfo> projectLanguages)
        {
            _tiaportal = tiaportal;
        }

        public void ScreensToTextlist(MenuSelectionProvider menuSelectionProvider)
        {
            HmiTarget hmiTarget = null;
            /// HMI software object
            /// 
            try
            {
                hmiTarget = GetHmiTarget(menuSelectionProvider);
            } catch (Exception ex) 
            {
                return;
            }
            /// Active languages
            /// 
            projectLanguages = GetProjectCultures(_tiaportal.Projects.FirstOrDefault());

            string names = "";

            List<Siemens.Engineering.Hmi.Screen.Screen> screens = new List<Siemens.Engineering.Hmi.Screen.Screen>(1);
            GetScreenByRecurrence(hmiTarget.ScreenFolder, ref screens);

            List<string> attr = new List<String>
                 { "ID" };

            foreach (Siemens.Engineering.Hmi.Screen.Screen screen in screens)
            {
                names += screen.GetAttributes(attr).ToString();
            }

            MessageBox.Show(names);

            /*
            #region Window
            /// 
            /// Window with text
            ///
            Form window = new Form();
            window.Width = 290;
            window.Height = 130;
            window.Text = "DB to Textlist";


            /// Option checkbox
            CheckBox checkBox = new CheckBox();
            checkBox.Checked = true;
            checkBox.Text = "";
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
            labelName.Text = screens;
            labelName.Top = 30;
            labelName.Left = 10;
            labelName.Width = 50;
         

            /// Window add controls and show
            window.Controls.Add(checkBox);
            window.Controls.Add(labelName);
            window.Controls.Add(textInput);

            window.ShowDialog();

            if (window.DialogResult != DialogResult.OK) return;

            #endregion
            */
            /// Exclusive access window
            tiaMessage = _tiaportal.ExclusiveAccess("Kopiowanie zawartości DB...");

            // Include Structure name?

            
            int sourceLangId = projectLanguages.IndexOf(selectedLanguage);


        }
       

        public bool CheckCancellation()
        {
            if (tiaMessage.IsCancellationRequested)
            {
                throw new Exception("Addin canceled");
            }
            return false;
        }



        private void GetScreenByRecurrence(Siemens.Engineering.Hmi.Screen.ScreenFolder folder, ref List<Siemens.Engineering.Hmi.Screen.Screen> screens)
        {
            foreach (Siemens.Engineering.Hmi.Screen.Screen screen in folder.Screens)
            {
                screens.Add(screen);
            }

            foreach (Siemens.Engineering.Hmi.Screen.ScreenFolder nextFolder in folder.Folders)
            {
                GetScreenByRecurrence(nextFolder, ref screens);
            }
        }

    }
}
