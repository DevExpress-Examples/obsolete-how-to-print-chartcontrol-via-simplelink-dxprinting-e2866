Imports System
Imports System.Windows
Imports System.Windows.Media
Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Printing
Imports System.Windows.Media.Imaging

Namespace WpfApplication1

    Partial Public Class Window1
        Inherits Window
        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub button1_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim sl As New SimpleLink()
            sl.DetailCount = 1
            sl.Detail = CType(Resources("Data"), DataTemplate)
            AddHandler sl.CreateDetail, AddressOf sl_CreateDetail
            sl.CreateDocument(True)
            sl.ShowPrintPreviewDialog(Me)
        End Sub

        Private Sub sl_CreateDetail(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
            Dim brush As New VisualBrush(chartControl1)
            Dim visual As New DrawingVisual()
            Dim context As DrawingContext = visual.RenderOpen()

            context.DrawRectangle(brush, Nothing, _
                                  New Rect(0, 0, chartControl1.ActualWidth, chartControl1.ActualHeight))
            context.Close()
            Dim bmp As New RenderTargetBitmap(CInt(Fix(chartControl1.ActualWidth)), _
                                              CInt(Fix(chartControl1.ActualHeight)), 96, 96, PixelFormats.Pbgra32)
            bmp.Render(visual)
            e.Data = bmp
        End Sub
    End Class
End Namespace
