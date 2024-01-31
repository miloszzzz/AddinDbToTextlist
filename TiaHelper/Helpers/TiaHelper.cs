using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Siemens.Engineering;
using Siemens.Engineering.AddIn.Menu;
using Siemens.Engineering.HW.Features;
using Siemens.Engineering.HW;
using Siemens.Engineering.SW;
using Siemens.Engineering.SW.Tags;
using Siemens.Engineering.SW.Blocks;
using Siemens.Engineering.Hmi.Globalization;
using Siemens.Engineering.Hmi.RuntimeScripting;
using Siemens.Engineering.Hmi.Screen;
using Siemens.Engineering.Hmi.Tag;
using Siemens.Engineering.Hmi;
using Siemens.Engineering.Library.MasterCopies;
using Siemens.Engineering.Library.Types;
using Siemens.Engineering.Library;
using Siemens.Engineering.SW.ExternalSources;
using Siemens.Engineering.SW.Types;
using System.Collections.ObjectModel;
using TiaHelperLibrary.Models;
using System.ComponentModel;
using TiaHelperLibrary.Enums;
using System.Text.RegularExpressions;
using System.Globalization;
using TiaXmlGenerator.Models;
using System.IO;
using System.Runtime.InteropServices;

namespace TiaHelperLibrary
{
    public static class TiaHelper
    {
        //public TiaPortalProcess process { get; set; } - it's used in openness not addins


        /// <summary>
        /// Connecting to the process
        /// </summary>
        /// 

        /* - openness function
        public TiaHelper(bool getProcess) 
        { 
            //if (getProcess) SelectProcess();
            var asdf = Siemens.Engineering.ConfirmationChoices.Abort;
        }*/

        /*
        /// <summary>
        /// Connecting to the process selected by it's index
        /// </summary>
        /// <param name="id">Index of process</param>
        public TiaHelper(int id)
        {
            IList<TiaPortalProcess> processes;
            processes = TiaPortal.GetProcesses();
            try { process = processes[id]; }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }*/

        /*
        #region Connecting to TIA
        /// <summary>
        /// Default function for selecting tia process.
        /// If theres only 1 process then no user input is needed
        /// </summary>
        private void SelectProcess()
        {
            int selectedProcess;
            string userInput;
            
            IList<TiaPortalProcess> processes = TiaPortal.GetProcesses();
            int i = 0;

            foreach (TiaPortalProcess proc in processes)
            {
                Console.WriteLine($"{i}\t{proc.ProjectPath}");
                i++;
            }

            if (i > 1)
            {
                Console.WriteLine("Select process:");
                
                userInput = Console.ReadLine();

                try
                {
                    selectedProcess = int.Parse(userInput);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
            else if (i > 0)
            {
                selectedProcess = 0;
            }
            else
            {
                throw new Exception("Tia process not found");
            }
            process = processes[selectedProcess];
            return;
        }*/

        #region Get PLC software

        /// <summary>
        /// Function returns first PLC software it finds in project
        /// ToDo; maybe it is possible to not iterate through everything?
        /// </summary>
        /// <param name="tiaPortal"></param>
        /// <param name="writeInConsole">Flag for writing scan information</param>
        /// <returns></returns>
        public static PlcSoftware GetPlcSoftware(TiaPortal tiaPortal)
        {
            ProjectComposition projectComposition = tiaPortal.Projects;

            return GetPlcSoftware(projectComposition[0]);
        }


        public static PlcSoftware GetPlcSoftware(Project project)
        {
            // find first PLC device
            Device device = project.Devices.FirstOrDefault(d => CheckIfPlc(d));

            // first PLCs device item is rack
            DeviceItem deviceItem = device.Items[0];


            // Only deviceitem with software is PLC
            DeviceItem item = deviceItem.Items.FirstOrDefault(i => (i.GetService<SoftwareContainer>() != null));

            // Get service & soft
            return GetPlcSoftware(item);
        }


        public static PlcSoftware GetPlcSoftware(DeviceItem plc)
        {
            return (PlcSoftware)plc.GetService<SoftwareContainer>().Software;
        }


