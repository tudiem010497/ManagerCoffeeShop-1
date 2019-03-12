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
            })
        }
        else {
            element_quantity = "#" + strID + " > td.Quantity";
            element_price = "#" + strID + " > td.Price";
            quantity = parseInt($(element_quantity).text()) + 1;
            price = parseInt($(element_price).text()) + parseInt($(element_price).text()) / (quantity-1);
            $(element_quantity).text(quantity.toString());
            $(element_price).text(price.toString());
        }
    });
    function ShowOrderByID(id) {
        
    }
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
        row += "<tr id='"+ "fdrink01010100"+ FDID +"'>";
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
});



