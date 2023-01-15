using Plugin.Payment.KNET.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace Plugin.Payment.KNET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Result()
        {
            return View("Index");
        }

        public ActionResult Reciept()
        {
            string trandata = Request.Form["trandata"];
            string errorText = Request.Form["ErrorText"];

            return View("Index");
        }

        public ActionResult Error()
        {
            string error = Request.QueryString["ErrorText"];

            return View("Index");
        }

        [HttpPost]
        [Route("GetPaymentUrl")]
        public JsonResult GetPaymentUrl(PaymentUrlRequestModel paymentUrlRequestModel)
        {
            string paymentUrl;
            var headers = Request.Headers;
            bool IsAuthorized = false;

            if (headers.AllKeys.Contains("PaymentAccessToken"))
            {
                string token = headers.GetValues("PaymentAccessToken").First();
                if (token == ConfigurationManager.AppSettings["PaymentAccessToken"])
                    IsAuthorized = true;
            }

            try
            {
                if (IsAuthorized)
                {
                    var knetPay = new KNETPay();
                    paymentUrl = knetPay.CreateRequest(paymentUrlRequestModel.LangId, paymentUrlRequestModel.Amount, paymentUrlRequestModel.TrackId,
                                    paymentUrlRequestModel.EntityId, paymentUrlRequestModel.CustomerId, paymentUrlRequestModel.RequestType);
                }
                else
                {
                    return Json("UnAuthorized", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                paymentUrl = string.Empty;
            }

            return Json(paymentUrl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("GetPaymentResult")]
        public JsonResult GetPaymentResult(PaymentResponseModel paymentResponseModel)
        {
            var paymentResponseDetailsModel = new PaymentResponseDetailsModel();
            var headers = Request.Headers;
            bool IsAuthorized = false;

            if (headers.AllKeys.Contains("PaymentAccessToken"))
            {
                string token = headers.GetValues("PaymentAccessToken").First();
                if (token == ConfigurationManager.AppSettings["PaymentAccessToken"])
                    IsAuthorized = true;
            }

            try
            {
                if (IsAuthorized)
                {
                    var knetPay = new KNETPay();
                    paymentResponseDetailsModel = knetPay.GetResponse(paymentResponseModel.Trandata, paymentResponseModel.ErrorText);
                }
                else
                {
                    return Json("UnAuthorized", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                paymentResponseDetailsModel.IsExceptionError = true;
                paymentResponseDetailsModel.ExceptionErrorText = ex.ToString();
            }

            return Json(paymentResponseDetailsModel, JsonRequestBehavior.AllowGet);
        }
    }
}