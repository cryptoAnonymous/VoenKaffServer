using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoenKaffServer
{
    public class ResultsSaver
    {
        public Boolean testsSaved = true;
        public Boolean studySaved = true;
        DataGridView _testsTable;
        DataGridView _studyTable;

        WordSaver ws = new WordSaver();
        SaveFileDialog sfd = new SaveFileDialog();
        

        public ResultsSaver(DataGridView testsTable, DataGridView studyTable)
        {
            _testsTable = testsTable;
            _studyTable = studyTable;
            sfd.Filter = "Word Documents (*.docx)|*.docx";
        }

        public void saveTests()
        {
            
            sfd.FileName = "Тестирование " + DateTime.Today.ToLongDateString() + ".docx";
            if (sfd.ShowDialog() == DialogResult.OK) ws.Export_Data_To_Word(_testsTable, sfd.FileName);
        }
        public void saveStudy()
        {
            sfd.FileName = "Обучение " + DateTime.Today.ToLongDateString() + ".docx";
            if (sfd.ShowDialog() == DialogResult.OK) ws.Export_Data_To_Word(_studyTable, sfd.FileName);
        }

    }
}
