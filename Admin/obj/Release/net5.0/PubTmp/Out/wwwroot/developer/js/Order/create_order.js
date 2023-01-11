var parentId = 0;
var normalProducts = null;
var categories = null;
var NormalTable = null;

$(document).ready(function () {
    fillDropDownList("governorateList", "Governorate/forDropDownList", "id", "name", getGovernorateList);
    fillDropDownList("areaList", "area/forDropDownList", "id", "name", getAreaList);
    validateCreateCustomer(); 
});
getGovernorateList = (data) => {
    console.log(data);
}
getAreaList = (data) => {
    console.log(data);
}
enableVerifyButton = (input) => { 
    if (input.value.length == 8) {
        $("#verifyButton").show(); 
        getCustomer();
    } else {
        $("#verifyButton").hide();
        $("#btnCreateCustomer").hide();
        $(".add-normal-product-btn").hide();
        disableFields(false);
    }
}
getCustomer = () => {
    var mobile = $("#customerMobile").val(); 
    ajaxGet('customer/GetByMobileNumber?mobilenumber=' + mobile, getCustomerSuccess)
} 
validateCreateCustomer = () => {
    $("#dataForm").validate({
        rules: {
            customerMobile: { required: true },
            customerName: { required: true },
            customerTypeList: { required: true },
        },
        messages: {
            customerMobile: { required: 'Required' },
            customerName: { required: 'Required' },
            customerTypeList : { required: 'Required' },
        },
        submitHandler: function (form, event) {
            event.preventDefault();
            //saveData();
            showLoader(); 
            let submitData = new FormData(); 
            submitData.append('name', getTextValue('customerName') );
            submitData.append('mobileNumber', getTextValue('customerMobile') );
            submitData.append('emailAddress', getTextValue('customerEmail')); 
            submitData.append('b2b', (getSelectedItemValue('customerTypeList') == 0 ? false : true));
            submitData.append('languageId', 1);
            ajaxPost("customer/Create", submitData,
                function (data) {
                    if (data.success) {
                        ToastAlert('success', 'New Customer', "Customer created successfully");
                    } else {
                        console.log(data);
                    }
            });
        }
    });

    $('input[type=radio][name=address]').change(function () {
        var selectedTab = $(this).attr('id');
        $(this).closest('.addresses').find('.inputGroup').removeClass('active');
        $(this).closest('.inputGroup').addClass('active');
        $('.continue-btn').removeClass('disabled');
    });

   
}

addCustomerAddress = () => {
    var addressType = $(".delivery-addresses .active a").attr('id');
    console.log(addressType);
}
 
getCustomerSuccess = (data) => {
    if (data.data.name != null) {
        var d = data.data;
        $("#customerId").val(d.id);
        $("#btnCreateCustomer").hide();
        $(".add-normal-product-btn").show();
        disableFields(true);
        $("#customerName").val(d.name);
        $("#customerEmail").val(d.emailAddress);
        setSelectedItemByValue('customerTypeList', d.b2B ? 1 : 0);
        setWalletView(d.wallet);
        loadCustomerAddress();
        ajaxGet('Product/GetAllProductAndCategoryForOfflineOrder?customerId=' + d.id, cbGetProductAndCategoryList);
    } else {
        $("#btnCreateCustomer").show();
        disableFields(false);
        $(".add-normal-product-btn").hide();
    } 
}

setWalletView = (wallet) => { 
    if (wallet != null) {
        if (wallet.cashbackBalance <= 0) {
            $("#payFromWallet").hide();
        } else {
            $("#payFromWallet").show();
        }
        setLabelValue("cashbackAmountFormatted", wallet.formattedCashbackBalance);
        setLabelValue("walletAmountFormatted", wallet.formattedWalletBalance);
    }
}

loadCustomerAddress = () => {
    ajaxGet('customer/getAllAddress?id=' + getTextValue("customerId"), showAddressListSuccess);

}
showAddressListSuccess = (data) => {

    if (data == null) { return; }
    var address = $("#display-addresses .row .addresses").html('');
    for (const r of data.data) { //no of rows
        var html = `<div class="col-12 col-md-4 mb-3">
                        <div class="inputGroup h-100 border border-secondary rounded-3 body-bg-secondary d-flex justify-content-between align-items-center p-3">
                            <label for="address-0" class="w-100">
                                <p class="address-name card-text mb-0 fw-bold">${r.name}</p>
                                <p class="address-content card-text mb-0"> ${r.addressText}</p>
                            </label>
                                <input data-id="${r.id}" id="${r.id}" name="${r.id}" type="radio" >
                        </div>
                    </div>`;
        address.append(html);
    }
}
 

disableFields = (enable) => {
    clearFields();
    $("#customerName").attr("disabled", enable);
    $("#customerEmail").attr("disabled", enable)
    $("#customerTypeList").attr("disabled", enable);

}

clearFields=()=>{
    $("#customerName").val('');
    $("#customerEmail").val('');
    $("#customerTypeList").val('');
}

cbGetProductAndCategoryList = (data) => {
    products = data.data.products; //temporary save for filter of selected item from dropdown list
    categories = data.data.categories;
    fillDropDownListData("normalProductList", data.data.products, false, null, "id", "nameEn"); 
    prepareNormalProductDatatable();
    getCartItems();
    $("#divProductTabs").show();
}
 
