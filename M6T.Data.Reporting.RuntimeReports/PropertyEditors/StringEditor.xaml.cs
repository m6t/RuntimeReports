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
    /// Interaction logic for StringEditor.xaml
    /// </summary>
    [ReportItemPropertyEditor(CanEdit = typeof(string))]
    public partial class StringEditor : UserControl
    {
        public object ReportItem;
        public string propName;
        ReportDesignerHubControl hub;
        public StringEditor(object item, string propertyName, ReportDesignerHubControl hub)
        {
            this.hub = hub;
            hub.Updated += Hub_Updated;
            InitializeComponent();
            ReportItem = item;
            propName = propertyName;
            value.Text = (string)item.GetType().Property(propertyName).GetValue(item);
            name.Content = propertyName;
        }

        private void Hub_Updated()
        {
            value.Text = (string)ReportItem.GetType().Property(propName).GetValue(ReportItem);
        }

        private void value_LostFocus(object sender, RoutedEventArgs e)
        {
            Changed();
        }

        private void Changed()
        {
            ReportItem.GetType().Property(propName).SetValue(ReportItem, value.Text);
            hub.InvokeUpdated();
        }

        private void value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Changed();
        }
    }
}
