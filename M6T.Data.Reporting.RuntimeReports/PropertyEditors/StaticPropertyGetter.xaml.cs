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
    /// Interaction logic for StaticPropertyGetter.xaml
    /// </summary>
    public partial class StaticPropertyGetter : UserControl
    {
        Type TFrom, TProperty;

        private void select_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var prop = TFrom.Property((string)select.SelectedItem).GetValue(null);
            if (prop.GetType() != TProperty)
            {
                return;
            }

            var staticPropGet = item.GetType().Property(PropertyName);
            Type elementType = staticPropGet.PropertyType.GetGenericArguments()[0];
            Type repositoryType = typeof(StaticPropertyGet<>).MakeGenericType(elementType);
            var repository = Activator.CreateInstance(repositoryType);
            repository.GetType().GetProperty("value").SetValue(repository, Convert.ChangeType(prop, TProperty));
            repository.GetType().GetProperty("valueFrom").SetValue(repository, (string)select.SelectedItem);
            item.GetType().Property(PropertyName).SetValue(item, repository);
            hub.InvokeUpdated();
        }
        ReportItem item;
        string PropertyName;
        ReportDesignerHubControl hub;
        public StaticPropertyGetter(ReportItem item, string PropertyName, Type TFrom, Type TProperty, string DefaultItemPropName, ReportDesignerHubControl hub)
        {
            InitializeComponent();
            this.hub = hub;
            this.item = item;
            this.PropertyName = PropertyName;
            this.TFrom = TFrom;
            this.TProperty = TProperty;
            foreach (string o in TFrom.Properties().Where(x => x.PropertyType == TProperty).Select(x => x.Name))
            {
                select.Items.Add(o);
            }
            var val = item.GetType().Property(PropertyName).GetValue(item);
            string selectItem = "";
            if (val == null)
            {
                selectItem = DefaultItemPropName;
            }
            else
            {
                selectItem = (string)val.GetType().Property("valueFrom").GetValue(val);
            }
            select.SelectedItem = selectItem;
            name.Content = PropertyName;
        }
    }
}