prepareNormalProductDatatable = () => {
    initilizeDataTable();
    // Temporary Code. Needs to be made generic by the developer
    $('.add-normal-product-btn').click(function () {
         
        var selectedProductId = getSelectedItemValue("normalProductList"); //dropdownlist selectedItem id
        if (selectedProductId) {
            //existing all products
            var product = products.filter(function (el) { return el.id == selectedProductId; })[0];
            if (product != undefined) { 
                addCartItem(product.id, 1);
            }
        }
    });
    checkQtyValue = (input, max) => {
        if (input.value > max) {
            alert('maximum ' + max + " quantity is allowed");
            input.value = max;
        }
    }
    

}

addCartItem = (productId, quantity) => {
    var fromBody = {
        'customerId': getTextValue('customerId'),
        'countryId': 1,
        'productId': productId,
        'quantity': quantity
    };
    ajaxPostCart("Cart/AddCartItem", fromBody,
        function (data) { 
            if (data.success) {
                setTimeout(getCartItems(), 500);
            } else {
                console.log(data);
            }
            //hideLoader();
        });
}

editCartItem = (cartId, productId, input) => {
    var qty = $(input).val();
    var fromBody = {
        'customerId': getTextValue('customerId'),
        'countryId': 1,
        'id': cartId,
        'cartId': cartId,
        'productId': productId,
        'quantity': qty
    };
    ajaxPostCart("Cart/editcartitem", fromBody,
        function (data) { 
            if (data.success) {
                setTimeout(getCartItems(), 500);
            } else {
                console.log(data);
            }
            //hideLoader();
        });
}
deleteCartItem = (id) => { 
    ajaxDeleteCart("Cart/deletecartitem?id="+id,
        function (data) { 
            if (data.success) {
                setTimeout(getCartItems(), 500);
            } else {
                console.log(data);
            }
            //hideLoader();
        });
}

getCartItems = () => {
    ajaxGetCart("Cart/getcartitem?customerId=" + $("#customerId").val(),
        function (data) { 
            if (data.success) { 
                setTimeout(displayCartItems(data), 500); 
            } else {
                emptyDataTable();
            }
            //hideLoader();
        });
}
 
displayCartItems = (data) => {
    var table = $(".normal-datatable-default- tbody").empty();
    if (data.data == null) return;
    var quantity = 0;
    $.each(data.data.cartItems, function (a, item) { 
        quantity += item.quantity;
        table.append(`<tr>
            <td><span data-id='${item.id}' data-productid='${item.productId}'  >${item.id}</span> </td>
            <td><img src='${item.product.imageUrl}' height='100px'> </td>
            <td>${item.product.title}</td>
            <td><span data-price='${item.formattedTotal}'>${item.formattedTotal}</span> </td>
            <td>
                <input type='number' class='form-control form-control-lg border-secondary'
                min=1 max=${item.stockQuantity} value=${item.quantity}
                onkeyup='checkQtyValue(this,${item.stockQuantity})'
                onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                onchange='editCartItem(${item.id},${item.productId},this)'>
            </td>
            <td><a href='javascript:;' onclick='deleteCartItem(${item.id})' class='mb-1 mt-1 me-1 btn btn-sm btn-danger btn-remove-normal-product'
                        data-bs-toggle='tooltip' data-bs-placement='bottom' title=''
                        data-bs-original-title='Remove Product' aria-label='Remove Product'>
                            <i class='fa fa-xmark'></i>
                    </a>
            </td>
        </tr>`);
    }); 

    if (quantity >= 0) { 
        $('.normal-datatable-default-').find('tfoot').show();
        $('.total-normal-price').html(data.formattedCartTotal);
        $('.total-normal-qty').html(quantity);
        $("#display-addresses").show();
        $("#divDeliveryDetials").show();
        $("#divdiscountPromotion").show();
        $("#divPaymentSummary").show();
    }

}

saveAttributes = (code) => {
    /*$.post("@Url.RouteUrl("savecartattributes")", { CouponCode: code, AttributeTypeId: attributeTypeId },
      SelectAddress = 1,
     AddWalletAmount = 2,
        RemoveWalletAmount = 3,
         ApplyCoupon = 4,
         RemoveCoupon = 5,
        SelectPaymentMethod = 6,
         Subscribe = 7
    
     * ,[CustomerId]
      ,[CouponId]
      ,[AddressId]
      ,[PaymentMethodId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[ModifiedBy]
      ,[ModifiedOn]
      ,[Deleted]
      ,[UseWalletAmount]
     */
    var fromBody = {
        'CouponCode': getTextValue('customerId'),
        'CouponCode': getTextValue('customerId'),
        'countryId': 1,
        'id': cartId,
        'cartId': cartId,
        'productId': productId,
        'quantity': qty
    };
    ajaxPostCart("Cart/savecartattributes", fromBody,
        function (data) {
            if (data.success) {
                setTimeout(getCartItems(), 500);
            } else {
                console.log(data);
            }
            //hideLoader();
        });
}
initilizeDataTable = () => {
    console.log("Ready normal product table");
    if ($.fn.dataTable.isDataTable(".normal-datatable-default-")) {
        $(".normal-datatable-default-").DataTable().destroy();
    } 
     $('.normal-datatable-default-').DataTable({
        responsive: true,
        searching: false,
        paging: false,
        info: false,
        ordering: false,
        serverSide: false,
        buttons: [],
        language: { emptyTable: "No Normal Products Added." },
        columnDefs: [{ "width": "5%", "targets": [0, 5] }],
        columnDefs: [{ "width": "20%", "targets": [1, 4] }]
    });
    
}
emptyDataTable = () => {
    $('.normal-datatable-default- tbody').empty();
     
}

