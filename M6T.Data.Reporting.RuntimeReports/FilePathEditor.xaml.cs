using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace M6T.Data.Reporting.RuntimeReports
{
    /// <summary>
    /// Interaction logic for FilePathEditor.xaml
    /// </summary>
    [ReportItemPropertyEditor(CanEdit = typeof(ReportFilePath))]
    public partial class FilePathEditor : UserControl
    {
        ReportItem item;
        string propName;
        ReportDesignerHubControl hub;
        public FilePathEditor(ReportItem item, string propName, ReportDesignerHubControl hub)
        {
            this.hub = hub;
            hub.Updated += Hub_Updated;
            InitializeComponent();
            this.item = item;
            this.propName = propName;
            filepath.Content = ((ReportFilePath)item.GetType().Property(propName).GetValue(item)).Path;
            name.Content = propName;
        }

        private void Hub_Updated()
        {
            filepath.Content = ((ReportFilePath)item.GetType().Property(propName).GetValue(item)).Path;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ((ReportFilePath)item.GetType().Property(propName).GetValue(item)).Path = ofd.FileName;
                hub.InvokeUpdated();
            }
        }
    }
}
