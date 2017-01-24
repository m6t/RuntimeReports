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
using System.Xml.Serialization;

namespace M6T.Data.Reporting.RuntimeReports
{
    /// <summary>
    /// Interaction logic for ReportDesignerHubControl.xaml
    /// </summary>
    public partial class ReportDesignerHubControl : UserControl
    {
        public ReportDesignerHubControl()
        {
            InitializeComponent();
            UseBottomCornerFixture = true;
            ReportElementPool.Init();
        }
        public bool _canOpenCreate = true;
        public bool CanOpenOrCreate
        {
            get
            {
                return _canOpenCreate;
            }
            set
            {
                _canOpenCreate = value;
                if (_canOpenCreate)
                {
                    ac.IsEnabled = true;
                    yeni.IsEnabled = true;
                }
                else
                {
                    ac.IsEnabled = false;
                    yeni.IsEnabled = false;
                }


            }
        }
        public bool PrintMode
        {
            get; private set;
        }
        public bool UseBottomCornerFixture
        {
            get; private set;
        }
        public bool DisableCorners
        {
            get; private set;
        }
        public void DisableBottomCornerFixture()
        {
            UseBottomCornerFixture = false;
        }
        public void DisableCornersEntirely()
        {
            DisableCorners = true;
        }
        public void EnablePrintMode()
        {
            PrintMode = true;
            topRow.Height = new GridLength(0);
            bottomRow.Height = new GridLength(0);
            leftCol.Width = new GridLength(0);
            rightCol.Width = new GridLength(0);
            switch (report.DondurmeYonu)
            {
                case ReportRotation.Duz: designer.GetRotateTransform.Angle = 0; break;
                case ReportRotation.Sag: designer.GetRotateTransform.Angle = 90; break;
                case ReportRotation.Sol: designer.GetRotateTransform.Angle = -90; break;
                case ReportRotation.Ters: designer.GetRotateTransform.Angle = 180; break;
            }
            foreach (var item in report.Items)
            {
                item.Selected = false;
            }
            ReportDrawer.ForceBackgroundRedraw();
            InvokeUpdated();
        }

        #region ReportDrawer Property
        private ReportDrawer _ReportDrawer = null;
        public ReportDrawer ReportDrawer
        {
            get
            {
                if (_ReportDrawer == null)
                {
                    _ReportDrawer = new ReportDrawer();
                }
                return _ReportDrawer;
            }
            set
            {
                _ReportDrawer = value;

            }
        }
        #endregion
        public void ForceRedraw()
        {
            SetReport(report, SampleData);
            ReportDrawer.ForceBackgroundRedraw();
            designer.DrawFrame();
        }
        public FrameworkElement GetReportRootVisualElement()
        {
            return designer.GetReportRootVisualElement();
        }
        public string FixedReportFileName { get; set; }
        public void InvokeUpdated()
        {

            Updated?.Invoke();
        }
        public object SampleData { get; set; }
        public event VoidHandler Updated;
        public M6TReport report;
        System.Windows.Forms.Timer frameTicker = new System.Windows.Forms.Timer();
        public void SetReport(M6TReport report, object SampleData)
        {
            if (report == null)
                return;
            this.report = report;
            this.SampleData = SampleData;
            width.Text = report.Page.ReportWidth + "";
            height.Text = report.Page.ReportHeight + "";
            switch (report.DondurmeYonu)
            {
                case ReportRotation.Duz: comboBox.SelectedIndex = 0; break;
                case ReportRotation.Sag: comboBox.SelectedIndex = 1; break;
                case ReportRotation.Sol: comboBox.SelectedIndex = 2; break;
                case ReportRotation.Ters: comboBox.SelectedIndex = 3; break;
            }
            toolbar.SetReport(report, this);
            designer.SetReport(report, SampleData, this);
            footer.SetReport(report, this);
            properties.SetReport(report, this, SampleData);
            arkayazdir.IsChecked = report.ArkaPlaniYazdir;
            designer.DrawFrame();

        }

