$(document).ready(function () {
    
    var href;
    $(".btnCancel").click(function () {
        if (confirm("Bạn có muốn hủy phục vụ loại đồ uống này!!!")) {
            href = $(".btnCancel").attr("href") + "&confirm=true";
            $(".btnCancel").attr("href", href);
        }
        else {
            href = $(".btnCancel").attr("href") + "&confirm=false";
            $(".btnCancel").attr("href", href);
        }
    });
    $("select#IngreID").change(function () {
        var IngreID = $("select#IngreID").find(":selected").attr("value")
        $.ajax({
            url: '/admin/batender/GetUnitByIngreID?IngreID=' + IngreID,
            type: "POST",
            contenType: "application/json; charset=utf-8",
            data: IngreID,
            dataType: "json",
            success: function (data) {
                $("#UnitIngre").attr("value", data.Unit)
            },
            error: function (err) {
                alert("Error1 : " + err.error);
            }
        });
    })
});