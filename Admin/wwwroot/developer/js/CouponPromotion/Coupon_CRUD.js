var id = 0;
$(document).ready(function () {
    id = getIntegerValue("Id");
    setup();
    loadDataFor();
});
setup = () => {
   
    $("#dataForm").validate({
        rules: {
            couponCode: { required: true },
            startDate: { required: true },
            endDate: { required: true }
        },
        messages: {
            couponCode: { required: '' },
            startDate: { required: '' },
            endDate: {required: ''},
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });

    $('.generate-code').click(function () {
        generateCouponCode();
    });
       $(document).on('click', '.LimitUsage-switch .ios-switch', function () {
                $('.LimitUsage').toggle();
       });
        if ($(document).find('.LimitUsage-switch [data-plugin-ios-switch]').prop('checked')) {
            $('.LimitUsage').show();
        } else {
            $('.LimitUsage').hide();
         }
                                        
      
};

loadDataFor = () => {
    if (id == 0) { return;}
    ajaxGet('Coupon/ById?Id=' + id, cbGetSuccess);
};
cbGetSuccess = (data) => {
    
    if (data.data == null) {  return; }
    var r = data.data;

    setTextValue("couponCode", r.couponCode);

    setTextValue("startDate", getFormatedDate(r.startDate));
    setTextValue("endDate", getFormatedDate(r.endDate));

    if (r.discountType == 1) {
        $("input[type=radio]")[0].checked = true;
        $("#discountPercentage").val(r.discountPercentage);
    } else {
        $("input[type=radio]")[1].checked = true;
        $("#discountAmount").val(r.discountAmount); 
    }
     
    if (r.limitUsageEnabled) {
        setCheckValue('limitUsageEnabled', r.limitUsageEnabled);
        setTextValue('quantity', r.quantity); 
    }
    setCheckValue('active', r.active);
     
    setHiddenData(r);
   
}

 
isFormValid = () => {

    if ($("input[type=radio]")[0].checked == false &&
        $("input[type=radio]")[1].checked == false) {
        ToastAlert('error', Resources.Coupons, Resources.PleaseSelectDiscountType);
        return false;
    }

    if ($("input[type=radio]")[0].checked && getFloatValue('discountPercentage') <=0 ) {
        ToastAlert('error', Resources.Coupons, Resources.PleaseEnterDiscountPercentageValue);
        $("#discountPercentage").focus();
        return false;
    }

    if ($("input[type=radio]")[1].checked && getFloatValue('discountAmount') <= 0) {
        ToastAlert('error', Resources.Coupons, Resources.PleaseEnterDiscountAmountValue );
        $("#discountAmount").focus();
        return false;
    }

    if (getCheckValue("limitUsageEnabled") && getIntegerValue('quantity') <= 0) {
        ToastAlert('error', Resources.Coupons, Resources.PleaseEnterQuantity);
        $("#quantity").focus();
        return false;
    }
}
saveData = () => {
    if (isFormValid() == false) { return;}
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id);

    submitData.append('couponCode', getTextValue('couponCode'));
    if (getDatePickerValue('startDate') != null) {
        submitData.append('startDate', getDatePickerValue('startDate'));
    }
    if (getDatePickerValue('endDate') != null) {
        submitData.append('endDate', getDatePickerValue('endDate'));
    }
    if ($("input[type=radio]")[0].checked) {
        submitData.append('discountType', 1); //percentage
        submitData.append('DiscountPercentage', getFloatValue('discountPercentage'));
    } else if ($("input[type=radio]")[1].checked) {
        submitData.append('discountType', 2); //amount
        submitData.append('discountAmount', getFloatValue('discountAmount'));
    }

    if (getCheckValue("limitUsageEnabled")) {
        submitData.append('limitUsageEnabled', true);
        submitData.append('quantity', getFloatValue('quantity'));
    }
    var active = $("input[type=checkbox]")[0].checked;
    submitData.append('active', active);

    submitHiddenData(submitData);
    ajaxPost("Coupon/AddEdit", submitData, cbPostSuccess, cbPostError);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', Resources.Coupons, Resources.SavedSuccessfully);
        setTimeout(() => location.href = "/CouponPromotion/CouponList", 1500);
    } else {
        //showLog(data);
        ToastAlert('error', Resources.Coupons, data.message);
    }
}

cbPostError = (error) => { 
    ToastAlert('error', Resources.Coupons, Resources.UnableTosave);
}

  

generateCouponCode = () => { 
    $('.coupon-code-input').val(generateRandomText(8));
}

function generateRandomText(limit) {
    var text = "";
    var possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    for (var i = 0; i < limit; i++)
        text += possible.charAt(Math.floor(Math.random() * possible.length));
    return text;
}