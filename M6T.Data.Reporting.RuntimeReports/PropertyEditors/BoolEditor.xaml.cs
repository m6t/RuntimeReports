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
    /// Interaction logic for BoolEditor.xaml
    /// </summary>
    [ReportItemPropertyEditor(CanEdit = typeof(bool))]
    public partial class BoolEditor : UserControl
    {
        object item;
        string propName;
        ReportDesignerHubControl hub;
        public BoolEditor(object item, string propName, ReportDesignerHubControl hub)
        {
            this.hub = hub;
            hub.Updated += Hub_Updated;
            InitializeComponent();
            this.item = item;
            this.propName = propName;
            cc.IsChecked = (bool)item.GetType().Property(propName).GetValue(item);
            name.Content = propName;
        }

        private void Hub_Updated()
        {
            cc.IsChecked = (bool)item.GetType().Property(propName).GetValue(item);
        }

        private void cc_Unchecked(object sender, RoutedEventArgs e)
        {
            item.GetType().Property(propName).SetValue(item, cc.IsChecked ?? false);
            hub.InvokeUpdated();
        }

        private void cc_Checked(object sender, RoutedEventArgs e)
        {
            item.GetType().Property(propName).SetValue(item, cc.IsChecked ?? true);
            hub.InvokeUpdated();
        }
    }
}
