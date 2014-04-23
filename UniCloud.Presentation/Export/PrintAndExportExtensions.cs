#region

using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls.RichTextBoxUI;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.TextBased.Csv;
using Telerik.Windows.Documents.Spreadsheet.Model;
using Telerik.Windows.Documents.UI;
using PrintSettings = UniCloud.Presentation.Export.PrintSettings;
using WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation;

#endregion

namespace UniCloud.Presentation.Export
{
    public static class PrintAndExportExtensions
    {
        public static void Export(this RadGridView grid, ExportSettings settings)
        {
            var dialog = new SaveFileDialog();

            switch (settings.Format)
            {
                case ExportFormat.Pdf:
                    dialog.DefaultExt = "*.pdf";
                    dialog.Filter = "Adobe PDF 文档 (*.pdf)|*.pdf";
                    break;
                case ExportFormat.Excel:
                    dialog.DefaultExt = "*.xlsx";
                    dialog.Filter = "Excel 工作簿 (*.xlsx)|*.xlsx";
                    break;
                case ExportFormat.Word:
                    dialog.DefaultExt = "*.docx";
                    dialog.Filter = "Word 文档 (*.docx)|*.docx";
                    break;
                case ExportFormat.Csv:
                    dialog.DefaultExt = "*.csv";
                    dialog.Filter = "CSV (逗号分隔) (*.csv)|*.csv";
                    break;
            }

            if (settings.FileName != null)
            {
                dialog.DefaultFileName = settings.FileName;
            }

            if (dialog.ShowDialog() == true)
            {
                switch (settings.Format)
                {
                    case ExportFormat.Csv:
                        using (var output = dialog.OpenFile())
                        {
                            grid.Export(output, new GridViewExportOptions
                                {
                                    Format = Telerik.Windows.Controls.ExportFormat.Csv,
                                    ShowColumnFooters = grid.ShowColumnFooters,
                                    ShowColumnHeaders = grid.ShowColumnHeaders,
                                    ShowGroupFooters = grid.ShowGroupFooters
                                });
                        }
                        break;
                    case ExportFormat.Excel:
                        {
                            var workbook = CreateWorkBook(grid);

                            if (workbook != null)
                            {
                                var provider = new XlsxFormatProvider();
                                using (var output = dialog.OpenFile())
                                {
                                    provider.Export(workbook, output);
                                }
                            }
                        }
                        break;
                    default:
                        {
                            var document = CreateDocument(grid, settings);

                            if (document != null)
                            {
                                document.LayoutMode = DocumentLayoutMode.Paged;
                                document.Measure(RadDocument.MAX_DOCUMENT_SIZE);
                                document.Arrange(new RectangleF(PointF.Empty, document.DesiredSize));

                                IDocumentFormatProvider provider = null;

                                switch (settings.Format)
                                {
                                    case ExportFormat.Pdf:
                                        provider = new PdfFormatProvider();
                                        break;
                                    case ExportFormat.Word:
                                        provider = new DocxFormatProvider();
                                        break;
                                }

                                using (var output = dialog.OpenFile())
                                {
                                    if (provider != null) provider.Export(document, output);
                                }
                            }
                        }
                        break;
                }
            }
        }

        public static void Print(this RadGridView grid, PrintSettings settings)
        {
            var rtb = CreateRadRichTextBox(grid, settings);
            var window = new RadWindow {Height = 0, Width = 0, Opacity = 0, Content = rtb};
            rtb.PrintCompleted += (s, e) => window.Close();
            window.Show();

            rtb.Print("MyDocument", PrintMode.Native);
        }

        public static void PrintPreview(this RadGridView grid, PrintSettings settings)
        {
            var rtb = CreateRadRichTextBox(grid, settings);
            var window = CreatePreviewWindow(rtb);
            window.ShowDialog();
        }

