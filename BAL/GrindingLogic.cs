using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class GrindingLogic
    {
        public static IEnumerable<Batch> GetGrindingBatches(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetGrindingBatches", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<Batch>(dt);
            }
            else
                return null;
        }

        public static bool Done(Batch batch)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", batch.ID);
            param.Add("@MinHymenGuage", batch.MinHymenGuage);
            param.Add("@MaxHymenGuage", batch.MaxHymenGuage);
            if (DBHelper.ExecuteNonQuery("SaveGrinding", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static IEnumerable<GrindingMaterial> Start(int ID,int EmployeeID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            param.Add("@EmployeeID", EmployeeID);
            DataTable dt = DBHelper.GetDataTable("StartGrinding", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<GrindingMaterial>(dt);
            }
            else
                return null;
        }

        public static string PrintRawMaterial(IEnumerable<GrindingMaterial> materials, string folderPath, int ID, string CreatedBy,int EmployeeID)
        {
            var employee = EmployeeLogic.GetEmployeeByID(EmployeeID).FirstOrDefault();
            var batchDetail = GrindingLogic.GetGrindingBatches(ID).FirstOrDefault();

            bool exists = Directory.Exists(folderPath);
            if (!exists)
                Directory.CreateDirectory(folderPath);

            DirectoryInfo info = new DirectoryInfo(folderPath);
            FileInfo[] files = info.GetFiles().Where(p => p.CreationTime <= DateTime.Now.AddDays(-1)).ToArray();
            foreach (var file in files)
            {
                file.Delete();
            }
            var document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 0f, 50f);
            var pdffilename = "GrindingMaterial" + DateTime.Now.ToString("h:mm:ss").Replace(":", "");
            var writer = PdfWriter.GetInstance(document, new FileStream(folderPath + "/" + pdffilename + ".PDF", FileMode.Create));
            writer.PageEvent = new PDFFooter(CreatedBy);
            document.Open();

            var widths = new float[] { 30f, 40f, 30f };

            var table = new PdfPTable(3);
            table.HeaderRows = 5;
            table.HorizontalAlignment = 0;
            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            table.LockedWidth = true;
            table.SetWidths(widths);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            var cell = new PdfPCell(new Phrase("Mehul Electro insulating industries", CommonFunction.fontTitle17));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("2610/A, Ph-4, G.I.D.C., Vata, Ahmedabad 382 445", CommonFunction.font10));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Phone : 2584 2331/2, Fax: 2584 0356", CommonFunction.font10));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Raw Materials Required For Grinding", CommonFunction.fontTitle13));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", CommonFunction.font10));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.MARKED;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            if (batchDetail != null)
            {
                cell = new PdfPCell(new Phrase("Batch No :" + batchDetail.BatchNo, CommonFunction.font10));
                cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_RIGHT;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Batch Start :" + DateTime.Now, CommonFunction.font10));
                cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_RIGHT;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Batch Qty :" + batchDetail.ProductionQty, CommonFunction.font10));
                cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_RIGHT;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Allocated To :" + employee.Name, CommonFunction.font10));
                cell.Colspan = table.NumberOfColumns;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            table.AddCell(new Phrase("Process", CommonFunction.font10));
            table.AddCell(new Phrase("Product", CommonFunction.font10));
            cell = new PdfPCell(new Phrase("Qty", CommonFunction.font10));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_RIGHT;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("", CommonFunction.font10));
            cell.Border = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            materials = materials.OrderBy(x => x.OrderID);
            foreach (var material in materials)
            {
                var innercell = new PdfPCell(new Phrase(" ", CommonFunction.font8));

                innercell = new PdfPCell(new Phrase(material.ProcessName, CommonFunction.font10Normal));
                innercell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                innercell.PaddingBottom = 4;
                table.AddCell(innercell);

                innercell = new PdfPCell(new Phrase(material.ProductName, CommonFunction.font10Normal));
                innercell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                table.AddCell(innercell);

                innercell = new PdfPCell(new Phrase(material.Qty.ToString(CommonFunction.QtyFormat), CommonFunction.font10Normal));
                innercell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                innercell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_RIGHT;
                table.AddCell(innercell);
            }

            cell = new PdfPCell(new Phrase(" ", CommonFunction.font8));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(" ", CommonFunction.font8));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            var batchDetails = BatchLogic.GetBatchDetails(ID);
            var packingTable = new PdfPTable(2);

            if (batchDetails != null)
            {
                var innercell = new PdfPCell(new Phrase("Packing Details", CommonFunction.fontTitle13));
                innercell.Border = iTextSharp.text.Rectangle.BOX;
                innercell.Colspan = 2;
                packingTable.AddCell(innercell);
                innercell = new PdfPCell(new Phrase("Name", CommonFunction.font10));
                innercell.Border = iTextSharp.text.Rectangle.BOX;
                packingTable.AddCell(innercell);
                innercell = new PdfPCell(new Phrase("Qty", CommonFunction.font10));
                innercell.Border = iTextSharp.text.Rectangle.BOX;
                packingTable.AddCell(innercell);
                foreach (var detail in batchDetails)
                {
                    innercell = new PdfPCell(new Phrase(detail.PackingName, CommonFunction.font10Normal));
                    innercell.Border = iTextSharp.text.Rectangle.BOX;
                    packingTable.AddCell(innercell);

                    innercell = new PdfPCell(new Phrase(detail.Qty, CommonFunction.font10Normal));
                    innercell.Border = iTextSharp.text.Rectangle.BOX;
                    packingTable.AddCell(innercell);
                }
            }

            document.Add(table);

            packingTable.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;
            packingTable.TotalWidth = (document.PageSize.Width - document.LeftMargin - document.RightMargin) / 2;
            packingTable.LockedWidth = true;
            document.Add(packingTable);

            var othersTable = new PdfPTable(2);
            othersTable.TotalWidth = (document.PageSize.Width - document.LeftMargin - document.RightMargin) / 2;
            othersTable.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;
            othersTable.LockedWidth = true;
            cell = new PdfPCell(new Phrase(" ", CommonFunction.font8));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(" ", CommonFunction.font8));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Grinding Information", CommonFunction.fontTitle13));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = 2;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Has Hymenguage?", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(batchDetail.HasHymenGuage ? "Yes" : "No", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Reading 1", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(" ", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Reading 2", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(" ", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Has Pigment Dispersion?", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(batchDetail.HasPigmentDispersion ? "Yes" : "No", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(" ", CommonFunction.font8));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(" ", CommonFunction.font8));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Shade Matching Type", CommonFunction.fontTitle13));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = 2;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Type : ", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase((batchDetail.ShadeMaching == "1" ? "Master Panel" : "Last Supply"), CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Panel Number : ", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(batchDetail.PanelNumber, CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase("Rack Number : ", CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            othersTable.AddCell(cell);
            cell = new PdfPCell(new Phrase(batchDetail.RackNumber, CommonFunction.font10Normal));
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            othersTable.AddCell(cell);

            document.Add(othersTable);
            document.Close();
            return pdffilename + ".PDF";
        }


    }
}
