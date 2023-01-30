using com.fss.plugin;
using Plugin.Payment.KNET.Helpers;
using Plugin.Payment.KNET.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Plugin.Payment.KNET
{
    public class KNETPay
    {
        public string CreateRequest(string langId, string amount, string trackId, string entityId, string customerId, string requestType)
        {
            string returnUrl = "";
            if (!string.IsNullOrEmpty(langId) & !string.IsNullOrEmpty(amount) & !string.IsNullOrEmpty(trackId) & !string.IsNullOrEmpty(entityId))
            {
                /** Request Processing**/
                //Merchant can connect iPay Plugin using below step 
                iPayPipe pipe = new iPayPipe();

                //Initialization 
                string resourcepath = ConfigurationManager.AppSettings["resourcepaths"];
                string resourcePath = resourcepath;
                string keystorePath = resourcepath;
                string recieptURL = ConfigurationManager.AppSettings["recieptURL"];
                string errorURL = ConfigurationManager.AppSettings["errorURL"];
                string aliasName = ConfigurationManager.AppSettings["aliasName"];

                //Set Values 
                pipe.setTrackId(trackId);
                pipe.setAlias(aliasName);
                pipe.setResourcePath(resourcePath);
                pipe.setLanguage(langId);
                pipe.setAction("1");// 1 – Purchase
                pipe.setAmt(string.Format("{0:0.000}", amount));
                pipe.setCurrency("414"); //Transaction Currency (ex: "414 ") 
                pipe.setUdf1(entityId);
                pipe.setUdf2(customerId);
                pipe.setUdf3(requestType);
                pipe.setUdf4("");
                pipe.setUdf5("");
                pipe.setKeystorePath(keystorePath);
                pipe.setResponseURL(recieptURL);
                pipe.setErrorURL(errorURL);

                //For Hosted Payment Integration , the method to be called is 
                int val = pipe.performPaymentInitializationHTTP();

                if (val == 0)
                {
                    returnUrl = pipe.getWebAddress();
                }
                else
                {
                    returnUrl = pipe.getErrorURL();
                    string encEntityId = WebHelper.Encrypt(requestType + "~" + entityId);
                    returnUrl = returnUrl + "?values=" + encEntityId + "&errorText=" + pipe.getError()
                        + "&resourcepath=" + resourcepath + "&recieptURL=" + recieptURL + "&errorURL=" + errorURL
                        + "&aliasName=" + aliasName;
                }
            }

            return returnUrl;
        }

        public PaymentResponseDetailsModel GetResponse(string trandata, string errorText)
        {
            PaymentResponseDetailsModel paymentResponseDetailsModel = new PaymentResponseDetailsModel();
            iPayPipe pipe = new iPayPipe();

            //Initialization 
            string resourcepath = ConfigurationManager.AppSettings["resourcepaths"];
            string resourcePath = resourcepath;
            string keystorePath = resourcepath;
            string aliasName = ConfigurationManager.AppSettings["aliasName"];

            //Set Values 
            pipe.setAlias(aliasName);
            pipe.setResourcePath(resourcePath);
            pipe.setKeystorePath(keystorePath);

            ///The method to be called is, 
            int result = pipe.parseEncryptedRequest(trandata);

            if (pipe.getResult() == "CAPTURED")
            {
                paymentResponseDetailsModel.Result = pipe.getResult();
                paymentResponseDetailsModel.Amount = pipe.getAmt();
                paymentResponseDetailsModel.Auth = pipe.getAuth();
                //paymentResponseDetailsModel.Language = pipe.getLanguage();
                paymentResponseDetailsModel.EntityId = pipe.getUdf1();
                paymentResponseDetailsModel.PaymentId = pipe.getPaymentId();
                paymentResponseDetailsModel.RefId = pipe.getRef();
                paymentResponseDetailsModel.TrackId = pipe.getTrackId();
                paymentResponseDetailsModel.TransId = pipe.getTransId();
                paymentResponseDetailsModel.CustomerId = pipe.getUdf2();
                paymentResponseDetailsModel.ErrorText = errorText;
                paymentResponseDetailsModel.RequestType = pipe.getUdf3();
            }
            else
            {
                paymentResponseDetailsModel.Result = pipe.getResult();
                paymentResponseDetailsModel.Amount = pipe.getAmt();
                paymentResponseDetailsModel.Auth = pipe.getAuth();
                //paymentResponseDetailsModel.Language = pipe.getLanguage();
                paymentResponseDetailsModel.EntityId = pipe.getUdf1();
                paymentResponseDetailsModel.PaymentId = pipe.getPaymentId();
                paymentResponseDetailsModel.RefId = pipe.getRef();
                paymentResponseDetailsModel.TrackId = pipe.getTrackId();
                paymentResponseDetailsModel.TransId = pipe.getTransId();
                paymentResponseDetailsModel.CustomerId = pipe.getUdf2();
                paymentResponseDetailsModel.ErrorText = errorText;
                paymentResponseDetailsModel.RequestType = pipe.getUdf3();
            }

            return paymentResponseDetailsModel;
        }
    }
}