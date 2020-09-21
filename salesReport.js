var dataTable = $("#workTaskSalesData").dataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "pageLength": 5,
    "autoWidth": false,
    "lengthMenu": [[5, 20, 30, 50, 100, -1], [5, 20, 30, 50, 100, "All"]],
    "order": [[0, "desc"]],
    "ajax": {
        "url": "/WorkTask/LoadWorkTask/",
        "type": "POST",
        "data": function (data) {
        },
        "complete": function (json) {
        }
    },
    "columns": [

        { "data": "managerName", "name": "ManagerName", "autowidth": true },
        { "data": "name", "name": "Name", "autowidth": true },
        { "data": "requirement", "name": "Requirement", "autowidth": true },
        { "data": "changeToDate", "name": "ChangeToDate", "autowidth": true },
        { "data": "changeEndDate", "name": "ChangeEndDate", "autowidth": true },
        { "data": "comment", "name": "Comment", "autowidth": true },
        {
            "render": function (data, type, full, meta) {
                var List = "";
                for (var i = 0; i < full.mark; i++) {
                    List += "<span class='fa fa-star text-warning'></span>";
                }
                for (var i = 0; i < (7 - Number(full.mark)); i++) {
                    List += "<span class='fa fa-star '></span>";
                }
                // console.log(List);
                return `${List}`;
            }
        },
       
        
        {
            "render": function (data, type, full, meta) {

                var buttonList ="";
                if (full.name != null) {
                    buttonList += `<button style="font-size: inherit;" class="btn btn-sm btn-rx btn-table workTaskTeplayListBtn" value="${full.id}" data-toggle="tooltip" title="Sales Report"><i class="fas fa-edit text-success font-16"></i></button>`;
                }
               // buttonList += `<button style="font-size: inherit;" class="btn btn-sm btn-rx btn-table " value="${full.id}" data-toggle="tooltip" title="Sales Report"><i class="fas fa-trash-alt text-danger font-16"></i></button>`;
                return buttonList;

            }
        }
    ]
});

$(document).ready(function () {

    $("body").on("click", ".workTaskTeplayListBtn", function (e) {
        e.preventDefault();

        $("#workTaskReport-Modal").modal("show");

        let reportId = {
            id: $(this).val()
        };

        $.ajax({
            url: "/WorkTask/SalesTask/",
            type: "GET",
            data: reportId,
            success: function (response) {
                $("#workTaskReport-FormDiv").html(response);
            }
        });
    });
});