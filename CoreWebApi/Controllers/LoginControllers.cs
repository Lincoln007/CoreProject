using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http.Authentication;
using System;
using Microsoft.AspNetCore.Http;
using CoreData.CoreUser;
using CoreModels.XyUser;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoreWebApi
{
    public class LoginController : ControllBase
    {
        [AllowAnonymous]
        [HttpGet("/api")]
        public async Task<ResponseResult> Get()
        {
            var test = new Models.Login();
            test.account = "admin";
            test.password = "admin";
            test.vcode = "123123";
            var user = new User();
            try{
            var data = UserHaddle.GetUserInfo("admin",GetMD5("admin","Xy@."));
            user = data.d as User;
            }
            catch
            {
                return CoreResult.NewResponse(1, "数据库", "Basic");
            }
            
            try
            {
             var userc = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] {
                        new Claim("uid", user.ID.ToString()),
                        new Claim("uname",user.Name.ToString()),
                        new Claim("coid",user.CompanyID.ToString()),
                        new Claim("roleid", user.RoleID.ToString())
                         },
                         "CoreInstance"));   
                    

            await HttpContext.Authentication.SignInAsync("CoreInstance", userc,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false
                });
            }
            catch(Exception ex)
            {
                return CoreResult.NewResponse(1, ex.ToString(), "Basic");
            }
            var ss = JsonConvert.SerializeObject(test);
            return CoreResult.NewResponse(1, user, "Basic");
        }

        [HttpGet("/api/{id}")]
        public IActionResult Get(int id)
        {
            var user = HttpContext.User;
            var cc = user.Claims;
             var test = new Models.Login();
            test.account = "admin";
            test.password = "admin";
            test.vcode = cc.FirstOrDefault(q => q.Type.Equals("uid")).Value;

            // cc.FirstOrDefault(q => q.Type.Equals("uid"));

            return new OkObjectResult(test);
        }

        [AllowAnonymous]
        [HttpPostAttribute("/Core/sign/in")]
        public async Task<ResponseResult> login([FromBodyAttribute]JObject lo)
        {
            var password = GetMD5(lo["password"].ToString(), "Xy@.");
            var data = UserHaddle.GetUserInfo(lo["account"].ToString(),password);
            var user = data.d as User;

            if(data.s<0)
            {
                return CoreResult.NewResponse(data.s, lo, "Indentity");
            }

             var userc = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new[] {
                        new Claim("uid", user.ID.ToString()),
                        new Claim("uname",user.Name.ToString()),
                        new Claim("coid",user.CompanyID.ToString()),
                        new Claim("roleid", user.RoleID.ToString())
                         },
                         "CoreInstance"));   

            await HttpContext.Authentication.SignInAsync("CoreInstance", userc,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false
                });
            return CoreResult.NewResponse(1, null, "Basic");
        }

        [HttpGetAttribute("/core/sign/mune")]
        public ResponseResult MenuList()
        {
            var roleid = GetRoleid();
            var coid = GetCoid();

            var m = UserHaddle.GetMenuList(roleid, coid);
            return CoreResult.NewResponse(m.s, m.d, "Basic");
        }

        // [HttpPostAttribute("/Core/sign/out")]    
        // public async Task loginout()
        // {
        //     await HttpContext.Authentication.SignOutAsync("CoreInstance");
        // }

        [HttpPostAttribute("/Core/sign/out")]    
        public ResponseResult  loginout()
        {
            var a =  HttpContext.Authentication.SignOutAsync("CoreInstance");
            return CoreResult.NewResponse(1,null, "Basic");
        }

        [HttpPostAttribute("/Core/account/password")]    
        public ResponseResult  editpassword([FromBodyAttribute]JObject lo   )
        {
            var response = CoreResult.NewResponse(1,null, "Indentity");
            string oldPwd = lo["oldPwd"].ToString();
            string newPwd = lo["newPwd"].ToString();
            string reNewPwd = lo["reNewPwd"].ToString();

            string regexstr = @".{6,18}";
            if (string.IsNullOrEmpty(oldPwd)) { response.s = -2006; return response; }
            if (string.IsNullOrEmpty(newPwd)) { response.s = -2012; return response; }
            if (!Regex.IsMatch(newPwd, regexstr)) { response.s = -2007;  return response;}
            if (newPwd != reNewPwd) { response.s = 2010; return response; }
            
            var m = UserHaddle.editPwd(GetUid(),GetMD5(oldPwd, "Xy@."),GetMD5(newPwd, "Xy@."));
            response.s = m.s;
            response.d =m.d;
            return response;
            //return CoreResult.NewResponse(m.s,m.d, "Indentity");

        }
    }
}