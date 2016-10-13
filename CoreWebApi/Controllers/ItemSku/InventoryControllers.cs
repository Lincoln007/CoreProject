using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using CoreData;
using CoreData.CoreCore;
using CoreData.CoreComm;
using CoreModels;
using CoreModels.XyCore;
using System;
using System.Collections.Generic;

namespace CoreWebApi.XyCore
{
    [AllowAnonymous]
    public class InventoryController : ControllBase
    {
        #region 库存管理 - 获取库存查询
        [HttpGetAttribute("Core/XyCore/Inventory/InventQuery")]
        public ResponseResult InventQuery(string SkuID, string SkuName, string StockQtyb, string StockQtye, string WarehouseID, string Status, string PageIndex, string PageSize, string SortField, string SortDirection)
        {
            var cp = new InvQueryParam();
            int x;
            if (!string.IsNullOrEmpty(SkuID))//商品编码
            {
                cp.SkuID = SkuID;
            }
            if (!string.IsNullOrEmpty(SkuName))//商品名称
            {
                cp.SkuName = SkuName;
            }
            if (!string.IsNullOrEmpty(StockQtyb) && int.TryParse(StockQtyb, out x))//主仓实际库存数量起
            {
                cp.StockQtyb = int.Parse(StockQtyb);
            }
            if (!string.IsNullOrEmpty(StockQtye) && int.TryParse(StockQtye, out x))//主仓实际库存数量迄
            {
                cp.StockQtye = int.Parse(StockQtye);
            }
            if (!string.IsNullOrEmpty(WarehouseID) && int.TryParse(WarehouseID, out x))//商品仓库
            {
                cp.WarehouseID = int.Parse(WarehouseID);
            }
            if (!string.IsNullOrEmpty(Status) && int.TryParse(Status, out x))//库存状态:0.全部,1.充足,2.预警
            {
                cp.Status = int.Parse(Status);
            }
            if (int.TryParse(PageIndex, out x))//页码
            {
                cp.PageIndex = int.Parse(PageIndex);
            }
            if (int.TryParse(PageSize, out x))//每页笔数
            {
                cp.PageSize = int.Parse(PageSize);
            }
            //排序参数赋值
            if (!string.IsNullOrEmpty(SortField))
            {
                var res = CommHaddle.SysColumnExists(DbBase.CoreConnectString, "inventory", SortField);
                if (res.s == 1)
                {
                    cp.SortField = SortField;
                    if (!string.IsNullOrEmpty(SortDirection) && (SortDirection.ToUpper() == "DESC" || SortDirection.ToUpper() == "ASC"))
                    {
                        cp.SortDirection = SortDirection.ToUpper();
                    }
                }
            }
            cp.CoID = int.Parse(GetCoid());
            var Result = InventoryHaddle.GetInvQuery(cp);
            return CoreResult.NewResponse(Result.s, Result.d, "General");
        }
        #endregion

        #region 库存管理 - 库存明细查询
        [HttpGetAttribute("Core/XyCore/Inventory/InvDetailQuery")]
        public ResponseResult InvDetailQuery(string SkuID, string WarehouseID, string DocType, string DocDateB, string DocDateE, string PageIndex, string PageSize, string SortField, string SortDirection)
        {

            var cp = new InvQueryParam();
            int x;
            DateTime date;
            if (!string.IsNullOrEmpty(SkuID))//商品编码
            {
                cp.SkuID = SkuID;
            }
            if (!string.IsNullOrEmpty(WarehouseID) && int.TryParse(WarehouseID, out x))//商品仓库
            {
                cp.WarehouseID = int.Parse(WarehouseID);
            }
            if (!string.IsNullOrEmpty(DocType))//单据类型
            {
                cp.DocType = DocType;
            }
            if (!string.IsNullOrEmpty(DocDateB) && (DateTime.TryParse(DocDateB, out date)))//单据日期起
            {
                cp.DocDateB = DateTime.Parse(DocDateB);
            }
            if (!string.IsNullOrEmpty(DocDateE) && (DateTime.TryParse(DocDateE, out date)))//单据日期迄
            {
                cp.DocDateE = DateTime.Parse(DocDateE);
            }
            if (int.TryParse(PageIndex, out x))//页码
            {
                cp.PageIndex = int.Parse(PageIndex);
            }
            if (int.TryParse(PageSize, out x))//每页笔数
            {
                cp.PageSize = int.Parse(PageSize);
            }
            //排序参数赋值
            if (!string.IsNullOrEmpty(SortField))
            {
                var res = CommHaddle.SysColumnExists(DbBase.CoreConnectString, "invinoutitem", SortField);
                if (res.s == 1)
                {
                    cp.SortField = SortField;
                    if (!string.IsNullOrEmpty(SortDirection) && (SortDirection.ToUpper() == "DESC" || SortDirection.ToUpper() == "ASC"))
                    {
                        cp.SortDirection = SortDirection.ToUpper();
                    }
                }
            }
            cp.CoID = int.Parse(GetCoid());
            var Result = InventoryHaddle.GetInvDetailQuery(cp);
            return CoreResult.NewResponse(Result.s, Result.d, "General");
        }
        #endregion

