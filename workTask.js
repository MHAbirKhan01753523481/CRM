var HiddenIndex;

var dataTable = $("#workTaskTable").dataTable({
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
      
        { "data": "name", "name": "Name", "autowidth": true },
        { "data": "requirement", "name": "Requirement", "autowidth": true },
        { "data": "salesManName", "name": "SalesManName", "autowidth": true },
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

                var buttonList = `<button style="font-size: inherit;" class="btn btn-sm btn-rx btn-table workTaskEditBtn" SalesManId="${full.salesManId}" ManagerId="${full.managerId}" IconClass="${full.iconClass}" IconColor="${full.iconColor}" Name="${full.name}" Requirement="${full.requirement}" ChangeEndDate="${full.changeEndDate}" ChangeToDate="${full.changeToDate}" value="${full.id}"  data-toggle="tooltip" title="Product details"><i class="fas fa-edit text-success font-16"></i></button>`;
                    buttonList += `<button style="font-size: inherit;" class="btn btn-sm btn-rx btn-table  workTaskReplay" value="${full.id}" data-toggle="tooltip" title="Task details"><i class="dripicon dripicons-reply text-warning font-16"></i></button>`;  
                    buttonList += `<button style="font-size: inherit;" class="btn btn-sm btn-rx btn-table " value="${full.id}" data-toggle="tooltip" title="Product details"><i class="fas fa-trash-alt text-danger font-16"></i></button>`;
                return `${buttonList}`;
           
            }
        }
    ]
});




$(document).ready(function () {

    $("body").on("click", "#workTaskSaveBtn", function (e) {
        e.preventDefault();

        if ($(this).text() =="Update") {
            $(this).text("Save");
            workTaskUpdate();
            $("#workTaskForm")[0].reset();

        } else {
            workTaskDataSave();
        }
       
    });

    $("body").on("click", "#workTaskresetBtn", function (e) {
        e.preventDefault();
        $("#workTaskSaveBtn").text("Save");
        $("#workTaskForm")[0].reset();
    });

    //$("body").on("click", ".workTaskBtn", function (e) {
    //    e.preventDefault();
    //    $("#workTask-Modal").modal("show");
    //    let workData = {
    //        id: $(this).val()
    //    };
    //    $.ajax({
    //        url: "/WorkTask/SalesTask/",
    //        type: "GET",
    //        data: workData,
    //        success: function (response) {
    //            $("#workTask-FormDiv").html(response);
    //        }
    //    });
    //});

    $("body").on("click", ".workTaskEditBtn", function (e) {
        e.preventDefault();
        $("#Name").val($(this).attr("Name"));
        $("#ToDate").val($(this).attr("ChangeToDate"));
        $("#EndDate").val($(this).attr("ChangeEndDate"));
        $("#SalesManId").val($(this).attr("SalesManId")).change();
        $("#ManagerId").val($(this).attr("ManagerId")).change();
        $("#IconColor").val($(this).attr("IconColor")).change();
        $("#IconClass").val($(this).attr("IconClass")).change();
        $("#Requirement").val($(this).attr("Requirement"));

        HiddenIndex = $(this).attr("value");

        $("#workTaskSaveBtn").text("Update");
    });


    $("body").on("click", ".workTaskReplay", function (e) {
        e.preventDefault();

        $("#replayTask-Modal").modal("show");
        let replayTask = {
            id: $(this).val()
        };
        $.ajax({
            url: "/WorkTask/ReplayeView",
            type: "GET",
            data: replayTask,
            success: function (response) {
                $("#replayTask-FormDiv").html(response);
            }
        });
    });



});


const workTaskUpdate = () => {

    if ($("#workTaskForm").valid()) {
        let workUpdateData = {
            Id: HiddenIndex,
            Name: $("#Name").val(),
            Requirement: $("#Requirement").val(),
            ChangeToDate: $("#ChangeToDate").val(),
            ChangeEndDate: $("#ChangeEndDate").val(),
            ManagerId: $("#ManagerId").val(),
            SalesManId: $("#SalesManId").val(),
            IconClass: $("#IconClass").val(),
            IconColor: $("#IconColor").val(),
        };
        $.ajax({
            url: "/WorkTask/UpdateWorkTask/",
            type: "POST",
            data: workUpdateData,
            success: function (response) {
                if (response == true) {
                    toastr.success("Work Task Data Save ", "Successfully").css("background-color", "blue");
                    dataTable.fnFilter();
                    $("#workTaskForm")[0].reset();
                }
                else {
                    toastr.error("Something Error", "Error!!!").css("background-color", "blue");    
                }
            }
        });
    }
    else {
        toastr.error('Please fill up the form with valid data.', 'Validation Failed!').css("background-color", "blue");
    }
}



const workTaskDataSave = () => {

    if ($("#workTaskForm").valid()) {
        let workData = {
            Name: $("#Name").val(),
            Requirement: $("#Requirement").val(),
            ToDate: $("#ToDate").val(),
            EndDate: $("#EndDate").val(),
            ManagerId: $("#ManagerId").val(),
            SalesManId: $("#SalesManId").val(),
            IconClass: $("#IconClass").val(),
            IconColor: $("#IconColor").val(),
            // FixedTime: $("#FixedTime").val(),
        }
        console.log(workData);

        $.ajax({
            url: "/WorkTask/SaveData/",
            type: "POST",
            data: workData,
            success: function (response) {
                if (response == true) {
                    toastr.success("Work Task Data Save ", "Successfully");
                    dataTable.fnFilter();
                    $("#workTaskForm")[0].reset();
                }
                else {
                    toastr.error("Something Error", "Error!!!");
                }
            }
        });
    }
    else {
        toastr.error('Please fill up the form with valid data.', 'Validation Failed!');
    }
}