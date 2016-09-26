using CoreModels.XyUser;
using Dapper;
using System;
using System.Data;

namespace CoreData.CoreUser
{
 public static class LogComm
 {

     public static int InsertUserLog(string Name, string LogType, string Contents, string UserName, string Company, DateTime Time)
     {
        string sqlCommandText = @"INSERT INTO Log(Name,LogType,Contents,UserName,Compnay,LogDate) VALUES(
            @Name,
            @LogType,
            @Contents,
            @UserName,
            @Compnay,
            @LogDate
        )";
        var log = new Log();
        log.Name = Name;
        log.LogType = LogType;
        log.Contents = Contents;
        log.UserName = UserName;
        log.Compnay = Company;
        log.LogDate = Time;
        int result = DbBase.UserDB.Execute(sqlCommandText,log);
        return result;//DbBase.UserDB.
     }

      public static int InsertUserLogTran(IDbTransaction Trans,string Name, string LogType, string Contents, string UserName, string Company, DateTime Time)
     {
        string sqlCommandText = @"INSERT INTO Log(Name,LogType,Contents,UserName,Compnay,LogDate) VALUES(
            @Name,
            @LogType,
            @Contents,
            @UserName,
            @Compnay,
            @LogDate
        )";
        var log = new Log();
        log.Name = Name;
        log.LogType = LogType;
        log.Contents = Contents;
        log.UserName = UserName;
        log.Compnay = Company;
        log.LogDate = Time;
        int result = DbBase.UserDB.Execute(sqlCommandText,log,Trans);
        return result;//DbBase.UserDB.
     }
 }

}