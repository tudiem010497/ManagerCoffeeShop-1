﻿$(document).ready(function () {
    $('button.preCreatePayroll').click(function () {
        document.getElementById('sum').style.display = 'block'
        //var employeeID = $('#employeeID').val()
        //var employeeName = $('#employeename').val()
        //var position = $('#Position').val()
        var BasicSalary = Number($('#BasicSalary').val())
        var WorkDay = Number($('#WorkDay').val())
        var Bonus = Number($('#Bonus').val())
        var Penalty = Number($('#Penalty').val())
        //var Desc = $('#Desc').val()
        var SalaryType = $('#SalaryType').val()
        var total
        if (SalaryType == 'Giờ') {
            total = BasicSalary * WorkDay + Bonus - Penalty;
        }
        else if (SalaryType == 'Ca') {
            total = BasicSalary * WorkDay + Bonus - Penalty;
        }
        else {//SalaryType == 'Tháng'
            total = BasicSalary + Bonus - Penalty;
        }
        //alert(total)
        $('div input#Total').attr('value', total)
            //điền thông tin vào form thông tin
        //$('#modalOptionPayroll form .EmployeeID input').attr('value', employeeID)
        //$('#modalOptionPayroll form .Name input').attr('value', employeeName)
        //$('#modalOptionPayroll form .Position input').attr('value', position)
        //$('#modalOptionPayroll form .BasicSalary input').attr('value', BasicSalary)
        //$('#modalOptionPayroll form .WorkDay input').attr('value', WorkDay)
        //$('#modalOptionPayroll form .Bonus input').attr('value', Bonus)
        //$('#modalOptionPayroll form .Penalty input').attr('value', Penalty)
        //$('#modalOptionPayroll form .Desc input').attr('value', Desc)
        //$('#modalOptionPayroll form .Total input').attr('value', total)
    })
    $('button.createPayroll').click(function () {
        var employeeID = $('#employeeID').val()
        var employeeName = $('#employeename').val()
        var WorkDay = Number($('#WorkDay').val())
        var Bonus = Number($('#Bonus').val())
        var Penalty = Number($('#Penalty').val())
        var Desc = $('#Desc').val()
        var total = $('#Total').val();
        var data = '{"EmployeeID" : ' + employeeID + ', '
        data += '"EmployeeName" : "' + employeeName + '", '
        data += '"WorkDay" : ' + WorkDay + ', '
        data += '"Total" : ' + total + ', '
        data += '"Bonus" : ' + Bonus + ', '
        data += '"Penalty" : ' + Penalty + ', '
        data += '"Desc" : "' + Desc + '"}'
        $.ajax({
            url: '/Admin/Accountant/DoCreatePayroll?json=' + data,
            type: "POST",
            contenType: "application/json; charset=utf-8",
            data: data,
            dataType: "json",
            success: function (data) {
                alert("Tạo phiếu lương thành công.");
                window.location = "http://localhost:64599/admin/web/GetAllEmployee";
            },
            error: function (err) {
                alert("Error: " + err.responseText);
            }
        })
    })

    $('button#submitButton').click(function () {
        alert("Lập bảng lương cho tất cả nhân viên thành công.")
    })
})