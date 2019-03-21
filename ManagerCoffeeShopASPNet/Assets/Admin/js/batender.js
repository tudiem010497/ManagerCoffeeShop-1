$(document).ready(function () {
    $(".btnClosed").click(function () {
        if (confirm("Bạn có muốn hủy phục vụ loại đồ uống này!!!")) {
            var data = $(".btnClosed").attr("href") + "&confirm=true";
            $(".btnClosed").attr("href", data);
        }
        else {
            var data = $(".btnClosed").attr("href") + "&confirm=false";
            $(".btnClosed").attr("href", data);
        }
    });
});