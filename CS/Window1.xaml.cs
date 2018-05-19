using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Printing;

namespace WpfApplication1 {

    public partial class Window1 : Window {
        public Window1 () {
            InitializeComponent();
        }

        private void button1_Click (object sender, RoutedEventArgs e) {
            SimpleLink sl = new SimpleLink();
            sl.DetailCount = 1;
            sl.DetailTemplate = (DataTemplate)Resources["Data"];
            sl.CreateDetail += new EventHandler<CreateAreaEventArgs>(sl_CreateDetail);
            sl.CreateDocument(true);
            sl.ShowPrintPreviewDialog(this);
        }

        void sl_CreateDetail (object sender, CreateAreaEventArgs e) {
            VisualBrush brush = new VisualBrush(chartControl1);
            DrawingVisual visual = new DrawingVisual();
            DrawingContext context = visual.RenderOpen();

            context.DrawRectangle(brush, null,
                new Rect(0, 0, chartControl1.ActualWidth, chartControl1.ActualHeight));
            context.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)chartControl1.ActualWidth,
                (int)chartControl1.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            bmp.Render(visual);
            e.Data = bmp;
        }
    }
}
