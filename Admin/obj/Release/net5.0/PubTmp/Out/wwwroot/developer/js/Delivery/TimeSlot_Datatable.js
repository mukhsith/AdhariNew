var days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
var daysAr = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

$(document).ready(function () {
    loadData();
});

loadData = () => {
    if ($.fn.dataTable.isDataTable("#datatable-default-")) {
        $("#datatable-default-").DataTable().destroy();
    }
    buildDeliveryTimeSlot();
    ajaxGet('DeliveryTimeSlot/getAll', cbGetSuccess);
}
cbGetSuccess = (data) => {

    if (data.data == null) { return; }
    var r = data.data;
    for (var index = 0; index < r.length; index++) {
        var item = r[index];
        setCheckValue("active"+ index, item.active);
        $("#startTime" + index).val(item.startTimeOnly);
        $("#endTime" + index).val(item.endTimeOnly);
        $("#maximumOrders" + index).val(item.maximumOrders);
        
    }
}

updateTimeSlot = () => {
    var items = [];
    for (var index = 0; index < days.length; index++) {

        items.push({
            id: 0,
            dayId: index + 1,
            nameEn: days[index],
            namAr: daysAr[index],
            active: $("#active" + index).prop("checked"),
            startTimeOnly: $("#startTime" + index).val(),
            endTimeOnly: $("#endTime" + index).val(),
            maximumOrders: $("#maximumOrders" + index).val(),
            displayOrder: index,
            deleted:false
        });
    } 

    showLoader();
    
    var group = { 'deliveryTimeSlots': items };
   console.log(group);  
    $.ajax({
        url: getAPIUrl() + "DeliveryTimeSlot/UpdateAll",
        method: 'POST',
        dataType: 'json',
        data: group,
        headers: {
            "Authorization": 'Bearer ' + getToken()
        },
        success: function (data) { 
            //validateAPIResponse(data.statusCode, data.isSuccess);
            hideLoader();
            if (data.success) {
                ToastAlert('success', 'Delivery Time Slot', 'Saved Successfully');
            }  else {
             showLog(data);
            ToastAlert('error', 'DeliveryTimeSlot', 'unable to save, please try again or contact to system admin');
            }
            
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(jqXHR);
            hideLoader();

        }
    });

    //ajaxPost("DeliveryTimeSlot/UpdateAll", submitData, cbPostSuccess);

};


cbPostSuccess = (data) => {
    if (data.success) {
        ToastAlert('success', 'DeliveryTimeSlot', 'Saved Successfully');
        setTimeout(() => loadData(), 1000);
    } else {
        //showLog(data);
        ToastAlert('error', 'DeliveryTimeSlot', 'unable to save, please try again or contact to system admin');
    }
}



buildDeliveryTimeSlot = () => {
    $("#datatable-default- tbody").empty();
     
    var html = "";
    for (var index = 0; index < days.length; index++) {
        html += `<tr id="day${index}">
        <td data-th="Day">${days[index]}</td>
        <td data-th="Status">
             <div class="switch switch-sm switch-primary my-0  ">
                <input type="checkbox" name="active${index}" id="active${index}" data-plugin-ios-switch  />
            </div> 
        </td>
        <td data-th="Delivery Slot Start">  
            <select name="startTime${index}" id="startTime${index}"
                    class="form-select form-select-lg rounded-4 border-secondary" aria-describedby="">
                <option>12:00 AM</option>
                <option>01:00 AM</option>
                <option>02:00 AM</option>
                <option>03:00 AM</option>
                <option>04:00 AM</option>
                <option>05:00 AM</option>
                <option>06:00 AM</option>
                <option>07:00 AM</option>
                <option>08:00 AM</option>
                <option>09:00 AM</option>
                <option>10:00 AM</option>
                <option>11:00 AM</option>
                <option>12:00 PM</option>
                <option>01:00 PM</option>
                <option>02:00 PM</option>
                <option>03:00 PM</option>
                <option>04:00 PM</option>
                <option>05:00 PM</option>
                <option>06:00 PM</option>
                <option>07:00 PM</option>
                <option>08:00 PM</option>
                <option>09:00 PM</option>
                <option>10:00 PM</option>
                <option>11:00 PM</option>
            </select> 
        </td>
        <td data-th="Delivery Slot End"> 
            <select  name="endTime${index}" id="endTime${index}"  class="form-select form-select-lg rounded-4 border-secondary" aria-describedby="">
                <option>12:00 AM</option>
                <option>01:00 AM</option>
                <option>02:00 AM</option>
                <option>03:00 AM</option>
                <option>04:00 AM</option>
                <option>05:00 AM</option>
                <option>06:00 AM</option>
                <option>07:00 AM</option>
                <option>08:00 AM</option>
                <option>09:00 AM</option>
                <option>10:00 AM</option>
                <option>11:00 AM</option>
                <option>12:00 PM</option>
                <option>01:00 PM</option>
                <option>02:00 PM</option>
                <option>03:00 PM</option>
                <option>04:00 PM</option>
                <option>05:00 PM</option>
                <option>06:00 PM</option>
                <option>07:00 PM</option>
                <option>08:00 PM</option>
                <option>09:00 PM</option>
                <option>10:00 PM</option>
                <option>11:00 PM</option>
            </select>
        </td>
        <td data-th="Order Qty. Deliverable">
            <input type="number" class="form-control form-control-lg rounded-4 border-secondary "
            name="maximumOrders${index}" id="maximumOrders${index}" aria-describedby="" placeholder="0" value="0">
        </td>
    </tr>`;
    }

    $("#datatable-default- tbody").append(html);
//    return html;

}