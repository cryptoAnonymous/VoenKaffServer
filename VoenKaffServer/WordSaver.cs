using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace VoenKaffServer
{
    class WordSaver
    {


        public void Export_Data_To_Word(DataGridView DGV, string filename)
        {
            if (DGV.Rows.Count != 0)
            {
                int RowCount = DGV.Rows.Count;
                int ColumnCount = DGV.Columns.Count;
                Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                //add rows
                int r = 0;
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                        DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                    } //end row loop
                } //end column loop

                Word.Document oDoc = new Word.Document();
                oDoc.Application.Visible = true;

                //page orintation
                oDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                dynamic oRange = oDoc.Content.Application.Selection.Range;
                string oTemp = "";
                for (r = 0; r <= RowCount - 1; r++)
                {
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                        oTemp = oTemp + DataArray[r, c] + "\t";

                    }
                }

                //table format
                oRange.Text = oTemp;

                object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                object ApplyBorders = true;
                object AutoFit = true;
                object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitContent;

                oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                      Type.Missing, Type.Missing, ref ApplyBorders,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, Type.Missing, Type.Missing,
                                      Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                oRange.Select();

                oDoc.Application.Selection.Tables[1].Select();
                oDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                oDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.InsertRowsAbove(1);
                oDoc.Application.Selection.Tables[1].Rows[1].Select();

                //header row style
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 1;
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Tahoma";
                oDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 14;

                //add header row manually
                for (int c = 0; c <= ColumnCount - 1; c++)
                {
                    oDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                }

                //table style 
                //oDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                oDoc.Application.Selection.Tables[1].Rows[1].Select();
                oDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                //header text
                foreach (Word.Section section in oDoc.Application.ActiveDocument.Sections)
                {
                    Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    headerRange.Text = "Результаты";
                    headerRange.Font.Size = 16;
                    headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                //save the file
                oDoc.SaveAs2(filename);

                //NASSIM LOUCHANI
            }
        }

























        //public static void createDoc(FormStart formStart, int mode)
        //{
        //    Word._Application application;
        //    Word._Document document;
        //    Object missingObj = System.Reflection.Missing.Value;
        //    Object trueObj = true;
        //    Object falseObj = false;

        //    application = new Word.Application();
        //    try
        //    {
        //        //document = application.Documents.Add(ref templatePathObj, ref missingObj, ref missingObj, ref missingObj); 
        //        document = application.Documents.Open(Environment.CurrentDirectory + @"\Word\WordTest.docx");  //You need to open Word Documents, not add them, as per 
        //    }
        //    catch (Exception error)
        //    {
        //        //document.Close(ref falseObj, ref missingObj, ref missingObj);
        //        application.Quit(ref missingObj, ref missingObj, ref missingObj);
        //        document = null;
        //        application = null;
        //        throw error;
        //    }
























        //DateTime date = new DateTime();
        //Object _missingObj = System.Reflection.Missing.Value;
        //var dataTable = new DataTable();

        //DataGridView dataTableTest = (DataGridView)formStart.Controls.Find("GridResultTest", true)[0];
        //DataGridView dataTableStudy = (DataGridView)formStart.Controls.Find("GridResultStudy", true)[0];
        //// создаем и заполняем таблицу

        //Word._Application wordApplication = new Word.Application();
        //Word._Document wordDocument = null;
        //wordApplication.Visible = true;


        //File.Copy(Environment.CurrentDirectory + @"\Word\WordTest.docx", Environment.CurrentDirectory + @"\Word\Результаты " + date.ToLongDateString() + ".docx");
        //var templatePathObj = Environment.CurrentDirectory + @"\Word\Результаты " + date.ToLongDateString() + ".docx"; 

        //try
        //{
        //    wordDocument = wordApplication.Documents.Add(templatePathObj);
        //}
        //catch (Exception exception)
        //{
        //    if (wordDocument != null)
        //    {
        //        wordDocument.Close(false);
        //        wordDocument = null;
        //    }
        //    wordApplication.Quit();
        //    wordApplication = null;
        //    throw;
        //}

        //if (mode == 0)
        //{
        //    wordApplication.Selection.Find.Execute("tableTest");
        //    Word.Range wordRange = wordApplication.Selection.Range;

        //    var wordTable = wordDocument.Tables.Add(wordRange,
        //        dataTableTest.Rows.Count, dataTableTest.Columns.Count, ref _missingObj, ref _missingObj);
        //    wordTable.Rows.Add(ref _missingObj);


        //    for (var j = 0; j < dataTableTest.Rows.Count; j++)
        //    {
        //        for (var k = 0; k < dataTableTest.Columns.Count; k++)
        //        {
        //            wordTable.Cell(j + 1, k + 1).Range.Text = dataTableTest[j, k].Value.ToString();
        //        }
        //    }
        //}

        //if (mode == 1)
        //{
        //    wordApplication.Selection.Find.Execute("tableStudy");
        //    Word.Range wordRange = wordApplication.Selection.Range;

        //    var wordTable = wordDocument.Tables.Add(wordRange,
        //        dataTableStudy.Rows.Count, dataTableStudy.Columns.Count);

        //    for (var j = 0; j < dataTableStudy.Rows.Count; j++)
        //    {
        //        for (var k = 0; k < dataTableStudy.Columns.Count; k++)
        //        {
        //            wordTable.Cell(j + 1, k + 1).Range.Text = dataTableStudy[j, k].Value.ToString();
        //        }
        //    }
        //}

        //if (mode == 2)
        //{

        //    wordApplication.Selection.Find.Execute("tableTest");
        //    Word.Range wordRange1 = wordApplication.Selection.Range;

        //    var wordTable1 = wordDocument.Tables.Add(wordRange1,
        //        dataTableTest.Rows.Count, dataTableTest.Columns.Count);

        //    for (var j = 0; j < dataTableTest.Rows.Count; j++)
        //    {
        //        for (var k = 0; k < dataTableTest.Columns.Count; k++)
        //        {
        //            wordTable1.Cell(j + 1, k + 1).Range.Text = dataTableTest[j, k].Value.ToString();
        //        }
        //    }

        //    wordApplication.Selection.Find.Execute("tableStudy");
        //    Word.Range wordRange2 = wordApplication.Selection.Range;

        //    var wordTable2 = wordDocument.Tables.Add(wordRange2,
        //        dataTableStudy.Rows.Count, dataTableStudy.Columns.Count);

        //    for (var j = 0; j < dataTableStudy.Rows.Count; j++)
        //    {
        //        for (var k = 0; k < dataTableStudy.Columns.Count; k++)
        //        {
        //            wordTable2.Cell(j + 1, k + 1).Range.Text = dataTableStudy[j, k].Value.ToString();
        //        }
        //    }
        //}





        //}
    }
}
