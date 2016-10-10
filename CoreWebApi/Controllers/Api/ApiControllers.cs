using CoreDate.CoreApi;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebApi.Api.JingDong{

    public class ApiControllers : ControllBase
    {

        #region //京东获取授权token
        [HttpGetAttribute("/core/Api/JingDong/getToken")]
        public ResponseResult getToken(string code)
        {            
            var m = JingDHaddle.GetToken("https://oauth.jd.com/oauth/token", "authorization_code", code, "http://192.168.30.81:8989/admin/shops", "7888CE9C0F3AAD424FEE8EEAAC99E10E", "a88b70e6c629465785088f9a9151701a");
            return CoreResult.NewResponse(m.s, m.d, "Api");
        }
        #endregion

        #region 
        [HttpGetAttribute("/core/Api/getAipLog")]
        public ResponseResult getAipLog(string shopid)
        {            
            string coid = GetCoid();
            var m = JingDHaddle.getAipLog(shopid,coid);
            return CoreResult.NewResponse(m.s, m.d, "Api");
        }

        #endregion



    }
}