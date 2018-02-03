using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoenKaffServer
{
    public class ResultsSaver
    {
        private readonly DynamicParams _parameters;

        public Boolean testsSaved = true;
        public Boolean studySaved = true;
        public DataGridView _testsTable;
        public DataGridView _studyTable;

        WordSaver ws = new WordSaver();
        SaveFileDialog sfd = new SaveFileDialog();

        Dictionary<string, List<DataGridViewRow>> courseAndRows;
        List<DataGridViewColumn> listColumns;
        Dictionary<string, List<DataGridViewRow>> docNameAndRows;

        public string _typeRes;

        public ResultsSaver(DataGridView testsTable, DataGridView studyTable)
        {
            _testsTable = testsTable;
            _studyTable = studyTable;
            sfd.Filter = "Word Documents (*.doc)|*.doc";

            _parameters = new DynamicParams();
        }

        public void saveResults(DataGridView dataGV)
        {
            courseAndRows = new Dictionary<string, List<DataGridViewRow>> { };
            listColumns = new List<DataGridViewColumn> { };
            docNameAndRows = new Dictionary<string, List<DataGridViewRow>> { };
            initCourseAndRows(dataGV);

            foreach (KeyValuePair<string, List<DataGridViewRow>> keyValue in courseAndRows)
            {
                docNameAndRows.Clear();
                string courseDir = _parameters.Get().ResultsPath  + @"\" + _typeRes + @"\" + keyValue.Key;
                if (!Directory.Exists(courseDir))
                {
                    Directory.CreateDirectory(courseDir);
                }
                
                DataGridView tempTable = new DataGridView();
                //Заголовки
                tempTable.Columns.Clear();
                
                foreach (DataGridViewColumn col in listColumns)
                {
                    var column = new DataGridViewColumn();
                    column.HeaderText = col.HeaderText;
                    column.Name = col.Name;
                    column.CellTemplate = new DataGridViewTextBoxCell();
                    tempTable.Columns.Add(column);
                }
                

                //Создание мапы <взвод - название теста> в конкретном предмете
                foreach (DataGridViewRow row in keyValue.Value)
                {
                    string name = row.Cells[2].Value.ToString() + "взвод. " + row.Cells[1].Value.ToString();
                    if (!docNameAndRows.ContainsKey(name))
                    {
                        docNameAndRows.Add(name, new List<DataGridViewRow> { });
                    }


                    DataGridViewRow rowForClone = new DataGridViewRow();
                    rowForClone = (DataGridViewRow)row.Clone();

                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        rowForClone.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }

                    docNameAndRows[name].Add(rowForClone);
                }

               
                
                
                DataTable dt = new DataTable();
                foreach (KeyValuePair<string, List<DataGridViewRow>> keyValueDocAndRows in docNameAndRows)
                {
                    tempTable.Rows.Clear();
                    foreach (DataGridViewRow rowTemp in keyValueDocAndRows.Value)
                    {
                        DataGridViewRow rowForInsert = new DataGridViewRow();
                        rowForInsert = (DataGridViewRow)rowTemp.Clone();

                        int intColIndex = 0;
                        foreach (DataGridViewCell cell in rowTemp.Cells)
                        {
                            rowForInsert.Cells[intColIndex].Value = cell.Value;
                            intColIndex++;
                        }

                        tempTable.Rows.Add(rowForInsert);
                    }

                    ws.Export_Data_To_Word(tempTable, courseDir + @"\" + keyValueDocAndRows.Key + ".docx");
                        

                }

                
            }
            listColumns.Clear();

            

        }


        

        private void initCourseAndRows(DataGridView dataGV)
        {

            foreach (DataGridViewColumn col in dataGV.Columns)
            {
                listColumns.Add(col);
            }
            
            
            

            //Коллекция предметов и результатов предметов
            if (dataGV.Rows.Count != 0)
            {
                string course;
                DataGridViewRow row = new DataGridViewRow();


                for (int i = 0; i < dataGV.Rows.Count - 1; i++)
                {
                    course = dataGV.Rows[i].Cells[0].Value.ToString();



                    row = (DataGridViewRow)dataGV.Rows[i].Clone();
                    int intColIndex = 0;
                    foreach (DataGridViewCell cell in dataGV.Rows[i].Cells)
                    {
                        row.Cells[intColIndex].Value = cell.Value;
                        intColIndex++;
                    }

                    if (!courseAndRows.ContainsKey(course))
                    {
                        courseAndRows.Add(course, new List<DataGridViewRow> { });
                    }

                    courseAndRows[course].Add(row);
                }


                


                
            }
        }

    }
}
