using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using VoenKaffServer.Wrappers;

namespace VoenKaffServer
{
    public partial class FormStart : Form
    {
        delegate void StringArgReturningVoidDelegate(string text);
        private FormSettings _settings;

        FormLogin _formLogin;

        public FormStart(FormLogin formLogin)
        {
            InitializeComponent();
            _formLogin = formLogin;

            var listener = new Listener(this);
            listener.Start();
            
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_settings == null || _settings.IsDisposed)
            {
                _settings = new FormSettings();
            }
            _settings.Visible = true;
        }

        public void AddResult(string result)
        {
            if (GridResultTest.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = AddResult;
                Invoke(d, result);
            }
            else
            {
                var resultObj = JsonConvert.DeserializeObject<Result>(result);
                if (resultObj.ResultType == "Экзамен")
                {
                    GridResultTest.Rows.Add(
                    resultObj.Course,
                    resultObj.TestName,
                    resultObj.Platoon,
                    resultObj.StudentName,
                    resultObj.Mark,
                    resultObj.Timestamp
                    );

                }
                
                if (resultObj.ResultType == "Тренировка")
                {
                    GridResultStudy.Rows.Add(
                    resultObj.Course,
                    resultObj.TestName,
                    resultObj.Platoon,
                    resultObj.StudentName,
                    resultObj.Mark,
                    resultObj.Timestamp
                    );
                }
            }
        }

        private void GridResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void выйтиИзАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            _formLogin.Visible = true;
        }

        private void тестированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WordSaver ws = new WordSaver();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Word Documents (*.docx)|*.docx";
            sfd.FileName = "Тестирование " + new DateTime().ToLongDateString() + ".docx";
            if (sfd.ShowDialog() == DialogResult.OK) ws.Export_Data_To_Word(GridResultTest, sfd.FileName);
            
            //WordSaver.createDoc(this, 0);
        }

        private void обучениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WordSaver ws = new WordSaver();
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Word Documents (*.docx)|*.docx";
            sfd.FileName = "Обучение " + new DateTime().ToLongDateString() + ".docx";
            if (sfd.ShowDialog() == DialogResult.OK) ws.Export_Data_To_Word(GridResultStudy, sfd.FileName);
        }

        private void тестированиеИОбучениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //WordSaver.createDoc(this, 2);
        }

        private void FormStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
