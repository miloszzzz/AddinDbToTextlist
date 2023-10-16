using Siemens.Engineering.SW.Blocks;
using System.Collections.Generic;
using System.Globalization;
using TiaHelperLibrary.Models.Tia;

namespace TiaHelperLibrary.Helpers
{
    public class SeqHelper
    {
        public static List<Sequence> GetSequence(PlcBlock block, List<CultureInfo> cultures)
        {
            /*List<XElement> sequenceTextElements = new List<XElement>();
            List<Sequence> sequenceTexts = new List<Sequence>();

            for (int i = 0; i < cultures.Count; i++)
            {
                sequenceTexts.Add(new Sequence());
            }

            GetElementsWithName(element, "MultilingualText", ref sequenceTextElements);

            foreach (XElement seqText in sequenceTextElements)
            {
                foreach (XElement objectList in seqText.Elements("ObjectList"))
                {
                    //Console.WriteLine(objectList.Value);
                    if (objectList.Elements("MultilingualText") == null) continue; //Console.WriteLine("MultilingualText");

                    foreach (XElement multilingualText in objectList.Elements("MultilingualText"))
                    {
                        Console.WriteLine(multilingualText.Value);
                        int i = 0;
                        foreach (XElement multilingualTextItem in multilingualText.Elements("ObjectList"))
                        {
                            if (multilingualTextItem.Element("AttributeList").Element("Text") != null) continue; //Console.WriteLine(@"Element('AttributeList').Element('Text')");

                            XElement text = multilingualText.Element("AttributeList").Element("Text");

                            if (text != null)
                            {
                                Step step = new Step();
                                step.Name = text.Value;

                                if (checkStepName(step.Name))
                                {
                                    //Console.WriteLine(step.Name);
                                    step = getStepInfoFromName(step.Name);
                                    sequenceTexts[i].Steps.Add(step);
                                }
                            }

                            i++;
                        }
                    }
                }
                */
            /*
            for (int i = 0; i < cultures.Count; i++)
            {

                if (multiTextItem.Element("AttributeList").Element("Culture") != null && multiTextItem.Element("AttributeList").Element("Text") != null)
                { 
                    Step step = new Step();
                    step.Name = multiTextItem.Element("AttributeList").Element("Text").Value;


                    if (checkStepName(step.Name))
                    {
                        //Console.WriteLine(step.Name);
                        step = getStepInfoFromName(step.Name);
                        sequenceTexts[i].Steps.Add(step);
                    }
                }
            }*/

            List<Sequence> sequences = new List<Sequence>();

            // Add sequence for each language
            foreach (CultureInfo c in cultures)
            {
                Sequence seq = new Sequence();
                seq.Language = c.Name;
                sequences.Add(seq);
            }
            /*
            Document sequenceFc = SequenceXmlDeserialize(block);

            List<object> objectsFromXml = new List<object>();

            // Get sequence items from Document object
            foreach (object item in sequenceFc.SWBlocksFC.ObjectList.Items)
            {
                objectsFromXml.Add(item);
            }

            // For objects found in sequence
            foreach (object obj in objectsFromXml)
            {
                if (obj is DocumentSWBlocksFCObjectListSWBlocksCompileUnit)
                {
                    DocumentSWBlocksFCObjectListSWBlocksCompileUnit compileUnit = obj as DocumentSWBlocksFCObjectListSWBlocksCompileUnit;
                    foreach (DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualText compileUnitText in compileUnit.ObjectList)
                    {
                        foreach (DocumentSWBlocksFCObjectListSWBlocksCompileUnitMultilingualTextMultilingualTextItem textItem in compileUnitText.ObjectList)
                        {
                            foreach (Sequence seq in sequences)
                            {
                                //Console.WriteLine(seq.Language + "\t" + textItem.AttributeList.Culture + "\t" + checkStepName(textItem.AttributeList.Text));
                                if (seq.Language == textItem.AttributeList.Culture
                                    && checkStepName(textItem.AttributeList.Text))
                                {
                                    Step step = getStepInfoFromName(textItem.AttributeList.Text);
                                    seq.Steps.Add(step);
                                }
                            }
                        }
                    }
                }
            }
            */
            return sequences;
        }
    }
}
