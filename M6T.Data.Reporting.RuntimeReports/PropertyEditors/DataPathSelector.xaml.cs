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
    public static class Extesnions
    {
        public static bool canCanConvertTo(this object input, Type type)
        {
            object result = null;
            try
            {
                result = Convert.ChangeType(input, type);
            }
            catch
            {
            }

            return result != null;
        }
    }
    /// <summary>
    /// Interaction logic for DataPathSelector.xaml
    /// </summary>
    public partial class DataPathSelector : UserControl
    {
        ReportDesignerHubControl hub;
        ReportItem ReportItem;
        public DataPathSelector(ReportItem ReportItem, M6TReport report, object sampleData, ReportDesignerHubControl hub)
        {
            this.hub = hub;
            InitializeComponent();
            hub.Updated += Hub_Updated;
            this.ReportItem = ReportItem;
            foreach (var prop in report.ReportDataType.Properties())
            {
                object propData = prop.GetValue(sampleData);
                foreach (Type accept in ReportItem.AcceptedTypes)
                {
                    if (propData.canCanConvertTo(accept))
                    {
                        path.Items.Add(prop.Name);
                    }
                }
                //if (ReportItem.AcceptedTypes.Contains(typeof(object)))
                //{ path.Items.Add(prop.Name); }
                //else if (ReportItem.AcceptedTypes.Contains(prop.PropertyType))
                //{

                //}
            }

            path.SelectedItem = (string)ReportItem.GetType().Property("DataPath").GetValue(ReportItem);

        }

        private void Hub_Updated()
        {
            path.SelectedItem = (string)ReportItem.GetType().Property("DataPath").GetValue(ReportItem);
        }

        private void path_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReportItem.GetType().Property("DataPath").SetValue(ReportItem, path.SelectedItem);
            hub.InvokeUpdated();
        }
    }
}
