using System;
using LibExcelCS = CarlosAg.ExcelXmlWriter;
using System.IO;
using AppMiTaller.Intranet.BE;


namespace AppMiTaller.Intranet.BL.Exportacion
{
    public class ExportarExcelXml
    {
        private void GenerateStyles(LibExcelCS.WorksheetStyleCollection styles)
        {
            try
            {
                // -----------------------------------------------
                //  Default
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle Default = styles.Add("Default");
                Default.Name = "Normal";
                Default.Font.FontName = "Arial";
                Default.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Bottom;
                // -----------------------------------------------
                //  s64
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s64 = styles.Add("s64");
                s64.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s64.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Bottom;
                s64.NumberFormat = "@";
                // -----------------------------------------------
                //  s65
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s65 = styles.Add("s65");
                s65.NumberFormat = "@";
                //-----------------------------------------------
                //  s66
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s66 = styles.Add("s66");
                s66.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s66.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Bottom;
                s66.NumberFormat = "@";
                // -----------------------------------------------
                //  s67
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s67 = styles.Add("s67");
                s67.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Right;
                s67.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Bottom;
                s67.NumberFormat = "@";
                // -----------------------------------------------
                //  s68
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s68 = styles.Add("s68");
                s68.Font.Bold = true;
                s68.Font.FontName = "Arial";
                s68.Font.Size = 8;
                s68.Font.Color = "#666699";
                s68.Interior.Color = "#C0C0C0";
                s68.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s68.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s68.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s68.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s68.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s68.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s68.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s68.NumberFormat = "@";
                // -----------------------------------------------
                //  s69
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s69 = styles.Add("s69");
                s69.Font.Bold = true;
                s69.Font.FontName = "Arial";
                s69.Font.Size = 8;
                s69.Font.Color = "#666699";
                s69.Interior.Color = "#C0C0C0";
                s69.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s69.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s69.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s69.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s69.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s69.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s69.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s69.NumberFormat = "@";
                // -----------------------------------------------
                //  s70
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s70 = styles.Add("s70");
                s70.Font.Bold = true;
                s70.Font.FontName = "Arial";
                s70.Font.Size = 8;
                s70.Font.Color = "#666699";
                s70.Interior.Color = "#C0C0C0";
                s70.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s70.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s70.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s70.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s70.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s70.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s70.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s70.NumberFormat = "@";
                // -----------------------------------------------
                //  s71
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s71 = styles.Add("s71");
                s71.Font.FontName = "Arial";
                s71.Font.Size = 8;
                s71.Font.Color = "#666699";
                s71.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s71.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s71.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s71.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s71.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s71.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s71.NumberFormat = "@";
                // -----------------------------------------------
                //  s72
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s72 = styles.Add("s72");
                s72.Font.FontName = "Arial";
                s72.Font.Size = 8;
                s72.Font.Color = "#666699";
                s72.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s72.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s72.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s72.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s72.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s72.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s72.NumberFormat = "@";
                // -----------------------------------------------
                //  s73
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s73 = styles.Add("s73");
                s73.Font.FontName = "Arial";
                s73.Font.Size = 8;
                s73.Font.Color = "#666699";
                s73.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s73.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s73.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s73.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s73.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s73.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s73.NumberFormat = "@";
                // -----------------------------------------------
                //  s74
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s74 = styles.Add("s74");
                s74.Font.FontName = "Arial";
                s74.Font.Size = 8;
                s74.Font.Color = "#666699";
                s74.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s74.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s74.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s74.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s74.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s74.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s74.NumberFormat = "@";
                // -----------------------------------------------
                //  s75
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s75 = styles.Add("s75");
                s75.Font.FontName = "Arial";
                s75.Font.Size = 8;
                s75.Font.Color = "#666699";
                s75.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s75.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s75.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s75.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s75.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s75.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s75.NumberFormat = "@";
                // -----------------------------------------------
                //  s76
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s76 = styles.Add("s76");
                s76.Font.FontName = "Arial";
                s76.Font.Size = 8;
                s76.Font.Color = "#666699";
                s76.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s76.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s76.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s76.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s76.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s76.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s76.NumberFormat = "@";
                // -----------------------------------------------
                //  s77
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s77 = styles.Add("s77");
                s77.Font.FontName = "Arial";
                s77.Font.Size = 8;
                s77.Font.Color = "#666699";
                s77.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s77.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s77.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s77.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s77.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s77.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s77.NumberFormat = "@";
                // -----------------------------------------------
                //  s78
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s78 = styles.Add("s78");
                s78.Font.FontName = "Arial";
                s78.Font.Size = 8;
                s78.Font.Color = "#666699";
                s78.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Right;
                s78.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s78.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s78.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s78.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s78.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s78.NumberFormat = "@";

                /***********************/
                // -----------------------------------------------
                //  s88
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s88 = styles.Add("s88");
                s88.Font.Bold = true;
                s88.Font.FontName = "Arial";
                s88.Font.Size = 8;
                s88.Font.Color = "#000000";
                s88.Interior.Color = "#C0C0C0";
                s88.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s88.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s88.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s88.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s88.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s88.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s88.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s88.NumberFormat = "@";
                // -----------------------------------------------
                //  s89
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s89 = styles.Add("s89");
                s89.Font.Bold = true;
                s89.Font.FontName = "Arial";
                s89.Font.Size = 8;
                s89.Font.Color = "#000000";
                s89.Interior.Color = "#C0C0C0";
                s89.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s89.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s89.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s89.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s89.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s89.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s89.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s89.NumberFormat = "@";
                // -----------------------------------------------
                //  s90
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s90 = styles.Add("s90");
                s90.Font.Bold = true;
                s90.Font.FontName = "Arial";
                s90.Font.Size = 8;
                s90.Font.Color = "#000000";
                s90.Interior.Color = "#C0C0C0";
                s90.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s90.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s90.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s90.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s90.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s90.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s90.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s90.NumberFormat = "@";
                // -----------------------------------------------
                //  s91
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s91 = styles.Add("s91");
                s91.Font.FontName = "Arial";
                s91.Font.Size = 8;
                s91.Font.Color = "#000000";
                s91.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s91.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s91.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s91.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s91.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s91.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s91.NumberFormat = "@";
                // -----------------------------------------------
                //  s92
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s92 = styles.Add("s92");
                s92.Font.FontName = "Arial";
                s92.Font.Size = 8;
                s92.Font.Color = "#000000";
                s92.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s92.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s92.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s92.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s92.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s92.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s92.NumberFormat = "@";
                // -----------------------------------------------
                //  s93
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s93 = styles.Add("s93");
                s93.Font.FontName = "Arial";
                s93.Font.Size = 8;
                s93.Font.Color = "#000000";
                s93.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s93.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s93.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s93.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s93.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s93.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s93.NumberFormat = "@";
                // -----------------------------------------------
                //  s94
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s94 = styles.Add("s94");
                s94.Font.FontName = "Arial";
                s94.Font.Size = 8;
                s94.Font.Color = "#000000";
                s94.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s94.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s94.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s94.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s94.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s94.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s94.NumberFormat = "@";
                // -----------------------------------------------
                //  s95
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s95 = styles.Add("s95");
                s95.Font.FontName = "Arial";
                s95.Font.Size = 8;
                s95.Font.Color = "#000000";
                s95.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s95.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s95.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s95.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s95.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s95.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s95.NumberFormat = "@";
                // -----------------------------------------------
                //  s96
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s96 = styles.Add("s96");
                s96.Font.FontName = "Arial";
                s96.Font.Size = 8;
                s96.Font.Color = "#000000";
                s96.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s96.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s96.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s96.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s96.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s96.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s96.NumberFormat = "@";
                // -----------------------------------------------
                //  s97
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s97 = styles.Add("s97");
                s97.Font.FontName = "Arial";
                s97.Font.Size = 8;
                s97.Font.Color = "#000000";
                s97.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s97.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s97.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s97.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s97.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s97.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s97.NumberFormat = "@";
                // -----------------------------------------------
                //  s98
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s98 = styles.Add("s98");
                s98.Font.FontName = "Arial";
                s98.Font.Size = 8;
                s98.Font.Color = "#000000";
                s98.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Right;
                s98.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s98.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s98.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s98.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s98.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s98.NumberFormat = "@";
                // -----------------------------------------------
                //  s99
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s99 = styles.Add("s99");
                s99.Font.Bold = true;
                s99.Font.FontName = "Arial";
                s99.Font.Size = 8;
                s99.Font.Color = "#000000";
                s99.Interior.Color = "#FFCC00";
                s99.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s99.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s99.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s99.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s99.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s99.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s99.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s99.NumberFormat = "@";
                // -----------------------------------------------
                //  s100
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s100 = styles.Add("s100");
                s100.Font.Bold = true;
                s100.Font.FontName = "Arial";
                s100.Font.Size = 8;
                s100.Font.Color = "#000000";
                s100.Interior.Color = "#FFCC00";
                s100.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s100.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s100.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s100.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s100.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#000000");
                s100.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s100.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 2, "#000000");
                s100.NumberFormat = "@";

                // -----------------------------------------------
                //  s101
                // -----------------------------------------------

                LibExcelCS.WorksheetStyle s101 = styles.Add("s101");
                s101.Font.Bold = true;
                s101.Font.FontName = "Arial";
                s101.Font.Size = 8;
                s101.Font.Color = "#666699";
                s101.Interior.Color = "#C0C0C0";
                s101.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s101.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s101.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s101.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s101.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s101.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 2, "#666699");
                s101.NumberFormat = "@";

