using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class DispatchFooter : PdfPageEventHelper
    {
        string _challanNo = "", _tinQty = "", _ltrQty = "", _partyName = "", _partyAddress = "";
        DateTime _challanDate = DateTime.Now;
        public DispatchFooter(string challanNo, DateTime challanDate, string tinQty, string ltrQty, string partyName, string partyAddress)
        {
            _challanNo = challanNo;
            _challanDate = challanDate;
            _tinQty = tinQty;
            _ltrQty = ltrQty;
            _partyName = partyName;
            _partyAddress = partyAddress;
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
            PdfPTable tabFot = new PdfPTable(2);
            tabFot.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPCell cell;
            tabFot.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

            cell = new PdfPCell(new Phrase("MEHUL ELECTRO INSULATING INDUSTRIES", CommonFunction.fontTitle17));
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 2;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase("Received Goods mention in our Challan No :" + _challanNo, CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase("Challan Date : " + _challanDate.ToString("dd/MM/yyyy"), CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase("Received Total Qty (In Tin) : " + _tinQty + ", Qty (In Ltr) : " + _ltrQty, CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase("Party Name : ", CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase(_partyName, CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase(_partyAddress, CommonFunction.font10));
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", CommonFunction.fontTitle13));
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            tabFot.AddCell(cell);
            
            cell = new PdfPCell(new Phrase(" ", CommonFunction.fontTitle13));
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase("Receiver Signature with stamp", CommonFunction.fontTitle13));
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
            cell.Border = Rectangle.TOP_BORDER;
            tabFot.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", CommonFunction.fontTitle13));
            cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
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
