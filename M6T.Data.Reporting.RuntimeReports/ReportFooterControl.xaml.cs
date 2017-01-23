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
    /// Interaction logic for ReportFooterControl.xaml
    /// </summary>
    public partial class ReportFooterControl : UserControl
    {
        public ReportFooterControl()
        {
            InitializeComponent();
        }
        public M6TReport Report { get; set; }
        ReportDesignerHubControl Hub;
        public void SetReport(M6TReport report, ReportDesignerHubControl hub)
        {
            Hub = hub;
            hub.Updated += refresh;
            Report = report;
        }
        public bool isSelectedValid()
        {
            foreach (var item in Report.Items)
            {
                if (item.Selected)
                    return item.Isim + "(" + item.GetType().Name + ")" == (string)listBox.SelectedItem;
            }
            return true;
        }
        public void refresh()
        {
            if (listBox.Items.Count != Report.Items.Count && isSelectedValid())
            {
                listBox.Items.Clear();
                foreach (var item in Report.Items)
                {
                    listBox.Items.Add(item.Isim + "(" + item.GetType().Name + ")");
                }
                if (Report.GetSelectedItem() != null)
                {
                    var item = Report.GetSelectedItem();

                    listBox.SelectedItem = item.Isim + "(" + item.GetType().Name + ")";
                }
            }
        }
        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (listBox.SelectedIndex != -1)
            {
                foreach (var item in Report.Items)
                {
                    item.Selected = false;
                }
                Report.Items[listBox.SelectedIndex].Selected = true;
                Hub.InvokeUpdated();
            }
        }
    }
}
