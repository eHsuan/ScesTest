using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SECS;
using System.Net;
using SECS.Sml;

namespace SecsTest
{
    public partial class Form1 : Form
    {
        SecsGem _secsGem;
        readonly ISecsGemLogger _logger;
        readonly BindingList<PrimaryMessageWrapper> recvBuffer = new BindingList<PrimaryMessageWrapper>();
        public Form1()
        {
            InitializeComponent();

            radioActiveMode.DataBindings.Add("Enabled", buttonConnect, "Enabled");
            radioPassiveMode.DataBindings.Add("Enabled", buttonConnect, "Enabled");
            txtAddress.DataBindings.Add("Enabled", buttonConnect, "Enabled");
            numPort.DataBindings.Add("Enabled", buttonConnect, "Enabled");
            numDeviceId.DataBindings.Add("Enabled", buttonConnect, "Enabled");
            numBufferSize.DataBindings.Add("Enabled", buttonConnect, "Enabled");
            recvMessageBindingSource.DataSource = recvBuffer;
            Application.ThreadException += (sender, e) => MessageBox.Show(e.Exception.ToString());
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => MessageBox.Show(e.ExceptionObject.ToString());
            _logger = new SecsLogger(this);
        }
        private void Test()
        {
            
            if (!radioActiveMode.Checked && !radioPassiveMode.Checked)
            {
                MessageBox.Show("Please Choice Connect Mode!!");
                return;
            }
            _secsGem?.Dispose();
            _secsGem = new SecsGem(
                radioActiveMode.Checked,
                IPAddress.Parse(txtAddress.Text),
                (int)numPort.Value,
                (int)numBufferSize.Value)
            { Logger = _logger, DeviceId = (ushort)numDeviceId.Value };

            SetTimerSetting();

            _secsGem.ConnectionChanged += delegate
            {
                this.Invoke((MethodInvoker)delegate
                {
                    lbStatus.Text = _secsGem.State.ToString();
                });
            };

            _secsGem.PrimaryMessageReceived += PrimaryMessageReceived;

            buttonConnect.Enabled = false;
            _secsGem.Start();
            buttonDisconnect.Enabled = true;
        }
        private void PrimaryMessageReceived(object sender, PrimaryMessageWrapper e)
        {
            this.Invoke(new MethodInvoker(() => recvBuffer.Add(e)));
        }

        class SecsLogger : ISecsGemLogger
        {
            readonly Form1 _form;
            internal SecsLogger(Form1 form)
            {
                _form = form;
            }
            public void MessageIn(SecsMessage msg, int systembyte)
            {
                string temp = msg.ToSml();
                _form.Invoke((MethodInvoker)delegate
                {
                    _form.richTextBox2.SelectionColor = Color.Gold;
                    _form.richTextBox2.AppendText($"<-- [0x{systembyte:X8}] {temp}\n");
                });

            }

            public void MessageOut(SecsMessage msg, int systembyte)
            {
                string temp = msg.ToSml();
                _form.Invoke((MethodInvoker)delegate {
                    _form.richTextBox2.SelectionColor = Color.Black;
                    _form.richTextBox2.AppendText($"--> [0x{systembyte:X8}] {temp}\n");
                });
            }

            public void Info(string msg)
            {
                _form.Invoke((MethodInvoker)delegate {
                    _form.richTextBox1.SelectionColor = Color.Blue;
                    _form.richTextBox1.AppendText($"{msg}\n");
                });
            }

            public void Warning(string msg)
            {
                _form.Invoke((MethodInvoker)delegate {
                    _form.richTextBox1.SelectionColor = Color.Green;
                    _form.richTextBox1.AppendText($"{msg}\n");
                });
            }

            public void Error(string msg, Exception ex = null)
            {
                _form.Invoke((MethodInvoker)delegate {
                    _form.richTextBox1.SelectionColor = Color.Red;
                    _form.richTextBox1.AppendText($"{msg}\n");
                    _form.richTextBox1.SelectionColor = Color.Gray;
                    _form.richTextBox1.AppendText($"{ex}\n");
                });
            }

            public void Debug(string msg)
            {
                _form.Invoke((MethodInvoker)delegate {
                    _form.richTextBox1.SelectionColor = Color.Yellow;
                    _form.richTextBox1.AppendText($"{msg}\n");
                });
            }
        }

        private void btnEnable_Click(object sender, EventArgs e)
        {
            Test();
        }

        private void btnDisable_Click(object sender, EventArgs e)
        {
            
            _secsGem?.Dispose();
            _secsGem = null;
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            lbStatus.Text = "Disable";
            recvBuffer.Clear();
            richTextBox2.Clear();
        }

        private async void buttonSendMessage_Click(object sender, EventArgs e)
        {
            if (_secsGem == null ||_secsGem.State != SECS.ConnectionState.Selected)
            {
                MessageBox.Show("SECS State Must be Selected!!");
                return;
            }
                
            //if (string.IsNullOrWhiteSpace(txtSendPrimary.Text))
            //{
            //    MessageBox.Show("Send Message SML is Null!!");
            //    return;
            //}
                

            try
            {
                //string test = "QueryLoadPortAccessMode:'S1F3' W    <L[3]    <U2[1] 18011>    <U2[1] 18012>    <U2[1] 18013>    >";
                //var reply = await _secsGem.SendAsync(test.ToSecsMessage());
                string str = txtSendPrimary.Text;
                var reply = await _secsGem.SendAsync(txtSendPrimary.Text.ToSecsMessage());
                //txtRecvSecondary.Text = reply.ToSml();
            }
            catch (SecsException ex)
            {
                richTextBox2.AppendText(ex.Message + "\r\n");
            }
        }

        private void buttonClearMessage_Click(object sender, EventArgs e)
        {
            txtSendPrimary.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void SetTimerSetting()
        {
            _secsGem.T3 = int.Parse(textBoxT3.Text) * 1000;
            _secsGem.T5 = int.Parse(textBoxT5.Text) * 1000;
            _secsGem.T6 = int.Parse(textBoxT6.Text) * 1000;
            _secsGem.T7 = int.Parse(textBoxT7.Text) * 1000;
            _secsGem.T8 = int.Parse(textBoxT8.Text) * 1000;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                S6F103 stream = new S6F103
                {
                    CEID = textBoxCEID.Text,
                    SUBCD = textBoxSUBCD.Text,
                    INLINEID = textBoxINLINEID.Text,
                    EQUIPMENTID = textBoxEQUIPMENTID.Text,
                    UNITID = textBoxUNITID.Text,
                    GLASSID = textBoxGLASSID.Text,
                    LOTID = textBoxLOTID.Text,
                    PRODUCTID = textBoxPRODUCTID.Text,
                    RECIPEID = textBoxRECIPEID.Text,
                    ROUTEID = textBoxROUTEID.Text,
                    OWNERID = textBoxOWNERID.Text,
                    OPERATIONID = textBoxOPERATIONID.Text,
                    CASSETTEID = textBoxCASSETTEID.Text,
                    OPERATOR = textBoxOPERATOR.Text,
                    CLMDATE = textBoxCLMDATE.Text,
                    CLMTIME = textBoxCLMTIME.Text,
                    DVNAME = textBoxDVNAME.Text,
                    DVTYPE = textBoxDVTYPE.Text,
                    DVVAL = textBoxDVVAL.Text
                };
                string str = new StreamGenerate().StreamFormatGenerate("S6F103", stream);
                var reply = await _secsGem.SendAsync(str.ToSecsMessage());
            }
            catch (SecsException ex)
            {
                richTextBox2.AppendText(ex.Message + "\r\n");
            }
        }
    }

}
