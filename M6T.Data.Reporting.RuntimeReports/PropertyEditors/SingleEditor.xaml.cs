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
    /// Interaction logic for SingleEditor.xaml
    /// </summary>
    [ReportItemPropertyEditor(CanEdit = typeof(Single))]
    public partial class SingleEditor : UserControl
    {
        public object ReportItem;
        public string propName;
        ReportDesignerHubControl hub;
        public SingleEditor(object item, string propertyName, ReportDesignerHubControl hub)
        {
            this.hub = hub;
            hub.Updated += Hub_Updated;
            InitializeComponent();
            ReportItem = item;
            propName = propertyName;
            value.Text = Convert.ToString(item.GetType().Property(propertyName).GetValue(item));
            name.Content = propertyName;
        }

        private void Hub_Updated()
        {
            value.Text = Convert.ToString(ReportItem.GetType().Property(propName).GetValue(ReportItem));
        }

        private void value_LostFocus(object sender, RoutedEventArgs e)
        {
            Changed();
        }

        private void Changed()
        {
            float val = 0;
            if (float.TryParse(value.Text.Replace(".", ","), out val))
            {
                ReportItem.GetType().Property(propName).SetValue(ReportItem, val);
            }
            else
            {
                value.Text = Convert.ToString(ReportItem.GetType().Property(propName).GetValue(ReportItem));
            }
            hub.InvokeUpdated();
        }

        private void value_LostFocus(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Changed();
        }
    }
}
