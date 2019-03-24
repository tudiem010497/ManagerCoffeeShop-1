$(document).ready(function () {
    var href;
    $(".btnClosed").click(function () {
        if (confirm("Bạn có muốn hủy phục vụ loại đồ uống này!!!")) {
            href = $(".btnClosed").attr("href") + "&confirm=true";
            $(".btnClosed").attr("href", data);
        }
        else {
            href = $(".btnClosed").attr("href") + "&confirm=false";
            $(".btnClosed").attr("href", data);
        }
    });
});