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
    public class QCLogic
    {
        public static IEnumerable<Batch> GetQCBatches()
        {
            DataTable dt = DBHelper.GetDataTable("GetQCBatches", null, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToEnumerable<Batch>(dt);
            }
            else
                return null;
        }

        public static bool Done(string ID, List<BillOfMaterialLabParameters> param)
        {
            Dictionary<string, object> parameter = new Dictionary<string, object>();
            parameter.Add("@ID", ID);

            DataTable dt = new DataTable();
            dt.Columns.Add("ParameterID", typeof(int));
            dt.Columns.Add("Value", typeof(string)).MaxLength = 100;
            if (param != null)
            {
                foreach (var p in param)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["ParameterID"] = p.ParameterID;
                    dt.Rows[dt.Rows.Count - 1]["Value"] = p.Standard;
                }
            }


            dt.TableName = "[dbo].[QCParameter]";
            parameter.Add("@QCParameter", dt);
            if (DBHelper.ExecuteNonQuery("SaveQC", parameter, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Start(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            if (DBHelper.ExecuteNonQuery("StartQC", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string PrintQCSlip(List<BillOfMaterialLabParameters> labParam, string folderPath, int ID, string CreatedBy)
        {
            var batchDetail = BatchLogic.GetBatchByID(ID);

            bool exists = Directory.Exists(folderPath);
            if (!exists)
                Directory.CreateDirectory(folderPath);

            DirectoryInfo info = new DirectoryInfo(folderPath);
            FileInfo[] files = info.GetFiles().Where(p => p.CreationTime <= DateTime.Now.AddDays(-1)).ToArray();
            foreach (var file in files)
            {
                file.Delete();
            }
            var document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 0f, 40f);
            var pdffilename = "LabApprovalSlip" + DateTime.Now.ToString("h:mm:ss").Replace(":", "");
            var writer = PdfWriter.GetInstance(document, new FileStream(folderPath + "/" + pdffilename + ".PDF", FileMode.Create));
            writer.PageEvent = new QCPDFFooter(CreatedBy);
            document.Open();

            var widths = new float[] { 30f, 30f };

            var table = new PdfPTable(2);
            table.HeaderRows = 6;
            table.HorizontalAlignment = 0;
            table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            table.LockedWidth = true;
            table.SetWidths(widths);
            table.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            var cell = new PdfPCell(new Phrase("MEHUL ELECTRO INSULATING INDUSTRIES", CommonFunction.fontTitle17));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);


            cell = new PdfPCell(new Phrase("2610/A, Ph-4, G.I.D.C., Vatva, Ahmedabad 382 445", CommonFunction.font10));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Phone : 2584 2331/2, Fax: 2584 0356", CommonFunction.font10));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Email : info@mehulelectro.com, sales@mehulelectro.com", CommonFunction.font10));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("Lab Approval Slip", CommonFunction.fontTitle13));
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
                cell = new PdfPCell(new Phrase("L.T.R. No : " + batchDetail.BatchNo, CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Colspan = 2;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Product : " + batchDetail.ProductName, CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Code : " + batchDetail.PrintName, CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Shade : " + batchDetail.ShadeName, CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Batch No : " + batchDetail.BatchNo, CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Party : ", CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(" ", CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(" ", CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Qty : " + batchDetail.ProductionQty + " " + batchDetail.BatchUnit, CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase(" ", CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Mfg. Date : " + DateTime.Now, CommonFunction.fontTitle13));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase(" ", CommonFunction.font10));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(" ", CommonFunction.font10));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);
            document.Add(table);
            var i = 0;
            var batchParams = BillOfMaterialLabParametersLogic.GetLabParameterByProductAndShadeID(batchDetail.ProductID, batchDetail.ShadeID);
            var paramTable = new PdfPTable(4);
            if (batchParams != null)
            {
                
                paramTable.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_LEFT;
                paramTable.TotalWidth = (document.PageSize.Width - document.LeftMargin - document.RightMargin);
                paramTable.LockedWidth = true;
                paramTable.SetWidths(new float[] { 5f, 55f, 20f, 20f });
                cell = new PdfPCell(new Phrase("No.", CommonFunction.font10));
                paramTable.AddCell(cell);
                cell = new PdfPCell(new Phrase("Perticular", CommonFunction.font10));
                paramTable.AddCell(cell);
                cell = new PdfPCell(new Phrase("Standard/Yours SAMP", CommonFunction.font10));
                paramTable.AddCell(cell);
                cell = new PdfPCell(new Phrase("Observed", CommonFunction.font10));
                paramTable.AddCell(cell);
                foreach (var param in batchParams)
                {
                    i++;
                    var innercell = new PdfPCell(new Phrase(" ", CommonFunction.font8));
                    innercell = new PdfPCell(new Phrase(i.ToString(), CommonFunction.font10Normal));
                    innercell.PaddingBottom = 4;
                    paramTable.AddCell(innercell);

                    innercell = new PdfPCell(new Phrase(param.LabParameterName, CommonFunction.font10Normal));
                    paramTable.AddCell(innercell);

                    innercell = new PdfPCell(new Phrase(!string.IsNullOrEmpty(param.Standard) ? param.Standard : param.Min + " - " + param.Max + " " + param.Unit, CommonFunction.font10Normal));
                    innercell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_RIGHT;
                    paramTable.AddCell(innercell);

                    var observed = labParam.FirstOrDefault(x => x.ParameterID == param.ParameterID);
                    innercell = new PdfPCell(new Phrase(observed.Standard + " " + param.Unit, CommonFunction.font10Normal));
                    innercell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_RIGHT;
                    paramTable.AddCell(innercell);
                }
            }
            
            document.Add(paramTable);
            
            document.Close();
            return pdffilename + ".PDF";
        }
    }
}
