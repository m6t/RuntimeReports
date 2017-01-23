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
    /// Interaction logic for ColorEditor.xaml
    /// </summary>
    [ReportItemPropertyEditor(CanEdit = typeof(ReportColor))]
    public partial class ColorEditor : UserControl
    {
        public ColorEditor(object item, string propName, ReportDesignerHubControl hub)
        {
            this.hub = hub;
            it = item;
            clr = (ReportColor)item.GetType().Property(propName).GetValue(item);
            InitializeComponent();
            hub.Updated += Hub_Updated;
            isim.Content = propName;
            clrpcker.SelectedColor = clr;
            propname = propName;
        }

        private void Hub_Updated()
        {
            clr = (ReportColor)it.GetType().Property(propname).GetValue(it);
            clrpcker.SelectedColor = clr;
        }
        string propname;
        object it;
        ReportColor clr;
        ReportDesignerHubControl hub;

        private void clrpcker_Closed(object sender, RoutedEventArgs e)
        {
            clr = clrpcker.SelectedColor;
            it.GetType().Property(propname).SetValue(it, clr);
            hub.InvokeUpdated();
        }

        private void clrpcker_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // clr = clrpcker.SelectedColor;
        }
    }
}
