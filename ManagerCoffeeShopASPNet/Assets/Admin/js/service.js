$(document).ready(function () {
    var arrayID = [];
    $(".fooddrinkgr").click(function () {
        var strID = $(this).attr("id");
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
            element_quantity = "." + strID + " > td.Quantity";
            element_price = "." + strID + " > td.Price";
            quantity = parseInt($(element_quantity).text()) + 1;
            price = parseInt($(element_price).text()) + parseInt($(element_price).text()) / (quantity-1);
            $(element_quantity).text(quantity.toString());
            $(element_price).text(price.toString());
        }
    });
    function CheckExistOrder(FDID, arrayID) {
        for (var item in arrayID) {
            if (FDID == arrayID[item]) {
                return true;
            }
        }
        return false;
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
        row += "</tr>";
        $("table#order > .order").append(row);
    }
    $("#btnSendToBatender").click(function () {
       // var temp = 0;
       
       // var chooseService = $("#chooseService option:selected").val();
       // var choosePos = $("#choosePos option:selected").val();
       //var data =  '{"Desc" : "' + chooseService + '",' +
       //             '"PosID" : ' + choosePos +
       //             ',"OrderItemModel" : [';
       // $("table#order tbody.order tr").each(function () {
       //     if (temp != 0)
       //         data = data + ',';
       //     var strID = $(this).attr("class");
       //     var ID = parseInt(strID.substr(14));
       //     var Name = $("table#order tbody.order tr." + strID + " td.Name").text();
       //     var Quantity = $("table#order tbody.order tr." + strID + " td.Quantity").text();
       //     var Price = $("table#order tbody.order tr." + strID + " td.Price").text();
       //     data = data + '{"FoodAndDrinkID" :' + ID + ',';
       //     data = data + '"Name" : "' + Name + '",';
       //     data = data + '"Quantity" :' + Quantity;
       //     data = data + '}';
       //     temp++;
       // });
        //  data = data + ']' + '}';
        var data = '{"Desc" : "Phục vụ tại quán","PosID" : 1,"OrderItemModel" : [{"FoodAndDrinkID" :3,"Name" : "Tra sua tran chau","Quantity" :1}]}';
        $.ajax({
            url: '/admin/service/SendOrderToBatender?json=' + data,
            type: "POST",
            contenType: "application/json; charset=utf-8",
            data: data,
            dataType: "json",
            success: function (data) {
            },
            error: function (err) {
                alert("Error : " + err.responseText);
            }
        });

    });
    
});



