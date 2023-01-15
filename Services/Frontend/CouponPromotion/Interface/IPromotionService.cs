﻿using Data.Content;
using Data.CouponPromotion;
using Data.Locations;
using Data.ProductManagement;
using Data.SystemUserManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility.API;

namespace Services.Frontend.CouponPromotion.Interface
{
    public interface IPromotionService
    {
        #region Promotion Service 
        Task<Promotion> GetDefault(); 
        #endregion


    }
}
