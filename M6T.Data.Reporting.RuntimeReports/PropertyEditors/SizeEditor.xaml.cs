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

namespace M6T.Data.Reporting.RuntimeReports
{
    /// <summary>
    /// Interaction logic for RegionEdit.xaml
    /// </summary>
    [ReportItemPropertyEditor(CanEdit = typeof(Size))]
    public partial class SizeEditor : UserControl
    {
        object item;
        public string PropertyName;
        ReportDesignerHubControl hub;
        public SizeEditor(object item, string PropertyName, ReportDesignerHubControl hub)
        {
            this.hub = hub;
            hub.Updated += Hub_Updated;
            this.item = item;
            this.PropertyName = PropertyName;
            InitializeComponent();
            Size itemRegion = (Size)item.GetType().Property(PropertyName).GetValue(item);
            PosX.Text = itemRegion.Width + "";
            PosY.Text = itemRegion.Height + "";
        }

        private void Hub_Updated()
        {
            Size itemRegion = (Size)item.GetType().Property(PropertyName).GetValue(item);
            PosX.Text = itemRegion.Width + "";
            PosY.Text = itemRegion.Height + "";
        }

        private void PosX_LostFocus(object sender, RoutedEventArgs e)
        {
            Changed();
        }

        private void Changed()
        {
            Size itemRegion = (Size)item.GetType().Property(PropertyName).GetValue(item);
            int newPosX;
            int newPosY;
            if (!int.TryParse(PosX.Text, out newPosX))
            {
                newPosX = itemRegion.Width.ToInt();
            }
            if (!int.TryParse(PosY.Text, out newPosY))
            {
                newPosY = itemRegion.Height.ToInt();
            }
            itemRegion.Width = newPosX;
            itemRegion.Height = newPosY;
            item.GetType().Property(PropertyName).SetValue(item, itemRegion);
            hub.InvokeUpdated();
        }

        private void PosY_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Changed();
        }
    }
}
