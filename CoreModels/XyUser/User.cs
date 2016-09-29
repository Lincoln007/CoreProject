﻿using System;
using System.Collections.Generic;

namespace CoreModels.XyUser
{
    public class User
    {
        #region Model
        private int _id;
        private string _account;
        // private string _secretid;
        private string _name;
        private string _password;
        private bool _enable = false;
        // private string _email;
        // private string _gender= "男";
        // private string _mobile;
        // private string _qq;
        private int? _companyid;
        private int? _roleid;
        // private string _creator;
        // private DateTime? _createdate= DateTime.UtcNow;
        private bool _islocked = false;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account
        {
            set { _account = value; }
            get { return _account; }
        }
        // /// <summary>
        // /// 密匙
        // /// </summary>
        // public string SecretID
        // {
        // 	set{ _secretid=value;}
        // 	get{return _secretid;}
        // }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable
        {
            set { _enable = value; }
            get { return _enable; }
        }
        // /// <summary>
        // /// 邮箱
        // /// </summary>
        // public string Email
        // {
        // 	set{ _email=value;}
        // 	get{return _email;}
        // }
        // /// <summary>
        // /// 性别
        // /// </summary>
        // public string Gender
        // {
        // 	set{ _gender=value;}
        // 	get{return _gender;}
        // }
        // /// <summary>
        // /// 联系电话
        // /// </summary>
        // public string Mobile
        // {
        // 	set{ _mobile=value;}
        // 	get{return _mobile;}
        // }
        // /// <summary>
        // /// QQ
        // /// </summary>
        // public string QQ
        // {
        // 	set{ _qq=value;}
        // 	get{return _qq;}
        // }
        /// <summary>
        /// 公司编号
        /// </summary>
        public int? CompanyID
        {
            set { _companyid = value; }
            get { return _companyid; }
        }
        /// <summary>
        /// 角色组
        /// </summary>
        public int? RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        // /// <summary>
        // /// 创建人
        // /// </summary>
        // public string Creator
        // {
        // 	set{ _creator=value;}
        // 	get{return _creator;}
        // }
        // /// <summary>
        // /// 创建时间
        // /// </summary>
        // public DateTime? CreateDate
        // {
        // 	set{ _createdate=value;}
        // 	get{return _createdate;}
        // }
        /// <summary>
        /// 用户是否锁屏
        /// </summary>
        public bool IsLocked
        {
            set { _islocked = value; }
            get { return _islocked; }
        }
        #endregion Model
    }

    #region 用户管理-查询结果
    public class UserQuery
    {
        public int ID { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int CompanyName { get; set; }
        public int RoleName { get; set; }
        public DateTime CreateDate { get; set; }
    }
    #endregion

	#region 用户管理 - 查询参数
	public class UserParam
	{
        public int CoID {get;set;}//公司编号
        public string Enable {get;set;}//是否启用
        public string Filter {get;set;}//过滤条件
        public int PageSize {get;set;}//每页笔数
        public int PageIndex {get;set;}//页码
        public string SortField {get; set;}//排序字段
        public string SortDirection {get;set;}//DESC,ASC  
	}

	public class UserData
	{
        public int PageCount {get;set;}//总页数
        public int DataCount {get;set;} //总行数
		public List<UserQuery> UserLst {get;set;}//返回查询结果
	}
	#endregion
}
