function createAccount() {
    var employeeID = $('input').closest('div.form-group').find('input').val();
    alert(employeeID);
}
$(document).ready(function () {
    

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
        var CSID = $("#information form #CSID option:selected").val()
        var FloorID = $("#information form input#FoorID").attr("value")
        var data = "";
        data = data + '{"widthDiagram" : ' + widthDiagram + ","
        data = data + '"heightDiagram" : ' + heightDiagram + ","
        data = data + '"ratioDiagram" : ' + ratioDiagram + ","
        data = data + '"CSID" :' + CSID + ","
        data = data + '"FloorID" : "' + FloorID + '",'
        data = data + '"ListImageDiagram" : '
        var temp = 0
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
            temp++;
            data = data + '{"ID":' + temp + ",";
            data = data + '"Href" : "' + Href + '",';
            data = data + '"x" :' + x + ",";
            data = data + '"y" :' + y + ",";
            data = data + '"width" : ' + width + ",";
            data = data + '"height" : ' + height + ",";
            data = data + '"rotate" : "' + rotate + '"}'
        })
        data = data + "]}"
        if (temp != 0) {
            $.ajax({
                url: '/admin/web/SaveDiagram?json=' + data,
                type: "POST",
                contenType: "application/json; charset=utf-8",
                data: data,
                dataType: "json",
                success: function (data) {
                    alert("Lưu dữ liệu thành công ");
                },
                error: function (err) {
                    alert("Error1 : " + err.error);
                }
            });
        }
        else {
            var r = confirm("Bạn chưa thêm hình vào sơ đồ");
            
        }
        
    })
    $("#btnPreSaveDiagram").click(function () {
        var CLSID = $("#information form input#CLSID").attr("value")
        var widthDiagram = $("#modalInfomation form input#width").val()
        var heightDiagram = $("#modalInfomation form input#height").val()
        var ratioDiagram = $("#modalInfomation form input#ratio").val()
        var CSID = $("#modalInfomation form #CSID option:selected").val()
        var CSName = $("#modalInfomation form #CSID option:selected").html()
        var FloorID = $("#modalInfomation form input#FoorID").val()
        $.ajax({
            url: '/admin/web/ResetDiagram',
            type: "POST",
            contenType: "application/json; charset=utf-8",
            data:{CLSID: CLSID, width : widthDiagram, height : heightDiagram, ratio:ratioDiagram, CSID : CSID, FloorID : FloorID},
            //dataType: "json",
            success: function (CLSID) {
                //window.location = "/admin/web/ResetDiagram?CLSID = " + CLSID + "&width =" + widthDiagram + "&height =" + heightDiagram
                //+ "&ratio=" + ratioDiagram + "&CSID=" + CSID + "&FloorID=" + FloorID
                var r = confirm("Lưu dữ liệu thành công");
                if (r == true) {
                    // user agreed, do something 
                    window.location="/admin/Web/ViewDiagram?CLSID=" + CLSID['CLSID']
                } else {
                    // user did not agree, do something else
                }
            },
            error: function (err) {
                alert("Error1 : " + err.error);
            }
        });
        //$("#information form input#width").attr("value", widthDiagram)
        //$("#information form input#height").attr("value", heightDiagram)
        //$("#information form input#ratio").attr("value", ratioDiagram)
        //$("#information form input#CSID").attr("value", CSID)
        //$("#information form input#CSName").attr("value", CSName)
        //$("#information form input#FloorID").attr("value", FloorID)
        //var svg = $("svg#svg-dropzone")
        //var viewBox = "0 0 " + Math.round(widthDiagram) * 100 + " " + Math.round(heightDiagram) * 100
        //svg.attr("viewBox", viewBox)
        //svg.attr('preserveAspectRatio', 'xMinYMin meet')
    })
    $("#btnSaveEditDiagram").click(function () {
        var CLSID, widthDiagram, heightDiagram, ratioDiagram, CSID, FloorID
        CLSID = $("svg#svg-dropzone").attr("clsid")
        widthDiagram = $("#information form input#width").attr("value")
        heightDiagram = $("#information form input#height").attr("value")
        ratioDiagram = $("#information form input#ratio").attr("value")
        CSID = $("#information form input#CSID").attr("value")
        FloorID = $("#information form input#FloorID").attr("value")
        var data = "";
        data = data + '{"widthDiagram" : ' + widthDiagram + ","
        data = data + '"CLSID" : ' +CLSID + ","
        data = data + '"heightDiagram" : ' + heightDiagram + ","
        data = data + '"ratioDiagram" : ' + ratioDiagram + ","
        data = data + '"CSID" :' + CSID + ","
        data = data + '"FloorID" : "' + FloorID + '",'
        data = data + '"ListImageDiagram" : '
        var temp = 0
        $("#svg-dropzone g.dragOn-drawArea").each(function (index, element) {
            
            $(this).children("image").each(function (index1, element1) {
                if (temp != 0) {
                    data = data + ","
                }
                else {
                    data = data + "["
                }
                temp++;
                var ID = $(this).attr("id")
                if (ID != null) {
                    var xlink = $(this).attr("xlink:href")
                    var width = $(this).attr("width")
                    var height = $(this).attr("height")
                    var rotate = $(this).attr("rotate")
                    if (rotate == null) rotate = "000"
                    var x = $(this).attr("x")
                    var y = $(this).attr("y")
                    var indexHref = xlink.indexOf("/Assets/Admin")
                    var Href = xlink.substr(indexHref)
                    data = data + '{ "ID":' + ID + ",";
                    data = data + '"Href" : "' + Href + '",';
                    data = data + '"x" :' + x + ",";
                    data = data + '"y" :' + y + ",";
                    data = data + '"width" : ' + width + ",";
                    data = data + '"height" : ' + height + ",";
                    data = data + '"rotate" : "' + rotate + '"}'
                }
                if (ID == null) {
                    var xlink = $(this).attr("xlink:href")
                    var width = $(this).attr("width")
                    var height = $(this).attr("height")
                    var rotate = $(this).attr("rotate")
                    if (rotate == null) rotate = "000"
                    var x = $(this).attr("x")
                    var y = $(this).attr("y")
                    var indexHref = xlink.indexOf("/Assets/Admin")
                    var Href = xlink.substr(indexHref)
                    data = data + '{ "ID":' + temp + ",";
                    data = data + '"Href" : "' + Href + '",';
                    data = data + '"x" :' + x + ",";
                    data = data + '"y" :' + y + ",";
                    data = data + '"width" : ' + width + ",";
                    data = data + '"height" : ' + height + ",";
                    data = data + '"rotate" : "' + rotate + '"}'
                }
            })
            
            
        })
        data = data + "]}"
        if (temp != 0) {
            $.ajax({
                url: '/admin/web/SaveEditDiagram?json=' + data,
                type: "POST",
                contenType: "application/json; charset=utf-8",
                data: data,
                dataType: "json",
                success: function (data) {
                    alert("Lưu dữ liệu thành công ");
                },
                error: function (err) {
                    alert("Error1 : " + err.error);
                }
            });
        }
        //$("svg.dropzone g.dragOn-drawArea image").each(function (index, element) {
        //    var xlink = $(this).attr("xlink:href")
        //    var width = $(this).attr("width")
        //    var height = $(this).attr("height")
        //    var rotate = $(this).attr("rotate")
        //    if (rotate == null) rotate = "000"
        //    var x = $(this).attr("x")
        //    var y = $(this).attr("y")
        //    var indexHref = xlink.indexOf("/Assets/Admin")
        //    var Href = xlink.substr(indexHref)
        //    if (index != 0) {
        //        data = data + ","
        //    }
        //    else {
        //        data = data + "["
        //    }
        //    data = data + '{ "ID":' + index + ",";
        //    data = data + '"Href" : "' + Href + '",';
        //    data = data + '"x" :' + x + ",";
        //    data = data + '"y" :' + y + ",";
        //    data = data + '"width" : ' + width + ",";
        //    data = data + '"height" : ' + height + ",";
        //    data = data + '"rotate" : "' + rotate + '"}'
        //})
        //data = data + "]}"
        //$.ajax({
        //    url: '/admin/web/SaveEditDiagram?json=' + data,
        //    type: "POST",
        //    contenType: "application/json; charset=utf-8",
        //    data: data,
        //    dataType: "json",
        //    success: function (data) {
        //        alert("Lưu dữ liệu thành công ");
        //    },
        //    error: function (err) {
        //        alert("Error1 : " + err.error);
        //    }
        //});
        
    })
    $("#btnDeleteDiagram").click(function () {
        var CLSID, widthDiagram, heightDiagram, ratioDiagram, CSID, FloorID
        CLSID = $("svg#svg-dropzone").attr("clsid")
        widthDiagram = $("#information form input#width").attr("value")
        heightDiagram = $("#information form input#height").attr("value")
        ratioDiagram = $("#information form input#ratio").attr("value")
        CSID = $("#information form input#CSID").attr("value")
        FloorID = $("#information form input#FloorID").attr("value")
        var data = "";
        data = data + '{"widthDiagram" : ' + widthDiagram + ","
        data = data + '"CLSID" : ' + CLSID + ","
        data = data + '"heightDiagram" : ' + heightDiagram + ","
        data = data + '"ratioDiagram" : ' + ratioDiagram + ","
        data = data + '"CSID" :' + CSID + ","
        data = data + '"FloorID" : "' + FloorID + '",'
        data = data + '"ListImageDiagram" : '
        var temp = 0
        $("#svg-dropzone g.dragOn-drawArea").each(function (index, element) {

            $(this).children("image").each(function (index1, element1) {
                if (temp != 0) {
                    data = data + ","
                }
                else {
                    data = data + "["
                }
                temp++;
                var ID = $(this).attr("id")
                if (ID != null) {
                    var xlink = $(this).attr("xlink:href")
                    var width = $(this).attr("width")
                    var height = $(this).attr("height")
                    var rotate = $(this).attr("rotate")
                    if (rotate == null) rotate = "000"
                    var x = $(this).attr("x")
                    var y = $(this).attr("y")
                    var indexHref = xlink.indexOf("/Assets/Admin")
                    var Href = xlink.substr(indexHref)
                    data = data + '{ "ID":' + ID + ",";
                    data = data + '"Href" : "' + Href + '",';
                    data = data + '"x" :' + x + ",";
                    data = data + '"y" :' + y + ",";
                    data = data + '"width" : ' + width + ",";
                    data = data + '"height" : ' + height + ",";
                    data = data + '"rotate" : "' + rotate + '"}'
                }
                if (ID == null) {
                    var xlink = $(this).attr("xlink:href")
                    var width = $(this).attr("width")
                    var height = $(this).attr("height")
                    var rotate = $(this).attr("rotate")
                    if (rotate == null) rotate = "000"
                    var x = $(this).attr("x")
                    var y = $(this).attr("y")
                    var indexHref = xlink.indexOf("/Assets/Admin")
                    var Href = xlink.substr(indexHref)
                    data = data + '{ "ID":' + temp + ",";
                    data = data + '"Href" : "' + Href + '",';
                    data = data + '"x" :' + x + ",";
                    data = data + '"y" :' + y + ",";
                    data = data + '"width" : ' + width + ",";
                    data = data + '"height" : ' + height + ",";
                    data = data + '"rotate" : "' + rotate + '"}'
                }
            })
        })
        data = data + "]}"
        //if (temp != 0) {
            $.ajax({
                url: '/admin/web/DeleteDiagram?json=' + data,
                type: "POST",
                contenType: "application/json; charset=utf-8",
                data: data,
                dataType: "json",
                success: function (data) {
                    alert("Xóa thành công ");
                    window.location = "/admin/web/CreateDiagram?width=5&height=5&ratio=0.01"
                },
                error: function (err) {
                    alert("Error1 : " + err.error);
                }
            });
        //}
    })
})