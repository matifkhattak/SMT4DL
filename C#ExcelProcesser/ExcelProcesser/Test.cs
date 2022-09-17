//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ExcelProcesser
//{
//    class Test
//    {
//        private void btnSingleFile_Click(object sender, EventArgs e)
//        {
//            // int[] rowsToBeProcessed = new int[] { 3,29, 34, 47, 59, 71, 98, 109, 128, 129, 143, 158, 174, 195, 212, 239, 245, 251, 260, 271, 284, 294, 302, 333, 341, 347, 399, 402, 444,473, 478, 496, 502, 526, 535, 544, 555, 571, 581, 585, 601, 627, 642,660, 676, 681, 700, 763,791, 811,945 };
//            int noOfSheets = 101;// 31
//            int[] rowsToBeProcessed = new int[] { };

//            // process user input entered rows
//            if (txtInstanceNo.Text != string.Empty)
//            {
//                rowsToBeProcessed = Array.ConvertAll(txtInstanceNo.Text.Split(','), s => int.Parse(s));
//            }
//            else
//            {
//                // process explicitly added row numbers
//                rowsToBeProcessed = new int[] { 14, 23, 34, 46, 51 };
//            }


//            foreach (int row in rowsToBeProcessed)
//            {
//                string writeFileLocation = txtSingleFileWriteLocation.Text + "\\" + row + ".xls";
//                // Reading Excel Sheet
//                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
//                xlApp.DisplayAlerts = false;
//                Microsoft.Office.Interop.Excel.Workbook xlReadWorkBook = xlApp.Workbooks.Open(txtSingleFileReadLocation.Text, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
//                Microsoft.Office.Interop.Excel.Worksheet xlReadWorkSheet = null;

//                //Writing Excel Sheet
//                Microsoft.Office.Interop.Excel.Workbook xlWriteWorkBook;
//                Microsoft.Office.Interop.Excel.Worksheet xlWriteWorkSheet;
//                object misValue = System.Reflection.Missing.Value;
//                xlWriteWorkBook = xlApp.Workbooks.Add(misValue);
//                xlWriteWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWriteWorkBook.Worksheets.get_Item(1);

//                xlWriteWorkSheet.Cells[1, 1] = "Class0";
//                xlWriteWorkSheet.Cells[1, 2] = "Class1";
//                xlWriteWorkSheet.Cells[1, 3] = "Class2";
//                xlWriteWorkSheet.Cells[1, 4] = "Class3";
//                xlWriteWorkSheet.Cells[1, 5] = "Class4";
//                xlWriteWorkSheet.Cells[1, 6] = "Class5";
//                xlWriteWorkSheet.Cells[1, 7] = "Class6";
//                xlWriteWorkSheet.Cells[1, 8] = "Class7";
//                xlWriteWorkSheet.Cells[1, 9] = "Class8";
//                xlWriteWorkSheet.Cells[1, 10] = "Class9";
//                int writeRowCount = 2;

//                string predictedClass;
//                string probabilityOfPredictedClass;
//                int rw = 0;
//                int cl = 0;
//                Microsoft.Office.Interop.Excel.Range range;

//                int rowToBeProcessed = row;// = Convert.ToInt16(txtInstanceNo.Text);

//                // count the nubmer of times specific class was predicted
//                int class0Count = 0;
//                int class1Count = 0;
//                int class2Count = 0;
//                int class3Count = 0;
//                int class4Count = 0;
//                int class5Count = 0;
//                int class6Count = 0;
//                int class7Count = 0;
//                int class8Count = 0;
//                int class9Count = 0;

//                //for (int i = 2; i <= 101; i++)
//                for (int i = 2; i <= noOfSheets; i++)
//                {
//                    xlReadWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlReadWorkBook.Worksheets.get_Item(i);
//                    range = xlReadWorkSheet.UsedRange;
//                    rw = range.Rows.Count;
//                    cl = range.Columns.Count;
//                    predictedClass = (string)(range.Cells[rowToBeProcessed, 3] as Microsoft.Office.Interop.Excel.Range).Value2;
//                    probabilityOfPredictedClass = (string)(range.Cells[rowToBeProcessed, 5] as Microsoft.Office.Interop.Excel.Range).Value2;

//                    //Write in Excel Sheet, this library has start index from 1 (so we have to add 1)
//                    xlWriteWorkSheet.Cells[writeRowCount, Convert.ToInt16(predictedClass) + 1] = probabilityOfPredictedClass;
//                    writeRowCount = writeRowCount + 1;

//                    #region

//                    if (predictedClass == "0")
//                        class0Count = class0Count + 1;
//                    else if (predictedClass == "1")
//                        class1Count = class1Count + 1;
//                    else if (predictedClass == "2")
//                        class2Count = class2Count + 1;
//                    else if (predictedClass == "3")
//                        class3Count = class3Count + 1;
//                    else if (predictedClass == "4")
//                        class4Count = class4Count + 1;
//                    else if (predictedClass == "5")
//                        class5Count = class5Count + 1;
//                    else if (predictedClass == "6")
//                        class6Count = class6Count + 1;
//                    else if (predictedClass == "7")
//                        class7Count = class7Count + 1;
//                    else if (predictedClass == "8")
//                        class8Count = class8Count + 1;
//                    else
//                        class9Count = class9Count + 1;

//                    #endregion
//                }

//                // Write the count (number of times the corresponsing class has been predicted) for each class

//                xlWriteWorkSheet.Cells[103, 1] = class0Count;
//                xlWriteWorkSheet.Cells[103, 2] = class1Count;
//                xlWriteWorkSheet.Cells[103, 3] = class2Count;
//                xlWriteWorkSheet.Cells[103, 4] = class3Count;
//                xlWriteWorkSheet.Cells[103, 5] = class4Count;
//                xlWriteWorkSheet.Cells[103, 6] = class5Count;
//                xlWriteWorkSheet.Cells[103, 7] = class6Count;
//                xlWriteWorkSheet.Cells[103, 8] = class7Count;
//                xlWriteWorkSheet.Cells[103, 9] = class8Count;
//                xlWriteWorkSheet.Cells[103, 10] = class9Count;

//                //xlWriteWorkBook.SaveAs(@"C:\Users\faqeerrehman\MSU\Research\CancerPrediction\ScientificSWTesting\Pilot1\P1B2Tests\Results\PerClassOutputs\20Epochs\ExcelProcesser\ProcessedResults\MR52\MRDecision\Class9\" + row + ".xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);
//                //xlWriteWorkBook.SaveAs(@"C:\Users\faqeerrehman\MSU\Research\CancerPrediction\ScientificSWTesting\Pilot1\P1B2Tests\Results\PerClassOutputs\20Epochs\ExcelProcesser\ProcessedResults_Clem\Origional\Origional_20.xls", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);
//                xlWriteWorkBook.SaveAs(writeFileLocation, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, misValue, misValue, misValue, misValue, misValue);
//                xlWriteWorkBook.Close(true, misValue, misValue);
//                xlReadWorkBook.Close(true, null, null);
//                xlApp.Quit();

//                Marshal.ReleaseComObject(xlReadWorkSheet);
//                Marshal.ReleaseComObject(xlReadWorkBook);
//                Marshal.ReleaseComObject(xlWriteWorkSheet);
//                Marshal.ReleaseComObject(xlWriteWorkBook);
//                Marshal.ReleaseComObject(xlApp);
//            }
//            MessageBox.Show("Process Completed Successfully.");


//        }
//    }
//}
