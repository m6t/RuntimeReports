using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for ReportDesignerPropertiesControl.xaml
    /// </summary>
    public partial class ReportDesignerPropertiesControl : UserControl
    {

        public ReportDesignerPropertiesControl()
        {
            InitializeComponent();
        }
        ReportDesignerHubControl Hub { get; set; }
        M6TReport Report { get; set; }
        object data;
        public void SetReport(M6TReport report, ReportDesignerHubControl hub, object sampleData)
        {
            Report = report;
            data = sampleData;
            Hub = hub;
            hub.Updated += GetSelectedProperties;
        }
        ReportItem lastitem;
        SolidColorBrush borderbrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb((byte)255, (byte)0, (byte)122, (byte)204));
        private void GetSelectedProperties()
        {
            if (Report.GetSelectedItem() != null)
            {
                if (lastitem == null || lastitem != Report.GetSelectedItem())
                {
                    items.Children.Clear();
                    var item = Report.GetSelectedItem();
                    if (item != null)
                    {
                        lastitem = item;
                        var proppss = item.GetType().Properties();
                        foreach (var prop in proppss.Where(x => x.IsDefined(typeof(ReportItemAttributeAttribute), true)))
                        {
                            List<Type> ttt = new List<Type>();
                            Type propType = prop.GetGetMethod().ReturnType;
                            if (prop.IsDefined(typeof(ReportItemGetFromClassAttribute), true))
                            {
                                ReportItemGetFromClassAttribute att = (ReportItemGetFromClassAttribute)
                                Attribute.GetCustomAttribute(prop, typeof(ReportItemGetFromClassAttribute), true);
                                propType = att.TypeProperty;
                                UserControl ri = new PropertyEditors.StaticPropertyGetter(
                                    item, prop.Name, att.TypeFrom, att.TypeProperty, att.DefaultValuePropName, Hub);
                                ri.HorizontalAlignment = HorizontalAlignment.Stretch;
                                ri.Width = double.NaN;
                                ri.BorderThickness = new Thickness(1);
                                ri.BorderBrush = borderbrush;
                                items.Children.Add(ri);
                                continue;
                            }

                            if (propType.IsEnum)
                            {
                                propType = typeof(Enum);
                            }
                            if (prop.Name == "DataPath")
                            {
                                DataPathSelector dps = new DataPathSelector(item, Report, data, Hub);
                                dps.HorizontalAlignment = HorizontalAlignment.Stretch;
                                dps.Width = double.NaN;
                                dps.BorderThickness = new Thickness(1);
                                dps.BorderBrush = borderbrush;
                                items.Children.Add(dps);
                                continue;
                            }
                            foreach (var type in ReportElementPool.PropertyEditors)
                            {
                                if (type.IsDefined(typeof(ReportItemPropertyEditorAttribute), true))
                                {
                                    ReportItemPropertyEditorAttribute att = (ReportItemPropertyEditorAttribute)
                                    Attribute.GetCustomAttribute(type, typeof(ReportItemPropertyEditorAttribute));

                                    if (att != null && att.CanEdit != null && att.CanEdit == propType)
                                    {
                                        ttt.Add(type);
                                    }
                                }
                            }
                            if (ttt.Count() > 0)
                            {
                                var editor = ttt.First();
                                UserControl ri = (UserControl)Activator.CreateInstance(editor, item, prop.Name, Hub);
                                ri.HorizontalAlignment = HorizontalAlignment.Stretch;
                                ri.Width = double.NaN;
                                ri.BorderThickness = new Thickness(1);
                                ri.BorderBrush = borderbrush;
                                items.Children.Add(ri);
                            }
                        }
                        items.Children.Add(new PropertyEditors.ItemDeleter(item, Hub));
                    }
                }
            }
            else
            {
                lastitem = null;
                items.Children.Clear();
            }
        }
    }
}
