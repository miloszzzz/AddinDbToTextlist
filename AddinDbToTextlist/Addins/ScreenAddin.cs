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
    public class ScreenAddin
    {
        TiaPortal _tiaportal;
        bool onlyValidValues = false;
        bool keyDuplicated = false;
        public List<CultureInfo> projectLanguages;
        public ExclusiveAccess tiaMessage;
        CultureInfo selectedLanguage = new CultureInfo("en-US");

        public ScreenAddin(TiaPortal tiaportal, List<CultureInfo> projectLanguages)
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

            /// textlist object
            /// 
            TextListGen textListGen = new TextListGen("textlist", projectLanguages.Count);

            #region Window
            /// 
            /// Window with text
            ///
            Form window = new Form();
            window.Width = 290;
            window.Height = 130;
            window.Text = "Screens to Textlist";


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
                window.DialogResult = DialogResult.OK;
                window.Close();
            };

            /// Window add controls and show
            window.Controls.Add(labelName);
            window.Controls.Add(textInput);
            window.Controls.Add(btnOk);

            window.ShowDialog();

            if (window.DialogResult != DialogResult.OK) return;

            #endregion

            /// Exclusive access window
            tiaMessage = _tiaportal.ExclusiveAccess("Wczytywanie ekranów...");


            List<Siemens.Engineering.Hmi.Screen.Screen> screens = new List<Siemens.Engineering.Hmi.Screen.Screen>(1);
            GetScreenByRecurrence(hmiTarget.ScreenFolder, ref screens);


            string names = "";

            /// 
            /// Adding screens
            /// 
            foreach (Siemens.Engineering.Hmi.Screen.Screen screen in screens)
            {
                int screenNumber = FindNumbersInString(screen.Name)[0];
                string screenName = screen.Name;

                int floorCharIndex = screenName.IndexOf("_");

                if (floorCharIndex > 0)
                {
                    screenName = screenName.Remove(0, floorCharIndex + 1);
                }

                screenName = screenName.Replace("_", " - ");
                screenName = SeparateWords(screenName);

                TextlistEntry newEntry = new TextlistEntry(projectLanguages.Count);
                newEntry.Number = screenNumber;

                if (textListGen.Entries.Any(e => e.Number == screenNumber)) keyDuplicated = true;

                for (int i = 0; i < newEntry.Texts.Count; i++)
                {
                    newEntry.Texts[i] = screenName;
                }

                textListGen.Entries.Add(newEntry);


                names += screenName + "\n";
            }

            //MessageBox.Show(names);


            #region Textlist generate
            /// 
            /// TEXTLIST
            /// 
            /*
            // Add comment
            foreach (CultureInfo culture in projectLanguages)
            {
                // todo comment translation?
                textListGen.Comment.Add(block.Name);
            }*/

            // Sort textlist by text number
            textListGen.Entries = textListGen.Entries.OrderBy(e => e.Number).ToList();
            CheckCancellation();

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
                comments.Add("Screen titles");
            }

            tempContant = XmlHelper.InsertName(tempContant, textListGen.Name);
            tempContant += XmlHelper.AddTextlistComment(projectLanguages, comments, ref id);
            tempContant = XmlHelper.InsertIds(tempContant, ref id);
            xmlContant += tempContant;
            CheckCancellation();

            /// 
            /// ENTRIES
            /// 
            int entryCounter = 0;
            decimal allEntries = (decimal)textListGen.Entries.Count;
            decimal procent = 0m;
            decimal procentMemo = 0m;
            foreach (TextlistEntry entry in textListGen.Entries)
            {
                tempContant = XmlHelper.AddTextlistEntry(entry, projectLanguages, ref id, true);
                xmlContant += tempContant;

                if (entryCounter % 10 == 0)
                {
                    procent = (decimal)entryCounter / allEntries;
                    if (procent > procentMemo + 0.009m)
                    {
                        tiaMessage.Text = $"Generowanie tekstów {procent:P}";
                        procentMemo = procent;
                    }
                    CheckCancellation();
                }
                entryCounter++;
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
            newXmlFilePath.Delete();

            #endregion


            if (keyDuplicated) MessageBox.Show($"Textlista zawiera zduplikowane wartości!\n Liczba tekstów: {textListGen.Entries.Count}");
            else MessageBox.Show($"Liczba tekstów: {textListGen.Entries.Count}");

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
