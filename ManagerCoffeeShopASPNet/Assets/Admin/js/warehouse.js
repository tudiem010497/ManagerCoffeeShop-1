
$(document).ready(function () {
    var arrIngreID = [];
    var arrGiftID = [];
    var arrIngre = [];
    var arrGift = [];
    $(".btnPreAddToReceipt").click(function () {
        var IngreID = $(this).attr("ingreid");
        var Name = $(this).attr("name");
        var UnitPrice = $(this).attr("unitprice");
        $("#modalOption form div.IngreID input").attr("value", IngreID);
        $("#modalOption form div.Name input").attr("value", Name);
        $("#modalOption form div.UnitPrice input").attr("value", UnitPrice);
    })
    $(".btnPreAddGiftToReceipt").click(function () {
        var GiftID = $(this).attr("giftid");
        var Name_gift = $(this).attr("name");
        var UnitPrice_gift = $(this).attr("unitprice");
        $("#modalOption_gift form div.GiftID input").attr("value", GiftID);
        $("#modalOption_gift form div.Name_gift input").attr("value", Name_gift);
        $("#modalOption_gift form div.UnitPrice_gift input").attr("value", UnitPrice_gift);
    })
    $(".btnAddToReceipt").click(function () {
        var row = "";
        var IngreID = $("#modalOption form div.IngreID input[name='IngreID']").attr("value");
        var Name = $("#modalOption form div.Name input[name='Name']").attr("value");
        var UnitPrice = $("#modalOption form div.UnitPrice input[name='UnitPrice']").attr("value");
        var Quantity = $("#modalOption form div.Quantity input[name='Quantity']").val();
        var ReferenceDesc = $("#modalOption form div.ReferenceDesc input[name='ReferenceDesc']").val();
        if (!CheckExistIngre(IngreID, arrIngreID)) {
            var item = [IngreID, Name, Quantity, UnitPrice, ReferenceDesc]
            arrIngreID.push(IngreID);
            arrIngre.push(item)
        }
        for (var index in arrIngre) {
            row = row + addRow(arrIngre[index][0], arrIngre[index][1], arrIngre[index][2], arrIngre[index][3], arrIngre[index][4])
        }
        $(".list-receiptdetail tbody").html(row);
    })
    $(".btnAddGiftToReceipt").click(function () {
        var row = "";
        var GiftID = $("#modalOption_gift form div.GiftID input[name='GiftID']").attr("value");
        var Name_gift = $("#modalOption_gift form div.Name_gift input[name='Name_gift']").attr("value");
        var UnitPrice_gift = $("#modalOption_gift form div.UnitPrice_gift input[name='UnitPrice_gift']").attr("value");
        var Quantity_gift = $("#modalOption_gift form div.Quantity_gift input[name='Quantity_gift']").val();
        var ReferenceDesc_gift = $("#modalOption_gift form div.ReferenceDesc_gift input[name='ReferenceDesc_gift']").val();
        if (!CheckExistGift(GiftID, arrGiftID)) {
            var item = [GiftID, Name_gift, Quantity_gift, UnitPrice_gift, ReferenceDesc_gift]
            arrGiftID.push(GiftID);
            arrGift.push(item)
        }
        for (var index in arrGift) {
            row = row + addRowGift(arrGift[index][0], arrGift[index][1], arrGift[index][2], arrGift[index][3], arrGift[index][4])
        }
        $(".list-receiptdetail-gift tbody").html(row);
    })

    $("table.list-receiptdetail").on('click', 'button.btnRemoveFromReceipt', function () {
        var row = "";
        var IngreID = $(this).attr("ingreid");
        if (CheckExistIngre(IngreID, arrIngreID)) {
            $(".list-receiptdetail tbody tr[ingreid='" + IngreID + "']")
            var index = arrIngreID.indexOf(IngreID);
            if (index > -1) {
                arrIngreID.splice(index, 1);
                arrIngre.splice(index, 1);
            }
            for (var index in arrIngre) {
                row = row + addRow(arrIngre[index][0], arrIngre[index][1], arrIngre[index][2], arrIngre[index][3], arrIngre[index][4])
            }
            $(".list-receiptdetail tbody").html(row);
        }
    })
    $("table.list-receiptdetail-gift").on('click', 'button.btnRemoveGiftFromReceipt', function () {
        var row = "";
        var GiftID = $(this).attr("giftid");
        if (CheckExistGift(GiftID, arrGiftID)) {
            $(".list-receiptdetail tbody tr[giftid='" + GiftID + "']")
            var index = arrGiftID.indexOf(GiftID);
            if (index > -1) {
                arrGiftID.splice(index, 1);
                arrGift.splice(index, 1);
            }
            for (var index in arrGift) {
                row = row + addRowGift([index][0], arrGift[index][1], arrGift[index][2], arrGift[index][3], arrGift[index][4])
            }
            $(".list-receiptdetail-gift tbody").html(row);
        }
    })
    $(".btnAddReceipt").on('click', function (event) {
        var temp = 0;
        var data = '';
        var SupplierID = $("select#SupplierID option:selected").val();
        data = data + '{"SupplierID" : ' + SupplierID + ',';
        data = data + '"ReceiptDetailModel" : ['
        $("table.list-receiptdetail tbody tr").each(function () {
            if (temp != 0)
                data = data + ',';
            var IngreID = $(this).attr("ingreid");
            var Name = $(this).children(".Name").text();
            //var Quantity = $(this).children(".Quantity").text();
            var Quantity = $(this).find("td.Quantity input").val();
            var TotalAmount = $(this).children(".TotalAmount").text();
            var ReferenceDesc = $(this).children(".ReferenceDesc").text();
            data = data + '{"IngreID" :' + IngreID + ',';
            data = data + '"Name" : "' + Name + '",';
            data = data + '"Quantity" :' + Quantity + ',';
            data = data + '"ReferenceDesc" : "' + ReferenceDesc + '",';
            data = data + '"TotalAmount" :' + TotalAmount + '}';
            temp++;
        })
        $("table.list-receiptdetail-gift tbody tr").each(function () {
            if (temp != 0)
                data = data + ',';
            var GiftID = $(this).attr("giftid");
            var Name_gift = $(this).children(".Name_gift").text();
            var Quantity_gift = $(this).find("td.Quantity_gift input").val();
            var TotalAmount_gift = $(this).children(".TotalAmount_gift").text();
            var ReferenceDesc_gift = $(this).children(".ReferenceDesc_gift").text();
            data = data + '{"GiftID" :' + GiftID + ',';
            data = data + '"Name_gift" : "' + Name_gift + '",';
            data = data + '"Quantity_gift" :' + Quantity_gift + ',';
            data = data + '"ReferenceDesc_gift" : "' + ReferenceDesc_gift + '",';
            data = data + '"TotalAmount_gift" :' + TotalAmount_gift + '}';
            temp++;
        })
        data = data + "]}";
        $.ajax({
            url: '/admin/warehouse/DoCreateReceipt?json=' + data,
            type: "POST",
            contenType: "application/json; charset=utf-8",
            data: data,
            dataType: "json",
            success: function (data) {
                alert("Gửi thành công phiếu nhập cho nhà cung cấp : " + data.SupplierID);
                $(".list-receiptdetail tbody").html("")
                $(".list-receiptdetail-gift tbody").html("")
                arrIngreID = []
                arrIngre = []
                arrGiftID = [];
                arrGift = [];
            },
            error: function (err) {
                alert("Error1 : " + err.error);
            }
        });
    })
    $("table.list-receiptdetail").on('click', 'button.update', function () {
        var parent = $(this).parent().parent();
        var ingreid = $(parent).attr("ingreid")
        var quantityNew = parent.find("td.Quantity input").val();
        var UnitPrice = parent.find("td.UnitPrice").text();
        //var item = [IngreID, Name, Quantity, UnitPrice, ReferenceDesc]
        var n = arrIngre.length;
        for (var i = 0; i< n; i++){
            if (arrIngre[i][0] == ingreid) {
                arrIngre[i][2] = quantityNew
            }
        }
        //var quantityNew = $("table.list-receiptdetail tbody tr td.Quantity input").val();
        //var UnitPrice = $("table.list-receiptdetail tbody tr td.UnitPrice").text();
        var totalNew = quantityNew * UnitPrice;
        parent.find("td.TotalAmount").text(totalNew);
    })
    $("table.list-receiptdetail-gift").on('click', 'button.update_gift', function () {
        var parent = $(this).parent().parent();
        var giftid = $(parent).attr("giftid")
        var quantityNew_gift = parent.find("td.Quantity_gift input").val();
        var UnitPrice_gift = parent.find("td.UnitPrice_gift").text();
        var n = arrGift.length;
        for (var i = 0; i < n; i++) {
            if (arrGift[i][0] == giftid) {
                arrGift[i][2] = quantityNew_gift
            }
        }
        var totalNew = quantityNew_gift * UnitPrice_gift;
        parent.find("td.TotalAmount_gift").text(totalNew);
    })
    $(".createReceipt").on('click', function () {
        var data = '{"IngredientEffete": [', temp = 0;
        $("table.table-hover tbody tr").each(function () {
            if ($(this).find('input[type="checkbox"]').is(":checked")) {
                if (temp != 0)
                    data = data + ',';
                var IngreID = Number($(this).find('input').attr('id').substring(6));
                var Name = $(this).find('td.Name').text();
                var UnitPrice = $(this).find('td.UnitPrice').text();
                data = data + '{"IngreID": ' + IngreID + ',';
                data = data + '"Name": "' + Name + '",';
                data = data + '"UnitPrice": ' + UnitPrice + '}';
                temp++;
            }
        })
        data = data + ']}';
        console.log(data)
        $.ajax({
            url: '/admin/warehouse/CreateReceipt?json=' + data,
            type: "POST",
            contenType: "application/json; charset=utf-8",
            data: data,
            dataType: "json",
            success: function (data) {
                //window.location = url;
                alert("OK");
            },
            error: function (err) {
                alert("Error1 : " + err.error);
            }
        });
    })
    function addRow(IngreID, Name, Quantity, UnitPrice, ReferenceDesc) {
        var TotalAmount = UnitPrice * Quantity;
        var row = "<tr ingreid=" + IngreID
            + "><td class= 'Name'>" + Name + "</td>"
            + "<td class= 'Quantity'><input type='text' id='changea' style='width:50px;height:28px'  value= " + Quantity + "> </td>"
            + "<td style = 'display:none' class='UnitPrice'>" + UnitPrice + "</td>"
            + "<td class='TotalAmount'>" + TotalAmount + "</td>"
            + "<td class='ReferenceDesc'>" + ReferenceDesc + "</td>"
            + "<td><button type='button' class='btn btn-sm btn-outline-success update' >Cập nhật</button> <button type='submit' class='btnRemoveFromReceipt btn btn-sm btn-danger' ingreid='" + IngreID + "'>Xóa</button></td>"
            + "</tr>";
        return row;
    }
    function addRowGift(GiftID, Name_gift, Quantity_gift, UnitPrice_gift, ReferenceDesc_gift) {
        var TotalAmount_gift = UnitPrice_gift * Quantity_gift;
        var row = "<tr giftid=" + GiftID
            + "><td class= 'Name_gift'>" + Name_gift + "</td>"
            + "<td class= 'Quantity_gift'><input type='text' id='changea' style='width:50px;height:28px'  value= " + Quantity_gift + "> </td>"
            + "<td style = 'display:none' class='UnitPrice_gift'>" + UnitPrice_gift + "</td>"
            + "<td class='TotalAmount_gift'>" + TotalAmount_gift + "</td>"
            + "<td class='ReferenceDesc_gift'>" + ReferenceDesc_gift + "</td>"
            + "<td><button type='button' class='btn btn-sm btn-outline-success update_gift' >Cập nhật</button> <button type='submit' class='btnRemoveGiftFromReceipt btn btn-danger' giftid='" + GiftID + "'>Xóa</button></td>"
            + "</tr>";
        return row;
    }
    function CheckExistIngre(IngreID, arrayIngreID) {
        for (var item in arrayIngreID) {
            if (IngreID == arrayIngreID[item]) {
                return true;
            }
        }
        return false;
    }
    function CheckExistGift(GiftID, arrayGiftID) {
        for (var item in arrayGiftID) {
            if (GiftID == arrayGiftID[item]) {
                return true;
            }
        }
        return false;
    }
})