        /// <summary>
        /// Function checking parent type, for use in addins
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static PlcSoftware GetPlcSoftware(PlcBlockGroup block) 
        {
            var parent = block.Parent;
            int i = 0;
            while (!(parent is PlcSoftware))
            {
                if (i > 10) throw new Exception("Can't find PLC software");
                parent = parent.Parent;
                i++;
            }

            return (PlcSoftware) parent;
        }


        public static PlcSoftware GetPlcSoftware(PlcBlock block)
        {
            return GetPlcSoftware((PlcBlockGroup)block.Parent);
        }


        public static PlcSoftware GetPlcSoftware(MenuSelectionProvider provider)
        {
            var selection = provider.GetSelection().FirstOrDefault();

            if (selection is Project) return GetPlcSoftware((Project)selection);
            else if (selection is PlcBlockGroup) return GetPlcSoftware((PlcBlockGroup)selection);
            else if (selection is PlcBlock) return GetPlcSoftware((PlcBlock)selection);
            else if (selection is DeviceItem) return GetPlcSoftware((DeviceItem)selection);
            return null;
        }

        /// <summary>
        /// Checking the name of Device
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static bool CheckIfPlc(Device device)
        {
            if (device.TypeIdentifier == null) return false;
            if (device.TypeIdentifier.Contains("1500") ||
                device.TypeIdentifier.Contains("1200") ||
                device.TypeIdentifier.Contains("ET200"))
            {
                return true;
            }
            return false;
        }

        #endregion

        public static HmiTarget GetHmiTarget(TiaPortal tiaPortal)
        {
            foreach (Device dev in tiaPortal.Projects[0].Devices)
            {
                //Console.WriteLine(dev.DeviceItems.Count);
                DeviceItem deviceItem = GetHmiDeviceItem(dev);
                if (deviceItem != null)
                {
                    SoftwareContainer softwareContainer = deviceItem.GetService<SoftwareContainer>();
                    if (softwareContainer == null) continue;

                    HmiTarget hmi = softwareContainer.Software as HmiTarget;
                    if (hmi != null) return hmi;
                }
            }

            return null;
        }


        public static DeviceItem GetHmiDeviceItem(Device device)
        { 
            var devi = device.DeviceItems.FirstOrDefault(d => !(d.GetService<SoftwareContainer>() is null));
            return devi;
        }


        //#endregion


        /// <summary>
        /// Function writes all tags in console.
        /// </summary>
        /// <param name="plcSoftware"></param>
        public static void ShowAllTags(PlcSoftware plcSoftware)
        {
            PlcTagTableSystemGroup plcTagTableSystemGroup = plcSoftware.TagTableGroup;
            foreach (PlcTagTable plcTagTable in plcTagTableSystemGroup.TagTables)
            {
                Console.WriteLine($"{plcTagTable.Name}");
                foreach (PlcTag plcTag in plcTagTable.Tags)
                {
                    Console.WriteLine($"\tTag:\t{plcTag.Name}\t\t\t{plcTag.DataTypeName}\t{plcTag.LogicalAddress}");
                    
                }

                foreach (PlcUserConstant plcUserConstant in plcTagTable.UserConstants)
                {
                    Console.WriteLine($"\tConst:\t{plcUserConstant.Name}\t\t\t{plcUserConstant.DataTypeName}\t{plcUserConstant.Value}");
                }
            }

            foreach (PlcTagTableUserGroup tagTableUserGroup in plcSoftware.TagTableGroup.Groups)
            {
                Console.WriteLine($"{tagTableUserGroup.Name}");

                foreach (PlcTagTable plcTagTable in tagTableUserGroup.TagTables)
                {
                    Console.WriteLine($"\t{plcTagTable.Name}");
                    foreach (PlcTag plcTag in plcTagTable.Tags)
                    {
                        Console.WriteLine($"\t\tTag:\t{plcTag.Name}\t\t\t{plcTag.DataTypeName}\t{plcTag.LogicalAddress}");
                    }

                    foreach (PlcUserConstant plcUserConstant in plcTagTable.UserConstants)
                    {
                        Console.WriteLine($"\t\tConst:\t{plcUserConstant.Name}\t\t\t{plcUserConstant.DataTypeName}\t{plcUserConstant.Value}");
                    }
                }
            }
        }


