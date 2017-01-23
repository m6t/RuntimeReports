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
    /// Interaction logic for EnumEditor.xaml
    /// </summary>
    [ReportItemPropertyEditor(CanEdit = typeof(Enum))]
    public partial class EnumEditor : UserControl
    {
        public object ReportItem;
        public string propName;
        ReportDesignerHubControl hub;
        public EnumEditor(object item, string propertyName, ReportDesignerHubControl hub)
        {
            this.hub = hub;

            InitializeComponent();
            hub.Updated += Hub_Updated;
            ReportItem = item;
            propName = propertyName;
            enumtype = item.GetType().Property(propertyName).GetGetMethod().ReturnType;
            string[] vals = Enum.GetNames(enumtype);
            foreach (string o in vals)
            {
                select.Items.Add(o);
            }
            string val = item.GetType().Property(propertyName).GetValue(item).ToString();
            select.SelectedItem = val;
            name.Content = propName;
        }

        private void Hub_Updated()
        {
            string val = ReportItem.GetType().Property(propName).GetValue(ReportItem).ToString();
            select.SelectedItem = val;
        }

        Type enumtype;

        private void select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReportItem.GetType().Property(propName).SetValue(ReportItem, Enum.Parse(enumtype, (string)select.SelectedItem));
            hub.InvokeUpdated();
        }
    }
}
