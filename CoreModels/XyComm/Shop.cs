using System;
using System.Collections.Generic;

namespace CoreModels.XyComm
{
       public class Shop
    {
        #region Model
        public int ID{get;set;}
        public string ShopName{get;set;}//店铺名称
        public int CoID{get;set;}
        public int SitType{get;set;}//店铺站点enum
        public string ShopSite{get;set;}//店铺归属平台
        public string ShopType{get;set;}
        public int? Istoken{get;set;}//是否被授权（0未授权，1授权，2过期）
        public string ShopUrl{get;set;}//店铺网址
        public string ShopSetting{get;set;}
        public bool Enable {get;set;}//启用店铺
        public string Creator{get;set;}
        public string CreateDate {get;set;}
        public string ShortName{get;set;}//店铺简称
        public string Shopkeeper{get;set;}//掌柜昵称
        public string SendAddress{get;set;}//发货地址
        public string TelPhone{get;set;}//联系电话
        public string IDcard{get;set;}//身份证号
        public string ContactName{get;set;}//退货联系人
        public string ReturnAddress{get;set;}//退货地址
        public string ReturnMobile{get;set;}//退货手机
        public string ReturnPhone{get;set;}//退货固话
        public string Postcode{get;set;}//退货邮编
        public bool UpdateSku{get;set;}//上传库存（自动同步）
        public bool DownGoods{get;set;}//下载商品（自动同步）
        public bool UpdateWayBill{get;set;}//上传快递单（发货信息）
        public string Token{get;set;}    
        public string ShopBegin{get;set;} // 创店日期
        public bool Deleted {get;set;} //是否删除

        public int PrintID{get;set;} // 打印模板
        #endregion
    }


     public class ShopQuery
    {
        #region Model
        public int ID{get;set;}
        public int CoID{get;set;}
        public string ShopName{get;set;}
        public bool Enable {get;set;}
        public int SitType{get;set;}
        public string ShopSite{get;set;}
        public string ShopUrl{get;set;}
        public string Shopkeeper{get;set;}
        public bool UpdateSku{get;set;}
        public bool DownGoods{get;set;}
        public bool Updatewaybill{get;set;}
        public string TelPhone{get;set;}
        public string SendAddress{get;set;}
        public string ShopBegin {get;set;}
        public int Istoken{get;set;} 
        public string ReturnAddress{get;set;}//退货地址
        #endregion
    }

    
     public class ShopParam
    {
        public int CoID {get;set;}//公司编号
        public int FilterType{get;set;}//搜索标识
        public string Enable {get;set;}//是否启用
        public string Filter {get;set;}//过滤条件
        public int PageSize {get;set;}//每页笔数
        public int PageIndex {get;set;}//页码
        public int PageCount {get;set;}//总页数
        public int DataCount {get;set;} //总行数
        public string SortField {get; set;}//排序字段
        public string SortDirection {get;set;}//DESC,ASC        
        public List<ShopQuery> ShopLst {get; set;}//返回资料        
    }

    public  class siteTree{
        public int value{get;set;}
        public string label{get;set;}
    }

    public class shopApi{
        public int sid{get;set;}
        public string  apiname{get;set;}
        public bool enable{get;set;}

    }

    public class shopEnum{
	    public int value{get;set;}
		public string label{get;set;}

    }
    public class shopWithPrint{
	    public int value{get;set;}
		public string label{get;set;}
        public int printID{get;set;}

    }

    public class shopPrintUpdate{
        public int id {get;set;}
        public int printID{get;set;}
    }


}