        public static void GetTagsConstantsLists(PlcSoftware plcSoftware, ref List<PlcTag> tags, ref List<PlcConstant> constants)
        {

            PlcTagTableSystemGroup plcTagTableSystemGroup = plcSoftware.TagTableGroup;

            foreach (PlcTagTable plcTagTable in plcTagTableSystemGroup.TagTables)
            {
                foreach (PlcTag plcTag in plcTagTable.Tags)
                {
                    tags.Add(plcTag);
                }

                foreach (PlcConstant plcConstant in plcTagTable.UserConstants)
                {
                    constants.Add(plcConstant);
                }
            }

            foreach (PlcTagTableGroup plcTagTableGroup in plcTagTableSystemGroup.Groups)
            {
                GetListRecurence(plcTagTableGroup, ref tags, ref constants);
            }
        }

        /// <summary>
        /// Recurrence used by GetTagsList method
        /// </summary>
        /// <param name="tagTableGroup"></param>
        /// <param name="tags"></param>
        /// <param name="constants"></param>
        private static void GetListRecurence(PlcTagTableGroup tagTableGroup, ref List<PlcTag> tags, ref List<PlcConstant> constants)
        {
            foreach (PlcTagTable plcTagTable in tagTableGroup.TagTables)
            {
                foreach (PlcTag plcTag in plcTagTable.Tags)
                {
                    tags.Add(plcTag);
                }

                foreach (PlcConstant plcConstant in plcTagTable.UserConstants)
                {
                    constants.Add(plcConstant);
                }
            }

            foreach (PlcTagTableGroup plcTagTableGroup in tagTableGroup.Groups)
            {
                GetListRecurence(plcTagTableGroup, ref tags, ref constants);
            }
        }





        /// <summary>
        /// Creates group of tag tables
        /// ToDo options to create subgroups
        /// </summary>
        /// <param name="plcSoftware">plcSoftware process</param>
        /// <param name="groupName">Name of group to create</param>
        public static void CreatePlcTagTableUserGroup(PlcSoftware plcSoftware, string groupName)
        {
            PlcTagTableSystemGroup systemGroup = plcSoftware.TagTableGroup;
            PlcTagTableUserGroupComposition groupComposition = systemGroup.Groups;
            PlcTagTableUserGroup myCreatedGroup = groupComposition.Create(groupName);
        }


        public static List<int> FindNumbersInString(string input)
        {
            List<int> numbers = new List<int>();

            // Wyrażenie regularne dopasowujące liczby całkowite w tekście
            string pattern = @"-?\d+"; // Dopasowanie liczb całkowitych, w tym liczby ujemne

            // Utworzenie obiektu Regex i dopasowanie wzorca do tekstu
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(input);

            // Przetwarzanie dopasowanych wyników i dodawanie ich do listy liczb
            foreach (Match match in matches)
            {
                if (int.TryParse(match.Value, out int number))
                {
                    numbers.Add(number);
                }
            }

            return numbers;
        }


        /// <summary>
        /// Function checks if tag name belong to actuator
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="tag"></param>
        /// <param name="actuators"></param>
        /// <returns>True if tag was assigned</returns>
        private static bool TryAssignTag(Regex regex, PlcTag tag, Dictionary<int, Actuator> actuators, EnumActTag tagType)
        {
            if (regex.IsMatch(tag.Name))
            {
                int pos = tag.Name.IndexOf('Y');

                if (pos == -1) return false;

                List<int> nums = FindNumbersInString(tag.Name.Substring(pos));

                if (nums.Count <= 0) return false;
                if (actuators.ContainsKey(nums[0]))
                {
                    switch (tagType)
                    {
                        case EnumActTag.InputRet:
                            actuators[nums[0]].InputRetract = tag.Name;
                            return true;

                        case EnumActTag.InputExt:
                            actuators[nums[0]].InputExtend = tag.Name;
                            return true;

                        case EnumActTag.OutputRet:
                            actuators[nums[0]].OutputRetract = tag.Name;
                            return true;

                        case EnumActTag.OutputExt:
                            actuators[nums[0]].OutputExtend = tag.Name;
                            return true;

                        default:
                            return false;
                    }
                }
            }
            return false;
        }

