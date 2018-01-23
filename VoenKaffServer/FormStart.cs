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
            if (GridResult.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = AddResult;
                Invoke(d, result);
            }
            else
            {
                var resultObj = JsonConvert.DeserializeObject<Result>(result);
                GridResult.Rows.Add(
                    resultObj.TestName,
                    resultObj.ResultType,
                    resultObj.Platoon, 
                    resultObj.StudentName, 
                    resultObj.Mark, 
                    resultObj.Timestamp             
                );
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
    }
}
