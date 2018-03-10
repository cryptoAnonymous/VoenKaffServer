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
        private PasswordResetSettings passwordReseter;

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
                switch (resultObj.ResultType)
                {
                    case "Экзамен":
                        {
                            GridResultTest.Rows.Add(
                            resultObj.Course,
                            resultObj.TestName,
                            resultObj.Platoon,
                            resultObj.StudentName,
                            resultObj.Mark,
                            resultObj.Timestamp);
                            resultsSaver.testsSaved = false;
                            break;
                        }
                    case "Тренировка":
                        {
                            GridResultStudy.Rows.Add(
                            resultObj.Course,
                            resultObj.TestName,
                            resultObj.Platoon,
                            resultObj.StudentName,
                            resultObj.Mark,
                            resultObj.Timestamp);
                            resultsSaver.studySaved = false;
                            break;
                        }
                }              
                UpdateResults(
                    resultObj.Course,
                    resultObj.TestName,
                    resultObj.Platoon,
                    resultObj.StudentName,
                    resultObj.Mark,
                    resultObj.Timestamp,
                    resultObj.ResultType);
            }
        }

        private void UpdateResults(params object[] newRow)
        {
            ResultsData.Get().Add(new Result
            {
                ResultType = newRow[6] as string,
                Mark = newRow[4] as string,
                Platoon = newRow[2] as string,
                StudentName = newRow[3] as string,
                TestName = newRow[1] as string,
                Timestamp = (DateTime)newRow[5],
                Course = newRow[0] as string
            });
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
            if (await TaskEx.Run(() => goSaveTests()))
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
            if (await TaskEx.Run(() => goSaveStudy()))
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
            await TaskEx.Delay(1000);
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
                Visible = false;
                var formLoading = new FormPicWhileDocsAreSaving {Visible = true};
                await goSaveAll();
                formLoading.Visible = false;
                Environment.Exit(0);
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
            if (await TaskEx.Run(() => goSaveAll2()))
            {
                formLoading.Visible = false;
            }

            this.Visible = true;



        }

        private void сменитьПарольToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (passwordReseter is null || passwordReseter.IsDisposed)
            {
                passwordReseter=new PasswordResetSettings();
            }
            passwordReseter.Visible = true;
        }
    }
}