        public static void AssingTagsToActuators(Dictionary<int, Actuator> actuators, List<PlcTag> tags)
        {
            string retract = "Ret";
            string extend = "Ext";
            string input = "I_ActY";
            string output = "Q_";

            string cof = "Cof";
            string wys = "Wys";
            string wej = @"I_Sil[\s\S]*Y";
            string wyj = "Q_";

            // StringBuilder would speed up a little bit

            RegexOptions regexOptions = RegexOptions.IgnoreCase;

            Regex inputRetRegex = new Regex(input + @"\d*" + retract + @"[\s\S]*", regexOptions);
            Regex inputRetRegexPl = new Regex(wej + @"\d*" + @"[\s\S]*" + cof, regexOptions);
            Regex inputRetRegexPl2 = new Regex(wej + @"\d*" + @"[\s\S]*" + "Otw" + @"[\s\S]*", regexOptions);

            Regex inputExtRegex = new Regex(input + @"\d*" + extend + @"[\s\S]*", regexOptions);
            Regex inputExtRegexPl = new Regex(wej + @"\d*" + @"[\s\S]*" + wys, regexOptions);
            Regex inputExtRegexPl2 = new Regex(wej + @"\d*" + @"[\s\S]*" + "Zam" + @"[\s\S]*", regexOptions);

            Regex outputRetRegex = new Regex(output + @"[\s\S]*Y\d{1,3}_" + retract + @"[\s\S]*", regexOptions);
            Regex outputRetRegexPl = new Regex(wyj + @"[\s\S]*Y\d{1,3}_" + cof + @"[\s\S]*", regexOptions);

            Regex outputExtRegex = new Regex(output + @"[\s\S]*Y\d{1,3}_" + extend + @"[\s\S]*", regexOptions);
            Regex outputExtRegexPl = new Regex(wyj + @"[\s\S]*Y\d{1,3}_" + wys + @"[\s\S]*", regexOptions);

            // Input retract
            foreach (PlcTag tag in tags)
            {
                // Inputs // maybe remove used tag from tag list?
                //
                if (TryAssignTag(inputRetRegex, tag, actuators, EnumActTag.InputRet)) continue;
                if (TryAssignTag(inputRetRegexPl, tag, actuators, EnumActTag.InputRet)) continue;
                if (TryAssignTag(inputRetRegexPl2, tag, actuators, EnumActTag.InputRet)) continue;

                if (TryAssignTag(inputExtRegex, tag, actuators, EnumActTag.InputExt)) continue;
                if (TryAssignTag(inputExtRegexPl, tag, actuators, EnumActTag.InputExt)) continue;
                if (TryAssignTag(inputExtRegexPl2, tag, actuators, EnumActTag.InputExt)) continue;

                // Outputs
                //
                if (TryAssignTag(outputRetRegex, tag, actuators, EnumActTag.OutputRet)) continue;
                if (TryAssignTag(outputRetRegexPl, tag, actuators, EnumActTag.OutputRet)) continue;

                if (TryAssignTag(outputExtRegex, tag, actuators, EnumActTag.OutputExt)) continue;
                if (TryAssignTag(outputExtRegexPl, tag, actuators, EnumActTag.OutputExt)) continue;
            }
        }


        #region Getting program structure
        /// <summary>
        /// Prints blocks in main group and then call recursive function
        /// </summary>
        /// <param name="plcSoftware">Connected software</param>
        public static void showProgramStructure(PlcSoftware plcSoftware)
        {
            SubLevel nestingLevel = new SubLevel();

            foreach (PlcBlock block in plcSoftware.BlockGroup.Blocks)
                Console.WriteLine($"-{(block as PlcBlock).Name}");

            programStructureRecursive(plcSoftware.BlockGroup.Groups, nestingLevel);
        }


