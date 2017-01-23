using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace M6T.Data.Reporting.RuntimeReports.ReportItems
{
    public abstract class ReportItemBase : ReportItem
    {
        public ReportItemBase()
        {
            Region = new Thickness(0);
            Size = new System.Windows.Size(100, 100);
            AcceptedTypes = new List<Type>();
        }
        [XmlIgnore]
        public virtual List<Type> AcceptedTypes
        {
            get;

            set;
        }
        [ReportItemAttribute]
        public virtual string DataPath
        {
            get;

            set;
        }


        [ReportItemAttribute]
        public virtual string Isim
        {
            get;

            set;
        }
        [ReportItemAttribute]
        public virtual Thickness Region
        {
            get;

            set;
        }

        public virtual bool Selected
        {
            get;

            set;
        }
        [ReportItemAttribute]
        public virtual int Sira
        {
            get;

            set;
        }
        [ReportItemAttribute]
        public System.Windows.Size Size
        {
            get;

            set;
        }

        public virtual void DrawTO(object data, bool printMode)
        {
            throw new NotImplementedException();
        }

        public virtual UIElement GetElement()
        {
            throw new NotImplementedException();
        }
    }
}
