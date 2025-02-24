
namespace erWinBatchPopupDisable {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }


        erWinPdfFilePopupDisableClass m_erWinFileDis;
        string m_currentMessage;
        int m_percent;

        private void Form1_Load(object sender, EventArgs e) {
            checkBoxRecursive.Checked = true;
            labelMessage.Text = "To get started, browse for the root directory of your manual then click Go.";

            m_erWinFileDis = new erWinPdfFilePopupDisableClass();
            m_erWinFileDis.StatusUpdate += m_erWinFileDis_StatusUpdate;
        }

        private void btnBrowser_Click(object sender, EventArgs e) {

            //Let the user use the windows file browser to find their directory
            using (FolderBrowserDialog fbd = new FolderBrowserDialog()) {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    textBoxDir.Text = fbd.SelectedPath;
            }

        }

        private void m_erWinFileDis_StatusUpdate(object? sender, erWinPdfFilePopupDisableEvent e) {
            m_currentMessage = e.msg;
            m_percent = e.percent;


            if (labelMessage.InvokeRequired)
                labelMessage.Invoke(m_UpdateText);
            else
                m_UpdateText();

            if (progressBar1.InvokeRequired)
                progressBar1.Invoke(m_UpdateProgress);
            else
                m_UpdateProgress();
        }

        private void m_UpdateText() {
            labelMessage.Text = m_currentMessage;
        }

        private void m_UpdateProgress() {
            progressBar1.Value = m_percent;
        }

        private void btnGo_Click(object sender, EventArgs e) {
            if (m_erWinFileDis.fileToConvertCount <= 0)
                m_erWinFileDis.LoadDirectory(textBoxDir.Text, checkBoxRecursive.Checked);

            m_erWinFileDis.Start();
        }

        private void btnStop_Click(object sender, EventArgs e) {
            m_erWinFileDis.Stop();
        }
    }
}