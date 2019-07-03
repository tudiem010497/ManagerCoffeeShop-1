$(document).ready(function () {
    // Xử lý đặt đồ uống
    var arrayID = [];
    $(".fooddrinkgr .btnPlus").click(function () {
        var strID = $(this).parent(".fooddrinkgr").attr("id");
        var id = strID.substr(14);
        if (!CheckExistOrder(id, arrayID)) {
            arrayID.push(id);
            
            $.ajax({
                url: '/admin/service/GetFoodAndDrinkByID?id=' + id,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    var FDID = data.FDID;
                    var Name = data.Name;
                    var UnitPrice = data.UnitPrice;
                    var Price = UnitPrice;
                    AppendNewOrder(FDID, Name, Price);
                },
                error: function (err) {
                    alert("Error : " + err.responseText);
                }
            });
        }
        else {
            PlusQuantity(strID);
        }
    });
    $(".fooddrinkgr .btnMinus").click(function () {
        var strID = $(this).parent(".fooddrinkgr").attr("id");
        var id = strID.substr(14);
        MinusQuantity(strID,arrayID);
    });
    function CheckExistOrder(FDID, arrayID) {
        for (var item in arrayID) {
            if (FDID == arrayID[item]) {
                return true;
            }
        }
        return false;
    }
    function PlusQuantity(strID) {
        element_quantity = "." + strID + " > td.Quantity";
        element_price = "." + strID + " > td.Price";
        quantity = parseInt($(element_quantity).text()) + 1;
        price = parseInt($(element_price).text()) + parseInt($(element_price).text()) / (quantity - 1);
        $(element_quantity).html(quantity.toString());
        $(element_price).html(price.toString());
    }
    function MinusQuantity(strID, arrayID) {
        element_quantity = "." + strID + " > td.Quantity";
        element_price = "." + strID + " > td.Price";
        quantity = parseInt($(element_quantity).text()) - 1;
        price = parseInt($(element_price).text()) - parseInt($(element_price).text()) / (quantity + 1);
        if (quantity != 0) {
            $(element_quantity).html(quantity.toString());
            $(element_price).html(price.toString());
        }
        else {
            $("tr." + strID).remove();
            var id = strID.substr(14);
            for (var i = 0; i < arrayID.length ; i++) {
                if (id == arrayID[i])
                    arrayID.splice(i);
            }
        }
    }
    function AppendNewOrder(FDID, Name, Price) {
        var row = ''; 
        row += "<tr class='"+ "fdrink01010100"+ FDID +"'>";
        row += "<td class='Name'>";
        row += Name;
        row += "</td>";
        row += "<td class='Quantity'>";
        row += "1";
        row += "</td>";
        row += "<td class='Price'>";
        row += Price;              
        row += "</td>";
        row += "<td class='Desc'>";
        row += "<input type='text' class='form-control'>"
        row += "</td>";
        row += "</tr>";
        $("table#order > .order").append(row);
    }

    // Xử lý button Gửi đến quầy pha chế
    $("#btnSendToBatender").click(function () {
       var temp = 0;
       var choosePromotion = $("#choosePromotion option:selected").val();
       var chooseService = $("#chooseService option:selected").val();
       var choosePos = $("#choosePos option:selected").val();
       var data =  '{"Desc" : "' + chooseService + '",' +
                    '"PosID" : ' + choosePos + ',' +
                    '"PromotionID" : '+choosePromotion +
                    ',"OrderItemModel" : [';
        $("table#order tbody.order tr").each(function () {
            if (temp != 0)
                data = data + ',';
            var strID = $(this).attr("class");
            var ID = parseInt(strID.substr(14));
            var Name = $("table#order tbody.order tr." + strID + " td.Name").text();
            var Quantity = $("table#order tbody.order tr." + strID + " td.Quantity").text();
            var Price = $("table#order tbody.order tr." + strID + " td.Price").text();
            var Desc = $("table#order tbody.order tr." + strID + " td.Desc input").val();
            data = data + '{"FoodAndDrinkID" :' + ID + ',';
            data = data + '"Name" : "' + Name + '",';
            data = data + '"Quantity" :' + Quantity + ',';
            data = data + '"Desc" :"' + Desc + '"';
            data = data + '}';
            temp++;
        });
          data = data + ']' + '}';
        //var data = '{"Desc" : "Phục vụ tại quán","PosID" : 1,"OrderItemModel" : [{"FoodAndDrinkID" :3,"Name" : "Tra sua tran chau","Quantity" :1}]}';
        $.ajax({
            url: '/admin/service/SendOrderToBatender?json=' + data,
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
    });
    
    // Xử lý nhân viên phục vụ cập nhật thông tin phục vụ
    $(".btnUpdatedOrderClosed").click(function () {
        if (confirm("Bạn muốn cập nhật thông tin của toàn bộ đơn hàng ???")) {
            var data = $(".btnUpdatedOrderClosed").attr("href") + "&confirm=true";
            $(this).attr("href", data);
        }
        else {
            var data = $(".btnUpdatedOrderClosed").attr("href") + "&confirm=false";
            $(this).attr("href", data);
        }
    });
});



