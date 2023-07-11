var BarChar1;
var BarChar2;
var BarChar3;
var BarChar4;

function GetInstituteList()
{
    $('#SelectInstitutes').html('<select id="ddlInstitutes" class="form-control"></select>')
    $('#ddlInstitutes').append('<option value="0" selected>All</option>');
    $.ajax({
        url: "Default.asmx/GetInstituteList",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            var i = 0;
            $.each(JSON.parse(data.d), function () {
                $('#ddlInstitutes').append('<option value="' + this.ddlValue + '">' + this.ddlText + '</option>');
            });

            RefreshData();
            $('#loader').hide();
            $('#ddlInstitutes').select2();
            $('#ddlInstitutes').change(function () {
                RefreshData();
            });
        }
    });
}

function RefreshData()
{
    GetBoxData();
    GetEmployeeAttendanceTeaching();
    GetEmployeeAttendanceNonTeaching();
    GetListOfEmployees();
    GetLeaveDetails();
    GetBrithdayDetails();
}

function GetBoxData()
{
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetBoxData",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            $.each(JSON.parse(data.d), function () {
                $('#lblTotalEmployee').html(this.TotalEmployee);
                $('#lblTotalPresent').html(this.TotalPresent);
                $('#lblTotalAbsent').html(this.TotalAbsent);
                $('#lblOnLeave').html(this.TotalLeave);
            });
        }
    });
}

function GetEmployeeAttendanceTeaching() {
    var header = [];
    var dataset = [];
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetEmployeeAttendanceTeaching",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            $.each(JSON.parse(data.d), function () {
                header.push(this.Header);
                dataset.push(this.Count);

            });
            setDataTableWithoutAll(JSON.parse(data.d), 'donut_chartTableEmployeeAttendanceTeaching', 0);
            var oilCanvas1 = document.getElementById('donut_chartEmployeeAttendanceTeaching');
            var oilData1 = {
                labels: header,
                datasets:
                    [
                        {
                            label: ['Employee Attendance [Teaching]'],
                            data: dataset,
                            backgroundColor: ['rgb(0, 2, 114)', 'rgb(131, 20, 44)', 'rgb(31, 66, 135)', 'rgb(0, 69, 74)', 'rgb(144, 12, 63)', 'rgb(57, 6, 90)', 'rgb(255, 0, 0)', 'rgb(157, 11, 40)', 'rgb(65, 30, 143)', 'rgb(9, 0, 48)']
                        }
                    ]
            };
            var pieChart1 = new Chart(oilCanvas1, { type: 'bar', data: oilData1 });
        }
    });
}

function GetEmployeeAttendanceNonTeaching() {
    var header = [];
    var dataset = [];
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetEmployeeAttendanceNonTeaching",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            $.each(JSON.parse(data.d), function () {
                header.push(this.Header);
                dataset.push(this.Count);

            });
            setDataTableWithoutAll(JSON.parse(data.d), 'donut_chartTableEmployeeAttendanceNonTeaching', 0);
            var oilCanvas1 = document.getElementById('donut_chartEmployeeAttendanceNonTeaching');
            var oilData1 = {
                labels: header,
                datasets:
                    [
                        {
                            label: ['Employee Attendance [Non Teaching]'],
                            data: dataset,
                            backgroundColor: ['rgb(0, 2, 114)', 'rgb(131, 20, 44)', 'rgb(31, 66, 135)', 'rgb(0, 69, 74)', 'rgb(144, 12, 63)', 'rgb(57, 6, 90)', 'rgb(255, 0, 0)', 'rgb(157, 11, 40)', 'rgb(65, 30, 143)', 'rgb(9, 0, 48)']
                        }
                    ]
            };
            var pieChart1 = new Chart(oilCanvas1, { type: 'bar', data: oilData1 });
        }
    });
}

function GetLeaveDetails() {
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetLeaveDetails",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            setDataTableWithoutAll(JSON.parse(data.d), 'datatableLeaveDetails', 0);
        }
    });
}

function GetBrithdayDetails() {
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetBrithdayDetails",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            setDataTableWithoutAll(JSON.parse(data.d), 'datatableBirthdaysDetails', 0);
        }
    });
}

function GetListOfEmployees() {
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetListOfEmployees",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            setDataTableWithoutAll(JSON.parse(data.d), 'datatableListOfEmployee', 0);
        }
    });
}

function GetAllTotalEmployee() {
    $('#NewLoader').show();
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetAllTotalEmployee",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            setDataTableWithoutAll(JSON.parse(data.d), 'detailsDataTable', 0);
            $('#NewLoader').hide();
            ShowPopup1();
        }
    });
}

function GetAllTotalPresent() {
    $('#NewLoader').show();
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetAllTotalPresent",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            setDataTableWithoutAll(JSON.parse(data.d), 'detailsDataTable', 0);
            $('#NewLoader').hide();
            ShowPopup1();
        }
    });
}

function GetAllTotalAbsent() {
    $('#NewLoader').show();
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetAllTotalAbsent",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            setDataTableWithoutAll(JSON.parse(data.d), 'detailsDataTable', 0);
            $('#NewLoader').hide();
            ShowPopup1();
        }
    });
}

function GetAllOnLeave() {
    $('#NewLoader').show();
    var InstituteCode = $('#ddlInstitutes option:selected').val();
    $.ajax({
        url: "Default.asmx/GetAllOnLeave",
        type: "POST",
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ 'InstituteCode': InstituteCode, 'Date': $("#txtDate").val() }),
        success: function (data) {
            setDataTableWithoutAll(JSON.parse(data.d), 'detailsDataTable', 0);
            $('#NewLoader').hide();
            ShowPopup1();
        }
    });
}
