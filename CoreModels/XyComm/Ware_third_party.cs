namespace CoreModels.XyComm
{
    public class wareThirdParty{
        public int id{get;set;}
        public string warename{get;set;}
        public string itname{get;set;}
        public string myremark{get;set;}
        public string itremark{get;set;}
        public int enable{get;set;}
        public int  source{get;set;} 
        //public string cdate{get;set;}
        public string mdate{get;set;}
        // public string pdate{get;set;}
        // public string enddate{get;set;}
        // public string endman{get;set;}
        

    }

    public class openWareRequset{
        public string username{get;set;}
        public string pwd{get;set;}
        public string warename{get;set;}
        public string wareadmin{get;set;}
    }

    public class remarkSqlRes{
        public string ItCode{get;set;}
        public string Code{get;set;}

    }



}