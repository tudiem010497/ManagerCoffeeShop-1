
$(document).ready(function () {
    var arrIngreID = [];
    var arrIngre = [];
    $(".btnPreAddToReceipt").click(function () {
        //document.addEventListener('rowclick', function (e) {
        //    console.log('data: ', e.detail.data);
        //    console.log('grid: ', e.detail.grid);
        //    console.log('original event: ', e.detail.originalEvent);
        //    var IngreID = e.detail.data["ingre-i-d"];
        //    if (!CheckExistIngre(IngreID, arrIngreID)) {
        //        var UnitPrice = e.detail.data["unit-price"];
        //        showOption(IngreID, e.detail.data.name, UnitPrice);
        //        row = row + addRow(IngreID, e.detail.data.name, 1, UnitPrice)
        //        $(".list-receiptdetail tbody").html(row);
        //        arrIngreID.push(IngreID);
        //    }
        //});
        var IngreID = $(this).attr("ingreid");
        var Name = $(this).attr("name");
        var UnitPrice = $(this).attr("unitprice");
        $("#modalOption form div.IngreID input").attr("value", IngreID);
        $("#modalOption form div.Name input").attr("value", Name);
        $("#modalOption form div.UnitPrice input").attr("value", UnitPrice);
        $("#modalOption form div.Quantity input").attr("value", 1);
    })
    $(".btnAddToReceipt").click(function () {
        var row = "";
        var IngreID = $("#modalOption form div.IngreID input[name='IngreID']").attr("value");
        var Name = $("#modalOption form div.Name input[name='Name']").attr("value");
        var UnitPrice = $("#modalOption form div.UnitPrice input[name='UnitPrice']").attr("value");
        var Quantity= $("#modalOption form div.Quantity input[name='Quantity']").attr("value");
        if (!CheckExistIngre(IngreID, arrIngreID)) {
            var item = [IngreID, Name, UnitPrice, Quantity]
            arrIngreID.push(IngreID);
            arrIngre.push(item)
        }
        for (var index in arrIngre) {
            row = row + addRow(arrIngre[index][0], arrIngre[index][1], arrIngre[index][2], arrIngre[index][3])
        }
        $(".list-receiptdetail tbody").html(row);
    })
    $(".btnRemoveFromReceipt").click(function () {
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
                row = row + addRow(arrIngre[index][0], arrIngre[index][1], arrIngre[index][2], arrIngre[index][3])
            }
            $(".list-receiptdetail tbody").html(row);
        }
    })
    $(".btnAddReceipt").click(function () {
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
            var Quantity = $(this).children(".Quantity").text();
            var TotalAmount = $(this).children(".TotalAmount").text();
            data = data + '{"IngreID" :' + IngreID + ',';
            data = data + '"Name" : "' + Name + '",';
            data = data + '"Quantity" :' + Quantity + ',';
            data = data + '"TotalAmount" :' + TotalAmount + '}';
            temp++;// alert(data);
        })
        data = data + "]}";
        //alert(data);
        $.ajax({
            url: '/admin/warehouse/DoCreateReceipt?json=' + data,
            type: "POST",
            contenType: "application/json; charset=utf-8",
            data: data,
            dataType: "json",
            success: function (data) {
                alert("Gửi thành công phiếu nhập cho nhà cung cấp : " + data.SupplierID);
            },
            error: function (err) {
                alert("Error1 : " + err.error);
            }
        });
    })
    function addRow(IngreID, Name, Quantity, UnitPrice) {
        var TotalAmount = UnitPrice * Quantity;
        var row = "<tr ingreid=" + IngreID
            + "><td class= 'Name'>" + Name + "</td>"
            + "<td class= 'Quantity'>" + Quantity  + "</td>"
            + "<td class='TotalAmount'>" + TotalAmount + "</td>"
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
})