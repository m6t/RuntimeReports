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

    public class Liste : ReportItems.ReportItemBase
    {
        public Liste() : base()
        {
            YaziRengi = Colors.Black;
            ArkaPlanRengi = Colors.Transparent;
            FontBoyutu = 15;
            lbl = new TextBlock();
            lbl.HorizontalAlignment = HorizontalAlignment.Left;
            lbl.VerticalAlignment = VerticalAlignment.Top;

            Region = new Thickness(0);
            AcceptedTypes = new List<Type>();
            AcceptedTypes.Add(typeof(string[]));
        }


        [ReportItemAttribute]
        public ReportColor YaziRengi
        {
            get; set;
        }



        [ReportItemAttribute]
        public ReportColor ArkaPlanRengi
        {
            get; set;
        }

        [ReportItemAttribute]
        public float FontBoyutu
        {
            get; set;
        }

        [ReportItemGetFromClass(TypeFrom = typeof(FontWeights), TypeProperty = typeof(FontWeight), DefaultValuePropName = "Normal")]
        [ReportItemAttribute]
        public StaticPropertyGet<FontWeight> FontKalınligi
        {
            get; set;
        }


        [ReportItemGetFromClass(TypeFrom = typeof(FontStyles), TypeProperty = typeof(FontStyle), DefaultValuePropName = "Normal")]
        [ReportItemAttribute]
        public StaticPropertyGet<FontStyle> FontStili
        {
            get; set;
        }
        TextBlock lbl;
        public override UIElement GetElement()
        {
            return lbl;
        }
        public override void DrawTO(object data, bool printMode)
        {
            string[] rows;
            if (data == null)
            {
                if (!printMode)
                {
                    rows = new string[] {
                        Isim + "(" + GetType().Name + ")",
                        "Liste eleman1",
                        "Liste eleman2" };
                }
                else
                {
                    rows = new string[0];
                }
            }
            else
            {
                rows = (string[])data;
            }
            SolidColorBrush foregroundBrush = new SolidColorBrush(YaziRengi);
            SolidColorBrush backgroundBrush = new SolidColorBrush(ArkaPlanRengi);
            lbl.FontSize = FontBoyutu;
            lbl.Foreground = foregroundBrush;
            lbl.Background = backgroundBrush;
            lbl.Text = string.Join("\n", rows);
            lbl.FontStyle = FontStili.value;
            lbl.Margin = Region;
            lbl.Width = Size.Width;
            lbl.Height = Size.Height;
            lbl.FontWeight = FontKalınligi.value;
        }
    }
}