        /// <summary>
        /// Checks group for blacks and checking subgroups by recurse
        /// </summary>
        /// <param name="groupComposition">Subgroup</param>
        /// <param name="nestLevel">saves sublevel</param>
        public static void programStructureRecursive(PlcBlockUserGroupComposition groupComposition, SubLevel nestLevel)
        {
            foreach (PlcBlockGroup blockGroup in groupComposition)
            {
                Console.WriteLine($"{nestLevel.GetTabs()}{blockGroup.Name}");
                nestLevel.NextLeveL();
                foreach (PlcBlock plcBlock in blockGroup.Blocks)
                {
                    Console.WriteLine($"{nestLevel.GetTabs()}-{plcBlock.Name}");
                }
                programStructureRecursive(blockGroup.Groups, nestLevel);
                nestLevel.PrevLeveL();
            }
        }
        #endregion


        #region Getting all elements
        /// <summary>
        /// Prints groups and blocks
        /// </summary>
        /// <param name="plcSoftware"></param>
        public static void ShowAllElements(PlcSoftware plcSoftware)
        {
            Collection<object> list = new Collection<object>();

            RecursiveGetAllElements(plcSoftware, ref list);

            foreach (object item in list)
            {
                if (item is PlcBlock) Console.WriteLine((item as PlcBlock).Name);
                else Console.WriteLine((item as PlcBlockGroup).Name);
            }
        }


        /// <summary>
        /// Recursively gathers all subitems and folder in the provided folder
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="collection">The collection.</param>
        private static void RecursiveGetAllElements(object folder, ref Collection<object> collection)
        {
            foreach (var item in GetSubItem(folder))
              collection.Add(item);

            foreach (var subfolder in GetSubFolder(folder))
            {
                collection.Add(subfolder);
                RecursiveGetAllElements(subfolder, ref collection);
            }
        }


        /// <summary>Returns a IEnumerable of all subitems of item</summary>
        /// <param name="item">The item.</param>
        /// <returns>IEnumerable&lt;object&gt;</returns>
        public static IEnumerable<object> GetSubItem(object item)
        {
            object subItem = null;
            if (item != null)
            {
                if (item is Project)
                {
                    subItem = new Collection<object>();
                    foreach (var device in (item as Project).Devices)
                        (subItem as Collection<object>).Add(device);
                    (subItem as Collection<object>).Add((item as Project).Graphics);
                }
                else if (item is DeviceUserGroup)
                    subItem = (item as DeviceUserGroup).Devices;
                else if (item is PlcSoftware)
                    subItem = new Collection<object>();
                else if (item is HmiTarget)
                    subItem = new Collection<object>();
                else if (item is Device)
                    subItem = (item as Device).Items;
                else if (item is DeviceItem)
                    subItem = (item as DeviceItem).Items;
                else if (item is PlcBlockGroup)
                    subItem = (item as PlcBlockGroup).Blocks;
                else if (item is PlcSystemBlockGroupComposition)
                    subItem = new Collection<object>();
                else if (item is PlcTagTableGroup)
                    subItem = (item as PlcTagTableGroup).TagTables;
                else if (item is PlcTypeGroup)
                    subItem = (item as PlcTypeGroup).Types;
                else if (item is PlcExternalSourceGroup)
                    subItem = (item as PlcExternalSourceGroup).ExternalSources;
                else if (item is TagFolder)
                    subItem = (item as TagFolder).TagTables;
                else if (item is ScreenFolder)
                    subItem = (item as ScreenFolder).Screens;
                else if (item is ScreenTemplateFolder)
                    subItem = (item as ScreenTemplateFolder).ScreenTemplates;
                else if (item is ScreenPopupFolder)
                    subItem = (item as ScreenPopupFolder).ScreenPopups;
                else if (item is ScreenSlideinSystemFolder)
                    subItem = (item as ScreenSlideinSystemFolder).ScreenSlideins;
                else if (item is VBScriptFolder)
                    subItem = (item as VBScriptFolder).VBScripts;
                else if (item is MultiLingualGraphicComposition)
                    subItem = item;
                else if (item is TagTable)
                    subItem = (item as TagTable).Tags;
                else if (item is IEngineeringComposition)
                    subItem = item;
                else if (item is ProjectLibrary)
                    subItem = new Collection<object>();
                else if (item is MasterCopyFolder)
                    subItem = (item as MasterCopyFolder).MasterCopies;
                else if (item is LibraryTypeFolder)
                    subItem = (item as LibraryTypeFolder).Types;
                else
                {
                    subItem = new Collection<object>();
                }
            }

            return subItem as IEnumerable<object>;
        }