        #region 修改现有库存-查询单笔库存明细
        [HttpGetAttribute("Core/XyCore/Inventory/InventorySingle")]
        public ResponseResult InventorySingle(string ID)
        {
            var res = new DataResult(1, null);
            int invID = 0;
            if (!string.IsNullOrEmpty(ID) && int.TryParse(ID, out invID))
            {
                invID = int.Parse(ID);
            }
            if (invID > 0)
            {
                int CoID = int.Parse(GetCoid());
                res = InventoryHaddle.GetInventorySingle(invID, CoID);
            }
            // int wid=0;
            // if (!string.IsNullOrEmpty(WarehouseID) && int.TryParse(WarehouseID, out wid))//商品仓库
            // {
            //     wid = int.Parse(WarehouseID);
            // } 
            // var res = InventoryHaddle.GetInventorySingle(SkuID,wid,CoID);
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion

        #region 修改现有库存 - 产生盘点交易
        [HttpPostAttribute("Core/XyCore/Inventory/UptStockQtySingle")]
        public ResponseResult UptStockQtySingle([FromBodyAttribute]JObject obj)
        {
            int ID = 0, SetQty = 0;
            var res = new DataResult(1, null);
            if (!string.IsNullOrEmpty(obj["ID"].ToString()) && int.TryParse(obj["ID"].ToString(), out ID))
            {
                ID = int.Parse(obj["ID"].ToString());
            }
            if (!string.IsNullOrEmpty(obj["SetQty"].ToString()) && int.TryParse(obj["SetQty"].ToString(), out SetQty))
            {
                SetQty = int.Parse(obj["SetQty"].ToString());
            }
            int CoID = int.Parse(GetCoid());
            string UserName = GetUname();
            if (ID > 0)
            {
                res = InventoryHaddle.SetStockQtySingle(ID, SetQty, CoID, UserName);
            }
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion

        #region 修改安全库存 - 查询库存明细
        [HttpGetAttribute("Core/XyCore/Inventory/InvSafeQtyLst")]
        public ResponseResult InvSafeQtyLst(string GoodsCode, string WarehouseID)
        {
            var res = new DataResult(1, null);
            int wid = 0;
            if (!string.IsNullOrEmpty(WarehouseID) && int.TryParse(WarehouseID, out wid))//商品仓库
            {
                wid = int.Parse(WarehouseID);
            }
            if (string.IsNullOrEmpty(GoodsCode) || wid == 0)
            {
                res.s = -1;
                res.d = "参数异常";
            }
            else
            {
                int CoID = int.Parse(GetCoid());
                string UserName = GetUname();
                res = InventoryHaddle.GetInvSafeQtyLst(GoodsCode, wid, CoID, UserName);
            }
            return CoreResult.NewResponse(res.s, res.d, "General");
        }
        #endregion

        #region 修改安全库存 - 保存商品库存
        [HttpPostAttribute("Core/XyCore/Inventory/UptInvSafeQty")]
        public ResponseResult UptInvSafeQty([FromBodyAttribute]JObject obj)
        {
            var res = new DataResult(1,null);
            var invLst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InventParams>>(obj["InvLst"].ToString());
            int CoID = int.Parse(GetCoid());
            string UserName = GetUname();
            res = InventoryHaddle.UptInvSafeQty(invLst,CoID,UserName);
            return CoreResult.NewResponse(res.s,res.d,"General");
        }
        #endregion

        #region 商品明细查询
        [HttpGetAttribute("Core/XyCore/Inventory/InvInOutQuery")]
        public ResponseResult InvInOutQuery(string SkuID, string SkuName, string RecordID, string DocDateB, string DocDateE, string PageIndex, string PageSize, string SortField, string SortDirection)
        {
            var cp = new InvQueryParam();
            int x;
            DateTime date;
            if (!string.IsNullOrEmpty(SkuID))//商品编码
            {
                cp.SkuID = SkuID;
            }
            if (!string.IsNullOrEmpty(SkuName))//商品名称
            {
                cp.SkuName = SkuName;
            }
            if (!string.IsNullOrEmpty(RecordID))//单据编号
            {
                cp.DocType = RecordID;
            }
            if (!string.IsNullOrEmpty(DocDateB) && (DateTime.TryParse(DocDateB, out date)))//单据日期起
            {
                cp.DocDateB = DateTime.Parse(DocDateB);
            }
            if (!string.IsNullOrEmpty(DocDateE) && (DateTime.TryParse(DocDateE, out date)))//单据日期迄
            {
                cp.DocDateE = DateTime.Parse(DocDateE);
            }
            if (int.TryParse(PageIndex, out x))//页码
            {
                cp.PageIndex = int.Parse(PageIndex);
            }
            if (int.TryParse(PageSize, out x))//每页笔数
            {
                cp.PageSize = int.Parse(PageSize);
            }
            //排序参数赋值
            if (!string.IsNullOrEmpty(SortField))
            {
                var res = CommHaddle.SysColumnExists(DbBase.CoreConnectString, "invinoutitem", SortField);
                if (res.s == 1)
                {
                    cp.SortField = SortField;
                    if (!string.IsNullOrEmpty(SortDirection) && (SortDirection.ToUpper() == "DESC" || SortDirection.ToUpper() == "ASC"))
                    {
                        cp.SortDirection = SortDirection.ToUpper();
                    }
                }
            }
            cp.CoID = int.Parse(GetCoid());
            var Result = InventoryHaddle.GetInvDetailQuery(cp);
            return CoreResult.NewResponse(Result.s, Result.d, "General");
        }
        #endregion
    }
}