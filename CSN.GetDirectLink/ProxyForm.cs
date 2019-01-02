using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSN
{
    public partial class ProxyForm : Form
    {
        public ProxyForm()
        {
            InitializeComponent();
            StringBuilder proxies = new StringBuilder();
            proxies.AppendLine("123.30.172.60:3128");
            proxies.AppendLine("117.2.155.139:9090");
            proxies.AppendLine("101.99.23.136:3128");
            proxies.AppendLine("42.115.26.154:8080");
            proxies.AppendLine("103.15.51.160:8080");
            proxies.AppendLine("27.68.131.218:8080");
            proxies.AppendLine("118.70.185.14:8080");
            proxies.AppendLine("14.188.166.19:8080");
            proxies.AppendLine("123.31.47.8:3128");
            proxies.AppendLine("116.105.225.104:9090");
            proxies.AppendLine("117.4.247.218:3128");
            proxies.AppendLine("113.161.90.156:3128");

            txtProxies.Text = proxies.ToString();
        }

        public List<String> GetProxiesList()
        {
            List<String> proxies = null;
            if (chkUseProxy.Checked)
            {
                proxies = new List<string>(Regex.Split(txtProxies.Text, Environment.NewLine));
            }
            return proxies;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ProxyForm_Load(object sender, EventArgs e)
        {
        }
    }
}
