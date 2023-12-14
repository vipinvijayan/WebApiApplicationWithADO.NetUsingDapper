using DryCleanerAppBuisinessLogic.Interfaces;
using DryCleanerAppBussinessLogic.Interfaces;
using DryCleanerAppDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using System.ComponentModel.DataAnnotations;

namespace DrCleanerAppWebApis.Controllers
{

    [ApiController]

    public class UserController : ControllerBase
    {

        private readonly IUserB _userB;
        private readonly IConfiguration _configuration;
        private readonly ISecurityB _securityB;

        public UserController(IUserB userB, IConfiguration configuration, ISecurityB securityB)
        {
            _userB = userB;

            _configuration = configuration;
            _securityB = securityB;

        }

        #region API Call
        /// <summary>
        /// This api used to login using their credentials
        /// </summary>
        /// <param name="UserLoginParams"></param>
        /// <returns> UserProfileModel or "Failed Message"</returns>
        [HttpPost, Route("user/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginParams param)
        {
            try
            {
                if (param != null)
                {
                    //Calling login method
                    UserProfileModel? userProfileModel = await loginCall(param.Username, param.Password, param.UserAgent);
                    if (userProfileModel != null)
                    {


                        string refreshToken = await _securityB.SaveRefreshToken(userProfileModel, ipAddress());
                        setTokenCookie(refreshToken);
                        //   return RedirectToAction("CompanyList", "CompanyMaster");
                        userProfileModel.AccessToken = await _securityB.GenerateJWTToken(userProfileModel.Username, userProfileModel.Id, param.UserAgent);
                        ResponseObj updatecartObj = new ResponseObj();
                        updatecartObj.Result = GeneralDTO.SuccessMessage;
                        updatecartObj.ResponseData = userProfileModel;

                        return Ok(updatecartObj);


                    }
                    else
                    {

                        var responseObj = new ResponseObj();
                        responseObj.Result = GeneralDTO.FailedMessage;
                        responseObj.ResponseData = "Login Failed";
                        return Ok(responseObj);
                    }

                }
                else
                {
                    var responseObj = new ResponseObj();
                    responseObj.Result = GeneralDTO.FailedMessage;
                    responseObj.ResponseData = "Login Failed";
                    return Ok(responseObj);

                }
            }
            catch (ValidationException ex)
            {
                var responseObj = new ResponseObj();
                responseObj.Result = GeneralDTO.FailedMessage;
                responseObj.ResponseData = ex.Message;
                return Ok(responseObj);


            }
            catch (ArgumentException ar)
            {
                var responseObj = new ResponseObj();
                responseObj.Result = GeneralDTO.FailedMessage;
                responseObj.ResponseData = ar.Message;
                return Ok(responseObj);
            }
            catch (Exception ex)
            {

                var responseObj = new ResponseObj();
                responseObj.Result = GeneralDTO.FailedMessage;
                responseObj.ResponseData = ex.Message;
                return Ok(responseObj);
            }
        }


        /// <summary>
        /// Saving New user to the system
        /// </summary>
        /// <param name="UserRegistrationParam"></param>
        /// <returns></returns>
        [HttpPost, Route("user/registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration(UserRegistrationParam param)
        {
            try
            {
                var companyId = _configuration.GetSection("AppSettings").GetValue<int>("CompanyId");
                var responseObj = new ResponseObj();

                if (param != null)
                {
                    param.CompanyId = companyId;
                    string result = await _userB.RegisterUser(param, companyId);
                    if (result == GeneralDTO.SuccessMessage)
                    {
                        //Calling login method
                        UserProfileModel? userProfileModel = await loginCall(param.Username, param.Password, param.UserAgent);
                        if (userProfileModel != null)
                        {
                            //if login success creating jwttoken
                            string refreshToken = await _securityB.SaveRefreshToken(userProfileModel, ipAddress());
                            //setting jwt token to cookie
                            setTokenCookie(refreshToken);
                            userProfileModel.AccessToken = await _securityB.GenerateJWTToken(userProfileModel.Username, userProfileModel.Id, param.UserAgent);
                            responseObj.Result = GeneralDTO.SuccessMessage;
                            responseObj.ResponseData = userProfileModel;

                        }
                        else
                        {

                            responseObj.Result = GeneralDTO.FailedMessage;
                            responseObj.ResponseData = "Registration Failed";

                        }

                    }
                    else if (result == GeneralDTO.AlreadyMessage)
                    {

                        responseObj.Result = GeneralDTO.FailedMessage;
                        responseObj.ResponseData = "User Already Registered";

                    }
                }
                else
                {

                    responseObj.Result = GeneralDTO.FailedMessage;
                    responseObj.ResponseData = "Registration Failed";

                }
                return Ok(responseObj);
            }
            catch (ValidationException ex)
            {
                var responseObj = new ResponseObj();
                responseObj.Result = GeneralDTO.FailedMessage;
                responseObj.ResponseData = ex.Message;
                return Ok(responseObj);
            }
            catch (ArgumentException ar)
            {
                var responseObj = new ResponseObj();
                responseObj.Result = GeneralDTO.FailedMessage;
                responseObj.ResponseData = ar.Message;
                return Ok(responseObj);
            }
            catch (Exception ex)
            {
                var responseObj = new ResponseObj();
                responseObj.Result = GeneralDTO.FailedMessage;
                responseObj.ResponseData = ex.Message;
                return Ok(responseObj);
            }
        }
        /// <summary>
        /// Deleting all user tokens from Database
        /// </summary>
        /// <param name="IdParam">User Id</param>
        /// <returns>Success / Failed</returns>
        [HttpPost, Route("user/revokeuser")]
        [Authorize]
        public async Task<IActionResult> RevokeUserToken(IdParam param)
        {
            try
            {
                if (param != null)
                {

                    string result = await _userB.DeleteAllRefreshTokenOfUser(param.Id);
                    ResponseObj responseObj = new ResponseObj();
                    responseObj.Result = GeneralDTO.SuccessMessage;
                    responseObj.ResponseData = result;
                    return Ok(responseObj);
                }
                else
                {

                    var responseObj = new ResponseObj();
                    responseObj.Result = GeneralDTO.FailedMessage;
                    responseObj.ResponseData = "Login Failed";
                    return Ok(responseObj);
                }
            }
            catch (ValidationException ex)
            {
                var responseObj = new ResponseObj();
                responseObj.Result = GeneralDTO.FailedMessage;
                responseObj.ResponseData = ex.Message;
                return Ok(responseObj);


            }
            catch (ArgumentException ar)
            {
                var responseObj = new ResponseObj();
                responseObj.Result = GeneralDTO.FailedMessage;
                responseObj.ResponseData = ar.Message;
                return Ok(responseObj);
            }
            catch (Exception ex)
            {

                var responseObj = new ResponseObj();
                responseObj.Result = GeneralDTO.FailedMessage;
                responseObj.ResponseData = ex.Message;
                return Ok(responseObj);
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Login using the credentials ,User agent for saving login log to check the browser type
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="userAgent"></param>
        /// <returns>UserProfileModel</returns>
        private async Task<UserProfileModel?> loginCall(string username, string password, string userAgent)
        {
            var companyId = _configuration.GetSection("AppSettings").GetValue<int>("CompanyId");
            //if user registration is success the try login with the credentials
            UserLoginParams loginParam = new UserLoginParams();
            loginParam.Username = username;
            loginParam.Password = password;
            loginParam.UserAgent = userAgent;
            loginParam.CompanyId = companyId;
            return await _userB.UserLogin(loginParam, companyId);

        }

        /// <summary>
        /// Getting the ip of the api call from the header
        /// </summary>
        /// <returns></returns>
        private string ipAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        /// <summary>
        /// Setting refresh token to into Cookie
        /// </summary>
        /// <param name="refreshToken"></param>
        private void setTokenCookie(string refreshToken)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,

                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Delete("refreshToken");

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
        #endregion
    }
}