                // -----------------------------------------------
                //  s102
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s102 = styles.Add("s102");
                s102.Font.Bold = true;
                s102.Font.FontName = "Arial";
                s102.Font.Size = 8;
                s102.Font.Color = "#666699";
                s102.Interior.Color = "#C0C0C0";
                s102.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s102.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s102.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s102.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s102.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s102.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s102.NumberFormat = "@";


                // -----------------------------------------------
                //  s103
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s103 = styles.Add("s103");
                s103.Font.Bold = true;
                s103.Font.FontName = "Arial";
                s103.Font.Size = 8;
                s103.Font.Color = "#666699";
                s103.Interior.Color = "#C0C0C0";
                s103.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s103.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Right;
                s103.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;

                s103.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s103.NumberFormat = "@";



                // -----------------------------------------------
                //  s104
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s104 = styles.Add("s104");
                s104.Font.Bold = true;
                s104.Font.FontName = "Arial";
                s104.Font.Size = 8;
                s104.Font.Color = "#666699";
                s104.Interior.Color = "#C0C0C0";
                s104.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s104.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Left;
                s104.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s104.NumberFormat = "@";


                // -----------------------------------------------
                //  s105
                // -----------------------------------------------

                LibExcelCS.WorksheetStyle s105 = styles.Add("s105");
                s105.Font.Bold = true;
                s105.Font.FontName = "Arial";
                s105.Font.Size = 8;
                s105.Font.Color = "#666699";
                s105.Interior.Color = "#C0C0C0";
                s105.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s105.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s105.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s105.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s105.NumberFormat = "@";

