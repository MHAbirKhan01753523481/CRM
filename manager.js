
function calcProfile(obj) {

    var totalLength = 380;
    var value = obj.id * 15;
    var borderLen = (value / 100) * totalLength;
    var backgroundPos = '';
    if (borderLen <= 90) {
        backgroundPos = 'background-position: ' + (-90 + borderLen) + 'px 0px, 85px -100px, 90px 95px, 0px 100px';
        $('.progress').attr('style', backgroundPos);
    } else if (borderLen <= 190) {
        backgroundPos = 'background-position: 0px 0px, 85px ' + (-100 + (borderLen - 90)) + 'px, 90px 95px, 0px 100px';
        $('.progress').attr('style', backgroundPos);
    } else if (borderLen <= 280) {
        backgroundPos = 'background-position: 0px 0px, 85px 0px, ' + (90 - (borderLen - 90 - 100)) + 'px 95px, 0px 100px';
        $('.progress').attr('style', backgroundPos);
    } else {
        backgroundPos = 'background-position: 0px 0px, 85px 0px, 0px 95px, 0px ' + (100 - (borderLen - (90 * 2) - 100)) + 'px';
        $('.progress').attr('style', backgroundPos);
    }

    return '<div class="profileProgress" style="' + backgroundPos + '" ><img style="margin:5px;" height="90" width="80" src="' + obj.photoUrl + '" alt="" ></div>';


}


var dataTable = $("#managerDataList").dataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "pageLength": 10,
    "autoWidth": false,
    "lengthMenu": [[10, 20, 30, 50, 100, -1], [10, 20, 30, 50, 100, "All"]],
    "order": [[0, "desc"]],
    "ajax": {
        "url": "/Manager/LoadManager/",
        "type": "POST",
        "data": function (data) {
        },
        "complete": function (json) {
        }
    },
    "columns": [
        //{ "data": "id", "name": "Id", "autowidth": true, "className": "text_center" },
        {
            "orderable": false, "render": function (data, type, full, meta) { return calcProfile(full); }
        },
        { "data": "name", "name": "Name", "autowidth": true },

        { "data": "email", "name": "Email", "autowidth": true },
        { "data": "phone", "name": "Phone", "autowidth": true },
        { "data": "address", "name": "Address", "autowidth": true },
        {
            "render": function (data, type, full, meta) {
                //return ` 
                //       <button style="font-size: inherit;" class="btn btn-sm btn-success btn-rx btn-table managerEditBtn" value="${full.id}" ><i class="fas fa-edit font-16"></i></button>
                //      `;
                var list = "";
                if (full.designation =="Lead Manager") {
                    list += `<button style="font-size: inherit;margin-right:10px;" class="btn btn-sm btn-success btn-rx btn-table managerEditBtn" value="${full.id}" ><i class="fas fa-edit font-16"></i></button>`;
                }
               
                list +=`<button style="font-size: inherit;" class="btn btn-sm btn-danger btn-rx btn-table managerDeleteBtn" value="${full.id}" ><i class="fas fa-trash font-16"></i></button>`;
                return list;
            }
        }
    ]
});


$(document).ready(function () {

$("body").on("click", ".managerAdd", function (e) {
    e.preventDefault();
    $("#managerAdd-Modal").modal('show');

    $.ajax({
        url: "/Manager/CreateManagerView/",
        type: "GET",
        success: function (response) {
            $("#managerAdd-FormDiv").html(response);
        }
    })
});


$("body").on("click", ".managerEditBtn", function (e) {
    e.preventDefault();

    $("#managerEdit-Modal").modal('show');

    let managerData = {
        id: $(this).val()
    };

    $.ajax({
        url: "/Manager/EditView/",
        type: "GET",
        data: managerData,
        success: function (response) {
            $("#managerEdit-FormDiv").html(response);
        }
    });
});

    $("body").on("click", ".managerDeleteBtn", function (e) {
        e.preventDefault();

        let managerData = {
            id: $(this).val()
        };
        $.ajax({
            url: "/Manager/Delete/",
            type: "GET",
            data: managerData,
            success: function (response) {
                if (response == true) {
                    toastr.success("Successs!!!", "Data Has been Delete Successfully").css("background-color", "blue");
                    dataTable.fnFilter();
                }
                else {
                    toastr.error("Manager Remove are not access ?").css("background-color", "red");
                }
            }
        });
    });


});