
var dataTable = $("#clientDataTable").dataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "pageLength": 10,
    "autoWidth": false,
    "lengthMenu": [[10, 20, 30, 50, 100, -1], [10, 20, 30, 50, 100, "All"]],
    "order": [[0, "desc"]],
    "ajax": {
        "url": "/Clients/LoadClient/",
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
                if (statuTypeValue(full.statusType) === "Folloup") {

                    return `<span class="badge badge-primary font-12">${statuTypeValue(full.statusType)}</span>`;
                }
                else if (statuTypeValue(full.statusType) === "Converted") {
                    return `<span class="badge badge-success font-12">${statuTypeValue(full.statusType)}</span>`;
                }

                else if (statuTypeValue(full.statusType) === "NewLead") {

                    return `<span class="badge badge-purple font-12">${statuTypeValue(full.statusType)}</span>`;
                }
                else if (statuTypeValue(full.statusType) === "LeadManager") {

                    return `<span class="badge badge-purple font-12">${statuTypeValue(full.statusType)}</span>`;
                }
                else {
                    return `<span class="badge badge-danger font-12">${statuTypeValue(full.statusType)}</span>`;
                }

            }
        },


        {
            "render": function (data, type, full, meta) {
                return ` <a href="#" class="mr-2"><i class="fas fa-edit text-success font-16"></i></a>
                         <a ><i class="fas fa-trash-alt text-danger font-16"></i></a>
                      `;
            }
        }
    ]
});

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


    return '<div class="profileProgress" style="' + backgroundPos + '" ><img style="margin:5px;border:2px solid black;" height="90" width="80" src="' + obj.photo + '" alt="" ></div>';


}

var statusValue = ["Lost", "Folloup", "Converted", "NewLead", "LeadManager"]

const statuTypeValue = (Id) => {
    var count = 0;
    for (var i = 102; i <=106; i++) {
        if (Id == i) {
            return statusValue[count];
        }
        count++;
    }
}


$(document).ready(function () {

    $("body").on("click", "#clientSubmit", function (e) {
        e.preventDefault();
        SaveClient();
    });



    // Manager Edit //

});

const SaveClient = () => {
   
    if ($("#clientForm").valid()) {
        let formData = new FormData();   
        formData.append("Image", $(".dropify[name='Image']")[0].files[0]);
        let clientdata = objectifyForm($("#clientForm").serializeArray());
        for (var key in clientdata) {
            formData.append(key, clientdata[key]);
        }
        console.log(clientdata);
        $.ajax({
            url: "/Clients/Create/",
            type: "POST",
            data:formData,
            dataType: "JSON",
            processData: false,
            contentType: false,
            success: function (response) {
                if (response == true) {
                    toastr.success("Successs!!!", "Data Has been Successfully");
                    dataTable.fnFilter();
                    $("#clientForm")[0].reset();
                }
                else {
                    toastr.error("Something Error");
                }
            }
        });
    }
    else {
        toastr.error('Please fill up the form with valid data.', 'Validation Failed!');
    }
}

