function createAccount() {
    var employeeID = $('input').closest('div.form-group').find('input').val();
    alert(employeeID);
}
$(document).ready(function () {
    
    //$("#grid").mousedown(function (event) {
    //    switch (event.which) {
    //        case 3:
    //            $("#context-menu").show()
    //            break;
    //    }
    //})
    $("#grid").contextmenu(function (event) {
        $("#context-menu").css({
            display: "block",
            left: event.pageX,
            top: event.pageY
        })
        return false;
    })
    $('#grid').click(function () {
        $("#context-menu").css({
            display:"none"
        });
    });
    $(".addImage").click(function () {
        $("#listImage").modal("show")
    })
    $(".demo-1").on('clayfy-resize clayfy-cancel', function () {
        console.log($(".demo-1").width() + ' x ' + $(".demo-1").height())
    });
    
    $("#btnSaveDiagram").click(function () {
        var widthDiagram = $("#information form input#width").attr("value")
        var heightDiagram = $("#information form input#height").attr("value")
        var ratioDiagram = $("#information form input#ratio").attr("value")
        var data = "";
        data = data + '{"widthDiagram" : ' + widthDiagram + ","
        data = data + '"heightDiagram" : ' + heightDiagram + ","
        data = data + '"ratioDiagram" : ' + ratioDiagram + ","
        data = data + '"ListImageDiagram" : '
        $("svg.dropzone g.dragOn-drawArea image").each(function (index, element) {
            var xlink = $(this).attr("xlink:href")
            var width = $(this).attr("width")
            var height = $(this).attr("height")
            var rotate = $(this).attr("rotate")
            if(rotate == null) rotate = "000"
            var x = $(this).attr("x")
            var y = $(this).attr("y")
            var indexHref = xlink.indexOf("/Assets/Admin")
            var Href = xlink.substr(indexHref)
            if (index != 0) {
                data = data + ","
            }
            else {
                data = data + "["
            }
            data = data + '{ "ID":' + index + ",";
            data = data + '"Href" : "' + Href + '",';
            data = data + '"x" :' + x + ",";
            data = data + '"y" :' + y + ",";
            data = data + '"width" : ' + width + ",";
            data = data + '"height" : ' + height + ",";
            data = data + '"rotate" : "' + rotate + '"}'
        })
        data = data + "]}"
        $.ajax({
            url: '/admin/web/SaveDiagram?json=' + data,
            type: "POST",
            contenType: "application/json; charset=utf-8",
            data: data,
            dataType: "json",
            success: function (data) {
                alert("Gửi thành công " + data.PosID);
            },
            error: function (err) {
                alert("Error1 : " + err.error);
            }
        });
    })
    
})