                // -----------------------------------------------
                //  s106
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s106 = styles.Add("s106");
                s106.Font.Bold = true;
                s106.Font.FontName = "Arial";
                s106.Font.Size = 8;
                s106.Font.Color = "#666699";
                s106.Interior.Color = "#C0C0C0";
                s106.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s106.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s106.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s106.Borders.Add(LibExcelCS.StylePosition.Bottom, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s106.Borders.Add(LibExcelCS.StylePosition.Left, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s106.Borders.Add(LibExcelCS.StylePosition.Right, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s106.Borders.Add(LibExcelCS.StylePosition.Top, LibExcelCS.LineStyleOption.Continuous, 1, "#666699");
                s106.NumberFormat = "@";


                // -----------------------------------------------
                //  s107
                // -----------------------------------------------
                LibExcelCS.WorksheetStyle s107 = styles.Add("s107");
                s107.Font.Bold = true;
                s107.Font.FontName = "Arial";
                s107.Font.Size = 8;
                s107.Font.Color = "#666699";
                s107.Interior.Color = "#C0C0C0";
                s107.Interior.Pattern = LibExcelCS.StyleInteriorPattern.Solid;
                s107.Alignment.Horizontal = LibExcelCS.StyleHorizontalAlignment.Center;
                s107.Alignment.Vertical = LibExcelCS.StyleVerticalAlignment.Center;
                s107.NumberFormat = "@";
            }
            catch
            {

            }
        }

        #region "Exportar Excel Parametros de Sistema"
        public String GenerarExcelExportableParametroSistema(ParametrosBackOffieBEList oParametrosBackOffieBEList, String strCurrentDir)
        {
            String strFile = String.Empty;
            try
            {

                strFile = "Excel_Parametro_Sistema" + "_" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".xls";

                /********************  Eliminamos archivo con el cual se crear el archivo ********************/
                if (File.Exists(strCurrentDir + strFile))
                {
                    File.Delete(strCurrentDir + strFile);
                }
                /********************  Fin *******************************************************************/


                //-------------- Generar Excel ----------------------//
                LibExcelCS.Workbook book = new LibExcelCS.Workbook();
                // -----------------------------------------------
                //  Properties
                // -----------------------------------------------

                book.Properties.Created = new System.DateTime(2011, 9, 30, 5, 0, 4, 0);  //Fecha Creacion - Configurable
                book.Properties.LastSaved = new System.DateTime(2011, 9, 30, 17, 36, 20, 0);  //Fecha Salvada - Configurable
                book.Properties.Version = "12.00";
                book.ExcelWorkbook.WindowHeight = 9210;
                book.ExcelWorkbook.WindowWidth = 15480;
                book.ExcelWorkbook.WindowTopX = 120;
                book.ExcelWorkbook.WindowTopY = 75;
                book.ExcelWorkbook.ActiveSheetIndex = 0;
                book.ExcelWorkbook.ProtectWindows = false;
                book.ExcelWorkbook.ProtectStructure = false;
                // -----------------------------------------------
                //  Generate Styles
                // -----------------------------------------------
                this.GenerateStyles(book.Styles);
                // -----------------------------------------------
                //  Generate GenerateWorksheetSolicitudDesarme Worksheet
                // -----------------------------------------------
                this.GenerateWorksheetParametroSistema(oParametrosBackOffieBEList, book.Worksheets);

                book.Save(strCurrentDir + strFile);
                //-------------- //Generar Excel ----------------------//
            }
            catch
            {
                strFile = "-1";
            }
            return strFile;
        }
        private void GenerateWorksheetParametroSistema(ParametrosBackOffieBEList oParametrosBackOffieBEList, LibExcelCS.WorksheetCollection sheets)
        {
            try
            {
                #region "1era Hoja"

                Int32 intColumnasMostrar = 3, intFilasMostrar = 1;

                if (oParametrosBackOffieBEList != null)
                {
                    intFilasMostrar = intFilasMostrar + oParametrosBackOffieBEList.Count;
                }

                LibExcelCS.Worksheet sheet = sheets.Add("Hoja1"); //Configurable el nombre
                sheet.Table.DefaultColumnWidth = 81.75F;
                sheet.Table.ExpandedColumnCount = intColumnasMostrar;   //Columnas a mostrar mas una columna en blanco.
                sheet.Table.ExpandedRowCount = intFilasMostrar;  //Count de Filas de la Tabla más las filas de la cabecera mas 1 reglon en blanco.
                sheet.Table.FullColumns = 1;
                sheet.Table.FullRows = 1;
                sheet.Table.StyleID = "s65";
                // -----------------------------------------------

                LibExcelCS.WorksheetColumn column0 = sheet.Table.Columns.Add();
                column0.Width = 120;
                LibExcelCS.WorksheetColumn column1 = sheet.Table.Columns.Add();
                column1.Width = 300;
                LibExcelCS.WorksheetColumn column2 = sheet.Table.Columns.Add();
                column2.Width = 100;




                // -----------------------------------------------
                LibExcelCS.WorksheetRow Row0 = sheet.Table.Rows.Add();
                Row0.Height = 14;
                Row0.AutoFitHeight = false;


                Row0.Cells.Add("IDPARAMETRO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("PARAMETRO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("VALOR", LibExcelCS.DataType.String, "s69");


                LibExcelCS.WorksheetCell cell;

                // -----------------------------------------------

                ParametrosBackOfficeBE oParametrosBackOffieBE = null;

                if (oParametrosBackOffieBEList != null && oParametrosBackOffieBEList.Count > 0)
                {
                    for (int i = 0; i < oParametrosBackOffieBEList.Count; i++)
                    {
                        oParametrosBackOffieBE = new ParametrosBackOfficeBE();
                        oParametrosBackOffieBE = (ParametrosBackOfficeBE)oParametrosBackOffieBEList[i];

                        LibExcelCS.WorksheetRow Row2 = sheet.Table.Rows.Add();



                        /*co_parametro*/
                        if (oParametrosBackOffieBE.co_parametro != null)
                        {
                            if (oParametrosBackOffieBE.co_parametro.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oParametrosBackOffieBE.co_parametro.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*no_parametro*/
                        if (oParametrosBackOffieBE.no_parametro != null)
                        {
                            if (oParametrosBackOffieBE.no_parametro.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oParametrosBackOffieBE.no_parametro.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*valor_texto*/

                        if (oParametrosBackOffieBE.valor_texto != null)
                        {
                            if (oParametrosBackOffieBE.valor_texto.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oParametrosBackOffieBE.valor_texto.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        // -----------------------------------------------
                    }
                }

                //  Options
                // -----------------------------------------------
                sheet.Options.Selected = true;   //Va True para la Hoja Activa (la que se muestra)
                sheet.Options.ProtectObjects = false;
                sheet.Options.ProtectScenarios = false;
                sheet.Options.PageSetup.Header.Margin = 0F;
                sheet.Options.PageSetup.Footer.Margin = 0F;
                sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Left = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Right = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Top = 0.3937008F;
                sheet.Options.Print.ValidPrinterInfo = true;

                #endregion
            }
            catch
            {

            }
        }
        #endregion

        #region "Exportar Excel Usuario"
        public String GenerarExcelExportableUsuario(UsuarioBEList oMaestroUsuariosBEList, String strCurrentDir)
        {
            String strFile = String.Empty;
            try
            {

                strFile = "Excel_Usuario" + "_" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".xls";

                /********************  Eliminamos archivo con el cual se crear el archivo ********************/
                if (File.Exists(strCurrentDir + strFile))
                {
                    File.Delete(strCurrentDir + strFile);
                }
                /********************  Fin *******************************************************************/


                //-------------- Generar Excel ----------------------//
                LibExcelCS.Workbook book = new LibExcelCS.Workbook();
                // -----------------------------------------------
                //  Properties
                // -----------------------------------------------

                book.Properties.Created = new System.DateTime(2011, 9, 30, 5, 0, 4, 0);  //Fecha Creacion - Configurable
                book.Properties.LastSaved = new System.DateTime(2011, 9, 30, 17, 36, 20, 0);  //Fecha Salvada - Configurable
                book.Properties.Version = "12.00";
                book.ExcelWorkbook.WindowHeight = 9210;
                book.ExcelWorkbook.WindowWidth = 15480;
                book.ExcelWorkbook.WindowTopX = 120;
                book.ExcelWorkbook.WindowTopY = 75;
                book.ExcelWorkbook.ActiveSheetIndex = 0;
                book.ExcelWorkbook.ProtectWindows = false;
                book.ExcelWorkbook.ProtectStructure = false;
                // -----------------------------------------------
                //  Generate Styles
                // -----------------------------------------------
                this.GenerateStyles(book.Styles);
                // -----------------------------------------------
                //  Generate GenerateWorksheetSolicitudDesarme Worksheet
                // -----------------------------------------------
                this.GenerateWorksheetUsuario(oMaestroUsuariosBEList, book.Worksheets);

                book.Save(strCurrentDir + strFile);
                //-------------- //Generar Excel ----------------------//
            }
            catch
            {
                strFile = "-1";
            }
            return strFile;
        }
        private void GenerateWorksheetUsuario(UsuarioBEList oMaestroUsuariosBEList, LibExcelCS.WorksheetCollection sheets)
        {
            try
            {
                #region "1era Hoja"
                //@002 I
                //Int32 intColumnasMostrar = 11, intFilasMostrar = 1;
                Int32 intColumnasMostrar = 13, intFilasMostrar = 1;
                //@002 F

                if (oMaestroUsuariosBEList != null)
                {
                    intFilasMostrar = intFilasMostrar + oMaestroUsuariosBEList.Count;
                }

                LibExcelCS.Worksheet sheet = sheets.Add("Hoja1"); //Configurable el nombre
                sheet.Table.DefaultColumnWidth = 81.75F;
                sheet.Table.ExpandedColumnCount = intColumnasMostrar;   //Columnas a mostrar mas una columna en blanco.
                sheet.Table.ExpandedRowCount = intFilasMostrar;  //Count de Filas de la Tabla más las filas de la cabecera mas 1 reglon en blanco.
                sheet.Table.FullColumns = 1;
                sheet.Table.FullRows = 1;
                sheet.Table.StyleID = "s65";
                // -----------------------------------------------

                LibExcelCS.WorksheetColumn column0 = sheet.Table.Columns.Add();
                column0.Width = 100;
                LibExcelCS.WorksheetColumn column1 = sheet.Table.Columns.Add();
                column1.Width = 120;
                LibExcelCS.WorksheetColumn column2 = sheet.Table.Columns.Add();
                column2.Width = 120;
                LibExcelCS.WorksheetColumn column3 = sheet.Table.Columns.Add();
                column3.Width = 120;
                LibExcelCS.WorksheetColumn column4 = sheet.Table.Columns.Add();
                column4.Width = 120;

                LibExcelCS.WorksheetColumn column5 = sheet.Table.Columns.Add();
                column5.Width = 150;

                LibExcelCS.WorksheetColumn column6 = sheet.Table.Columns.Add();
                column6.Width = 120;
                LibExcelCS.WorksheetColumn column7 = sheet.Table.Columns.Add();
                column7.Width = 120;
                LibExcelCS.WorksheetColumn column8 = sheet.Table.Columns.Add();
                column8.Width = 180;

                LibExcelCS.WorksheetColumn column9 = sheet.Table.Columns.Add();
                column9.Width = 200;

                LibExcelCS.WorksheetColumn column10 = sheet.Table.Columns.Add();
                column10.Width = 100;


                // -----------------------------------------------
                LibExcelCS.WorksheetRow Row0 = sheet.Table.Rows.Add();
                Row0.Height = 14;
                Row0.AutoFitHeight = false;


                Row0.Cells.Add("NRO DOC", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("NOMBRE", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("AP PATERNO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("AP MATERNO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("USUARIO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("CORREO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("CELULAR", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("PERFIL", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("DEPARTAMENTO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("PROVINCIA", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("DISTRITO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("TALLER", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("ESTADO", LibExcelCS.DataType.String, "s69");


                LibExcelCS.WorksheetCell cell;

                // -----------------------------------------------

                UsuarioBE oMaestroUsuariosBE = null;

                if (oMaestroUsuariosBEList != null && oMaestroUsuariosBEList.Count > 0)
                {
                    for (int i = 0; i < oMaestroUsuariosBEList.Count; i++)
                    {
                        oMaestroUsuariosBE = new UsuarioBE();
                        oMaestroUsuariosBE = (UsuarioBE)oMaestroUsuariosBEList[i];

                        LibExcelCS.WorksheetRow Row2 = sheet.Table.Rows.Add();



                        /*Nu_tipo_documento*/
                        if (oMaestroUsuariosBE.Nu_tipo_documento != null)
                        {
                            if (oMaestroUsuariosBE.Nu_tipo_documento.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.Nu_tipo_documento.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*VNOMUSR*/
                        if (oMaestroUsuariosBE.VNOMUSR != null)
                        {
                            if (oMaestroUsuariosBE.VNOMUSR.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.VNOMUSR.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*No_ape_paterno*/

                        if (oMaestroUsuariosBE.No_ape_paterno != null)
                        {
                            if (oMaestroUsuariosBE.No_ape_paterno.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.No_ape_paterno.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*No_ape_materno*/

                        if (oMaestroUsuariosBE.No_ape_materno != null)
                        {
                            if (oMaestroUsuariosBE.No_ape_materno.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.No_ape_materno.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*CUSR_ID*/

                        if (oMaestroUsuariosBE.CUSR_ID != null)
                        {
                            if (oMaestroUsuariosBE.CUSR_ID.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.CUSR_ID.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }

                        //@002 I

                        /* Correo */

                        if (oMaestroUsuariosBE.va_correo != null)
                        {
                            if (oMaestroUsuariosBE.va_correo.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.va_correo.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }

                        /* Celular */
                        if (oMaestroUsuariosBE.nro_telf != null)
                        {
                            if (oMaestroUsuariosBE.nro_telf.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.nro_telf, LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }
                        //@002 F

                        /*Perfil*/

                        if (oMaestroUsuariosBE.Perfil != null)
                        {
                            if (oMaestroUsuariosBE.Perfil.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.Perfil.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*Dpto*/

                        if (oMaestroUsuariosBE.Dpto != null)
                        {
                            if (oMaestroUsuariosBE.Dpto.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.Dpto.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }

                        /*Prov*/

                        if (oMaestroUsuariosBE.Prov != null)
                        {
                            if (oMaestroUsuariosBE.Prov.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.Prov.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }

                        /*Dist*/

                        if (oMaestroUsuariosBE.Dist != null)
                        {
                            if (oMaestroUsuariosBE.Dist.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.Dist.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }

                        /*No_taller*/

                        if (oMaestroUsuariosBE.No_taller != null)
                        {
                            if (oMaestroUsuariosBE.No_taller.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.No_taller.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }

                        /*Fl_activo*/

                        if (oMaestroUsuariosBE.Fl_activo != null)
                        {
                            if (oMaestroUsuariosBE.Fl_activo.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroUsuariosBE.Fl_activo.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        // -----------------------------------------------
                    }
                }

                //  Options
                // -----------------------------------------------
                sheet.Options.Selected = true;   //Va True para la Hoja Activa (la que se muestra)
                sheet.Options.ProtectObjects = false;
                sheet.Options.ProtectScenarios = false;
                sheet.Options.PageSetup.Header.Margin = 0F;
                sheet.Options.PageSetup.Footer.Margin = 0F;
                sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Left = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Right = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Top = 0.3937008F;
                sheet.Options.Print.ValidPrinterInfo = true;

                #endregion
            }
            catch
            {

            }
        }
        #endregion


        #region "Exportar Excel Tipo Servicio"
        public String GenerarExcelExportarTipoServicio(TipoServicioBEList oMaestroTipoServicioBEList, String strCurrentDir)
        {
            String strFile = String.Empty;
            try
            {

                strFile = "Excel_Tipo_Servicio" + "_" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".xls";

                /********************  Eliminamos archivo con el cual se crear el archivo ********************/
                if (File.Exists(strCurrentDir + strFile))
                {
                    File.Delete(strCurrentDir + strFile);
                }
                /********************  Fin *******************************************************************/


                //-------------- Generar Excel ----------------------//
                LibExcelCS.Workbook book = new LibExcelCS.Workbook();
                // -----------------------------------------------
                //  Properties
                // -----------------------------------------------

                book.Properties.Created = new System.DateTime(2011, 9, 30, 5, 0, 4, 0);  //Fecha Creacion - Configurable
                book.Properties.LastSaved = new System.DateTime(2011, 9, 30, 17, 36, 20, 0);  //Fecha Salvada - Configurable
                book.Properties.Version = "12.00";
                book.ExcelWorkbook.WindowHeight = 9210;
                book.ExcelWorkbook.WindowWidth = 15480;
                book.ExcelWorkbook.WindowTopX = 120;
                book.ExcelWorkbook.WindowTopY = 75;
                book.ExcelWorkbook.ActiveSheetIndex = 0;
                book.ExcelWorkbook.ProtectWindows = false;
                book.ExcelWorkbook.ProtectStructure = false;
                // -----------------------------------------------
                //  Generate Styles
                // -----------------------------------------------
                this.GenerateStyles(book.Styles);
                // -----------------------------------------------
                //  Generate GenerateWorksheetSolicitudDesarme Worksheet
                // -----------------------------------------------
                this.GenerateWorksheetTipoServicio(oMaestroTipoServicioBEList, book.Worksheets);

                book.Save(strCurrentDir + strFile);
                //-------------- //Generar Excel ----------------------//
            }
            catch
            {
                strFile = "-1";
            }
            return strFile;
        }
        private void GenerateWorksheetTipoServicio(TipoServicioBEList oMaestroTipoServicioBEList, LibExcelCS.WorksheetCollection sheets)
        {
            try
            {
                #region "1era Hoja"

                Int32 intColumnasMostrar = 3, intFilasMostrar = 1;

                if (oMaestroTipoServicioBEList != null)
                {
                    intFilasMostrar = intFilasMostrar + oMaestroTipoServicioBEList.Count;
                }

                LibExcelCS.Worksheet sheet = sheets.Add("Hoja1"); //Configurable el nombre
                sheet.Table.DefaultColumnWidth = 81.75F;
                sheet.Table.ExpandedColumnCount = intColumnasMostrar;   //Columnas a mostrar mas una columna en blanco.
                sheet.Table.ExpandedRowCount = intFilasMostrar;  //Count de Filas de la Tabla más las filas de la cabecera mas 1 reglon en blanco.
                sheet.Table.FullColumns = 1;
                sheet.Table.FullRows = 1;
                sheet.Table.StyleID = "s65";
                // -----------------------------------------------

                LibExcelCS.WorksheetColumn column0 = sheet.Table.Columns.Add();
                column0.Width = 60;
                LibExcelCS.WorksheetColumn column1 = sheet.Table.Columns.Add();
                column1.Width = 150;
                LibExcelCS.WorksheetColumn column2 = sheet.Table.Columns.Add();
                column2.Width = 60;



                // -----------------------------------------------
                LibExcelCS.WorksheetRow Row0 = sheet.Table.Rows.Add();
                Row0.Height = 14;
                Row0.AutoFitHeight = false;


                Row0.Cells.Add("CODIGO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("NOMBRE_TIPO_SERVICIO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("ESTADO", LibExcelCS.DataType.String, "s69");


                LibExcelCS.WorksheetCell cell;

                // -----------------------------------------------

                TipoServicioBE oMaestroTipoServicioBE = null;

                if (oMaestroTipoServicioBEList != null && oMaestroTipoServicioBEList.Count > 0)
                {
                    for (int i = 0; i < oMaestroTipoServicioBEList.Count; i++)
                    {
                        oMaestroTipoServicioBE = new TipoServicioBE();
                        oMaestroTipoServicioBE = (TipoServicioBE)oMaestroTipoServicioBEList[i];

                        LibExcelCS.WorksheetRow Row2 = sheet.Table.Rows.Add();



                        /*Co_tipo_servicio*/
                        if (oMaestroTipoServicioBE.Co_tipo_servicio != null)
                        {
                            if (oMaestroTipoServicioBE.Co_tipo_servicio.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTipoServicioBE.Co_tipo_servicio.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*No_tipo_servicio*/
                        if (oMaestroTipoServicioBE.No_tipo_servicio != null)
                        {
                            if (oMaestroTipoServicioBE.No_tipo_servicio.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTipoServicioBE.No_tipo_servicio.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*Fl_activo*/

                        if (oMaestroTipoServicioBE.Fl_activo != null)
                        {
                            if (oMaestroTipoServicioBE.Fl_activo.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTipoServicioBE.Fl_activo.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        // -----------------------------------------------
                    }
                }

                //  Options
                // -----------------------------------------------
                sheet.Options.Selected = true;   //Va True para la Hoja Activa (la que se muestra)
                sheet.Options.ProtectObjects = false;
                sheet.Options.ProtectScenarios = false;
                sheet.Options.PageSetup.Header.Margin = 0F;
                sheet.Options.PageSetup.Footer.Margin = 0F;
                sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Left = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Right = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Top = 0.3937008F;
                sheet.Options.Print.ValidPrinterInfo = true;

                #endregion
            }
            catch
            {

            }
        }
        #endregion

        #region "Exportar Excel Servicio"
        public String GenerarExcelExportarServicio(ServicioBEList oMaestroServicioBEList, String strCurrentDir)
        {
            String strFile = String.Empty;
            try
            {

                strFile = "Excel_Servicio" + "_" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".xls";

                /********************  Eliminamos archivo con el cual se crear el archivo ********************/
                if (File.Exists(strCurrentDir + strFile))
                {
                    File.Delete(strCurrentDir + strFile);
                }
                /********************  Fin *******************************************************************/


                //-------------- Generar Excel ----------------------//
                LibExcelCS.Workbook book = new LibExcelCS.Workbook();
                // -----------------------------------------------
                //  Properties
                // -----------------------------------------------

                book.Properties.Created = new System.DateTime(2011, 9, 30, 5, 0, 4, 0);  //Fecha Creacion - Configurable
                book.Properties.LastSaved = new System.DateTime(2011, 9, 30, 17, 36, 20, 0);  //Fecha Salvada - Configurable
                book.Properties.Version = "12.00";
                book.ExcelWorkbook.WindowHeight = 9210;
                book.ExcelWorkbook.WindowWidth = 15480;
                book.ExcelWorkbook.WindowTopX = 120;
                book.ExcelWorkbook.WindowTopY = 75;
                book.ExcelWorkbook.ActiveSheetIndex = 0;
                book.ExcelWorkbook.ProtectWindows = false;
                book.ExcelWorkbook.ProtectStructure = false;
                // -----------------------------------------------
                //  Generate Styles
                // -----------------------------------------------
                this.GenerateStyles(book.Styles);
                // -----------------------------------------------
                //  Generate GenerateWorksheetSolicitudDesarme Worksheet
                // -----------------------------------------------
                this.GenerateWorksheetServicio(oMaestroServicioBEList, book.Worksheets);

                book.Save(strCurrentDir + strFile);
                //-------------- //Generar Excel ----------------------//
            }
            catch
            {
                strFile = "-1";
            }
            return strFile;
        }
        private void GenerateWorksheetServicio(ServicioBEList oMaestroServicioBEList, LibExcelCS.WorksheetCollection sheets)
        {
            try
            {
                #region "1era Hoja"

                Int32 intColumnasMostrar = 5, intFilasMostrar = 1;

                if (oMaestroServicioBEList != null)
                {
                    intFilasMostrar = intFilasMostrar + oMaestroServicioBEList.Count;
                }

                LibExcelCS.Worksheet sheet = sheets.Add("Hoja1"); //Configurable el nombre
                sheet.Table.DefaultColumnWidth = 81.75F;
                sheet.Table.ExpandedColumnCount = intColumnasMostrar;   //Columnas a mostrar mas una columna en blanco.
                sheet.Table.ExpandedRowCount = intFilasMostrar;  //Count de Filas de la Tabla más las filas de la cabecera mas 1 reglon en blanco.
                sheet.Table.FullColumns = 1;
                sheet.Table.FullRows = 1;
                sheet.Table.StyleID = "s65";
                // -----------------------------------------------

                LibExcelCS.WorksheetColumn column0 = sheet.Table.Columns.Add();
                column0.Width = 60;
                LibExcelCS.WorksheetColumn column1 = sheet.Table.Columns.Add();
                column1.Width = 100;
                LibExcelCS.WorksheetColumn column2 = sheet.Table.Columns.Add();
                column2.Width = 150;

                LibExcelCS.WorksheetColumn column3 = sheet.Table.Columns.Add();
                column3.Width = 100;
                LibExcelCS.WorksheetColumn column4 = sheet.Table.Columns.Add();
                column4.Width = 60;


                // -----------------------------------------------
                LibExcelCS.WorksheetRow Row0 = sheet.Table.Rows.Add();
                Row0.Height = 14;
                Row0.AutoFitHeight = false;


                Row0.Cells.Add("CODIGO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("NOMBRE SERVICIO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("TIPO SERVICIO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("TIEMPO PROM", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("ESTADO", LibExcelCS.DataType.String, "s69");


                LibExcelCS.WorksheetCell cell;

                // -----------------------------------------------

                ServicioBE oMaestroServicioBE = null;

                if (oMaestroServicioBEList != null && oMaestroServicioBEList.Count > 0)
                {
                    for (int i = 0; i < oMaestroServicioBEList.Count; i++)
                    {
                        oMaestroServicioBE = new ServicioBE();
                        oMaestroServicioBE = (ServicioBE)oMaestroServicioBEList[i];

                        LibExcelCS.WorksheetRow Row2 = sheet.Table.Rows.Add();



                        /*Co_Servicio*/
                        if (oMaestroServicioBE.Co_Servicio != null)
                        {
                            if (oMaestroServicioBE.Co_Servicio.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroServicioBE.Co_Servicio.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*No_Servicio*/
                        if (oMaestroServicioBE.No_Servicio != null)
                        {
                            if (oMaestroServicioBE.No_Servicio.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroServicioBE.No_Servicio.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*No_tipo_servicio*/
                        if (oMaestroServicioBE.No_tipo_servicio != null)
                        {
                            if (oMaestroServicioBE.No_tipo_servicio.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroServicioBE.No_tipo_servicio.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*Qt_tiempo_prom*/
                        if (!oMaestroServicioBE.Qt_tiempo_prom.ToString().Trim().Equals(String.Empty))
                        {
                            if (oMaestroServicioBE.Qt_tiempo_prom.ToString().Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s71";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroServicioBE.Qt_tiempo_prom.ToString().Trim(), LibExcelCS.DataType.String, "s71");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s71";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*Fl_activo*/

                        if (oMaestroServicioBE.Fl_activo != null)
                        {
                            if (oMaestroServicioBE.Fl_activo.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroServicioBE.Fl_activo.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        // -----------------------------------------------
                    }
                }

                //  Options
                // -----------------------------------------------
                sheet.Options.Selected = true;   //Va True para la Hoja Activa (la que se muestra)
                sheet.Options.ProtectObjects = false;
                sheet.Options.ProtectScenarios = false;
                sheet.Options.PageSetup.Header.Margin = 0F;
                sheet.Options.PageSetup.Footer.Margin = 0F;
                sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Left = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Right = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Top = 0.3937008F;
                sheet.Options.Print.ValidPrinterInfo = true;

                #endregion
            }
            catch
            {

            }
        }
        #endregion

        #region "Exportar Excel Modelo"
        public String GenerarExcelExportarModelo(ModeloBEList oModeloBEList, String strCurrentDir)
        {
            String strFile = String.Empty;
            try
            {

                strFile = "Excel_Modelo" + "_" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".xls";

                /********************  Eliminamos archivo con el cual se crear el archivo ********************/
                if (File.Exists(strCurrentDir + strFile))
                {
                    File.Delete(strCurrentDir + strFile);
                }
                /********************  Fin *******************************************************************/


                //-------------- Generar Excel ----------------------//
                LibExcelCS.Workbook book = new LibExcelCS.Workbook();
                // -----------------------------------------------
                //  Properties
                // -----------------------------------------------

                book.Properties.Created = new System.DateTime(2011, 9, 30, 5, 0, 4, 0);  //Fecha Creacion - Configurable
                book.Properties.LastSaved = new System.DateTime(2011, 9, 30, 17, 36, 20, 0);  //Fecha Salvada - Configurable
                book.Properties.Version = "12.00";
                book.ExcelWorkbook.WindowHeight = 9210;
                book.ExcelWorkbook.WindowWidth = 15480;
                book.ExcelWorkbook.WindowTopX = 120;
                book.ExcelWorkbook.WindowTopY = 75;
                book.ExcelWorkbook.ActiveSheetIndex = 0;
                book.ExcelWorkbook.ProtectWindows = false;
                book.ExcelWorkbook.ProtectStructure = false;
                // -----------------------------------------------
                //  Generate Styles
                // -----------------------------------------------
                this.GenerateStyles(book.Styles);
                // -----------------------------------------------
                //  Generate GenerateWorksheetSolicitudDesarme Worksheet
                // -----------------------------------------------
                this.GenerateWorksheetModelo(oModeloBEList, book.Worksheets);

                book.Save(strCurrentDir + strFile);
                //-------------- //Generar Excel ----------------------//
            }
            catch
            {
                strFile = "-1";
            }
            return strFile;
        }
        private void GenerateWorksheetModelo(ModeloBEList oModeloBEList, LibExcelCS.WorksheetCollection sheets)
        {
            try
            {
                #region "1era Hoja"

                Int32 intColumnasMostrar = 6, intFilasMostrar = 1;

                if (oModeloBEList != null)
                {
                    intFilasMostrar = intFilasMostrar + oModeloBEList.Count;
                }

                LibExcelCS.Worksheet sheet = sheets.Add("Hoja1"); //Configurable el nombre
                sheet.Table.DefaultColumnWidth = 81.75F;
                sheet.Table.ExpandedColumnCount = intColumnasMostrar;   //Columnas a mostrar mas una columna en blanco.
                sheet.Table.ExpandedRowCount = intFilasMostrar;  //Count de Filas de la Tabla más las filas de la cabecera mas 1 reglon en blanco.
                sheet.Table.FullColumns = 1;
                sheet.Table.FullRows = 1;
                sheet.Table.StyleID = "s65";
                // -----------------------------------------------

                LibExcelCS.WorksheetColumn column0 = sheet.Table.Columns.Add();
                column0.Width = 150;
                LibExcelCS.WorksheetColumn column1 = sheet.Table.Columns.Add();
                column1.Width = 60;
                LibExcelCS.WorksheetColumn column2 = sheet.Table.Columns.Add();
                column2.Width = 150;

                LibExcelCS.WorksheetColumn column3 = sheet.Table.Columns.Add();
                column3.Width = 100;
                LibExcelCS.WorksheetColumn column4 = sheet.Table.Columns.Add();
                column4.Width = 200;
                LibExcelCS.WorksheetColumn column5 = sheet.Table.Columns.Add();
                column5.Width = 60;


                // -----------------------------------------------
                LibExcelCS.WorksheetRow Row0 = sheet.Table.Rows.Add();
                Row0.Height = 14;
                Row0.AutoFitHeight = false;


                Row0.Cells.Add("MARCA", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("CODIGO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("NOMBRE", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("NEGOCIO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("FAMILIA", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("ESTADO", LibExcelCS.DataType.String, "s69");


                LibExcelCS.WorksheetCell cell;

                // -----------------------------------------------

                ModeloBE oModeloBE = null;

                if (oModeloBEList != null && oModeloBEList.Count > 0)
                {
                    for (int i = 0; i < oModeloBEList.Count; i++)
                    {
                        oModeloBE = new ModeloBE();
                        oModeloBE = (ModeloBE)oModeloBEList[i];

                        LibExcelCS.WorksheetRow Row2 = sheet.Table.Rows.Add();



                        /*Co_Servicio*/
                        if (oModeloBE.no_marca != null)
                        {
                            if (oModeloBE.no_marca.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oModeloBE.no_marca.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*co_modelo*/
                        if (oModeloBE.co_modelo != null)
                        {
                            if (oModeloBE.co_modelo.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s71";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oModeloBE.co_modelo.Trim(), LibExcelCS.DataType.String, "s71");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s71";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*no_modelo*/
                        if (oModeloBE.no_modelo != null)
                        {
                            if (oModeloBE.no_modelo.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oModeloBE.no_modelo.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*no_negocio*/
                        if (oModeloBE.no_negocio != null)
                        {
                            if (oModeloBE.no_negocio.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oModeloBE.no_negocio.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*no_familia*/
                        if (oModeloBE.no_familia != null)
                        {
                            if (oModeloBE.no_familia.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oModeloBE.no_familia.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }

                        if (oModeloBE.estado != null)
                        {
                            if (oModeloBE.estado.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oModeloBE.estado.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        // -----------------------------------------------
                    }
                }

                //  Options
                // -----------------------------------------------
                sheet.Options.Selected = true;   //Va True para la Hoja Activa (la que se muestra)
                sheet.Options.ProtectObjects = false;
                sheet.Options.ProtectScenarios = false;
                sheet.Options.PageSetup.Header.Margin = 0F;
                sheet.Options.PageSetup.Footer.Margin = 0F;
                sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Left = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Right = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Top = 0.3937008F;
                sheet.Options.Print.ValidPrinterInfo = true;

                #endregion
            }
            catch
            {

            }
        }
        #endregion


        #region "Exportar Excel Vehiculos"
        public String GenerarExcelExportarVehiculo(VehiculoBEList oMaestroVehiculoBEList, String strCurrentDir)
        {
            String strFile = String.Empty;
            try
            {

                strFile = "Excel_Vehiculo" + "_" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".xls";

                /********************  Eliminamos archivo con el cual se crear el archivo ********************/
                if (File.Exists(strCurrentDir + strFile))
                {
                    File.Delete(strCurrentDir + strFile);
                }
                /********************  Fin *******************************************************************/


                //-------------- Generar Excel ----------------------//
                LibExcelCS.Workbook book = new LibExcelCS.Workbook();
                // -----------------------------------------------
                //  Properties
                // -----------------------------------------------

                book.Properties.Created = new System.DateTime(2011, 9, 30, 5, 0, 4, 0);  //Fecha Creacion - Configurable
                book.Properties.LastSaved = new System.DateTime(2011, 9, 30, 17, 36, 20, 0);  //Fecha Salvada - Configurable
                book.Properties.Version = "12.00";
                book.ExcelWorkbook.WindowHeight = 9210;
                book.ExcelWorkbook.WindowWidth = 15480;
                book.ExcelWorkbook.WindowTopX = 120;
                book.ExcelWorkbook.WindowTopY = 75;
                book.ExcelWorkbook.ActiveSheetIndex = 0;
                book.ExcelWorkbook.ProtectWindows = false;
                book.ExcelWorkbook.ProtectStructure = false;
                // -----------------------------------------------
                //  Generate Styles
                // -----------------------------------------------
                this.GenerateStyles(book.Styles);
                // -----------------------------------------------
                //  Generate GenerateWorksheetSolicitudDesarme Worksheet
                // -----------------------------------------------
                this.GenerateWorksheetVehiculo(oMaestroVehiculoBEList, book.Worksheets);

                book.Save(strCurrentDir + strFile);
                //-------------- //Generar Excel ----------------------//
            }
            catch
            {
                strFile = "-1";
            }
            return strFile;
        }
        private void GenerateWorksheetVehiculo(VehiculoBEList oMaestroVehiculoBEList, LibExcelCS.WorksheetCollection sheets)
        {
            try
            {
                #region "1era Hoja"

                Int32 intColumnasMostrar = 6, intFilasMostrar = 1;

                if (oMaestroVehiculoBEList != null)
                {
                    intFilasMostrar = intFilasMostrar + oMaestroVehiculoBEList.Count;
                }

                LibExcelCS.Worksheet sheet = sheets.Add("Hoja1"); //Configurable el nombre
                sheet.Table.DefaultColumnWidth = 81.75F;
                sheet.Table.ExpandedColumnCount = intColumnasMostrar;   //Columnas a mostrar mas una columna en blanco.
                sheet.Table.ExpandedRowCount = intFilasMostrar;  //Count de Filas de la Tabla más las filas de la cabecera mas 1 reglon en blanco.
                sheet.Table.FullColumns = 1;
                sheet.Table.FullRows = 1;
                sheet.Table.StyleID = "s65";
                // -----------------------------------------------

                LibExcelCS.WorksheetColumn column0 = sheet.Table.Columns.Add();
                column0.Width = 60;
                LibExcelCS.WorksheetColumn column1 = sheet.Table.Columns.Add();
                column1.Width = 100;
                LibExcelCS.WorksheetColumn column2 = sheet.Table.Columns.Add();
                column2.Width = 100;

                LibExcelCS.WorksheetColumn column3 = sheet.Table.Columns.Add();
                column3.Width = 120;
                LibExcelCS.WorksheetColumn column4 = sheet.Table.Columns.Add();
                column4.Width = 80;
                LibExcelCS.WorksheetColumn column5 = sheet.Table.Columns.Add();
                column5.Width = 60;


                // -----------------------------------------------
                LibExcelCS.WorksheetRow Row0 = sheet.Table.Rows.Add();
                Row0.Height = 14;
                Row0.AutoFitHeight = false;


                Row0.Cells.Add("PLACA", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("NRO VIN", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("MARCA", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("MODELO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("KILOMETRAJE", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("ESTADO", LibExcelCS.DataType.String, "s69");


                LibExcelCS.WorksheetCell cell;

                // -----------------------------------------------

                VehiculoBE oMaestroVehiculoBE = null;

                if (oMaestroVehiculoBEList != null && oMaestroVehiculoBEList.Count > 0)
                {
                    for (int i = 0; i < oMaestroVehiculoBEList.Count; i++)
                    {
                        oMaestroVehiculoBE = new VehiculoBE();
                        oMaestroVehiculoBE = (VehiculoBE)oMaestroVehiculoBEList[i];

                        LibExcelCS.WorksheetRow Row2 = sheet.Table.Rows.Add();



                        /*nu_placa*/
                        if (oMaestroVehiculoBE.nu_placa != null)
                        {
                            if (oMaestroVehiculoBE.nu_placa.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroVehiculoBE.nu_placa.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*nu_vin*/
                        if (oMaestroVehiculoBE.nu_vin != null)
                        {
                            if (oMaestroVehiculoBE.nu_vin.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroVehiculoBE.nu_vin.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*no_marca*/
                        if (oMaestroVehiculoBE.no_marca != null)
                        {
                            if (oMaestroVehiculoBE.no_marca.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroVehiculoBE.no_marca.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*no_negocio*/
                        if (oMaestroVehiculoBE.no_modelo != null)
                        {
                            if (oMaestroVehiculoBE.no_modelo.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroVehiculoBE.no_modelo.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*qt_km_actual*/
                        if (!oMaestroVehiculoBE.qt_km_actual.ToString().Trim().Equals(String.Empty))
                        {
                            if (oMaestroVehiculoBE.qt_km_actual.ToString().Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s71";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroVehiculoBE.qt_km_actual.ToString().Trim(), LibExcelCS.DataType.String, "s71");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s71";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*Estado*/

                        if (oMaestroVehiculoBE.fl_activo != null)
                        {
                            if (oMaestroVehiculoBE.fl_activo.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroVehiculoBE.fl_activo.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        // -----------------------------------------------
                    }
                }

                //  Options
                // -----------------------------------------------
                sheet.Options.Selected = true;   //Va True para la Hoja Activa (la que se muestra)
                sheet.Options.ProtectObjects = false;
                sheet.Options.ProtectScenarios = false;
                sheet.Options.PageSetup.Header.Margin = 0F;
                sheet.Options.PageSetup.Footer.Margin = 0F;
                sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Left = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Right = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Top = 0.3937008F;
                sheet.Options.Print.ValidPrinterInfo = true;

                #endregion
            }
            catch
            {

            }
        }
        #endregion

        #region "Exportar Excel Clientes"
        public String GenerarExcelExportarCliente(ClienteBEList oMestroClienteBEList, String strCurrentDir)
        {
            String strFile = String.Empty;
            try
            {

                strFile = "Excel_Cliente" + "_" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".xls";

                /********************  Eliminamos archivo con el cual se crear el archivo ********************/
                if (File.Exists(strCurrentDir + strFile))
                {
                    File.Delete(strCurrentDir + strFile);
                }
                /********************  Fin *******************************************************************/


                //-------------- Generar Excel ----------------------//
                LibExcelCS.Workbook book = new LibExcelCS.Workbook();
                // -----------------------------------------------
                //  Properties
                // -----------------------------------------------

                book.Properties.Created = new System.DateTime(2011, 9, 30, 5, 0, 4, 0);  //Fecha Creacion - Configurable
                book.Properties.LastSaved = new System.DateTime(2011, 9, 30, 17, 36, 20, 0);  //Fecha Salvada - Configurable
                book.Properties.Version = "12.00";
                book.ExcelWorkbook.WindowHeight = 9210;
                book.ExcelWorkbook.WindowWidth = 15480;
                book.ExcelWorkbook.WindowTopX = 120;
                book.ExcelWorkbook.WindowTopY = 75;
                book.ExcelWorkbook.ActiveSheetIndex = 0;
                book.ExcelWorkbook.ProtectWindows = false;
                book.ExcelWorkbook.ProtectStructure = false;
                // -----------------------------------------------
                //  Generate Styles
                // -----------------------------------------------
                this.GenerateStyles(book.Styles);
                // -----------------------------------------------
                //  Generate GenerateWorksheetSolicitudDesarme Worksheet
                // -----------------------------------------------
                this.GenerateWorksheetCliente(oMestroClienteBEList, book.Worksheets);

                book.Save(strCurrentDir + strFile);
                //-------------- //Generar Excel ----------------------//
            }
            catch
            {
                strFile = "-1";
            }
            return strFile;
        }
        private void GenerateWorksheetCliente(ClienteBEList oMestroClienteBEList, LibExcelCS.WorksheetCollection sheets)
        {
            try
            {
                #region "1era Hoja"

                Int32 intColumnasMostrar = 7, intFilasMostrar = 1;

                if (oMestroClienteBEList != null)
                {
                    intFilasMostrar = intFilasMostrar + oMestroClienteBEList.Count;
                }

                LibExcelCS.Worksheet sheet = sheets.Add("Hoja1"); //Configurable el nombre
                sheet.Table.DefaultColumnWidth = 81.75F;
                sheet.Table.ExpandedColumnCount = intColumnasMostrar;   //Columnas a mostrar mas una columna en blanco.
                sheet.Table.ExpandedRowCount = intFilasMostrar;  //Count de Filas de la Tabla más las filas de la cabecera mas 1 reglon en blanco.
                sheet.Table.FullColumns = 1;
                sheet.Table.FullRows = 1;
                sheet.Table.StyleID = "s65";
                // -----------------------------------------------

                LibExcelCS.WorksheetColumn column0 = sheet.Table.Columns.Add();
                column0.Width = 60;
                LibExcelCS.WorksheetColumn column1 = sheet.Table.Columns.Add();
                column1.Width = 100;
                LibExcelCS.WorksheetColumn column2 = sheet.Table.Columns.Add();
                column2.Width = 120;

                LibExcelCS.WorksheetColumn column3 = sheet.Table.Columns.Add();
                column3.Width = 120;
                LibExcelCS.WorksheetColumn column4 = sheet.Table.Columns.Add();
                column4.Width = 80;
                LibExcelCS.WorksheetColumn column5 = sheet.Table.Columns.Add();
                column5.Width = 80;
                LibExcelCS.WorksheetColumn column6 = sheet.Table.Columns.Add();
                column6.Width = 80;

                // -----------------------------------------------
                LibExcelCS.WorksheetRow Row0 = sheet.Table.Rows.Add();
                Row0.Height = 14;
                Row0.AutoFitHeight = false;


                Row0.Cells.Add("CODIGO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("NOMBRE", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("AP PATERNO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("AP MATERNO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("TIPO DOC", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("NRO DOC", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("ESTADO", LibExcelCS.DataType.String, "s69");


                LibExcelCS.WorksheetCell cell;

                // -----------------------------------------------

                ClienteBE oMestroClienteBE = null;

                if (oMestroClienteBEList != null && oMestroClienteBEList.Count > 0)
                {
                    for (int i = 0; i < oMestroClienteBEList.Count; i++)
                    {
                        oMestroClienteBE = new ClienteBE();
                        oMestroClienteBE = (ClienteBE)oMestroClienteBEList[i];

                        LibExcelCS.WorksheetRow Row2 = sheet.Table.Rows.Add();



                        /*nid_cliente*/
                        if (!oMestroClienteBE.nid_cliente.ToString().Trim().Equals(String.Empty))
                        {
                            if (oMestroClienteBE.nid_cliente.ToString().Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMestroClienteBE.nid_cliente.ToString().Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*no_cliente*/
                        if (oMestroClienteBE.no_cliente != null)
                        {
                            if (oMestroClienteBE.no_cliente.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMestroClienteBE.no_cliente.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*no_ape_pat*/
                        if (oMestroClienteBE.no_ape_pat != null)
                        {
                            if (oMestroClienteBE.no_ape_pat.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMestroClienteBE.no_ape_pat.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*no_ape_mat*/
                        if (oMestroClienteBE.no_ape_mat != null)
                        {
                            if (oMestroClienteBE.no_ape_mat.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMestroClienteBE.no_ape_mat.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*Documento*/
                        if (oMestroClienteBE.Documento != null)
                        {
                            if (oMestroClienteBE.Documento.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s71";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMestroClienteBE.Documento.Trim(), LibExcelCS.DataType.String, "s71");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s71";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*nu_documento*/
                        if (oMestroClienteBE.nu_documento != null)
                        {
                            if (oMestroClienteBE.nu_documento.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMestroClienteBE.nu_documento.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }




                        /*Estado*/

                        if (oMestroClienteBE.Estado != null)
                        {
                            if (oMestroClienteBE.Estado.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMestroClienteBE.Estado.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        // -----------------------------------------------
                    }
                }

                //  Options
                // -----------------------------------------------
                sheet.Options.Selected = true;   //Va True para la Hoja Activa (la que se muestra)
                sheet.Options.ProtectObjects = false;
                sheet.Options.ProtectScenarios = false;
                sheet.Options.PageSetup.Header.Margin = 0F;
                sheet.Options.PageSetup.Footer.Margin = 0F;
                sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Left = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Right = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Top = 0.3937008F;
                sheet.Options.Print.ValidPrinterInfo = true;

                #endregion
            }
            catch
            {

            }
        }
        #endregion

        #region "Exportar Excel Talleres"
        public String GenerarExcelExportarTaller(TallerBEList oMaestroTallerBEList, String strCurrentDir)
        {
            String strFile = String.Empty;
            try
            {

                strFile = "Excel_Taller" + "_" + String.Format("{0:yyyyMMddHHmmss}", DateTime.Now) + ".xls";

                /********************  Eliminamos archivo con el cual se crear el archivo ********************/
                if (File.Exists(strCurrentDir + strFile))
                {
                    File.Delete(strCurrentDir + strFile);
                }
                /********************  Fin *******************************************************************/


                //-------------- Generar Excel ----------------------//
                LibExcelCS.Workbook book = new LibExcelCS.Workbook();
                // -----------------------------------------------
                //  Properties
                // -----------------------------------------------

                book.Properties.Created = new System.DateTime(2011, 9, 30, 5, 0, 4, 0);  //Fecha Creacion - Configurable
                book.Properties.LastSaved = new System.DateTime(2011, 9, 30, 17, 36, 20, 0);  //Fecha Salvada - Configurable
                book.Properties.Version = "12.00";
                book.ExcelWorkbook.WindowHeight = 9210;
                book.ExcelWorkbook.WindowWidth = 15480;
                book.ExcelWorkbook.WindowTopX = 120;
                book.ExcelWorkbook.WindowTopY = 75;
                book.ExcelWorkbook.ActiveSheetIndex = 0;
                book.ExcelWorkbook.ProtectWindows = false;
                book.ExcelWorkbook.ProtectStructure = false;
                // -----------------------------------------------
                //  Generate Styles
                // -----------------------------------------------
                this.GenerateStyles(book.Styles);
                // -----------------------------------------------
                //  Generate GenerateWorksheetSolicitudDesarme Worksheet
                // -----------------------------------------------
                this.GenerateWorksheetTaller(oMaestroTallerBEList, book.Worksheets);

                book.Save(strCurrentDir + strFile);
                //-------------- //Generar Excel ----------------------//
            }
            catch
            {
                strFile = "-1";
            }
            return strFile;
        }
        private void GenerateWorksheetTaller(TallerBEList oMaestroTallerBEList, LibExcelCS.WorksheetCollection sheets)
        {
            try
            {
                #region "1era Hoja"

                Int32 intColumnasMostrar = 7, intFilasMostrar = 1;

                if (oMaestroTallerBEList != null)
                {
                    intFilasMostrar = intFilasMostrar + oMaestroTallerBEList.Count;
                }

                LibExcelCS.Worksheet sheet = sheets.Add("Hoja1"); //Configurable el nombre
                sheet.Table.DefaultColumnWidth = 81.75F;
                sheet.Table.ExpandedColumnCount = intColumnasMostrar;   //Columnas a mostrar mas una columna en blanco.
                sheet.Table.ExpandedRowCount = intFilasMostrar;  //Count de Filas de la Tabla más las filas de la cabecera mas 1 reglon en blanco.
                sheet.Table.FullColumns = 1;
                sheet.Table.FullRows = 1;
                sheet.Table.StyleID = "s65";
                // -----------------------------------------------

                LibExcelCS.WorksheetColumn column0 = sheet.Table.Columns.Add();
                column0.Width = 60;
                LibExcelCS.WorksheetColumn column1 = sheet.Table.Columns.Add();
                column1.Width = 200;
                LibExcelCS.WorksheetColumn column2 = sheet.Table.Columns.Add();
                column2.Width = 100;

                LibExcelCS.WorksheetColumn column3 = sheet.Table.Columns.Add();
                column3.Width = 100;
                LibExcelCS.WorksheetColumn column4 = sheet.Table.Columns.Add();
                column4.Width = 150;
                LibExcelCS.WorksheetColumn column5 = sheet.Table.Columns.Add();
                column5.Width = 200;
                LibExcelCS.WorksheetColumn column6 = sheet.Table.Columns.Add();
                column6.Width = 80;

                // -----------------------------------------------
                LibExcelCS.WorksheetRow Row0 = sheet.Table.Rows.Add();
                Row0.Height = 14;
                Row0.AutoFitHeight = false;


                Row0.Cells.Add("CODTALLER", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("NOMBRE COMPLETO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("DEPARTAMENTO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("PROVINCIA", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("DISTRITO", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("LOCAL", LibExcelCS.DataType.String, "s69");
                Row0.Cells.Add("ESTADO", LibExcelCS.DataType.String, "s69");


                LibExcelCS.WorksheetCell cell;

                // -----------------------------------------------

                TallerBE oMaestroTallerBE = null;

                if (oMaestroTallerBEList != null && oMaestroTallerBEList.Count > 0)
                {
                    for (int i = 0; i < oMaestroTallerBEList.Count; i++)
                    {
                        oMaestroTallerBE = new TallerBE();
                        oMaestroTallerBE = (TallerBE)oMaestroTallerBEList[i];

                        LibExcelCS.WorksheetRow Row2 = sheet.Table.Rows.Add();



                        /*co_taller*/
                        if (oMaestroTallerBE.co_taller != null)
                        {
                            if (oMaestroTallerBE.co_taller.ToString().Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTallerBE.co_taller.ToString().Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }



                        /*no_taller*/
                        if (oMaestroTallerBE.no_taller != null)
                        {
                            if (oMaestroTallerBE.no_taller.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTallerBE.no_taller.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*nomdpto*/
                        if (oMaestroTallerBE.nomdpto != null)
                        {
                            if (oMaestroTallerBE.nomdpto.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTallerBE.nomdpto.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*nomprov*/
                        if (oMaestroTallerBE.nomprov != null)
                        {
                            if (oMaestroTallerBE.nomprov.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTallerBE.nomprov.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*nomdist*/
                        if (oMaestroTallerBE.nomdist != null)
                        {
                            if (oMaestroTallerBE.nomdist.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTallerBE.nomdist.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        /*no_ubica*/
                        if (oMaestroTallerBE.no_ubica != null)
                        {
                            if (oMaestroTallerBE.no_ubica.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTallerBE.no_ubica.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }




                        /*Estado*/

                        if (oMaestroTallerBE.fl_activo != null)
                        {
                            if (oMaestroTallerBE.fl_activo.Trim().Equals(String.Empty))
                            {
                                cell = Row2.Cells.Add();
                                cell.StyleID = "s73";
                                cell.Data.Type = LibExcelCS.DataType.String;
                            }
                            else
                            {
                                Row2.Cells.Add(oMaestroTallerBE.fl_activo.Trim(), LibExcelCS.DataType.String, "s73");
                            }
                        }
                        else
                        {
                            cell = Row2.Cells.Add();
                            cell.StyleID = "s73";
                            cell.Data.Type = LibExcelCS.DataType.String;
                        }


                        // -----------------------------------------------
                    }
                }

                //  Options
                // -----------------------------------------------
                sheet.Options.Selected = true;   //Va True para la Hoja Activa (la que se muestra)
                sheet.Options.ProtectObjects = false;
                sheet.Options.ProtectScenarios = false;
                sheet.Options.PageSetup.Header.Margin = 0F;
                sheet.Options.PageSetup.Footer.Margin = 0F;
                sheet.Options.PageSetup.PageMargins.Bottom = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Left = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Right = 0.3937008F;
                sheet.Options.PageSetup.PageMargins.Top = 0.3937008F;
                sheet.Options.Print.ValidPrinterInfo = true;

                #endregion
            }
            catch
            {

            }
        }
        #endregion

    }
}
