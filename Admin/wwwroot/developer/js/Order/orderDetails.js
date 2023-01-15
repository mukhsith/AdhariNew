////var id = 0;
////var customerId = 0;
////$(document).ready(function () {
////    id = getIntegerValue("Id");
////    customerId = getIntegerValue("CustomerId");
////    loadDataFor();
////});
 
downloadPDF = (orderId) => {
    ajaxGet('Order/GetOrderPDF?id=' + orderId, cbGetPDFSuccess);
}
cbGetPDFSuccess = (data) => {
    window.open(data.data, "_blank");
}