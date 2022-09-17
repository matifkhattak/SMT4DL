using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using System.Runtime.InteropServices;
using System.Globalization;
using System.IO;
using System.Net;

namespace ExcelProcesser
{
    public partial class ProcessExcelFiles : Form
    {
        public ProcessExcelFiles()
        {
            InitializeComponent();
        }
        //http://csharp.net-informations.com/excel/csharp-read-excel.htm
        //http://csharp.net-informations.com/excel/csharp-create-excel.htm
        // Just take the predicted class + its probability and save it in a new file
        private void btnSingleFile_Click(object sender, EventArgs e)
        {
            // int[] rowsToBeProcessed = new int[] { 3,29, 34, 47, 59, 71, 98, 109, 128, 129, 143, 158, 174, 195, 212, 239, 245, 251, 260, 271, 284, 294, 302, 333, 341, 347, 399, 402, 444,473, 478, 496, 502, 526, 535, 544, 555, 571, 581, 585, 601, 627, 642,660, 676, 681, 700, 763,791, 811,945 };
            int noOfSheets = 30;
            int[] rowsToBeProcessed = new int[] { };
            
            // process user input entered rows
            if (txtInstanceNo.Text != string.Empty)
            {
                rowsToBeProcessed = Array.ConvertAll(txtInstanceNo.Text.Split(','), s => int.Parse(s));
            }
            else
            {
                // process explicitly added row numbers
                rowsToBeProcessed = new int[]  {13};
            }


            foreach (int row in rowsToBeProcessed)
            {               
                string writeFileLocation = txtSingleFileWriteLocation.Text + "\\" + row + ".xls";
                // Reading Excel Sheet
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlApp.DisplayAlerts = false;
                Microsoft.Office.Interop.Excel.Workbook xlReadWorkBook = xlApp.Workbooks.Open(txtSingleFileReadLocation.Text, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet xlReadWorkSheet = null;

                //Writing Excel Sheet
                Microsoft.Office.Interop.Excel.Workbook xlWriteWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWriteWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlWriteWorkBook = xlApp.Workbooks.Add(misValue);
                xlWriteWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWriteWorkBook.Worksheets.get_Item(1);

                xlWriteWorkSheet.Cells[1, 1] = "Class0";
                xlWriteWorkSheet.Cells[1, 2] = "Class1";
                int writeRowCount = 2;

                string predictedClass;
                string probabilityOfPredictedClass;
                int rw = 0;
                int cl = 0;
                Microsoft.Office.Interop.Excel.Range range;

                int rowToBeProcessed = row;// = Convert.ToInt16(txtInstanceNo.Text);

                // count the nubmer of times specific class was predicted
                int class0Count = 0;
                int class1Count = 0;

                // iterate over all sheets to extract the needed data
                for (int i = 1; i <= noOfSheets; i++)
                {
                    xlReadWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlReadWorkBook.Worksheets.get_Item(i);
                    range = xlReadWorkSheet.UsedRange;
                    rw = range.Rows.Count;
                    cl = range.Columns.Count;
                    predictedClass = (string)(range.Cells[rowToBeProcessed, 2] as Microsoft.Office.Interop.Excel.Range).Value2; // column number which represents the predicted class
                    probabilityOfPredictedClass = (string)(range.Cells[rowToBeProcessed, 4] as Microsoft.Office.Interop.Excel.Range).Value2;

                    //Write in Excel Sheet, this library has start index from 1 (so we have to add 1)
                    xlWriteWorkSheet.Cells[writeRowCount, Convert.ToInt16(predictedClass) + 1] = probabilityOfPredictedClass;
                    writeRowCount = writeRowCount + 1;

                    #region

                    if (predictedClass == "0")
                        class0Count = class0Count + 1;
                    else if (predictedClass == "1")
                        class1Count = class1Count + 1;

                    #endregion
                }

                // Write the count (number of times the corresponsing class has been predicted) for each class

                xlWriteWorkSheet.Cells[35, 1] = class0Count; // row number + column number at which the count will be written/printed
                xlWriteWorkSheet.Cells[35, 2] = class1Count;

                //xlWriteWorkBook.SaveAs(@"C:\Users\faqeerrehman\MSU\Research\CancerPrediction\ScientificSWTesting\Pilot1\P1B2Tests\Results\PerClassOutputs\20Epochs\ExcelProcesser\ProcessedResults\MR52\MRDecision\Class9\" + row + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);
                //xlWriteWorkBook.SaveAs(@"C:\Users\faqeerrehman\MSU\Research\CancerPrediction\ScientificSWTesting\Pilot1\P1B2Tests\Results\PerClassOutputs\20Epochs\ExcelProcesser\ProcessedResults_Clem\Origional\Origional_20.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);
                xlWriteWorkBook.SaveAs(writeFileLocation, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);
                xlWriteWorkBook.Close(true, misValue, misValue);
                xlReadWorkBook.Close(true, null, null);
                xlApp.Quit();

                Marshal.ReleaseComObject(xlReadWorkSheet);
                Marshal.ReleaseComObject(xlReadWorkBook);
                Marshal.ReleaseComObject(xlWriteWorkSheet);
                Marshal.ReleaseComObject(xlWriteWorkBook);
                Marshal.ReleaseComObject(xlApp);
            }
            MessageBox.Show("Process Completed Successfully.");


        }
        


        // First see which class is predicted in origional file. After that pick the probability (from MR file(s) attached with the class predicted in the origional file.
        private void btnProcessProbabilityWrtOrigional_Click(object sender, EventArgs e)
        {
            int noOfSheets = 101;
            //string writeFileLocation = txtSingleFileWriteLocation.Text;
            // Reading Excel Sheet
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            //C:\Users\faqeerrehman\MSU\Research\CancerPrediction\ScientificSWTesting\Pilot1\P1B2Tests\Results\PerClassOutputs\20Epochs\MR0\MR00.xls
            Microsoft.Office.Interop.Excel.Workbook xlReadWorkBook = xlApp.Workbooks.Open(txtSingleFileReadLocation.Text, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Microsoft.Office.Interop.Excel.Worksheet xlReadWorkSheet = null;

            // Origional File
            Microsoft.Office.Interop.Excel.Workbook xlReadWorkBookOrigionalFile = xlApp.Workbooks.Open(@"C:\Users\faqeerrehman\MSU\Research\CancerPrediction\ScientificSWTesting\Pilot1\P1B2Tests\Results\PerClassOutputs\20Epochs\Orgional\Origional.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Microsoft.Office.Interop.Excel.Worksheet xlReadWorkSheetOrigionalFile = null;

            //Writing Excel Sheet
            Microsoft.Office.Interop.Excel.Workbook xlWriteWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWriteWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWriteWorkBook = xlApp.Workbooks.Add(misValue);
            xlWriteWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWriteWorkBook.Worksheets.get_Item(1);

            xlWriteWorkSheet.Cells[1, 1] = "Class0";
            xlWriteWorkSheet.Cells[1, 2] = "Class1";
            xlWriteWorkSheet.Cells[1, 3] = "Class2";
            xlWriteWorkSheet.Cells[1, 4] = "Class3";
            xlWriteWorkSheet.Cells[1, 5] = "Class4";
            xlWriteWorkSheet.Cells[1, 6] = "Class5";
            xlWriteWorkSheet.Cells[1, 7] = "Class6";
            xlWriteWorkSheet.Cells[1, 8] = "Class7";
            xlWriteWorkSheet.Cells[1, 9] = "Class8";
            xlWriteWorkSheet.Cells[1, 10] = "Class9";

            int writeRowCount = 2;
            string predictedClass;
            string probabilitiesOfPredictedClasses;
            double probabilityOfOrigionalPredictedClass;
            string[] predictedProbabilitesArrary;
            int rw = 0;
            int cl = 0;
            Microsoft.Office.Interop.Excel.Range range;
            Microsoft.Office.Interop.Excel.Range rangeOrigionalFile;
            
            int rowToBeProcessed = Convert.ToInt16(txtInstanceNo.Text);

            for (int i = 2; i <= noOfSheets; i++)
            {
                xlReadWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlReadWorkBook.Worksheets.get_Item(i);
                xlReadWorkSheetOrigionalFile = (Microsoft.Office.Interop.Excel.Worksheet)xlReadWorkBookOrigionalFile.Worksheets.get_Item(i); // Orgional File
                rangeOrigionalFile = xlReadWorkSheetOrigionalFile.UsedRange;
                range = xlReadWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;
                predictedClass = (string)(rangeOrigionalFile.Cells[rowToBeProcessed, 3] as Microsoft.Office.Interop.Excel.Range).Value2; // Origional File Predicted Class
                probabilitiesOfPredictedClasses = (string)(range.Cells[rowToBeProcessed, 4] as Microsoft.Office.Interop.Excel.Range).Value2;
                predictedProbabilitesArrary = probabilitiesOfPredictedClasses.Replace("\n", "").Replace("[", "").Replace("]", "").Split(' ');
                predictedProbabilitesArrary = predictedProbabilitesArrary.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                //Write in Excel Sheet, this library has start index from 1 (so we have to add 1)
                probabilityOfOrigionalPredictedClass = double.Parse(predictedProbabilitesArrary[Convert.ToInt16(predictedClass)], CultureInfo.InvariantCulture); // Get probability against predicted class in orgional file
                xlWriteWorkSheet.Cells[writeRowCount, Convert.ToInt16(predictedClass) + 1] = double.Parse(predictedProbabilitesArrary[Convert.ToInt16(predictedClass)], CultureInfo.InvariantCulture);
                writeRowCount = writeRowCount + 1;
            }

            //xlWriteWorkBook.SaveAs(writeFileLocation, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWriteWorkBook.Close(true, misValue, misValue);
            xlReadWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlReadWorkSheet);
            Marshal.ReleaseComObject(xlReadWorkBook);
            Marshal.ReleaseComObject(xlReadWorkSheetOrigionalFile);
            Marshal.ReleaseComObject(xlReadWorkBookOrigionalFile);
            Marshal.ReleaseComObject(xlWriteWorkSheet);
            Marshal.ReleaseComObject(xlWriteWorkBook);
            Marshal.ReleaseComObject(xlApp);

            MessageBox.Show("Process Completed Successfully.");
        }

        // Record the probability of predicted class & the expected class
        private void btnRecordProbabilities_Click(object sender, EventArgs e)
        {
            int noOfSheets = 101;
            //string writeFileLocation = txtSingleFileWriteLocation.Text;
            // Reading Excel Sheet
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlReadWorkBook = xlApp.Workbooks.Open(txtSingleFileReadLocation.Text, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Microsoft.Office.Interop.Excel.Worksheet xlReadWorkSheet = null;

            // Origional File
            Microsoft.Office.Interop.Excel.Workbook xlReadWorkBookOrigionalFile = xlApp.Workbooks.Open(@"C:\Users\faqeerrehman\MSU\Research\CancerPrediction\ScientificSWTesting\Pilot1\P1B2Tests\Results\PerClassOutputs\20Epochs\Orgional\Origional.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Microsoft.Office.Interop.Excel.Worksheet xlReadWorkSheetOrigionalFile = null;

            //Writing Excel Sheet
            Microsoft.Office.Interop.Excel.Workbook xlWriteWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWriteWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWriteWorkBook = xlApp.Workbooks.Add(misValue);
            xlWriteWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWriteWorkBook.Worksheets.get_Item(1);

            xlWriteWorkSheet.Cells[1, 1] = "OrigionalClass";
            xlWriteWorkSheet.Cells[1, 2] = "PredictedClass";
            xlWriteWorkSheet.Cells[1, 3] = "IsCorrectOutputPredicted";
            xlWriteWorkSheet.Cells[1, 4] = "PredictedClassProbability";
            xlWriteWorkSheet.Cells[1, 5] = "Expected(Origional)ClassProbability";

            int writeRowCount = 2;
            string origionalPredictedClass;
            string currentMRPredictedClass;
            string probabilityOfCurrentMRPredictedClass;
            string probabilitiesOfCurrentMRClasses;
            int rw = 0;
            int cl = 0;
            Microsoft.Office.Interop.Excel.Range range;
            Microsoft.Office.Interop.Excel.Range rangeOrigionalFile;
            double probabilityOfOrigionalPredictedClassInCurrentMR;
            string[] predictedProbabilitesArrary;
            int rowToBeProcessed = Convert.ToInt16(txtInstanceNo.Text);

            for (int i = 2; i <= noOfSheets; i++)
            {
                xlReadWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlReadWorkBook.Worksheets.get_Item(i);
                xlReadWorkSheetOrigionalFile = (Microsoft.Office.Interop.Excel.Worksheet)xlReadWorkBookOrigionalFile.Worksheets.get_Item(i); // Orgional File
                rangeOrigionalFile = xlReadWorkSheetOrigionalFile.UsedRange;
                range = xlReadWorkSheet.UsedRange;
                rw = range.Rows.Count;
                cl = range.Columns.Count;
                origionalPredictedClass = (string)(rangeOrigionalFile.Cells[rowToBeProcessed, 3] as Microsoft.Office.Interop.Excel.Range).Value2; // Origional File Predicted Class

                currentMRPredictedClass = (string)(range.Cells[3, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
                probabilityOfCurrentMRPredictedClass = (string)(range.Cells[rowToBeProcessed, 5] as Microsoft.Office.Interop.Excel.Range).Value2;
                probabilitiesOfCurrentMRClasses = (string)(range.Cells[rowToBeProcessed, 4] as Microsoft.Office.Interop.Excel.Range).Value2;
                predictedProbabilitesArrary = probabilitiesOfCurrentMRClasses.Replace("\n", "").Replace("[", "").Replace("]", "").Split(' ');
                predictedProbabilitesArrary = predictedProbabilitesArrary.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                //Write in Excel Sheet, this library has start index from 1 (so we have to add 1)
                probabilityOfOrigionalPredictedClassInCurrentMR = double.Parse(predictedProbabilitesArrary[Convert.ToInt16(origionalPredictedClass)], CultureInfo.InvariantCulture); // Get probability against predicted class in orgional file
                xlWriteWorkSheet.Cells[writeRowCount, 1] = origionalPredictedClass;
                xlWriteWorkSheet.Cells[writeRowCount, 2] = currentMRPredictedClass;
                xlWriteWorkSheet.Cells[writeRowCount, 3] = origionalPredictedClass == currentMRPredictedClass ? 1 : 0;
                xlWriteWorkSheet.Cells[writeRowCount, 4] = probabilityOfCurrentMRPredictedClass;
                xlWriteWorkSheet.Cells[writeRowCount, 5] = probabilityOfOrigionalPredictedClassInCurrentMR;
                writeRowCount = writeRowCount + 1;
            }
            xlWriteWorkBook.Close(true, misValue, misValue);
            xlReadWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlReadWorkSheet);
            Marshal.ReleaseComObject(xlReadWorkBook);
            Marshal.ReleaseComObject(xlReadWorkSheetOrigionalFile);
            Marshal.ReleaseComObject(xlReadWorkBookOrigionalFile);
            Marshal.ReleaseComObject(xlWriteWorkSheet);
            Marshal.ReleaseComObject(xlWriteWorkBook);
            Marshal.ReleaseComObject(xlApp);

            MessageBox.Show("Process Completed Successfully.");
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
        
        private void btnExtractProbabilities_Click(object sender, EventArgs e)
        {
            //========Guidance=================//
            //Step1: Frequency Distribution Files Path (Folder/Directory): Pointing to files which are used to find out the class that was predicted max times during source execution(Always pointing to Source execution results)
            //C:\Users\faqeerrehman\MSU\OneDrive - Montana State University\Research\Clem\ImageClassification\ProcessedPredictionsAndResults\MaxVoting\Mutant1\MRs\Source\Instances

            //Once we identified, which class was predicted maximum times for specific instance during source execution(Step1), we can then extract the probablity for the same class for the ith iteration
            //for the follow-up execution(file path that contains the raw predicted results after running python based image classifiers model i.e., Mutant1_Blur2.xls).
            //C:\Users\faqeerrehman\MSU\OneDrive - Montana State University\Research\Clem\ImageClassification\PneumoniaDetection\Results\Mutants\Mutant1\Followup\Mutant1_Blur2.xls

            //Write location: the place where we want to have our newly generated files.
            //C:\Users\faqeerrehman\MSU\OneDrive - Montana State University\Research\Clem\ImageClassification\ProcessedPredictionsAndResults\TestStatistic\Mutant1\MRs\MRBlur\Instances
            //=======End Guidance===========//


            int noOfSheets = 30;// 101;
            // First see that which class was predicted max time (if multiple then select first of them to break the tie), then extract the proability for the max class for the instance during each iteration

            //====frequencyDistributionFilePath will contain the source(origional) file frequency distribution results e.g. C:\Users\faqeerrehman\MSU\CourseWork\ThirdSemester\STAT511\Project\Dataset\Mutant4\SourceExecution\InstancePredictedClass\FrequencyDistribution.xls. The last row will contain the number of times each class was predicted.====
            //string frequencyDistributionFilePath = @"C:\Users\faqeerrehman\MSU\OneDrive - Montana State University\Research\Clem\Candle_SoftmaxProbabilities\P1B2Tests\Results\Research1\Mutant1\SourceExecution\InstancePredictedClass\10.xls"; //C:\Users\faqeerrehman\MSU\OneDrive - Montana State University\Research\Clem\Candle_SoftmaxProbabilities\P1B2Tests\Results\Research1\Mutant1\SourceExecution\InstancePredictedClass\10.xls
            string toProcessFilePath = txtSingleFileReadLocation.Text;// @"";
                                                                      // Reading Excel Sheet


            #region Extract Max Count Class from Frequency Distribution file

            // Count of each class is at row 103, this value should be entered in txtCountClassLabelsRowNo
            int rowNoToBeProcessed = Convert.ToInt32(35); //35th row contains the count (how many time a class is predicted)
            int[] instancesToBeProcessed = new int[] { };

            // process user input entered rows
            if (txtInstanceNo.Text != string.Empty)
            {
                instancesToBeProcessed = Array.ConvertAll(txtInstanceNo.Text.Split(','), s => int.Parse(s));
            }
            else
            {
                // process explicitly added row numbers
                instancesToBeProcessed = new int[] {5,11,13,18,39,42};
            }

            foreach (int instanceNo in instancesToBeProcessed)
            {
                string writeFileLocation = txtSingleFileWriteLocation.Text + "\\" + instanceNo + ".xls";
                string frequencyDistributionFilePath = txtFrequencyDistributionFilesPath.Text + "\\" + instanceNo + ".xls"; //C:\Users\faqeerrehman\MSU\OneDrive - Montana State University\Research\Clem\ImageClassification\ProcessedPredictionsAndResults\MaxVoting\Mutant1\MRs\Source\Instances
                Microsoft.Office.Interop.Excel.Application xlAppForWriting = new Microsoft.Office.Interop.Excel.Application();
                xlAppForWriting.DisplayAlerts = false;

                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlApp.DisplayAlerts = false;
                Microsoft.Office.Interop.Excel.Workbook xlReadWorkBookFDFile = xlApp.Workbooks.Open(frequencyDistributionFilePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet xlReadWorkSheetFDFile = null;

                Microsoft.Office.Interop.Excel.Range rangeFDFile;
                xlReadWorkSheetFDFile = (Microsoft.Office.Interop.Excel.Worksheet)xlReadWorkBookFDFile.Worksheets.get_Item(1); // first sheet of file to be processed            
                rangeFDFile = xlReadWorkSheetFDFile.UsedRange;
                int maxClassCount = 0;
                int eachClassCount = 0;
                string maxClassCountLabel = "";
                int maxClassCountIndex = 0;
                for (int i = 1; i <= 2; i++) // the number of class labels
                {
                    eachClassCount = (Int32)(rangeFDFile.Cells[rowNoToBeProcessed, i] as Microsoft.Office.Interop.Excel.Range).Value2;
                    if (eachClassCount > maxClassCount)
                    {
                        maxClassCount = eachClassCount;
                        maxClassCountLabel = "Class" + (i - 1).ToString();
                        maxClassCountIndex = i - 1;
                    }
                }

                #endregion

                //Writing Excel Sheet
                Microsoft.Office.Interop.Excel.Workbook xlWriteWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWriteWorkSheet;
                object misValue = System.Reflection.Missing.Value;
                xlWriteWorkBook = xlAppForWriting.Workbooks.Add(misValue);
                xlWriteWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWriteWorkBook.Worksheets.get_Item(1);

                xlWriteWorkSheet.Cells[1, 1] = maxClassCountLabel;

                // Read the File to extract the probability for the class that highest count in Frequency Distribution(FD) file
                Microsoft.Office.Interop.Excel.Workbook xlReadWorkBookFile = xlApp.Workbooks.Open(toProcessFilePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                Microsoft.Office.Interop.Excel.Worksheet xlReadWorkSheetFile = null;
                Microsoft.Office.Interop.Excel.Range rangeFile;
                string probabilitiesInFile;
                decimal probabilityOfMaxTimePredictedClass;
                string[] probabilitiesInFileArrary;
                int rowToBeProcessed = instanceNo;
                int writeRowCount = 2;
                
                for (int i = 1; i <= noOfSheets; i++)
                {
                    xlReadWorkSheetFile = (Microsoft.Office.Interop.Excel.Worksheet)xlReadWorkBookFile.Worksheets.get_Item(i); // first sheet of file to be processed            
                    rangeFile = xlReadWorkSheetFile.UsedRange;
                    //predictedClass = (string)(rangeOrigionalFile.Cells[rowToBeProcessed, 3] as Microsoft.Office.Interop.Excel.Range).Value2; // Origional File Predicted Class
                    probabilitiesInFile = (string)(rangeFile.Cells[rowToBeProcessed, 3] as Microsoft.Office.Interop.Excel.Range).Value2; // 3rd column is 'PredictedProbabilities'
                    probabilitiesInFileArrary = probabilitiesInFile.Replace("\n", "").Replace("[", "").Replace("]", "").Split(',');


                    //probabilitiesInSourceFileArrary = probabilitiesInSourceFileArrary.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                    //Write in Excel Sheet, this library has start index from 1 (so we have to add 1)
                    probabilityOfMaxTimePredictedClass = decimal.Parse(probabilitiesInFileArrary[maxClassCountIndex] == "" ? "0" : probabilitiesInFileArrary[maxClassCountIndex], System.Globalization.NumberStyles.Float); // Get probability against the needed class from orgional file
                    probabilityOfMaxTimePredictedClass = Math.Round(probabilityOfMaxTimePredictedClass, 3);
                    xlWriteWorkSheet.Cells[writeRowCount, 1] = probabilityOfMaxTimePredictedClass.ToString();
                    writeRowCount = writeRowCount + 1;
                }
                //txtSingleFileWriteLocation.Text = "C:\Users\faqeerrehman\MSU\OneDrive - Montana State University\Research\Clem\Candle_SoftmaxProbabilities\P1B2Tests\Results\Research2_T_Test\Mutant1\SourceExecution\InstancePredictedClass";
                xlWriteWorkBook.SaveAs(writeFileLocation, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);
                xlWriteWorkBook.Close(true, misValue, misValue);
                xlReadWorkBookFDFile.Close(true, null, null);
                xlReadWorkBookFile.Close(true, null, null);
                xlApp.Quit();
                xlAppForWriting.Quit();

                Marshal.ReleaseComObject(xlReadWorkSheetFDFile);
                Marshal.ReleaseComObject(xlReadWorkBookFDFile);
                Marshal.ReleaseComObject(xlReadWorkSheetFile);
                Marshal.ReleaseComObject(xlReadWorkBookFile);
                Marshal.ReleaseComObject(xlWriteWorkSheet);
                Marshal.ReleaseComObject(xlWriteWorkBook);
                Marshal.ReleaseComObject(xlApp);
                Marshal.ReleaseComObject(xlAppForWriting);
            }
            MessageBox.Show("Process Completed Successfully.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
    }
}

