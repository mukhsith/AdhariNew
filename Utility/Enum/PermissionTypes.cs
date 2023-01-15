using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Enum
{
    public enum PermissionTypes
    {   
        None=0, // default permssion in baseController, when no value is assigned in derived controller
        Dashboard = 1,
        UserManagement = 2,
        SystemUser = 3,
        SystemUserRole = 4,
        SystemUserPermission = 5,
        WebsiteContentManagement = 6,
        Banner = 7,
        
        AboutUs = 8,
        TermsConditions = 9,
        PrivacyPolicy = 10,
        RefundPolicy = 11,
        ContactDetails = 12,
        SocialMediaLinks = 13,
        CustomerFeedback = 14,
        SiteContent = 80,//multiple control access (AboutUs,TermsAndContion,PrivacyPolicy,RefundPolicy)
        Category = 16,
        ItemSize =17,
        Product=18, 
        BundledProduct=19,
        Subscription=20,
        Promotion=22,
        Coupon=23,
        Orders=25,
        SubscriptionProducts=26,
        CreateOfflineOrder=27,
        DeliveriesDashboard=28,
        DriversDashboard=30,
        Governorates=32,
        Areas=33,
        DeliveryTimeslots=34,
        DeliveryBlockedDates=35,
        QuickPaymentLinks = 36,
        NotificationTemplates =37,
        SendNotificationsToCustomers=38,
        AdminNotifications=40,
        CustomerManagement=41,
        WalletPackages=44,
        WalletPackageOrders=45, 
        PaymentMethods = 43,
    }
}