        private void height_LostFocus(object sender, RoutedEventArgs e)
        {
            int widthN;
            int heightN;
            if (int.TryParse(width.Text, out widthN))
            {
                if (widthN < 100)
                    widthN = report.Page.ReportWidth;
                report.Page.ReportWidth = widthN;
            }
            if (int.TryParse(height.Text, out heightN))
            {
                if (heightN < 100)
                    heightN = report.Page.ReportHeight;
                report.Page.ReportHeight = heightN;
            }
            ReportDrawer.ForceBackgroundRedraw();
            designer.DrawFrame();
            width.Text = report.Page.ReportWidth + "";
            height.Text = report.Page.ReportHeight + "";
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Filter = "*.jpg|*.jpg|*.png|*.png|*.bmp|*.bmp";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                report.Page.BackgroundImage = new ReportFilePath();
                report.Page.BackgroundImage.Path = ofd.FileName;
                report.Page.BackgroundType = ReportBackgroundType.Image;
                ReportDrawer.ForceBackgroundRedraw();
                InvokeUpdated();

            }
        }
        public void SetSample(object data)
        {
            SampleData = data;
            SetReport(report, SampleData);
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
        public void Save()
        {
            if (CanOpenOrCreate)
            {
                System.Windows.Forms.SaveFileDialog ofd = new System.Windows.Forms.SaveFileDialog();
                ofd.Filter = "*.mrpt|*.mrpt";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filename = ofd.FileName;
                    Save(filename);
                }
            }
            else
            {
                Save(FixedReportFileName);
            }
        }
        public void Save(string filename)
        {
            List<Type> types = new List<Type>();
            foreach (var item in this.report.Items)
            {
                types.Add(item.GetType());
            }
            var x = new XmlSerializer(report.GetType(), types.ToArray());
            if (System.IO.File.Exists(filename))
                System.IO.File.WriteAllText(filename, "");
            using (System.IO.FileStream wr = System.IO.File.OpenWrite(filename))
            {
                x.Serialize(wr, report);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            TriggerOpen();
        }

        public void TriggerOpen()
        {
            if (CanOpenOrCreate)
            {
                System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
                ofd.Filter = "*.mrpt|*.mrpt";
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string filename = ofd.FileName;
                    Open(filename);
                }
            }
            else
            {
                if (System.IO.File.Exists(FixedReportFileName))
                    Open(FixedReportFileName);
                else
                    New();

            }
        }

        private void Open(string filename)
        {
            M6TReport report;
            List<Type> types = new List<Type>();
            types.AddRange(ReportElementPool.ReportItemTypes);
            var x = new XmlSerializer(typeof(M6TReport), types.ToArray());
            using (System.IO.FileStream wr = System.IO.File.OpenRead(filename))
            {
                report = (M6TReport)x.Deserialize(wr);
            }
            report.ReportDataType = SampleData.GetType();
            report.UnpackReferences();
            SetReport(report, SampleData);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            New();
        }

        private void New()
        {
            M6TReport report = new M6TReport();
            report.ReportDataType = SampleData.GetType();
            report.Page.BackgroundType = ReportBackgroundType.Color;
            report.Page.BackgroundColor = Colors.White;
            SetReport(report, SampleData);
        }

        private void arkayazdir_Checked(object sender, RoutedEventArgs e)
        {
            if (report != null)
                report.ArkaPlaniYazdir = true;
            ReportDrawer.ForceBackgroundRedraw();
            InvokeUpdated();
        }

        private void arkayazdir_Unchecked(object sender, RoutedEventArgs e)
        {
            if (report != null)
                report.ArkaPlaniYazdir = false;
            ReportDrawer.ForceBackgroundRedraw();
            InvokeUpdated();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0: report.DondurmeYonu = ReportRotation.Duz; break;
                case 1: report.DondurmeYonu = ReportRotation.Sag; break;
                case 2: report.DondurmeYonu = ReportRotation.Sol; break;
                case 3: report.DondurmeYonu = ReportRotation.Ters; break;
            }
        }
    }
    public delegate void VoidHandler();
}
