using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace M6T.Data.Reporting.RuntimeReports.ReportItems
{
    public class TextItem : ReportItemBase
    {
        public TextItem() : base()
        {
            YaziRengi = Colors.Black;
            ArkaPlanRengi = Colors.Transparent;
            FontBoyutu = 15;
            lbl = new TextBlock();
            lbl.HorizontalAlignment = HorizontalAlignment.Left;
            lbl.VerticalAlignment = VerticalAlignment.Top;
            SatırKaydır = true;
            Region = new Thickness(0);
            AcceptedTypes = new List<Type>();
            AcceptedTypes.Add(typeof(string));
        }
        [ReportItemAttribute]
        public ReportColor YaziRengi
        {
            get; set;
        }

        TextBlock lbl;

        [ReportItemAttribute]
        public ReportColor ArkaPlanRengi
        {
            get; set;
        }


        [ReportItemAttribute]
        public bool SatırKaydır
        {
            get; set;
        }
        [ReportItemAttribute]
        public float FontBoyutu
        {
            get; set;
        }
        [ReportItemAttribute]
        public string SabitYazı
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

        public override UIElement GetElement()
        {
            return lbl;
        }
        public override void DrawTO(object data, bool printMode)
        {
            if (!printMode)
            {
                if (data == null)
                    data = Isim + GetType().Name;
            }
            else
            {
                if (data == null)
                    data = "";
            }
            string showdata = Convert.ToString(data);
            if (!string.IsNullOrEmpty(SabitYazı))
            {
                showdata = SabitYazı;
            }
            SolidColorBrush foregroundBrush = new SolidColorBrush(YaziRengi);
            SolidColorBrush backgroundBrush = new SolidColorBrush(ArkaPlanRengi);
            lbl.FontSize = FontBoyutu;
            lbl.Foreground = foregroundBrush;
            lbl.Background = backgroundBrush;
            lbl.Text = showdata;
            lbl.FontStyle = FontStili.value;
            lbl.Margin = Region;
            lbl.Width = Size.Width;
            lbl.Height = Size.Height;
            lbl.FontWeight = FontKalınligi.value;
            lbl.TextWrapping = SatırKaydır ? TextWrapping.Wrap : TextWrapping.NoWrap;
        }
    }
}
