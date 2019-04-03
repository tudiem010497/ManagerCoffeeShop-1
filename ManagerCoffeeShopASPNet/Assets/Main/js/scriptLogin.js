function prelogin() {
    document.getElementById('id01').style.display = 'block'
}
function preRegister() {
    document.getElementById('id02').style.display = 'block'
}

$(document).ready(function () {
    //xử lý button scoll on top 
    $(window).scroll(function () {
        if ($(window).scrollTop() > 300) {
            $('#button').addClass('show');
        } else {
            $('#button').removeClass('show');
        }
    });

    $('#button').on('click', function () {
        $('html, body').animate({ scrollTop: 0 }, '300');
    });

    //xử lý button AddToCart lấy thông tin foodanddrink điền vào form
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
    //xử lý button Add cập nhật đồ uống lên giỏ hàng 
    $('button.addToCard').click(function () {
        let fdId = $(this).closest('div').find('input:eq(0)').val()
        let fdName = $(this).closest('div').find('span:eq(1)').text()
        let fdQuantity = $(this).closest('div').find('input:eq(1)').val()
        let fdPrice = $(this).closest('div').find('span:eq(2)').text()
        let price = fdQuantity * fdPrice;
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
                alert("Thêm vào giỏ hàng thành công!! Bạn hãy vào giỏ hàng để xem chi tiết.");
            },
            error: function (err) {
                alert("Error : " + err.responseText);
            }
        })
    })
    //xử lý khi nhấn button pay lấy thông tin đặt hàng và thông tin người đặt
    $('a#pay').click(function () {
        
    })
})

function preAdd() {
    document.getElementById('id04').style.display = 'block'
    var fdId = $('input').closest('div#test').find('input').val()
    console.log(fdId)
    var fdname = $('#foodname').text()
    var fdprice = $('#price').text()
    $('span#foodname').text(fdname)
    $('span#price').text(fdprice)
}

function addCart() {
    var foodName = $('#foodname').text()
    console.log(foodName)
}
