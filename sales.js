
var dataTable = $("#salesDataTable").dataTable({
    "processing": true,
    "serverSide": true,
    "filter": true,
    "pageLength": 10,
    "autoWidth": false,
    "lengthMenu": [[10, 20, 30, 50, 100, -1], [10, 20, 30, 50, 100, "All"]],
    "order": [[0, "desc"]],
    "ajax": {
        "url": "/Salesman/LoadSales/",
        "type": "POST",
        "data": function (data) {
        },
        "complete": function (json) {
        }
    },
    "columns": [
        //{ "data": "id", "name": "Id", "autowidth": true, "className": "text_center" },
       
        { "data": "name", "name": "Name", "autowidth": true },
       
        {
            "render": function (data, type, full, meta) {
                return `<button style="font-size: inherit;" class="btn btn-sm btn-rx btn-table detailsBtn" value="${full.id}" data-toggle="tooltip" title="Product details"><i class="fas fa-file-alt"></i></button>
                        <button style="font-size: inherit;" class="btn btn-sm btn-rx btn-table salesTaskBtn" value="${full.id}" data-toggle="tooltip" title="Update product info!"><i class="fa fa-pen-fancy"></i></button>
                        <button style="font-size: inherit;" class="btn btn-sm btn-rx btn-table deleteBtn" value="${full.id}" data-toggle="tooltip" title="Delete product!"><i class="fa fa-trash"></i></button>`;
            }
        }
    ]
});





(function () {

    $("body").on("click", ".salesTaskBtn", function (e) {
        e.preventDefault();
        $("#salesModified-Modal").modal('show');
        let data = {
            id: $(this).val()
        };
      
        $.ajax({
            url: "/Salesman/TaskModified/",
            type: "GET",
            data: data,
            success: function (response) {
                $("#salesmodified-FormDiv").html(response);
            }
        });
    });



    $("body").on("click", "#salesManSave", function (e) {
        e.preventDefault();
        if ($("#salesManForm").valid()) {
            let formData = new FormData();
            formData.append("Image", $("[name='Image']")[0].files[0]);
            let salesData = objectifyForm($("#salesManForm").serializeArray());
            for (var key in salesData) {
                formData.append(key, salesData[key]);
            }

            console.log("Data" + salesData);

            $.ajax({
                url: "/Salesman/Create/",
                type: "POST",
                data: formData,
                dataType: "JSON",
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response == true) {
                        toastr.success("Sales Information", "Saved Successfully");

                    }
                    else {
                        toastr.error("Sales information", "Error!!!");
                    }
                }

            });

        } else {
            toastr.error('Please fill up the form with valid data.', 'Validation Failed!');
        }
    });

})();


