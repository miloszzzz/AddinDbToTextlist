using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Siemens.Engineering;

namespace AddinDbToTextlist.Helpers
{
    public class LogToFile
    {
        public static void Save(Exception ex)
        {
            switch (ex.GetType())
            {
                default:
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt";
                    saveFileDialog.Title = "Save log file";
                    saveFileDialog.FileName = "log.txt";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = saveFileDialog.FileName;

                        try
                        {
                            using (StreamWriter writer = new StreamWriter(filePath))
                            {
                                writer.WriteLine("Date: " + DateTime.Now);
                                writer.WriteLine("Exception: " + ex.GetType());
                                writer.WriteLine("Message: " + ex.Message);
                                writer.WriteLine("Source: " + ex.Source);
                                writer.WriteLine("Stack trace: " + ex.StackTrace);

                                // Dodaj dodatkowe informacje, jeśli są dostępne
                                if (ex.InnerException != null)
                                {
                                    writer.WriteLine("Internal exception:");
                                    writer.WriteLine("Exception: " + ex.ToString());
                                    writer.WriteLine("Message: " + ex.InnerException.Message);
                                    writer.WriteLine("Source: " + ex.InnerException.Source);
                                    writer.WriteLine("Stack trace: " + ex.InnerException.StackTrace);
                                }

                                writer.WriteLine();
                                writer.WriteLine("--- Log end ---");
                            }

                            MessageBox.Show("Log file was saved.");
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Error occured during log file save.");
                        }
                    }
                    return;
            }
        }
    }
}
