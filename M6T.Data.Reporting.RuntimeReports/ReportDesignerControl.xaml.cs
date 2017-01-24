using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows;

namespace M6T.Data.Reporting.RuntimeReports
{
    /// <summary>
    /// Interaction logic for ReportDesignerControl.xaml
    /// </summary>
    public partial class ReportDesignerControl : UserControl
    {
        public ReportDesignerControl()
        {
            InitializeComponent();
            Report = null;
            System.Windows.EventManager.RegisterClassHandler(typeof(Control), KeyDownEvent, new KeyEventHandler(reportimage_KeyDown));
            System.Windows.EventManager.RegisterClassHandler(typeof(Control), KeyUpEvent, new KeyEventHandler(reportimage_KeyUp));

        }
        public ReportDrawer ReportDrawer
        {
            get
            {
                return Hub.ReportDrawer;
            }
        }
        public void DrawFrame()
        {
            if (Report != null)
            {
                Canvas cvs = ReportDrawer.DrawReport(Report, SampleData, Hub.PrintMode, Hub.UseBottomCornerFixture, Hub.DisableCorners);
                try
                {
                    if (!reportimage.Children.Contains(cvs))
                    {
                        reportimage.Children.Clear();
                        reportimage.Children.Add(cvs);
                    }
                }
                catch
                {
                    ReportDrawer.ForceBackgroundRedraw();
                    DrawFrame();
                }
            }

        }
        public object SampleData = null;
        public System.Windows.Media.RotateTransform GetRotateTransform
        {
            get
            {
                return rtt;
            }
        }
        public M6TReport Report { get; set; }
        public void SetReport(M6TReport report, object SampleData, ReportDesignerHubControl hub)
        {
            Report = report;
            this.SampleData = SampleData;
            Hub = hub;
            hub.Updated += Hub_Updated;
        }

        private void Hub_Updated()
        {
            DrawFrame();
        }

        ReportDesignerHubControl Hub { get; set; }
        private void reportimage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var st = scaletrfm;
            if (e.Delta < 0)
                if (st.ScaleX < 0.2)
                    return;
            double zoom = e.Delta > 0 ? .1 : -.1;
            st.ScaleX += zoom;
            st.ScaleY += zoom;
            scale = st.ScaleX;
            var yzudekaci = (st.ScaleX * 100);
            zoomtext.Text = Math.Round(yzudekaci, 2) + "%";
        }
        double scale = 1;
        System.Windows.Point absstart;
        System.Windows.Point start;
        System.Windows.Point origin;
        private void reportimage_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (spaced)
            {
                var tt = translatetrfrm;
                start = e.GetPosition(imgborder);
                origin = new System.Windows.Point(tt.X, tt.Y);
                reportimage.CaptureMouse();
            }
            else
            {
                if (Hub.PrintMode)
                    return;
                if (!ReportDrawer.IsCornerCaptured)
                {
                    captureItemMove = true;

                    start = e.GetPosition(imgborder);
                    absstart = e.GetPosition(reportimage);
                }
                else
                {
                    start = e.GetPosition(imgborder);
                    absstart = e.GetPosition(reportimage);
                }

            }
        }
        public FrameworkElement GetReportRootVisualElement()
        {
            return reportimage;
        }
        bool captureItemMove;
        private void reportimage_MouseMove(object sender, MouseEventArgs e)
        {
            if (reportimage.IsMouseCaptured)
            {
                var tt = translatetrfrm;
                System.Windows.Vector v = start - e.GetPosition(imgborder);
                tt.X = origin.X - v.X;
                tt.Y = origin.Y - v.Y;
            }
            if (captureItemMove)
            {
                if (Hub.PrintMode)
                    return;
                System.Windows.Vector v = start - e.GetPosition(imgborder);
                var item = Report.GetSelectedItem();
                if (item != null)
                {
                    var rect = item.Region;
                    rect.Left -= Convert.ToInt32(((v.X / (scale * 100)) * 100));
                    rect.Top -= Convert.ToInt32((v.Y / (scale * 100)) * 100);
                    item.Region = rect;
                    Hub.InvokeUpdated();
                }
                start = e.GetPosition(imgborder);
            }
            if (ReportDrawer.CornerResize)
            {
                if (Hub.PrintMode)
                    return;
                if (e.LeftButton == MouseButtonState.Pressed)
                {


                    System.Windows.Vector v = start - e.GetPosition(imgborder);
                    var item = Report.GetSelectedItem();
                    if (item != null)
                    {
                        var rect = item.Size;
                        rect.Width -= Convert.ToInt32(((v.X / (scale * 100)) * 100));
                        rect.Height -= Convert.ToInt32((v.Y / (scale * 100)) * 100);
                        item.Size = rect;
                        Hub.InvokeUpdated();
                    }
                    start = e.GetPosition(imgborder);
                }
                else
                {
                    ReportDrawer.CornerResize = false;
                }
            }

        }

        private void reportimage_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            reportimage.ReleaseMouseCapture();
            if (captureItemMove)
            {
                var now = e.GetPosition(reportimage);
                int farkx = Convert.ToInt32(now.X - absstart.X);
                int farky = Convert.ToInt32(now.Y - absstart.Y);
                if (farkx < 0)
                    farkx *= -1;
                if (farky < 0)
                    farky *= -1;
                int fark = farkx + farky;
                if (fark < 10)
                {
                    foreach (var item in Report.Items)
                    {
                        item.Selected = false;
                    }
                    foreach (var item in Report.Items)
                    {
                        int X = Convert.ToInt32(now.X);
                        int Y = Convert.ToInt32(now.Y);

                        //X = Convert.ToInt32((X / (scale * 100)) * 100);
                        //Y = Convert.ToInt32((Y / (scale * 100)) * 100);
                        System.Drawing.Rectangle rec = new System.Drawing.Rectangle(
                            item.Region.Left.ToInt(), item.Region.Top.ToInt(), item.Size.Width.ToInt(), item.Size.Height.ToInt());
                        if (rec.Contains(new System.Drawing.Point(X, Y)))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    Hub.InvokeUpdated();
                }
                captureItemMove = false;
            }
        }
        bool spaced = false;
        private void reportimage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                spaced = true;
                Cursor = Cursors.Hand;
            }
        }

        private void reportimage_KeyUp(object sender, KeyEventArgs e)
        {
            spaced = false;
            Cursor = Cursors.Arrow;
        }


    }


}
