using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Utility.ResponseMapper
{
     
    public class ResponseMapper<T> : APIResponseModel<T> where T : new()
    {
        public ResponseMapper()
        {

        }

       

        public void GetAll(T list)
        {
            this.Message = "Get All Success";
            this.Success = true;
            this.Data = list;
            this.StatusCode = 200;
        }
        public void GetDefault(T model)
        {
            if (model is not null)
            {
                this.Message = "Get Default Success";
                this.Success = true;
                this.Data = model;
                this.StatusCode = 200;
            }
            else
            {
                this.Message = "No record found";
                this.Success = false; 
                this.StatusCode = 400;
            }
        }
        public void GetById(T model)
        {
            if (model is not null)
            {
                this.Message = "GetById Success";
                this.Success = true;
                this.Data = model;
                this.StatusCode = 200;
            }
            else
            {
                this.Message = "No record found";
                this.Success = false;
               // this.Data = null;
                this.StatusCode = 400;
            }
        }
        public void Create(T model)
        {
            if (model is not null)
            {
                this.Message = "New record created successfully";
                this.Success = true;
                this.Data = model;
                this.StatusCode = 200;
            }
            else
            {
                this.Message = "New record creation failed";
                this.Success = false;
                this.Data = model;
                this.StatusCode = 400;
            }
        }
        public void Update(T model)
        {
            if (model is not null)
            {
                this.Message = "Record updated successfully";
                this.Success = true;
                this.Data = model;
                this.StatusCode = 200;
            }
            else
            {
                this.Message = "Record updation failed";
                this.Success = false;
                this.StatusCode = 400;
            }
        }
        public void Delete(bool status)
        {
            if (status)
            {
                this.Message = "Record deleted successfully";
                this.Success = status;
                this.StatusCode = 200;
            }
            else
            {
                this.Message = "Record deletion failed";
                this.Success = status;
                this.StatusCode = 400;
            }
        }
        public void Status(bool status)
        {
            if (status)
            {
                this.Message = "Status updated successfully";
                this.Success = status;
                this.StatusCode = 200;
            }
            else
            {
                this.Message = "Status updation failed";
                this.Success = status;
                this.StatusCode = 400;
            }
        }

        public void NoRecord(T model)
        {
            this.Message = "No record found";
            this.Success = false;
            this.Data = model;
            this.StatusCode = 200;
        }

        public void CacheException(Exception ex)
        {
            this.Success = false;
            if (ex.InnerException == null)
            {
                this.Message = ex.Message;
            }
            else
            {
               var msg = (ex.InnerException.Message.ToString().Length > 500 ? ex.InnerException.Message.Substring(1, 500) : ex.InnerException.Message.ToString());
               this.Message = msg.Replace("\"", "");
            }
        }

        public void Login(T model)
        {
            if (model is not null)
            {
                this.Message = "LoggedIn successfully";
                this.Success = true;
                this.Data = model;
                this.StatusCode = 200;
            }
            else
            {
                this.Message = "login failed";
                this.Success = false;
                //this.Data = null;
                this.StatusCode = 400;
            }
        }

        public void ToggleActive(bool status)
        {
            if (status)
            {
                this.Message = "Status changed successfully";
                this.Success = status;
              //  this.Data = null;
                this.StatusCode = 200;
            }
            else
            {
                this.Message = "Status failed";
                this.Success = status;
               // this.Data = null;
                this.StatusCode = 400;
            }
        }
        //public void ToggleAssigned(T model)
        //{
        //    if (model is not null)
        //    {
        //        this.Message = "Toggle assigned sucess";
        //        this.Success = true;
        //        this.Data = model;
        //        this.StatusCode = 200;
        //    }
        //    else
        //    {
        //        this.Message = "Toggle assigned failed";
        //        this.Success = false;
        //      //  this.Data = null;
        //        this.StatusCode = 400;
        //    }
        //}
        public void DisplayOrder(T model)
        {
            this.Message = "Display order";
            this.Success = true ;
            this.Data = model;
            this.StatusCode = 200;
        }
        public void DisplayOrder(bool status)
        {
            this.Message = "Display order";
            this.Success = status;
            //  this.Data = null;
            this.StatusCode = 200;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        
    }
    //public partial class Response<T>
    //{
    //    public int Id { get; set; }
    //    public T Data { get; set; }
    //    public bool Success { get; set; }
    //    public string Message { get; set; }
    //    public int StatusCode { get; set; }
    //}

    //class LogMessage
    //{
    //    private LogMessage(string value) { Value = value; }

    //    public string Value { get; private set; }

    //    public static LogMessage Trace { get { return new LogMessage("Trace"); } }
    //    public static LogMessage Debug { get { return new LogMessage("Debug"); } }
    //    public static LogMessage Info { get { return new LogMessage("Info"); } }
    //    public static LogMessage Warning { get { return new LogMessage("Warning"); } }
    //    public static LogMessage Error { get { return new LogMessage("Error"); } }
    //}
    //public void CacheListException(Exception ex)
    //{
    //    this.Success = false;
    //    this.Data = null;
    //    this.Message = ex.Message;
    //    this.StatusCode = 400;
    //}
    //public partial class ApiListResponse<T> : ApiResponse<T>
    //{
    //    public int TotalRecords { get; set; }
    //    public int TotalRecordsAfterFilter { get; set; }
    //    public int RecordsInOneSlot { get; set; }
    //    public int PageSize { get; set; }
    //    public OrderMetaData OrderMetaData { get; set; }
    //}
    //public partial class OrderMetaData
    //{
    //    public int TotalOrders { get; set; }
    //    public int TotalSales { get; set; }
    //    public int TotalQuickPay { get; set; }
    //    public int TotalCancelled { get; set; }
    //}

}