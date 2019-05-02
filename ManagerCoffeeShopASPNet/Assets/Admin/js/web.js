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
})