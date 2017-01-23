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

namespace M6T.Data.Reporting.RuntimeReports.PropertyEditors
{
    /// <summary>
    /// Interaction logic for ItemDeleter.xaml
    /// </summary>
    public partial class ItemDeleter : UserControl
    {
        public ReportItems.ReportItemBase ReportItem;
        ReportDesignerHubControl hub;
        public ItemDeleter(ReportItems.ReportItemBase item, ReportDesignerHubControl hub)
        {
            this.hub = hub;

            InitializeComponent();
            ReportItem = item;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            hub.report.Items.Remove(ReportItem);
            hub.InvokeUpdated();
        }
    }
}
