
 
addPopupAction = (apibase, row) => {
    var html =
        `<span data-bs-toggle1="modal" data-bs-target1="#sort_modal">
                    <a href="javascript:;" onclick="javascript:doDisplayOrder('${apibase}',${row.id},${row.displayOrder})"
                        class="mb-1 mt-1 me-1 btn btn-sm btn-warning" data-bs-toggle="tooltip" data-bs-placement="bottom"
                        title="Display Order" data-bs-original-title="Display Order" aria-label="Display Order">
                        <i class="fas fa-sort"></i>
                    </a>
                </span>`;
    return html;
}

doDisplayOrder = (apiName, id, num) => {
    $('#DisplayOrderModel').modal('toggle');
    $("#displayorderid").val(id);
    $("#displayapiname").val(apiName);
    $("#displayordervalue").val(num);
    $("#displayordervalue").focus(); 
}

updateDisplayOrder = () => {
    var endpoint = getAPIUrl() + $('#displayapiname').val() +
        "/UpdateDisplayOrder?id=" + $('#displayorderid').val() +
        "&num=" + $('#displayordervalue').val();

    $('#DisplayOrderModel').modal('toggle');

    showLoader();
    $.ajax({
        url: endpoint,
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": 'Bearer ' + getToken()
        },
        success: function (data) {
            hideLoader();
            if (data.success) {
                updateDataTableDisplayOrder(data);
                ToastAlert('success', 'Display Order', 'Successfully updated'); 
            } else {
                showLog(data);
                ToastAlert('danger', 'Display Order', data.message);
            }

        },
        error: function (xhr) {
            hideLoader();
            ToastAlert('error', 'Display Order', xhr);
        }
    });
}

updateDataTableDisplayOrder = (data) => {
    showLog(data);
    if (data.data == null) { return; }
    var row = data.data;
    $("[data-rowid=" + row["id"] + "]").find('[data-displayorder]').html(row["displayOrder"]);
    //var item = $("[data-displayOrder=" + row["id"] + "]").html(row["displayOrder"])
    //$("[data-displayorder-id=" + row["id"] + "]").html(row["displayOrder"])
}


