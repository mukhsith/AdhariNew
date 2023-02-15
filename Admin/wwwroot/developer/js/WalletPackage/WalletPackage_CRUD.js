var id = 0;
$(document).ready(function () {
    setup();
    id = getIntegerValue("Id");
    loadDataFor();
});
setup = () => {
   
    $("#dataForm").validate({
        errorPlacement: 
            function(error,element) { },
        rules: {
            nameEn: { required: true },
            nameAr: { required: true },
            descriptionEn: { required: true },
            descriptionAr: { required: true },
            amount: { required: true },
            walletAmount: { required: true },
        },
        //messages: {
        //    nameEn: { required: '' },
        //    nameAr: { required: '' },
        //    descriptionEn: { required: '' },
        //    descriptionAr: { required: '' },
        //    amount: { required: '' },
        //    walletAmount: { required: '' },
        //},
        submitHandler: function (form, event) {
            event.preventDefault();
            saveData();
        }
    });


};

loadDataFor = () => { 
    if (id == 0) { return;}
    ajaxGet('WalletPackage/ById?Id=' + id, cbGetSuccess);
};
cbGetSuccess = (data) => {
    console.log(data.data);
    if (data.data == null) {  return; }
    var r = data.data;

    setTextValue("nameEn", r.nameEn);
    setTextValue("nameAr", r.nameAr);
    setTextValue("descriptionEn", r.descriptionEn);
    setTextValue("descriptionAr", r.descriptionAr);
    setTextValue("amount", r.amount);
    setTextValue("walletAmount", r.walletAmount);   
    setHiddenData(r);
   
}

 

saveData = () => {
    showLoader();
    let submitData = new FormData();
    submitData.append("Id", id); 
    submitData.append('nameEn', getTextValue('nameEn'));
    submitData.append('nameAr', getTextValue('nameAr'));
    submitData.append('descriptionEn', getTextValue('descriptionEn'));
    submitData.append('descriptionAr', getTextValue('descriptionAr'));
    submitData.append('amount', getFloatValue('amount'));
    submitData.append('walletAmount', getFloatValue('walletAmount'));

    submitHiddenData(submitData);
    ajaxPost("WalletPackage/AddEdit", submitData, cbPostSuccess);

};


cbPostSuccess = (data) => {
    if (data.success) { 
        ToastAlert('success', Resources.WalletPrepaidCards, Resources.SavedSuccessfully);
        setTimeout(() => location.href = "/WalletPackage/WalletPackageList", 1500);
    } else { 
        ToastAlert('error', Resources.WalletPrepaidCards, Resources.UnableTosave);
    }
}
 

 
       
   
 