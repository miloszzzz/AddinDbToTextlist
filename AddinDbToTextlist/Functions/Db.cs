using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.Hmi.TextGraphicList;
using Siemens.Engineering.Hmi;
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
using TiaHelperLibrary.Models.DB;

namespace AddinDbToTextlist.Functions
{
    public class Db
    {
        TiaPortal _tiaportal;
        bool onlyValidValues = false;
        bool keyDuplicated = false;
        public List<CultureInfo> projectLanguages;
        public ExclusiveAccess tiaMessage;
        CultureInfo selectedLanguage = new CultureInfo("en-US");

        public Db(TiaPortal tiaportal, List<CultureInfo> projectLanguages)
        {
            _tiaportal = tiaportal;
        }
        public void DbToTextlist(MenuSelectionProvider menuSelectionProvider)
        {
            /// PLC software object
            /// 
            PlcSoftware software = GetPlcSoftware(menuSelectionProvider);

            /// Active languages
            /// 
            projectLanguages = GetProjectCultures(_tiaportal.Projects.FirstOrDefault());

            /// Chosen DB block
            /// 
            PlcBlock block = menuSelectionProvider.GetSelection<PlcBlock>().FirstOrDefault();

            /// Exporting block
            /// 
            FileInfo xmlFilePath = new FileInfo(Path.GetTempFileName() + ".xml");

            if (!block.IsConsistent)
            {
                MessageBox.Show("Block not compiled!");
                return;
            }

            if (block is InstanceDB)
            {
                MessageBox.Show("Instances are not supported!");
                return;
            }

            block.Export(xmlFilePath, ExportOptions.WithDefaults);

            /// Reading xml file to string
            ///
            string xmlData = File.ReadAllText(xmlFilePath.FullName);
            xmlFilePath.Delete();

            /// Serialize xml to class
            /// 
            XmlSerializer serializer = new XmlSerializer(typeof(Document));

            /// text - all texts from db for debugging
            /// prefix - if nested structure occurs prefix from parent objects name is used
            string text = string.Empty;
            string prefix = string.Empty;

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
            window.Text = "DB to Textlist";


            /// Option checkbox
            CheckBox checkBox = new CheckBox();
            checkBox.Checked = true;
            checkBox.Text = "Rewrite only integers";
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
            labelName.Text = "Name:";
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
                window.DialogResult = DialogResult.OK;
                window.Close();
            };

            /// Window add controls and show
            window.Controls.Add(checkBox);
            window.Controls.Add(labelName);
            window.Controls.Add(textInput);
            window.Controls.Add(btnOk);

            window.ShowDialog();

            if (window.DialogResult != DialogResult.OK) return;

            #endregion

            /// Exclusive access window
            tiaMessage = _tiaportal.ExclusiveAccess("Copying DB content...");

            // Include Structure name?

            using (StringReader reader = new StringReader(xmlData))
            {
                /*try 
                { */
                Document document = (Document)serializer.Deserialize(reader);

                /*
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
}*/

                MemberRecurrence(
                    document.SWBlocksGlobalDB.AttributeList.Interface.Sections.Section.Member,
                    ref text,
                    ref prefix,
                    ref textListGen
                    );

                /* }
                 catch (Exception ex) {

                     MessageBox.Show("Nieobsługiwany blok"
                         + "\n" + "Source: " + ex.Source
                         + "\n" + "Message: " + ex.Message
                         + "\n" + "Stack: " + ex.StackTrace
                         + "\n" + "Exception: " + ex.InnerException); 

                     _tiaportal.Dispose();
                     return;
                 }*/
            }


            text = "";
            int sourceLangId = projectLanguages.IndexOf(selectedLanguage);

            /*
            foreach (TextlistEntry entry in textListGen.Entries)
            {
                text += entry.Number + "\t" + entry.Texts[0] + "\n";

                if (translate)
                {
                    foreach (CultureInfo language in projectLanguages)
                    {
                        if (language.Name == selectedLanguage.Name) continue;
                        else
                        {
                            int currentLangId = projectLanguages.IndexOf(language);

                            
                            entry.Texts[currentLangId] =
                                transl.Translate(
                                    entry.Texts[sourceLangId],
                                    selectedLanguage.TwoLetterISOLanguageName,
                                    language.TwoLetterISOLanguageName).GetAwaiter().GetResult();

                        }
                    }
                }
            }
        */

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
                comments.Add(block.Name);
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
                        tiaMessage.Text = $"Generating texts {procent:P}";
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

            if (keyDuplicated) MessageBox.Show($"Textlist contains duplicated values!\n Lines: {textListGen.Entries.Count}");
            else MessageBox.Show($"Lines: {textListGen.Entries.Count}");
        }


