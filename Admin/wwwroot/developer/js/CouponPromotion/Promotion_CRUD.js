 
$(document).ready(function () { 
    setup();
    ajaxGet('Promotion/GetDefault', cbGetSuccess);
});
setup = () => {
   
    $("#dataForm").validate({
        rules: { },
        messages: {  },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });

    $(document).on('click', '.SignupPromotion-switch .ios-switch', function () {
        $('.SignupPromotion').toggle();
    });
    if ($(document).find('.SignupPromotion-switch [data-plugin-ios-switch]').prop('checked')) {
        $('.SignupPromotion').show();
    } else {
        $('.SignupPromotion').hide();
    }

    $(document).on('click', '.CashbackOnPurchasePromotion-switch .ios-switch', function () {
        $('.CashbackOnPurchasePromotion').toggle();
    });
    if ($(document).find('.CashbackOnPurchasePromotion-switch [data-plugin-ios-switch]').prop('checked')) {
        $('.CashbackOnPurchasePromotion').show();
    } else {
        $('.CashbackOnPurchasePromotion').hide();
    }
                                        

    $(document).on('click', '.EnableCashbackUse-switch .ios-switch', function () {
        $('.EnableCashbackUse').toggle();
    });
    if ($(document).find('.EnableCashbackUse-switch [data-plugin-ios-switch]').prop('checked')) {
        $('.EnableCashbackUse').show();
    } else {
        $('.EnableCashbackUse').hide();
    }

};
 
 
cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;

    
    //sign up pormotion
    setCheckValue('signupEnabled', r.signupEnabled);
    if (r.signupEnabled) {
        $('.SignupPromotion').show();
    }
    setTextValue("signupCashbackValue", r.signupCashbackValue);
    setTextValue("signupCashbackValueExpiryInNoOfDays", r.signupCashbackValueExpiryInNoOfDays);
    
    if (r.signupFromDate != null && r.signupToDate != null) {
        setTextValue("signupFromDate", getFormatedDate(r.signupFromDate));
        setTextValue("signupToDate", getFormatedDate(r.signupToDate));
    }

    //cashback on purchases
    setCheckValue('cashbackOnPurchaseEnabled', r.cashbackOnPurchaseEnabled);
    if (r.cashbackOnPurchaseEnabled) {
        $('.CashbackOnPurchasePromotion').show();
        setTextValue("cashbackOnPurchaseMinOrderAmount", r.cashbackOnPurchaseMinOrderAmount);
        setTextValue("cashbackOnPurchaseValue", r.cashbackOnPurchaseValue);
        setTextValue("cashbackOnPurchaseExpiryInNoOfDays", r.cashbackOnPurchaseExpiryInNoOfDays);
        
    }

    if (r.cashbackOnPurchaseFromDate != null & r.cashbackOnPurchaseToDate !=null ) {
        setTextValue("cashbackOnPurchaseFromDate", getFormatedDate(r.cashbackOnPurchaseFromDate));
        setTextValue("cashbackOnPurchaseToDate", getFormatedDate(r.cashbackOnPurchaseToDate));
    }

    //cashback Redeem condition
    setCheckValue('cashbackRedeemEnabled', r.cashbackRedeemEnabled);
    if (r.cashbackRedeemEnabled) {
        $('.EnableCashbackUse').show();
        setTextValue("cashbackRedeemMinWalletAmount", r.cashbackRedeemMinWalletAmount);
        setTextValue("cashbackRedeemMinOrderAmount", r.cashbackRedeemMinOrderAmount);
        setTextValue("cashbackValueToDeduct", r.cashbackValueToDeduct);
    }
     
    setHiddenData(r);
   
}

 
isFormValid = () => {
    //sign up pormotion
    var signupEnabled = getCheckValue('signupEnabled');
    if (signupEnabled) {
        var signupCashbackValue = getIntegerValue('signupCashbackValue');
        if (signupCashbackValue == 0) {
            ToastAlert('error','Promotion', 'Please enter signup Cashback value');
            $("#signupCashbackValue").focus();
            return false;
        }
    }
    //cashback on purchases
    var cashbackOnPurchaseEnabled = getCheckValue('cashbackOnPurchaseEnabled');
    if (cashbackOnPurchaseEnabled) {
        var cashbackOnPurchaseMinOrderAmount = getFloatValue('cashbackOnPurchaseMinOrderAmount');
        if (cashbackOnPurchaseMinOrderAmount == 0) {
            ToastAlert('error', 'Promotion',  "Please enter cashback On Purchase Minimum Order Amount value");
            $("#cashbackOnPurchaseMinOrderAmount").focus();
            return false;
        }

        var cashbackOnPurchaseValue = getFloatValue('cashbackOnPurchaseValue');
        if (cashbackOnPurchaseValue == 0) {
            ToastAlert('error', 'Promotion', "Please enter cashback On Purchase value");
            $("#cashbackOnPurchaseValue").focus();
            return false;
        }
    }
    //cashback Redeem condition
    var cashbackRedeemEnabled = getCheckValue('cashbackRedeemEnabled');
    if (cashbackRedeemEnabled) {
        var cashbackRedeemMinWalletAmount = getFloatValue('cashbackRedeemMinWalletAmount');
        if (cashbackRedeemMinWalletAmount == 0) {
            ToastAlert('error', 'Promotion',"Please enter cashback Redeem Minimum Wallet Amount Value");
            $("#cashbackRedeemMinWalletAmount").focus();
            return false;
        }

        var cashbackRedeemMinOrderAmount = getFloatValue('cashbackRedeemMinOrderAmount');
        if (cashbackRedeemMinOrderAmount == 0) {
            ToastAlert('error', 'Promotion', "Please enter cashback Redeem Minimum Order Amount Value");
            $("#cashbackRedeemMinOrderAmount").focus();
            return false;
        }

        var cashbackValueToDeduct = getFloatValue('cashbackValueToDeduct');
        if (cashbackValueToDeduct == 0) {
            ToastAlert('error', 'Promotion', "Please enter cashback Value to Deduct");
            $("#cashbackValueToDeduct").focus();
            return false;
        }

    } 
}
saveData = () => {
    if (isFormValid() == false) { return;}
    showLoader();
    let submitData = new FormData();

    //sign up pormotion
    var signupEnabled = getCheckValue('signupEnabled');
    if (signupEnabled) {
        submitData.append('signupEnabled', getCheckValue('signupEnabled'));
        submitData.append('signupCashbackValue', getFloatValue('signupCashbackValue'));
        submitData.append('signupCashbackValueExpiryInNoOfDays', getIntegerValue('signupCashbackValueExpiryInNoOfDays'));
        if (getDatePickerValue('signupFromDate') != null) {
            submitData.append('signupFromDate', getDatePickerValue('signupFromDate'));
        }
        if (getDatePickerValue('signupToDate') != null) {
            submitData.append('signupToDate', getDatePickerValue('signupToDate'));
        }
    }
    //cashback on purchases
    var cashbackOnPurchaseEnabled = getCheckValue('cashbackOnPurchaseEnabled'); 
    if (cashbackOnPurchaseEnabled) {
        submitData.append('cashbackOnPurchaseEnabled', cashbackOnPurchaseEnabled);
            submitData.append('cashbackOnPurchaseMinOrderAmount', getFloatValue('cashbackOnPurchaseMinOrderAmount'));
            submitData.append('cashbackOnPurchaseValue', getFloatValue('cashbackOnPurchaseValue'));
            submitData.append('cashbackOnPurchaseExpiryInNoOfDays', getIntegerValue('cashbackOnPurchaseExpiryInNoOfDays'));
        
      
        if (getDatePickerValue('cashbackOnPurchaseFromDate') != null) {
            submitData.append('cashbackOnPurchaseFromDate', getDatePickerValue('cashbackOnPurchaseFromDate'));
        }
        if (getDatePickerValue('cashbackOnPurchaseToDate') != null) {
            submitData.append('cashbackOnPurchaseToDate', getDatePickerValue('cashbackOnPurchaseToDate'));
        }
    }

    //cashback on purchases
    var cashbackRedeemEnabled = getCheckValue('cashbackRedeemEnabled');
    submitData.append('cashbackRedeemEnabled', cashbackRedeemEnabled);
    if (cashbackRedeemEnabled) {
        submitData.append('cashbackRedeemMinWalletAmount', getFloatValue('cashbackRedeemMinWalletAmount'));
        submitData.append('cashbackRedeemMinOrderAmount', getFloatValue('cashbackRedeemMinOrderAmount'));
        submitData.append('cashbackValueToDeduct', getFloatValue('cashbackValueToDeduct'));
    }
     
    submitHiddenData(submitData);
    ajaxPost("Promotion/Update", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', 'Promotion', 'Saved Successfully');
        cbGetSuccess(data);
    } else {
        //showLog(data);
        ToastAlert('error', 'Promotion', data.message);
    }
}

cbPostError = (error) => { 
    ToastAlert('error', 'Promotion', 'unable to save, please try again or contact to system admin');
}

 