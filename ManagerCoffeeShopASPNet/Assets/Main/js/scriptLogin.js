
function prelogin() {
    console.log('test')
    document.getElementById('id01').style.display = 'block'
}
function preRegister() {
    console.log('test')
    document.getElementById('id02').style.display = 'block'
}

$(document).ready(function () {
    $("button.preAddToCart").click(function () {
        document.getElementById('id04').style.display = 'block'
        let fdId = $(this).closest('div').attr('id')
        element_name = '#' + fdId + ' #foodname'
        let fdname = $(element_name).text()
        let fdprice = $('#' + fdId + ' #price').text()
        $('input#fdId').val(fdId)
        $('#Quantity').val('1')
        $('span#foodname').text(fdname)
        $('span#price').text(fdprice)
    })
    $('button.addToCard').click(function () {
        let fdId = $(this).closest('div').find('input:eq(0)').val()
        let fdName = $(this).closest('div').find('span:eq(1)').text()
        let fdQuantity = $(this).closest('div').find('input:eq(1)').val()
        let fdPrice = $(this).closest('div').find('span:eq(2)').text()
        //console.log(fdId)
        //console.log(fdName)
        //console.log(fdQuantity)
        //console.log(fdPrice)
        let price = fdQuantity * fdPrice;
        console.log(price)
        $('span#price').text(price)
        var data = '{"FDID" : ' + fdId + ', '
        data = data + '"Name" :  "' + fdName + '",'
        data = data + '"Quantity" : ' + fdQuantity + ', '
        data = data + '"Price" : ' + fdPrice + ', '
        data = data + '"Total" : ' + price + '}'
        console.log(data)
        $.ajax({
            url: '/main/cart/AddToCart?json=' + data,
            type: "POST",
            contenType: "application/json; charset=utf-8",
            data: data,
            dataType: "json",
            success: function (data) {
                document.getElementById('id04').style.display = 'none'
                alert("Đặt thành công!! Bạn hãy vào giỏ hàng để xem đồ uống mình đã đặt.");
            },
            error: function (err) {
                alert("Error 2 : " + err.responseText);
            }
        });
    })
    //$('button.cancel').click(function () {
    //    alert('cancel')
    //    //$('.table-responsive').remove();
    //})
    //$("giohang").click(function () {
    //    $.ajax() --- gọi đến 1 controller
    //})
});

function preAdd() {
    document.getElementById('id04').style.display = 'block'
    //var fdId = $(this).attr("id")
    var fdId = $('input').closest('div#test').find('input').val()
    console.log(fdId)
    var fdname = $('#foodname').text()
    //console.log(fdname)
    var fdprice = $('#price').text()
    //console.log(fdprice)
    $('span#foodname').text(fdname)
    $('span#price').text(fdprice)
}

function addCart() {
    var foodName = $('#foodname').text()
    console.log(foodName)
    //console.log('test')
}

//function signUp() {
//    console.log('test')
//    let password = $('#psw').val()
//    let repeat = $('#psw-repeat').val()
//    console.log(password)
//    console.log(repeat)
//    if (password != repeat) {
//        let warning = 'Password does not match. Please fill out.'
//        document.getElementById('demo').innerHTML = warning
//    }
//}
//$('.signupbtn').on('click', function () {

//})
//function login() {
//let username = $('#username').val()
//let password = $('#password').val()
//console.log(username)
//console.log(password)
//let error = $('div #error').text()
//console.log(error)
//if (error != null || error != '') {
//    document.getElementById('id01').style.display = 'block'
//}
//else {
//    dodocument.getElementById('error').innerHtml = 'hahaha'
//}
////document.getElementById('resultLogin').innerHTML = 'Wellcome'
//document.getElementById('id01').style.display = 'none'
//}

function login() {
    //    let err = $("#error").text().trim()
    //    console.log(err)
    //    if (err === "Username or Password is incorrect")
    //        document.getElementById('id01').style.display = 'block'
}
