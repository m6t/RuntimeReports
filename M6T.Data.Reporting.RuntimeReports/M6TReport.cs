using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Media;
using System.Windows;

namespace M6T.Data.Reporting.RuntimeReports
{
    public static class Extensions
    {
        public static int ToInt(this double number)
        {
            return Convert.ToInt32(number);
        }
        public static System.Reflection.PropertyInfo[] Properties(this Type type)
        {
            List<System.Reflection.PropertyInfo> props = new List<System.Reflection.PropertyInfo>();

            props.AddRange(type.GetProperties());
            //props.AddRange(type.BaseType.GetProperties());
            return props.ToArray();
        }

        public static System.Reflection.PropertyInfo Property(this Type type, string propName)
        {
            List<System.Reflection.PropertyInfo> props = new List<System.Reflection.PropertyInfo>();

            props.AddRange(type.Properties());
            //props.AddRange(type.BaseType.Properties());
            return props.Where(x => x.Name == propName).First();
        }
    }
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ReportItemGetFromClassAttribute : Attribute
    {
        public Type TypeFrom
        {
            get; set;
        }
        public Type TypeProperty
        {
            get; set;
        }
        public string DefaultValuePropName
        {
            get; set;
        }
    }
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public class ReportItemAttributeAttribute : Attribute
    { }
    [AttributeUsage(AttributeTargets.Class)]
    public class ReportItemPropertyEditorAttribute : Attribute
    {
        private Type _canedit;
        public Type CanEdit
        {
            get { return _canedit; }
            set { _canedit = value; }
        }
    }
    public class ReportFilePath
    {
        public string Path { get; set; }
    }
    public class ReportColor
    {
        public int A;
        public int R;
        public int G;
        public int B;
        public Color GetColor()
        {
            return Color.FromArgb((byte)A, (byte)R, (byte)G, (byte)B);
        }
        public static implicit operator Color(ReportColor clr)
        {
            return clr.GetColor();
        }

        public static implicit operator ReportColor(System.Windows.Media.Color clr)
        {
            return new ReportColor() { A = clr.A, R = clr.R, G = clr.G, B = clr.B };
        }
    }
    public class StaticPropertyGet<TProperty>
    {
        [XmlIgnore]
        public TProperty value { get; set; }
        public string valueFrom { get; set; }

        public void SetFromObject(TProperty value)
        {
            this.value = value;
        }
    }
    public class M6TReport
    {
        public void UnpackReferences()
        {
            foreach (var item in Items)
            {
                var props = item.GetType().Properties().Where(x => x.PropertyType.Name.Contains("StaticPropertyGet"));
                foreach (var staticPropGet in props)
                {
                    //bosna kofte
                    //orman kebabı
                    //prinic
                    //kurufalsulte
                    //ciger
                    //doner
                    //kadayıf

                    ReportItemGetFromClassAttribute att = (ReportItemGetFromClassAttribute)Attribute.GetCustomAttribute(staticPropGet, typeof(ReportItemGetFromClassAttribute), true);
                    var val = staticPropGet.GetValue(item);

                    if (val == null)
                    {
                        Type elementType = staticPropGet.PropertyType.GetGenericArguments()[0];
                        Type repositoryType = typeof(StaticPropertyGet<>).MakeGenericType(elementType);
                        var repository = Activator.CreateInstance(repositoryType);
                        val = repository;
                        val.GetType().Property("valueFrom").SetValue(val, att.DefaultValuePropName);
                    }
                    string staticpropname = (string)val.GetType().Property("valueFrom").GetValue(val);
                    var setval = att.TypeFrom.Property(staticpropname).GetValue(null);

                    val.GetType().GetProperty("value").SetValue(val, Convert.ChangeType(setval, att.TypeProperty));
                    staticPropGet.SetValue(item, val);
                }
            }
        }
        public M6TReport()
        {
            Page = new ReportPage() { ReportHeight = 300, ReportWidth = 300 };
            Items = new List<ReportItems.ReportItemBase>();
            ArkaPlaniYazdir = true;
        }
        public bool ArkaPlaniYazdir { get; set; }
        public ReportPage Page { get; set; }
        public ReportRotation DondurmeYonu { get; set; }
        [XmlIgnore]
        public Type ReportDataType { get; set; }
        public List<ReportItems.ReportItemBase> Items { get; set; }
        public ReportItems.ReportItemBase GetSelectedItem()
        => Items.Where(x => x.Selected)
                .Count() > 0 ?
                Items.Where(x => x.Selected).First() : null;

    }
    public enum ReportRotation
    {
        Duz, Sol, Sag, Ters
    }
    public enum ReportBackgroundType
    {
        Color, Image
    }

    public class ReportElementPool
    {
        private static bool _initialized = false;
        public static void Init()
        {
            if (!_initialized)
            {
                PropertyEditors.Add(typeof(RectangleEditor));
                PropertyEditors.Add(typeof(EnumEditor));
                PropertyEditors.Add(typeof(SingleEditor));
                PropertyEditors.Add(typeof(IntEditor));
                PropertyEditors.Add(typeof(StringEditor));
                PropertyEditors.Add(typeof(FilePathEditor));
                PropertyEditors.Add(typeof(BoolEditor));
                PropertyEditors.Add(typeof(SizeEditor));
                PropertyEditors.Add(typeof(PropertyEditors.ColorEditor));
                ReportItemTypes.Add(typeof(ReportItems.TextItem));
                ReportItemTypes.Add(typeof(Liste));
                _initialized = true;
            }
        }
        public static List<Type> PropertyEditors = new List<Type>();
        public static List<Type> ReportItemTypes = new List<Type>();
    }
    public class ReportPage
    {
        public int ReportWidth { get; set; }
        public int ReportHeight { get; set; }
        public ReportBackgroundType BackgroundType { get; set; }
        public Color BackgroundColor { get; set; }
        public ReportFilePath BackgroundImage { get; set; }

    }


    public interface ReportItem
    {
        string Isim { get; set; }
        Thickness Region { get; set; }
        bool Selected { get; set; }
        string DataPath { get; set; }
        Size Size { get; set; }
        void DrawTO(object data, bool printMode);
        List<Type> AcceptedTypes { get; set; }
        System.Windows.UIElement GetElement();
        int Sira { get; set; }
    }
    public class Helper
    {

        public static bool IsMyself(object data, Type myself)
        {
            return (data is string) && ((string)data) == myself.Name;
        }

        public static System.Windows.Media.Imaging.BitmapImage BitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                System.Windows.Media.Imaging.BitmapImage bitmapimage = new System.Windows.Media.Imaging.BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;
            }
        }
    }
}