        /// <summary>Returns a IEnumerbale of all subfolder of item</summary>
        /// <param name="item">The item.</param>
        /// <returns>IEnumerable&lt;object&gt;</returns>
        public static IEnumerable<object> GetSubFolder(object item)
        {
            object subFolder;

            if (item is PlcSoftware)
            {
                var sw = item as PlcSoftware;
                var col = new Collection<object>();
                col.Add(sw.BlockGroup);
                col.Add(sw.TagTableGroup);
                col.Add(sw.TypeGroup);
                col.Add(sw.ExternalSourceGroup);
                subFolder = col;
            }
            else if (item is HmiTarget)
            {
                var target = item as HmiTarget;
                var col = new Collection<object>();
                col.Add(target.ScreenFolder);
                col.Add(target.ScreenTemplateFolder);
                col.Add(target.TagFolder);
                col.Add(target.Connections);
                col.Add(target.VBScriptFolder);
                col.Add(target.Cycles);
                subFolder = col;
            }
            else if (item is Device)
                subFolder = new Collection<object>();
            else if (item is DeviceItem)
                subFolder = new Collection<object>();
            else if (item is PlcBlockSystemGroup)
            {
                var col = new Collection<object>();
                var group = item as PlcBlockSystemGroup;
                foreach (var element in group.Groups)
                {
                    col.Add(element);
                }
                if ((item as PlcBlockSystemGroup).SystemBlockGroups != null)
                    col.Add(group.SystemBlockGroups);
                subFolder = col;
            }
            else if (item is PlcBlockGroup)
                subFolder = (item as PlcBlockGroup).Groups;
            else if (item is PlcSystemBlockGroupComposition)
            {
                var col = new Collection<object>();
                var group = item as PlcSystemBlockGroupComposition;
                foreach (var element in group)
                {
                    col.Add(element);
                }
                subFolder = col;
            }
            else if (item is PlcSystemBlockGroup)
                subFolder = (item as PlcSystemBlockGroup).Groups;
            else if (item is TagFolder)
                subFolder = (item as TagFolder).Folders;
            else if (item is PlcTagTableGroup)
                subFolder = (item as PlcTagTableGroup).Groups;
            else if (item is PlcTypeGroup)
                subFolder = (item as PlcTypeGroup).Groups;
            else if (item is VBScriptFolder)
                subFolder = (item as VBScriptFolder).Folders;
            else if (item is ScreenFolder)
                subFolder = (item as ScreenFolder).Folders;
            else if (item is ScreenTemplateFolder)
                subFolder = (item as ScreenTemplateFolder).Folders;
            else if (item is ScreenPopupFolder)
                subFolder = (item as ScreenPopupFolder).Folders;
            else if (item is ProjectLibrary)
                subFolder = (item as ProjectLibrary).MasterCopyFolder.Folders;
            else if (item is TagTable)
                subFolder = (item as TagTable).Tags;
            else if (item is LibraryTypeFolder)
                subFolder = (item as LibraryTypeFolder).Folders;
            else if (item is MasterCopyFolder)
                subFolder = (item as MasterCopyFolder).Folders;
            else if (item is DeviceUserGroup)
                subFolder = item;
            else if (item is Project)
            {
                subFolder = new Collection<object>();
                //foreach (object obj in GetAllTargets(item))
                //    subFolder.Add(obj);
                foreach (var folder in (item as Project).DeviceGroups)
                    (subFolder as Collection<object>).Add(folder);
            }
            else
            {
                subFolder = new Collection<object>();
            }

            return subFolder as IEnumerable<object>;
        }
        #endregion

