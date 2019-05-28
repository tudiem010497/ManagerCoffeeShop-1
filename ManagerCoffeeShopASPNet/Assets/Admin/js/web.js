function createAccount() {
    var employeeID = $('input').closest('div.form-group').find('input').val();
    alert(employeeID);
}
$(document).ready(function () {
    //$("#btnCreateAccount").click(function (e) {
    //    var employeeID = $('input').closest('div.form-group').find('input').val();
    //    var username = $("#username").val();
    //    var password = $("#password").val();
    //    var acctype = $("#acctype").val();
    //    var position = $("#position").val();
    //    var tempAvatar = $("#avatar").val();
    //    //alert(username);
    //    //alert(password);
    //    //alert(acctype);
    //    //alert(position);
        
    //    var avatar = tempAvatar.slice(12);
    //    alert(avatar);

    //    //var file = e.target.files;

    //    var data = '{"EmployeeID" : "' + employeeID + '",' +
    //                '"UserName" : "' + username + '",' +
    //                '"Password" : "' + password + '",' +
    //                '"AccType" : "' + acctype + '",' +
    //                '"Position" : "' + position + '",' +
    //                '"Avatar" : "' + avatar + '"}';
    //    $.ajax({
    //        url: '/admin/web/CreateAccountForEmployee?json=' + data,
    //        type: "POST",
    //        contenType: "application/json; charset=utf-8",
    //        data: data,
    //        dataType: "json",
    //        success: function (data) {
    //            alert("Gửi thành công ");
    //            window.location = "http://localhost:64599/admin/web/GetAllEmployee";
    //        },
    //        error: function (err) {
    //            alert("Error1 : " + err.error);
    //        }
    //    });
    //})

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
            data = data + '"height" : ' + height + "}";
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