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
});