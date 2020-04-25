using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NumberTextBox.Text == string.Empty)
                return;

            if(this.RadioButton_REST.IsChecked == true)
            {
                string serverPath = ConfigurationManager.AppSettings["WCF_SERVER"].ToString();
                string url = serverPath + "/ConvertNumberToWord/" + this.NumberTextBox.Text + "?format=json";
                string word = WcfRequest.SendWcfRequest(url);
                this.CurrencyTextBox.Text = word;
            }

            if (this.RadioButton_ServiceRef.IsChecked == true)
            {
                ServiceReference.WcfServerClient objWcfServer = new ServiceReference.WcfServerClient();
                string word = objWcfServer.ConvertNumberToWord(this.NumberTextBox.Text);
                this.CurrencyTextBox.Text = word;
            }
        }
    }
}
