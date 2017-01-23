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
    /// Interaction logic for ReportDesignerToolBar.xaml
    /// </summary>
    public partial class ReportDesignerToolBar : UserControl
    {
        public ReportDesignerToolBar()
        {
            InitializeComponent();
            RefreshItems();
        }
        public void RefreshItems()
        {
            if (items.Items.Count == ReportElementPool.ReportItemTypes.Count)
                return;
            items.Items.Clear();
            foreach (var item in ReportElementPool.ReportItemTypes)
            {
                items.Items.Add(item.Name);
            }
        }
        M6TReport Report = null;
        ReportDesignerHubControl Hub;
        public void SetReport(M6TReport report, ReportDesignerHubControl hub)
        {
            Hub = hub;
            hub.Updated += Hub_Updated;
            Report = report;
            RefreshItems();
        }

        private void Hub_Updated()
        {
            RefreshItems();
        }

        private void items_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddItem();
        }
        public void AddItem()
        {
            if (items.SelectedIndex != -1)
            {
                Type item = ReportElementPool.ReportItemTypes[items.SelectedIndex];
                var constructors = item.GetConstructors();
                ReportItems.ReportItemBase ri = (ReportItems.ReportItemBase)Activator.CreateInstance(item);
                ri.Isim = items.SelectedItem + "" + last;
                last++;
                Report.Items.Add(ri);
                Report.UnpackReferences();
                Hub.InvokeUpdated();
            }
        }
        int last = 0;
        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
        }
    }
}
