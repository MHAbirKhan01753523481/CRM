
$(document).ready(function () {

    $("body").on("click", "#salesTaskItem", function (e) {
        e.preventDefault();
        $("#managerTaskList-Modal").modal('show');
        $.ajax({
            url: "/DashBord/SalesList/",
            type: "GET",
            success: function (response) {
                $("#managerTaskList-FormDiv").html(response);
            }
        });
    });

});