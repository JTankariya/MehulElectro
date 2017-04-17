using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ViewModels;

namespace BAL
{
    public class ProductLogic
    {
        public static IEnumerable<Product> GetProductByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetProductByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
            {
                var products = DBHelper.ConvertToList<Product>(dt);
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        var shades = ShadeLogic.GetShadeByProductID(product.ID);
                        if (shades != null)
                            product.Shades = shades.ToList();
                        var packings = PackingLogic.GetPackingByProductID(product.ID);
                        if (packings != null)
                            product.Packings = packings.ToList();
                        product.Parties = ProductPartyLogic.GetPartiesForProduct(product.ID);
                    }
                }
                return products;
            }
            else
                return null;
        }

        public static IEnumerable<Product> CheckDuplicateProduct(int ID,string Name)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            param.Add("@Name", Name);
            DataTable dt = DBHelper.GetDataTable("CheckDuplicateProduct", param, true);

            if (dt != null && dt.Rows.Count > 0)
            {
                return DBHelper.ConvertToList<Product>(dt);
            }
            else
                return null;
        }

        public static IEnumerable<Product> GetProductsForView()
        {
            DataTable dt = DBHelper.GetDataTable("GetProductsForView", null, true);
            if (dt != null && dt.Rows.Count > 0)
            {
                var products = DBHelper.ConvertToList<Product>(dt);
                return products;
            }
            else
                return null;
        }

        public static bool SaveProduct(Product productModel)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", productModel.ID);
            param.Add("@Name", productModel.Name.ToUpper());
            param.Add("@ProductGroupID", productModel.ProductGroupID);
            param.Add("@RawMaterialTypeID", productModel.RawMaterialTypeID);
            param.Add("@PrintName", productModel.PrintName.ToUpper());
            param.Add("@ProductUnitID", productModel.ProductUnitID);
            param.Add("@HasShade", productModel.HasShade);
            param.Add("@HasPacking", productModel.HasPacking);
            param.Add("@OpeningQty", productModel.OpeningQty);
            param.Add("@OpeningRate", productModel.OpeningRate);
            param.Add("@OpeningValue", productModel.OpeningValue);
            param.Add("@MinQty", productModel.MinQty);
            param.Add("@MaxQty", productModel.MaxQty);
            param.Add("@ReorderQty", productModel.ReorderQty);
            param.Add("@Gravity", productModel.Gravity);
            param.Add("@SolidGravity", productModel.SolidGravity);
            param.Add("@SolidPercentage", productModel.SolidPercentage);
            param.Add("@BatchMin", productModel.BatchMin);
            param.Add("@BatchIdeal", productModel.BatchIdeal);
            param.Add("@BatchMax", productModel.BatchMax);
            param.Add("@BatchUnitID", productModel.BatchUnitID);
            param.Add("@SalesRate", productModel.SalesRate);
            param.Add("@PurchaseRate", productModel.PurchaseRate);
            param.Add("@PartyIDs", productModel.PartyIDs);
            param.Add("@CreatedBy", productModel.CreatedBy);
            param.Add("@UpdatedBy", productModel.UpdatedBy);
            if (productModel.Packings != null && productModel.Packings.Count > 0)
            {
                var packings = new DataTable();
                packings.TableName = "[dbo].[ProductPacking]";
                packings.Columns.Add("ID", typeof(int));
                packings.Columns.Add("PackingID", typeof(int));
                packings.Columns.Add("OpeningQty", typeof(string));
                packings.Columns.Add("OpeningRate", typeof(string));
                packings.Columns.Add("OpeningValue", typeof(string));
                packings.Columns.Add("ProductID", typeof(int));
                packings.Columns.Add("PartyIDs", typeof(string));

                foreach (var packing in productModel.Packings)
                {
                    packings.Rows.Add();
                    packings.Rows[packings.Rows.Count - 1]["ID"] = packing.ID;
                    packings.Rows[packings.Rows.Count - 1]["PackingID"] = packing.PackingID;
                    packings.Rows[packings.Rows.Count - 1]["OpeningQty"] = packing.OpeningQty;
                    packings.Rows[packings.Rows.Count - 1]["OpeningRate"] = packing.OpeningRate;
                    packings.Rows[packings.Rows.Count - 1]["OpeningValue"] = packing.OpeningValue;
                    packings.Rows[packings.Rows.Count - 1]["ProductID"] = packing.ProductID;
                    packings.Rows[packings.Rows.Count - 1]["PartyIDs"] = packing.PartyIDs;
                }
                param.Add("@ProductPackings", packings);
            }
            if (productModel.Shades != null && productModel.Shades.Count > 0)
            {
                var shades = new DataTable();
                shades.TableName = "[dbo].[ProductShade]";
                shades.Columns.Add("ID", typeof(int));
                shades.Columns.Add("ShadeID", typeof(int));
                shades.Columns.Add("ProductID", typeof(int));
                shades.Columns.Add("PackingID", typeof(int));
                shades.Columns.Add("OpeningQty", typeof(string));
                shades.Columns.Add("OpeningRate", typeof(string));
                shades.Columns.Add("OpeningValue", typeof(string));
                shades.Columns.Add("PartyIDs", typeof(string));

                foreach (var shade in productModel.Shades)
                {
                    shades.Rows.Add();
                    shades.Rows[shades.Rows.Count - 1]["ID"] = shade.ID;
                    shades.Rows[shades.Rows.Count - 1]["ShadeID"] = shade.ShadeID;
                    shades.Rows[shades.Rows.Count - 1]["ProductID"] = shade.ProductID;
                    shades.Rows[shades.Rows.Count - 1]["PackingID"] = shade.PackingID;
                    shades.Rows[shades.Rows.Count - 1]["OpeningQty"] = shade.OpeningQty;
                    shades.Rows[shades.Rows.Count - 1]["OpeningRate"] = shade.OpeningRate;
                    shades.Rows[shades.Rows.Count - 1]["OpeningValue"] = shade.OpeningValue;
                    shades.Rows[shades.Rows.Count - 1]["PartyIDs"] = shade.PartyIDs;
                }

                param.Add("@ProductShades", shades);
            }

            if (DBHelper.ExecuteNonQuery("SaveProduct", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DeleteProductByID(string ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataSet ds = DBHelper.GetDataSet("DeleteProductByID", param, true);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
            {
                return false;
            }
            else
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && (ds.Tables[0].Rows.Count > 1 || ds.Tables[0].Columns.Count > 1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        public static IEnumerable<Product> GetFinishedProducts()
        {
            DataTable dt = DBHelper.GetDataTable("GetFinishedProducts", null, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<Product>(dt);
            else
                return null;
        }

        public static IEnumerable<Product> GetRawMaterialProducts()
        {
            DataTable dt = DBHelper.GetDataTable("GetRawMaterialProducts", null, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<Product>(dt);
            else
                return null;
        }

        public static IEnumerable<Product> GetDrums()
        {
            DataTable dt = DBHelper.GetDataTable("GetDrumProducts", null, true);
            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<Product>(dt);
            else
                return null;
        }

        public static bool Convert(ProductConversion conversion)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", conversion.ID);
            param.Add("@DocNo", conversion.DocNo);
            param.Add("@DocDate", conversion.DocDate);
            param.Add("@FromProductId", conversion.FromProductId);
            param.Add("@FromShadeId", conversion.FromShadeId);
            param.Add("@FromPackingId", conversion.FromPackingId);
            param.Add("@FromConvertQty", conversion.FromConvertQty);
            param.Add("@ToProductId", conversion.ToProductId);
            param.Add("@ToShadeId", conversion.ToShadeId);
            param.Add("@ToPackingId", conversion.ToPackingId);
            param.Add("@ToConvertQty", conversion.ToConvertQty);

            if (DBHelper.ExecuteNonQuery("SaveProductConversion", param, true) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<ProductConversion> GetConversionByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataTable dt = DBHelper.GetDataTable("GetConversionByID", param, true);

            if (dt != null && dt.Rows.Count > 0)
                return DBHelper.ConvertToList<ProductConversion>(dt);
            else
                return null;
        }

        public static int GetNewConverstionDocNo()
        {
            DataTable dt = DBHelper.GetDataTable("GetNewConverstionDocNo", null, true);

            if (dt != null && dt.Rows.Count > 0)
                return System.Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
            else
                return 1;
        }

        public static bool DeleteConversionByID(int ID)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", ID);
            DataSet ds = DBHelper.GetDataSet("DeleteConversionByID", param, true);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 1)
            {
                return false;
            }
            else
            {
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && (ds.Tables[0].Rows.Count > 1 || ds.Tables[0].Columns.Count > 1))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