        private static RadWindow CreatePreviewWindow(RadRichTextBox rtb)
        {
            var printButton = new RadButton
                {
                    Content = "Print",
                    Margin = new Thickness(10, 0, 10, 0),
                    FontWeight = FontWeights.Bold,
                    Width = 80
                };

            printButton.Click += (s, e) => rtb.Print("MyDocument", PrintMode.Native);

            var sp = new StackPanel {Height = 26, Orientation = Orientation.Horizontal, Margin = new Thickness(10)};
            sp.Children.Add(new RadRichTextBoxStatusBar
                {
                    AssociatedRichTextBox = rtb,
                    Margin = new Thickness(20, 0, 10, 0)
                });

            sp.Children.Add(new TextBlock
                {
                    Text = "Orientation:",
                    Margin = new Thickness(10, 0, 3, 0),
                    VerticalAlignment = VerticalAlignment.Center
                });

            var radComboBoxPageOrientation = new RadComboBox
                {
                    ItemsSource = new[] {"Portrait", "Landscape"},
                    SelectedIndex = 0
                };
            sp.Children.Add(radComboBoxPageOrientation);

            radComboBoxPageOrientation.SelectionChanged +=
                (s, e) => rtb.ChangeSectionPageOrientation((PageOrientation) Enum.Parse(typeof (PageOrientation),
                                                                                        radComboBoxPageOrientation.Items
                                                                                            [
                                                                                                radComboBoxPageOrientation
                                                                                                    .SelectedIndex]
                                                                                            .ToString(), true));

            sp.Children.Add(new TextBlock
                {
                    Text = "Size:",
                    Margin = new Thickness(10, 0, 3, 0),
                    VerticalAlignment = VerticalAlignment.Center
                });

            var radComboBoxPageSize = new RadComboBox
                {
                    ItemsSource = new[] {"A0", "A1", "A2", "A3", "A4", "A5", "Letter"},
                    Height = 25,
                    SelectedIndex = 4,
                };
            sp.Children.Add(radComboBoxPageSize);

            radComboBoxPageSize.SelectionChanged += (s, e) =>
                                                    rtb.ChangeSectionPageSize(
                                                        PaperTypeConverter.ToSize(
                                                            (PaperTypes) Enum.Parse(typeof (PaperTypes),
                                                                                    radComboBoxPageSize.Items[
                                                                                        radComboBoxPageSize
                                                                                            .SelectedIndex]
                                                                                        .ToString(), true)));

            sp.Children.Add(printButton);

            var g = new Grid();
            g.RowDefinitions.Add(new RowDefinition {Height = GridLength.Auto});
            g.RowDefinitions.Add(new RowDefinition());
            g.Children.Add(sp);
            g.Children.Add(rtb);

            Grid.SetRow(rtb, 1);

            return new RadWindow
                {
                    Content = g,
                    Width = 900,
                    Height = 600,
                    Header = "Print Preview",
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
        }

        private static RadRichTextBox CreateRadRichTextBox(GridViewDataControl grid, PrintSettings settings)
        {
            return new RadRichTextBox
                {
                    IsReadOnly = true,
                    LayoutMode = DocumentLayoutMode.Paged,
                    IsSelectionEnabled = false,
                    IsSpellCheckingEnabled = false,
                    Document = CreateDocument(grid, settings)
                };
        }

        private static RadDocument CreateDocument(GridViewDataControl grid, PrintSettings settings)
        {
            RadDocument document;

            using (var stream = new MemoryStream())
            {
                EventHandler<GridViewElementExportingEventArgs> elementExporting = (s, e) =>
                    {
                        switch (e.Element)
                        {
                            case ExportElement.Table:
                                e.Attributes["border"] = "0";
                                break;
                            case ExportElement.HeaderRow:
                                e.Styles.Add("background-color", settings.HeaderBackground.ToString().Remove(1, 2));
                                break;
                            case ExportElement.GroupHeaderRow:
                                e.Styles.Add("background-color", settings.GroupHeaderBackground.ToString().Remove(1, 2));
                                break;
                            case ExportElement.Row:
                                e.Styles.Add("background-color", settings.RowBackground.ToString().Remove(1, 2));
                                break;
                        }
                    };

                grid.ElementExporting += elementExporting;

                grid.Export(stream, new GridViewExportOptions
                    {
                        Format = Telerik.Windows.Controls.ExportFormat.Html,
                        ShowColumnFooters = grid.ShowColumnFooters,
                        ShowColumnHeaders = grid.ShowColumnHeaders,
                        ShowGroupFooters = grid.ShowGroupFooters
                    });

                grid.ElementExporting -= elementExporting;

                stream.Position = 0;

                document = new HtmlFormatProvider().Import(stream);
            }

            return document;
        }

        private static Workbook CreateWorkBook(GridViewDataControl grid)
        {
            Workbook book;

            using (var stream = new MemoryStream())
            {
                grid.Export(stream, new GridViewExportOptions
                    {
                        Format = Telerik.Windows.Controls.ExportFormat.Csv,
                        ShowColumnFooters = grid.ShowColumnFooters,
                        ShowColumnHeaders = grid.ShowColumnHeaders,
                        ShowGroupFooters = grid.ShowGroupFooters,
                    });

                stream.Position = 0;

                book = new CsvFormatProvider().Import(stream);
            }

            return book;
        }
    }
}