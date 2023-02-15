
$(document).ready(function () {

    //  fillDropDownList("governorateList", "Governorate/ForDropDownList", "id", "name", getGovernorateList);
    //fillDropDownList("areaList", "area/ForDropDownList", "id", "name");
    getCustomer();
  //  alert(Resources.AreYouSureTheOrderIsDelivered);
 
});


EditAddress = () => {

    var selectedAddressId = $('input[type=radio][name=address]:checked').val();


    setTextValue("dialogCustomerId", getTextValue('customerId'));
    setTextValue("dialogAddressId", selectedAddressId);


    ajaxGet('Governorate/ForDropDownList', geteditGovernorateList);

}



getCustomer = () => {
    var Id = $("#customerId").val();
    ajaxGet('customer/GetById?customerId=' + getTextValue('customerId'), getCustomerSuccess)
}


getGovernorateList = (data) => {
    console.log(data);
    fillDropDownListData("governorateList", data.data, false, null, "id", "name");
}


GetCityDropDownData = (id) => {
    ajaxGet('Area/ForDropDownListByCity?governorateId=' + id, getAreaList);

}
getAreaList = (data) => {
    console.log(data);
    fillDropDownListData("areaList", data.data, false, null, "id", "name");
}

$(document).on("change", "input[type=radio][name=address]", function () {
    var selectedTab = $(this).attr('id');
    console.log(selectedTab);
    $(this).closest('.addresses').find('.inputGroup').removeClass('active');
    $(this).closest('.inputGroup').addClass('active');
    $('.continue-btn').removeClass('disabled');
});


addCustomerAddress = () => {
    var addressType = $(".delivery-addresses .active a").attr('id');
    console.log(addressType);
}
 
getCustomerSuccess = (data) => {

    if (data.data.name != null) {
        var d = data.data;
        $("#customerId").val(d.id);
        $("#customerName").html(d.name);
        $("#customerEmail").html(d.emailAddress);
        $("#customerMobile").html(d.mobileNumber);

        loadCustomerAddress();

    } 
}


loadCustomerAddress = () => {
    ajaxGet('customer/getAllAddress?id=' + getTextValue("customerId"), showAddressListSuccess);

}
showAddressListSuccess = (data) => {

    if (data == null) { return; }
    var address = $("#display-addresses .row .addresses").html('');
    if (data.data.length > 0) {
        $("#noSavedAddress").hide();

        $(data.data).each(function (index, item) {
            var html = `<div class="col-12 col-md-4 mb-3">
                        <div class="inputGroup ${index === 0 ? "active" : ""} h-100 border border-secondary rounded-3 body-bg-secondary d-flex justify-content-between align-items-center p-3">
                            <label for="address-0" class="w-100">
                                <p class="address-name card-text mb-0 fw-bold">${item.name}</p>
                                <p class="address-content card-text mb-0"> ${item.addressText}</p>
                            </label>
                                <input data-id="${item.id}" id="${item.id}" name="address" value="${item.id}" onclick="saveAttributes()" type="radio" ${index === 0 ? "checked" : ""} >
                        </div>
                    </div>`;
            address.append(html);
        });
    }
    else {
        
        $("#noSavedAddress").show();
        $("#editAdd").hide();
    }

    //for (const r of data.data) { //no of rows
    //    var html = `<div class="col-12 col-md-4 mb-3">
    //                    <div class="inputGroup h-100 border border-secondary rounded-3 body-bg-secondary d-flex justify-content-between align-items-center p-3">
    //                        <label for="address-0" class="w-100">
    //                            <p class="address-name card-text mb-0 fw-bold">${r.name}</p>
    //                            <p class="address-content card-text mb-0"> ${r.addressText}</p>
    //                        </label>
    //                            <input data-id="${r.id}" id="${r.id}" name="address" type="radio" >
    //                    </div>
    //                </div>`;
    //    address.append(html);
    //}
    //ajaxGet('Product/GetAllProductAndCategoryForOfflineOrder?customerId=' + getTextValue("customerId"), cbGetProductAndCategoryList);
}
 

disableFields = (enable) => {
    clearFields();
    $("#customerName").attr("disabled", enable);
    $("#customerEmail").attr("disabled", enable)
    $("#customerTypeList").attr("disabled", enable);

}

 