        #region Exporting program structure
        /// <summary>
        /// Exports blocks in main group and then call recursive function
        /// </summary>
        /// <param name="plcSoftware">Connected software</param>
        public static List<string> exportProgramStructure(PlcSoftware plcSoftware, string path)
        {
            SubLevel nestingLevel = new SubLevel();
            List<string> unexported = new List<string>();

            foreach (PlcBlock block in plcSoftware.BlockGroup.Blocks)

                if (block.IsConsistent) block.Export(new FileInfo(path + $"\\{block.Name}.xml"), ExportOptions.WithDefaults);
                else unexported.Add(path + $"\\{block.Name}");

            exportProgramStructureRecursive(plcSoftware.BlockGroup.Groups, nestingLevel, path, ref unexported);

            return unexported;
        }


        /// <summary>
        /// Checks group for blacks and checking subgroups by recurse
        /// </summary>
        /// <param name="groupComposition">Subgroup</param>
        /// <param name="nestLevel">saves sublevel</param>
        public static void exportProgramStructureRecursive(PlcBlockUserGroupComposition groupComposition, SubLevel nestLevel, string path, ref List<string> unexported)
        {
            foreach (PlcBlockGroup blockGroup in groupComposition)
            {
                string subpath = path + $"\\{blockGroup.Name}";
                Directory.CreateDirectory(subpath);

                foreach (PlcBlock plcBlock in blockGroup.Blocks)
                {
                    if (plcBlock.IsConsistent) plcBlock.Export(new FileInfo(subpath + $"\\{plcBlock.Name}.xml"), ExportOptions.WithDefaults);
                    else unexported.Add(subpath + $"\\{plcBlock.Name}");
                }
                exportProgramStructureRecursive(blockGroup.Groups, nestLevel, subpath, ref unexported);
                nestLevel.PrevLeveL();
            }
        }
        #endregion


        #region Finding system group
        /// <summary>
        /// Finds defalut philogic block user group eg. !!!!!Functions
        /// </summary>
        /// <param name="mainBlockGroup">Program blocks</param>
        /// <param name="defGroup">eg. !!!!!Functions defined in def groups</param>
        /// <returns>Default philogic group or null</returns>
        public static PlcBlockUserGroup GetDefaultGroup(PlcBlockSystemGroup mainBlockGroup, DefGroup defGroup)
        {
            PlcBlockUserGroup plcSystemGroup = null;

            
            string groupName = GetTypeDescription(defGroup);

            //Console.WriteLine(groupName);

            if (groupName != null) 
            {
                plcSystemGroup = mainBlockGroup.Groups.Find(groupName);
            }

            return plcSystemGroup;
        }

        /// <summary>
        /// Get description of defined program element
        /// </summary>
        /// <param name="defGroup">Group from DefGroup enum</param>
        /// <returns>String with group name</returns>
        private static string GetTypeDescription(DefGroup defGroup)
        {
            Type enumType = typeof(DefGroup);

            var groupInfo = enumType.GetField(defGroup.ToString());
            var attribute = groupInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = (DescriptionAttribute)attribute[0];

            return description.Description;
        }

        #endregion

        /// <summary>
        /// Finds sequence blocks
        /// </summary>
        /// <param name="mainBlockGroup">Group with sequences</param>
        /// <returns>Collection of sequences blocks</returns>
        public static Collection<PlcBlock> GetSequencesBlocks(PlcBlockSystemGroup mainBlockGroup)
        {
            PlcBlockUserGroup sequencesGroup = GetDefaultGroup(mainBlockGroup, DefGroup.Sequences);
            //Console.WriteLine(sequencesGroup.Name);
            Collection<PlcBlock> sequences = new Collection<PlcBlock>();

            RecursiveGetSeqBlocks(sequencesGroup, ref sequences);
            
            return sequences;
        }

        
        private static void RecursiveGetSeqBlocks(PlcBlockUserGroup group, ref Collection<PlcBlock> sequences)
        {

            string pattern = @"(Step\w*)|(DB\w*)|((FC|FB)\w*(Handling))";
            // Setting flag RegexOptions.IgnoreCase and RegexOptions.Multiline
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Multiline;
            foreach (PlcBlock block in group.Blocks)
            {
                
                if ( !( Regex.IsMatch(block.Name, pattern, options) ) )
                {
                    sequences.Add(block);
                    //Console.WriteLine(block.Name);
                }
            }

            foreach (PlcBlockUserGroup userGroup in group.Groups)
            {
                RecursiveGetSeqBlocks(userGroup, ref sequences);
                //Console.WriteLine(userGroup.Name);
            }
        }