        private void MemberRecurrence(SectionsSectionMember[] members, ref string membersText, ref string prefix, ref TextListGen textListGen)
        {
            string entry_text = string.Empty;
            if (members.Length > 0)
            {
                tiaMessage.Text = $"Copying DB content... {prefix}";
                CheckCancellation();
            }


            foreach (SectionsSectionMember member in members)
            {
                TextlistEntry entry = new TextlistEntry(textListGen.CulturesNumber);
                membersText += prefix + member.Name + "\t" + member.Datatype + "\t" + member.StartValue + "\n";

                if (member.Comment != null)
                {
                    /*
                    entry_text = member.Comment.FirstOrDefault(c => c.Value.Length > 0).Value;

                    for (int i = 0; i < member.Comment.Length; i++)
                    {
                        if (member.Comment[i].Value.Length > 0) entry.Texts[i] = member.Comment[i].Value;
                        else entry.Texts[i] = membersText;
                    }

                    for (int i = 0; i < entry.Texts.Count; i++)
                    {
                        if (entry.Texts[i].Length < 1) entry.Texts[i] = entry_text;
                    }*/
                    for (int i = 0; i < entry.Texts.Count; i++)
                    {
                        string text;
                        try
                        {
                            text = member.Comment.FirstOrDefault(c => c.Lang == projectLanguages[i].Name).Value;
                        }
                        catch
                        {
                            text = SeparateWords(prefix + member.Name);

                        }

                        entry.Texts[i] = text;
                    }


                    int.TryParse(member.StartValue, out int entryNumber);
                    entry.Number = entryNumber;

                    textListGen.Entries.Add(entry);
                }
                else
                {
                    entry_text = SeparateWords(prefix + member.Name);
                    AddEntry(member, entry_text, ref textListGen);
                }

                string subprefix = prefix;



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
            string entry_text = string.Empty;
            //CheckCancellation();

            foreach (SectionsSectionMemberSubelement subElement in members)
            {
                TextlistEntry entry = new TextlistEntry(textListGen.CulturesNumber);
                membersText += prefix + "[" + subElement.Path + "]" + "\t" + subElement.StartValue + "\n";

                if (subElement.Comment != null)
                {
                    /*
                    entry_text = subElement.Comment.FirstOrDefault(c => c.Value.Length > 0).Value;

                    for (int i = 0; i < subElement.Comment.Length; i++)
                    {
                        if (subElement.Comment[i].Value.Length > 0) entry.Texts[i] = subElement.Comment[i].Value;
                        else entry.Texts[i] = entry_text;
                    }

                    for (int i = 0; i < entry.Texts.Count; i++)
                    {
                        if (entry.Texts[i].Length < 1) entry.Texts[i] = entry_text;
                    }*/
                    for (int i = 0; i < entry.Texts.Count; i++)
                    {
                        string text;
                        try
                        {
                            text = subElement.Comment.FirstOrDefault(c => c.Lang == projectLanguages[i].Name).Value;
                        }
                        catch
                        {
                            text = SeparateWords(prefix + "[" + subElement.Path + "]");
                        }

                        entry.Texts[i] = text;
                    }

                    int.TryParse(subElement.StartValue, out int entryNumber);
                    entry.Number = entryNumber;

                    textListGen.Entries.Add(entry);

                }
                else
                {
                    entry_text = SeparateWords(prefix + "[" + subElement.Path + "]");
                    AddEntry(subElement, entry_text, ref textListGen);
                }

                /*
                if (subElement.Comment != null) entry_text = subElement.Comment.FirstOrDefault(c => c.Value.Length > 0).Value;
                else entry_text = SeparateWords(prefix + "[" + subElement.Path + "]");
                */
                AddEntry(subElement, entry_text, ref textListGen);


            }
        }


        private void AddEntry(SectionsSectionMember member, string text, ref TextListGen textListGen)
        {
            int.TryParse(member.StartValue, out int entryNumber);

            if (onlyValidValues)
            {
                if (!CheckIfInt(member.Datatype)) return;
                if (textListGen.Entries.Any(e => e.Number == entryNumber)) keyDuplicated = true;
                TextlistEntry newEntry = new TextlistEntry(textListGen.CulturesNumber);
                newEntry.Texts = newEntry.Texts.Select(t => text).ToList();
                newEntry.Number = entryNumber;
                textListGen.Entries.Add(newEntry);
            }
            else
            {
                if (textListGen.Entries.Any(e => e.Number == entryNumber)) keyDuplicated = true;
                TextlistEntry newEntry = new TextlistEntry(textListGen.CulturesNumber);
                newEntry.Texts = newEntry.Texts.Select(t => text).ToList();
                newEntry.Number = entryNumber;
                textListGen.Entries.Add(newEntry);
            }
        }


        private void AddEntry(SectionsSectionMemberSubelement member, string text, ref TextListGen textListGen)
        {
            int.TryParse(member.StartValue, out int entryNumber);

            if (onlyValidValues)
            {
                if (textListGen.Entries.Any(e => e.Number == entryNumber)) keyDuplicated = true;
                TextlistEntry newEntry = new TextlistEntry(textListGen.CulturesNumber);
                newEntry.Texts = newEntry.Texts.Select(t => text).ToList();
                newEntry.Number = entryNumber;
                textListGen.Entries.Add(newEntry);
            }
            else
            {
                if (textListGen.Entries.Any(e => e.Number == entryNumber)) keyDuplicated = true;
                TextlistEntry newEntry = new TextlistEntry(textListGen.CulturesNumber);
                newEntry.Texts = newEntry.Texts.Select(t => text).ToList();
                newEntry.Number = entryNumber;
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
