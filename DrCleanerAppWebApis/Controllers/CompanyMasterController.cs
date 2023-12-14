using DryCleanerAppBuisinessLogic.Interfaces;
using DryCleanerAppDataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DrCleanerAppWebApis.Controllers
{

    [ApiController]
    [Authorize]
    public class CompanyMasterController : ControllerBase
    {
        readonly ICompanyB _companyB;
        public CompanyMasterController(ICompanyB companyB, IConfiguration config)
        {
            _companyB = companyB;
        }
        #region API Call
        /// <summary>
        /// Getting all company Masters
        /// </summary>
        /// <param name="CommonSearchParam"></param>
        /// <returns>List of CompanyListModel</returns>
        [HttpPost, Route("companymaster/getallcompanymasters")]

        public async Task<IActionResult> GetAllCompanyMasters(CommonSearchParam param)
        {
            try
            {
                IEnumerable<CompanyListModel> result = await _companyB.GetAll(param);
                if (result == null)
                {
                    var response = new ResponseObj();
                    response.Result = GeneralDTO.FailedMessage;
                    response.ResponseData = "No Data";
                    return Ok(response);
                }
                else
                {
                    ResponseObj updatecartObj = new ResponseObj();
                    updatecartObj.Result = GeneralDTO.SuccessMessage;
                    updatecartObj.ResponseData = result;
                    return Ok(updatecartObj);
                }
            }
            catch (ValidationException ex)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ex.Message;
                return Ok(response);
            }
            catch (ArgumentException ar)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ar.Message;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Getting Company Master Details By Id
        /// </summary>
        /// <param name="IdParam"></param>
        /// <returns>CompanyListModel</returns>
        [HttpPost, Route("companymaster/getcompanybyid")]
        public async Task<IActionResult> GetCompanyById(IdParam param)
        {
            try
            {
                CompanyListModel result = await _companyB.GetCompanyById(param.Id);
                if (result == null)
                {
                    var response = new ResponseObj();
                    response.Result = GeneralDTO.FailedMessage;
                    response.ResponseData = "No Data";
                    return Ok(response);
                }
                else
                {
                    ResponseObj updatecartObj = new ResponseObj();
                    updatecartObj.Result = GeneralDTO.SuccessMessage;
                    updatecartObj.ResponseData = result;
                    return Ok(updatecartObj);
                }
            }
            catch (ValidationException ex)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ex.Message;
                return Ok(response);
            }
            catch (ArgumentException ar)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ar.Message;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Creating New Company
        /// </summary>
        /// <param name="CompanyParam"></param>
        /// <returns>Success / Failed </returns>
        [HttpPost, Route("companymaster/createcompany")]
        public async Task<IActionResult> CreateCompany(CompanyParam param)
        {
            try
            {
                string result = await _companyB.CreateCompany(param);
                if (result == null)
                {
                    var response = new ResponseObj();
                    response.Result = GeneralDTO.FailedMessage;
                    response.ResponseData = "No Data";
                    return Ok(response);
                }
                else
                {
                    ResponseObj updatecartObj = new ResponseObj();
                    updatecartObj.Result = GeneralDTO.SuccessMessage;
                    updatecartObj.ResponseData = result;
                    return Ok(updatecartObj);
                }
            }
            catch (ValidationException ex)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ex.Message;
                return Ok(response);
            }
            catch (ArgumentException ar)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ar.Message;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Updating Company Master
        /// </summary>
        /// <param name="CompanyListModel"></param>
        /// <returns>Success / Failed </returns>
        [HttpPost, Route("companymaster/updatecompany")]
        public async Task<IActionResult> UpdateCompany(CompanyListModel param)
        {
            try
            {
                string result = await _companyB.UpdateCompany(param);
                if (result == null)
                {
                    var response = new ResponseObj();
                    response.Result = GeneralDTO.FailedMessage;
                    response.ResponseData = "No Data";
                    return Ok(response);
                }
                else
                {
                    ResponseObj updatecartObj = new ResponseObj();
                    updatecartObj.Result = GeneralDTO.SuccessMessage;
                    updatecartObj.ResponseData = result;
                    return Ok(updatecartObj);
                }
            }
            catch (ValidationException ex)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ex.Message;
                return Ok(response);
            }
            catch (ArgumentException ar)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ar.Message;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Deleting Company Master
        /// </summary>
        /// <param name="IdParam"></param>
        /// <returns>Success/ Failed</returns>
        [HttpPost, Route("companymaster/deletecompanybyid")]
        public async Task<IActionResult> DeleteCompanyById(IdParam param)
        {
            try
            {
                string result = await _companyB.DeleteCompanyById(param.Id, param.CreatedBy);
                if (result == null)
                {
                    var response = new ResponseObj();
                    response.Result = GeneralDTO.FailedMessage;
                    response.ResponseData = "No Data";
                    return Ok(response);
                }
                else
                {
                    ResponseObj updatecartObj = new ResponseObj();
                    updatecartObj.Result = GeneralDTO.SuccessMessage;
                    updatecartObj.ResponseData = result;
                    return Ok(updatecartObj);
                }
            }
            catch (ValidationException ex)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ex.Message;
                return Ok(response);
            }
            catch (ArgumentException ar)
            {
                var response = new ResponseObj();
                response.Result = GeneralDTO.FailedMessage;
                response.ResponseData = ar.Message;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        #endregion
    }
}
