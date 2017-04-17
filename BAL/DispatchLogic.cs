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
    public class DispatchLogic
    {

        public static bool DeleteDispatch(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DBHelper.ExecuteNonQuery("DeleteDispatchById", param, true);
            return true;
        }

        public static IEnumerable<Dispatch> GetDispatchByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetDispatchByID", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var dispatches = DBHelper.ConvertToList<Dispatch>(dt);
                if (dispatches != null)
                {
                    foreach (var dispatch in dispatches)
                    {
                        dispatch.details = GetDispatchDetailByDispatchID(dispatch.ID);
                    }
                }
                return dispatches;
            }
            else
                return null;
        }

        public static List<DispatchDetail> GetDispatchDetailByDispatchID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@DispatchID", ID);
            DataTable dt = DBHelper.GetDataTable("GetDispatchDetailByDispatchID", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<DispatchDetail>(dt);
            else
                return null;
        }

        public static bool SaveDispatch(Dispatch dispatch)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", dispatch.ID);
            param.Add("@DONo", dispatch.DONo);
            param.Add("@OrderID", dispatch.OrderID);
            param.Add("@DODate", dispatch.DODate);
            param.Add("@PartyID", dispatch.PartyID);
            param.Add("@BookingAt", dispatch.BookingAt);
            param.Add("@DeliveryAddressID", dispatch.DeliveryAddressID);
            param.Add("@TransportID", dispatch.TransportID);
            param.Add("@LRNo", dispatch.LRNo);
            param.Add("@LRDate", dispatch.LRDate);
            param.Add("@GatePassNo", dispatch.GatePassNo);
            param.Add("@GatePassDate", dispatch.GatePassDate);
            param.Add("@Remarks", dispatch.Remarks);
            param.Add("@Total", dispatch.Total);
            param.Add("@CreatedBy", dispatch.CreatedBy);
            param.Add("@UpdatedBy", dispatch.UpdatedBy);
            DataTable dt = new DataTable();
            dt.TableName = "[dbo].[DispatchDetail]";
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("OrderDetailID", typeof(int));
            dt.Columns.Add("DispatchID", typeof(int));
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("ShadeID", typeof(int));
            dt.Columns.Add("PackingID", typeof(int));
            dt.Columns.Add("Qty", typeof(string));
            dt.Columns.Add("Rate", typeof(string));
            dt.Columns.Add("Total", typeof(string));
            dt.Columns.Add("IsDeleted", typeof(bool));

            if (dispatch.details != null && dispatch.details.Count > 0)
            {
                foreach (var detail in dispatch.details)
                {
                    dt.Rows.Add();
                    dt.Rows[dt.Rows.Count - 1]["ID"] = detail.ID;
                    dt.Rows[dt.Rows.Count - 1]["OrderDetailID"] = detail.OrderDetailID;
                    dt.Rows[dt.Rows.Count - 1]["DispatchID"] = detail.DispatchID;
                    dt.Rows[dt.Rows.Count - 1]["ProductID"] = detail.ProductID;
                    dt.Rows[dt.Rows.Count - 1]["ShadeID"] = detail.ShadeID;
                    dt.Rows[dt.Rows.Count - 1]["PackingID"] = detail.PackingID;
                    dt.Rows[dt.Rows.Count - 1]["Qty"] = detail.Qty;
                    dt.Rows[dt.Rows.Count - 1]["Rate"] = detail.Rate;
                    dt.Rows[dt.Rows.Count - 1]["Total"] = detail.Total;
                    dt.Rows[dt.Rows.Count - 1]["IsDeleted"] = detail.IsDeleted;
                }
            }

            param.Add("@DispatchDetails", dt);

            if (DBHelper.ExecuteNonQuery("SaveDispatch", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<DispatchDetail> GetDispatchDetail(int ID, int OrderID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@DispatchID", ID);
            param.Add("@OrderID", OrderID);
            DataTable dt = DBHelper.GetDataTable("GetDispatchDetail", param, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<DispatchDetail>(dt);
            else
                return null;
        }

        public static string GetMaxDispatchNo()
        {
            DataTable dt = DBHelper.GetDataTable("GetMaxDispatchNo", null, true);
            if (dt != null && dt.Rows.Count > 0)
                return Convert.ToString(dt.Rows[0]["DispatchNo"]);
            else
                return "1";
        }
        public static string Print(int ID, string folderPath, string CreatedBy)
        {
            var dispatch = GetDispatchByID(ID).FirstOrDefault();
            var order = OrderLogic.GetOrderByID(dispatch.OrderID).FirstOrDefault();
            var address = PartyAddressLogic.GetAddressById(dispatch.DeliveryAddressID);
            var dispatchDetail = GetDispatchDetailByDispatchID(ID);

            bool exists = Directory.Exists(folderPath);
            if (!exists)
                Directory.CreateDirectory(folderPath);

            DirectoryInfo info = new DirectoryInfo(folderPath);
            FileInfo[] files = info.GetFiles().Where(p => p.CreationTime <= DateTime.Now.AddDays(-1)).ToArray();
            foreach (var file in files)
            {
                file.Delete();
            }
            var document = new iTextSharp.text.Document(PageSize.A4, 30f, 30f, 0f, 250f);
            var pdffilename = "Dispatch" + DateTime.Now.ToString("h:mm:ss").Replace(":", "");
            var writer = PdfWriter.GetInstance(document, new FileStream(folderPath + "/" + pdffilename + ".PDF", FileMode.Create));
            document.Open();

            var widths = new float[] { 15f, 30f, 10f, 60f };

            var table = new PdfPTable(4);
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

            cell = new PdfPCell(new Phrase(" ", CommonFunction.font10));
            cell.HorizontalAlignment = iTextSharp.text.Rectangle.ALIGN_CENTER;
            cell.Border = iTextSharp.text.Rectangle.MARKED;
            cell.Colspan = table.NumberOfColumns;
            table.AddCell(cell);

            if (dispatch != null)
            {
                cell = new PdfPCell(new Phrase(dispatch.PartyName, CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Challan No :" + dispatch.DONo, CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(address.Address1 + "," + address.Address2 + "," + address.Address3 + "," + address.City + "," + address.State, CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Challan Date :" + dispatch.DODate.ToString("dd/MM/yyyy"), CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                cell.Colspan = 2;
                table.AddCell(cell);

                var detailTable = new PdfPTable(8);
                detailTable.HorizontalAlignment = 0;
                detailTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                detailTable.LockedWidth = true;
                var detailWidths = new[] { 5f, 35f, 16f, 10f, 8f, 8f, 10f, 8f };
                detailTable.SetWidths(detailWidths);
                detailTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                cell = new PdfPCell(new Phrase("Sr.", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Product", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Shade", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Packing", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Qty", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Ltr", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                detailTable.AddCell(cell);

                //cell = new PdfPCell(new Phrase("Drum/Box", CommonFunction.font10));
                //cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                //cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rate", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Per", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                detailTable.AddCell(cell);
                decimal i = 1, totalQty = 0, totalLtr = 0, totalRate = 0;
                foreach (var detail in dispatchDetail)
                {
                    var product = ProductLogic.GetProductByID(detail.ProductID).FirstOrDefault();
                    var packing = PackingLogic.GetPackingByID(detail.PackingID).FirstOrDefault();
                    cell = new PdfPCell(new Phrase(i.ToString(), CommonFunction.font10Normal));
                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    detailTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(detail.ProductName + " (" + product.PrintName + ")", CommonFunction.font10Normal));
                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    detailTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(detail.ShadeName, CommonFunction.font10Normal));
                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    detailTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(detail.PackingName, CommonFunction.font10Normal));
                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    detailTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(detail.Qty, CommonFunction.font10Normal));
                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    detailTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(Convert.ToString(Convert.ToDecimal(detail.Qty) * Convert.ToDecimal(packing.ConversionFactorWithLtr)), CommonFunction.font10Normal));
                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    detailTable.AddCell(cell);

                    //decimal drumCount = 0;
                    //cell = new PdfPCell(new Phrase(drumCount.ToString(), CommonFunction.font10Normal));
                    //cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    //cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    //detailTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(detail.Rate, CommonFunction.font10Normal));
                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    detailTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(product.ProuctUnitName, CommonFunction.font10Normal));
                    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    detailTable.AddCell(cell);

                    i++;
                    totalQty += Convert.ToDecimal(detail.Qty);
                    totalLtr += Convert.ToDecimal(detail.Qty) * Convert.ToDecimal(packing.ConversionFactorWithLtr);
                    //totalDrum += drumCount;
                    totalRate += Convert.ToDecimal(detail.Rate);
                }
                writer.PageEvent = new DispatchFooter(dispatch.DONo, dispatch.DODate, totalQty.ToString(), totalLtr.ToString(), dispatch.PartyName, address.Address1);

                cell = new PdfPCell(new Phrase(" ", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Colspan = 8;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Colspan = 8;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Totals : ", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Colspan = 4;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(totalQty.ToString(), CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(totalLtr.ToString(), CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                detailTable.AddCell(cell);

                //cell = new PdfPCell(new Phrase(totalDrum.ToString(), CommonFunction.font10));
                //cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
                //cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                //detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(totalRate.ToString(), CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                detailTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.TOP_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                detailTable.AddCell(cell);

                cell = new PdfPCell(detailTable);
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Colspan = 4;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", CommonFunction.font10));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Colspan = 4;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                

                document.Add(table);
            }

            document.Close();
            return pdffilename + ".PDF";
        }

        public static Dispatch GetDispatchByDONo(string DONo)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@DONo", DONo);
            DataTable dt = DBHelper.GetDataTable("GetDispatchByDONo", param, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var dispatches = DBHelper.ConvertToList<Dispatch>(dt);
                return dispatches.FirstOrDefault();
            }
            else
                return null;
        }
    }
}
