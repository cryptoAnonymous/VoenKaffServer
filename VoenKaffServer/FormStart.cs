using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using VoenKaffServer.Properties;
using VoenKaffServer.Wrappers;

namespace VoenKaffServer
{
    public partial class FormStart : Form
    {
        delegate void StringArgReturningVoidDelegate(string text);
        private FormSettings _settings;

        FormLogin _formLogin;
        public ResultsSaver resultsSaver;

        public FormStart(FormLogin formLogin)
        {
            InitializeComponent();
            _formLogin = formLogin;
            ResultInitializer();
            var listener = new Listener(this);
            listener.Start();


            resultsSaver = new ResultsSaver(GridResultTest, GridResultStudy);

            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        private void Initialize()
        {
            var json = File.ReadAllText(Resources.ResultsData);
            AddResult(json);
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
                    resultsSaver.testsSaved = false;
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
                    resultsSaver.studySaved = false;
                }
                UpdateResults();
            }
        }

        private void UpdateResults()
        {
            //var results = new List<Result>();
            foreach (DataGridViewRow row in GridResultStudy.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    ResultsData.Get().Add(new Result
                    {
                        ResultType = "Тренировка",
                        Mark = row.Cells[4].Value.ToString(),
                        Platoon = row.Cells[2].Value.ToString(),
                        StudentName = row.Cells[3].Value.ToString(),
                        TestName = row.Cells[1].Value.ToString(),
                        Timestamp = DateTime.Parse(row.Cells[5].Value.ToString()),
                        Course = row.Cells[0].Value.ToString()
                    });
                }
            }

            foreach (DataGridViewRow row in GridResultTest.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    ResultsData.Get().Add(new Result
                    {
                        ResultType = "Экзамен",
                        Mark = row.Cells[4].Value.ToString(),
                        Platoon = row.Cells[2].Value.ToString(),
                        StudentName = row.Cells[3].Value.ToString(),
                        TestName = row.Cells[1].Value.ToString(),
                        Timestamp = DateTime.Parse(row.Cells[5].Value.ToString()),
                        Course = row.Cells[0].Value.ToString()
                    });
                }
            }

            File.WriteAllText(Resources.ResultsData, JsonConvert.SerializeObject(ResultsData.Get()));
        }

        private void GridResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ResultInitializer()
        {
            if (File.Exists(Resources.ResultsData) && File.ReadAllText(Resources.ResultsData)!="")
            {
                var json = File.ReadAllText(Resources.ResultsData);
                var results = JsonConvert.DeserializeObject<List<Result>>(json);
                foreach (var result in results)
                {
                    ResultsData.Get().Add(result);
                    
                }
            }
            //UpdateResults();
        }

        private void выйтиИзАккаунтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            _formLogin.Visible = true;
        }

        private async void тестированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpdateResults();
            this.Visible = false;

            FormPicWhileDocsAreSaving formLoading = new FormPicWhileDocsAreSaving();
            formLoading.Visible = true;
            if (await Task.Run(() => goSaveTests()))
            {
                formLoading.Visible = false;
            }

            this.Visible = true;
        }

        private async void обучениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpdateResults();
            this.Visible = false;

            FormPicWhileDocsAreSaving formLoading = new FormPicWhileDocsAreSaving();
            formLoading.Visible = true;
            if (await Task.Run(() => goSaveStudy()))
            {
                formLoading.Visible = false;
            }

            this.Visible = true;
        }

        public Boolean goSaveTests()
        {
            resultsSaver.saveResultsTest();
            return true;
        }
        public Boolean goSaveStudy()
        {

            resultsSaver.saveResultsStudy();
            return true;
        }
        public async Task goSaveAll()
        {
            resultsSaver.saveAll();
            await Task.Delay(1000);
            //return true;
        }
        public Boolean goSaveAll2()
        {
            resultsSaver.saveAll();
            return true;
        }




        private  void FormStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                saveResByClosing();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        public async void saveResByClosing()
        {
            //UpdateResults();
            if (!resultsSaver.testsSaved || !resultsSaver.studySaved)
            {

                this.Visible = false;
                FormPicWhileDocsAreSaving formLoading = new FormPicWhileDocsAreSaving();

                formLoading.Visible = true;

                await goSaveAll();
                formLoading.Visible = false;
                Environment.Exit(0);
                //if (await Task.Run(() => goSaveAll()))
                //{
                //    formLoading.Visible = false;
                //    //this.Visible = true;
                //    //Environment.Exit(0);
                //}
            }
            else
            {
                Environment.Exit(0);
            }
            

        }

        private async void скачатьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UpdateResults();

            this.Visible = false;

            FormPicWhileDocsAreSaving formLoading = new FormPicWhileDocsAreSaving();
            formLoading.Visible = true;
            if (await Task.Run(() => goSaveAll2()))
            {
                formLoading.Visible = false;
            }

            this.Visible = true;



        }
    }
}
