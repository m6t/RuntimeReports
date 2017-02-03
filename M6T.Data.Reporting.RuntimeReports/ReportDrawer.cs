using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.IO;

namespace M6T.Data.Reporting.RuntimeReports
{
    public class ReportDrawer
    {
        public System.Windows.Shapes.Rectangle selectRed;
        public System.Windows.Shapes.Rectangle solust;
        public System.Windows.Shapes.Rectangle sagalt;
        public System.Windows.Shapes.Ellipse corner;
        public Canvas bgr;
        public void ForceBackgroundRedraw()
        {
            if (bgr != null)
                bgr.Children.Clear();
            bgr = null;
            corner = null;
            selectRed = null;
            sagalt = null;
            solust = null;
        }
        public bool IsCornerCaptured = false;
        public Canvas DrawReport(M6TReport Report, object data, bool YazdirmaIslemi = false, bool useBottomCorner = true, bool DisableCorners = false)
        {
            if (bgr == null)
            {
                bgr = new Canvas();
                if (!DisableCorners)
                {
                    solust = new System.Windows.Shapes.Rectangle();
                    solust.Width = 2;
                    solust.Height = 2;
                    solust.Name = "selectionred";
                    solust.Fill = Brushes.Black;
                    solust.StrokeThickness = 0;
                    solust.VerticalAlignment = VerticalAlignment.Top;
                    solust.HorizontalAlignment = HorizontalAlignment.Left;
                    solust.Margin = new Thickness(0);
                    bgr.Children.Add(solust);
                    sagalt = new System.Windows.Shapes.Rectangle();
                    sagalt.Width = 2;
                    sagalt.Height = 2;
                    sagalt.Fill = Brushes.Black;
                    sagalt.StrokeThickness = 0;
                    sagalt.Name = "selectionred";
                    sagalt.VerticalAlignment = VerticalAlignment.Top;
                    sagalt.HorizontalAlignment = HorizontalAlignment.Left;
                    if (useBottomCorner)
                    {
                        sagalt.Margin = new Thickness(Report.Page.ReportWidth - 2, Report.Page.ReportHeight - 2, 0, 0);
                        bgr.Children.Add(sagalt);
                    }
                    else
                    {
                        sagalt.Margin = new Thickness(Report.Page.ReportWidth - 2, 0, 0, 0);
                        bgr.Children.Add(sagalt);
                    }
                }
                selectRed = new System.Windows.Shapes.Rectangle();
                selectRed.Fill = Brushes.Transparent;
                selectRed.Stroke = Brushes.Red;
                selectRed.StrokeThickness = 1;
                bgr.Children.Add(selectRed);
                selectRed.Name = "selectionred";
                corner = new System.Windows.Shapes.Ellipse();
                corner.HorizontalAlignment = HorizontalAlignment.Left;
                corner.VerticalAlignment = VerticalAlignment.Top;
                corner.Fill = Brushes.DarkGray;
                corner.Stroke = Brushes.LightGray;
                corner.StrokeThickness = 1;
                corner.MouseDown += Corner_MouseDown;
                corner.MouseEnter += Corner_MouseEnter;
                corner.MouseUp += Corner_MouseUp;
                corner.MouseLeave += Corner_MouseLeave;
                corner.Width = 10;
                corner.Height = 10;
                corner.Name = "resizecorner";
                bgr.Children.Add(corner);
                Panel.SetZIndex(selectRed, 99990);
                Panel.SetZIndex(corner, 99991);
                if (!DisableCorners)
                {
                    Panel.SetZIndex(solust, 99992);
                    Panel.SetZIndex(sagalt, 99993);
                }
                bgr.Width = Report.Page.ReportWidth;
                bgr.Height = Report.Page.ReportHeight;
                if (Report.Page.BackgroundType == ReportBackgroundType.Color)
                {
                    SolidColorBrush backgroundbrush = new SolidColorBrush(Report.Page.BackgroundColor);
                    bgr.Background = backgroundbrush;
                }
                else if (Report.Page.BackgroundType == ReportBackgroundType.Image)
                {
                    if (YazdirmaIslemi && !Report.ArkaPlaniYazdir)
                    {

                    }
                    else
                    {
                        if (File.Exists(Report.Page.BackgroundImage.Path))
                        {
                            Image a = new Image();
                            a.Stretch = Stretch.Fill;
                            a.HorizontalAlignment = HorizontalAlignment.Stretch;
                            a.VerticalAlignment = VerticalAlignment.Stretch;
                            a.Name = "backgroundimg";
                            a.Width = Report.Page.ReportWidth;
                            a.Height = Report.Page.ReportHeight;
                            a.Margin = new Thickness(0);
                            a.Source = Helper.BitmapToImageSource(new System.Drawing.Bitmap(Report.Page.BackgroundImage.Path));
                            bgr.Children.Add(a);
                        }
                        else
                        {
                            bgr.Background = Brushes.White;
                        }
                    }
                }
                else
                {
                    bgr.Background = Brushes.White;
                }
            }
            List<UIElement> todelete = new List<UIElement>();
            foreach (UIElement element in bgr.Children)
            {

                if (element is System.Windows.Shapes.Rectangle)
                {
                    if ((element as System.Windows.Shapes.Rectangle).Name == "selectionred")
                        continue;
                }
                if (element is Image)
                {
                    if ((element as Image).Name == "backgroundimg")
                        continue;
                }
                if (element is System.Windows.Shapes.Ellipse)//resizecorner
                {
                    if ((element as System.Windows.Shapes.Ellipse).Name == "resizecorner")
                        continue;
                }
                if (!(Report.Items.Where(x => x.GetElement() == element).Count() > 0))
                {
                    todelete.Add(element);
                }
            }
            foreach (var del in todelete)
            {
                bgr.Children.Remove(del);
            }
            foreach (ReportItem item in Report.Items.OrderBy(x => x.Sira))
            {
                if (!bgr.Children.Contains(item.GetElement()))
                {
                    bgr.Children.Add(item.GetElement());
                    Panel.SetZIndex(item.GetElement(), item.Sira);
                }
                string dataPath = item.DataPath;
                object drawData;
                if (string.IsNullOrEmpty(dataPath))
                {
                    drawData = null;
                }
                else
                {
                    bool found = false;
                    foreach (var prop in data.GetType().Properties())
                    {
                        if (prop.Name == dataPath)
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        drawData = null;
                    }
                    else
                    {
                        drawData = data.GetType().Property(dataPath).GetValue(data);
                    }
                }
                item.DrawTO(drawData, YazdirmaIslemi);
                if (item.Selected)
                {
                    selectRed.Margin = new Thickness(item.Region.Left - 1, item.Region.Top - 1, 0, 0);
                    selectRed.Width = item.Size.Width + 2;
                    selectRed.Height = item.Size.Height + 2;
                    corner.Width = 10;
                    corner.Height = 10;
                    corner.Margin = new Thickness(item.Region.Left + item.Size.Width - 5, item.Region.Top + item.Size.Height - 5, 0, 0);
                }
            }
            if (Report.GetSelectedItem() == null)
            {
                selectRed.Margin = new Thickness(0);
                selectRed.Width = 0;
                selectRed.Height = 0;
                corner.Width = 0;
                corner.Height = 0;
                corner.Margin = new Thickness(0);
            }
            return bgr;
        }
        public bool CornerResize = false;
        private void Corner_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsCornerCaptured = false;
        }

        private void Corner_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            IsCornerCaptured = true;
        }

        private void Corner_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsCornerCaptured = false;
            CornerResize = false;
        }

        private void Corner_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsCornerCaptured = true;
            CornerResize = true;
        }
    }
}