        public static PlcBlockGroup GetGroupByBlockName(PlcBlockGroup blockGroup, string blockName)
        {
            PlcBlock plcBlock = blockGroup.Blocks.FirstOrDefault(block => block.Name == blockName);
            if (plcBlock != null) return blockGroup;
            else
            {
                foreach (PlcBlockGroup plcBlockGroup in blockGroup.Groups)
                {
                    //Console.WriteLine(plcBlockGroup.Name);
                    PlcBlockGroup resultGroup = GetGroupByBlockName(plcBlockGroup, blockName);
                    if (resultGroup != null) return resultGroup;
                }
            }

            return null;
        }


        public static PlcBlockGroup GetGroupByGroupName(PlcBlockGroup blockGroup, string groupName)
        {
            PlcBlockGroup plcBlockGroup = blockGroup.Groups.FirstOrDefault(group => group.Name == groupName);
            //Console.WriteLine(plcBlockGroup.Name);
            if (plcBlockGroup != null) return plcBlockGroup;
            else
            {
                foreach (PlcBlockGroup subBlockGroup in blockGroup.Groups)
                {
                    //Console.WriteLine(subBlockGroup.Name);
                    PlcBlockGroup resultGroup = GetGroupByGroupName(subBlockGroup, groupName);
                    if (resultGroup != null) return resultGroup;
                }
            }
            return null;
        }



        /// <summary>
        /// Get list of active languages
        /// </summary>
        /// <returns></returns>
        public static List<CultureInfo> GetProjectCultures(Project project)
        {
            List<CultureInfo> cultureList = new List<CultureInfo>();

            foreach (Language language in project.LanguageSettings.ActiveLanguages)
            {
                cultureList.Add(language.Culture);
            }

            return cultureList;
        }


        public static string NewBlockNameIfExist(string blockName, PlcBlockGroup blockGroup)
        {
            int nameSuflex = 1;
            string name = blockName;

            while (true)
            {
                PlcBlock block = blockGroup.Blocks.Find(name);
                if (block == null)
                {
                    return name;
                }

                if (block.Name == name)
                {
                    name = $"{blockName}_{nameSuflex++}";
                }
                else return name;
            }
        }



        /// <summary>
        /// Displaying composition informations
        /// </summary>
        /// <param name="obj"></param>
        public static void DisplayCompositionInfos(IEngineeringObject obj)
        {
            IList<EngineeringCompositionInfo> compositionInfos = obj.GetCompositionInfos();
            foreach (EngineeringCompositionInfo compositionInfo in compositionInfos)
            {
                Console.WriteLine(compositionInfo.Name);
            }
        }


        public static string SeparateWords(string text)
        {
            if (text is null) return text;
            if (text.Length < 2) return text;

            StringBuilder stringBuilder = new StringBuilder(text);

            bool isDigit, isDigitNext, isWhiteSpace, isWhiteSpaceNext, isWhiteSpacePrev;

            for (int i = 1; i < stringBuilder.Length - 1; i++)
            {
                if (stringBuilder[i] == '_') 
                {
                    stringBuilder.Replace('_', ' ', i, 1);
                    continue;
                }

                if (char.IsLower(stringBuilder[i])) continue;
                if (char.IsWhiteSpace(stringBuilder[i])) continue;
                if (char.IsWhiteSpace(stringBuilder[i - 1])) continue;
                if (char.IsWhiteSpace(stringBuilder[i + 1])) continue;
                if (char.IsUpper(stringBuilder[i - 1])) continue;
                if (char.IsUpper(stringBuilder[i + 1])) continue;
                if (char.IsDigit(stringBuilder[i])) continue;
                if (char.IsDigit(stringBuilder[i + 1])) continue;

                {
                    stringBuilder[i] = char.ToLower(stringBuilder[i]);
                    stringBuilder.Insert(i, ' ');
                }
            }

            return stringBuilder.ToString();
        }


    }
}
