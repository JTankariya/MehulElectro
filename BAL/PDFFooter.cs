using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class PDFFooter : PdfPageEventHelper
    {
        string _strName = "";
        public PDFFooter(string strName)
        {
            _strName = strName;
        }

        public PDFFooter()
        {
            _strName = "Prepared By : "+ "Jayesh";
        }
        // write on top of document
        public override void OnOpenDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnOpenDocument(writer, document);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(3);
            tabFot.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPCell cell;
            tabFot.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            cell = new PdfPCell(new Phrase(_strName, CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);
            
            cell = new PdfPCell(new Phrase(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"), CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);
            cell = new PdfPCell(new Phrase(writer.PageNumber.ToString(), CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);

            tabFot.WriteSelectedRows(0, -1, Convert.ToInt32(Math.Round(document.LeftMargin)), document.Bottom, writer.DirectContent);
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnCloseDocument(writer, document);
        }

    }
}
