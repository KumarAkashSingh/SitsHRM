<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SetMonthlySalarySD.aspx.cs" Inherits="Pages_SetMonthlySalarySD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" lang="javascript">
        function ShowPopup() {
            $('.modal-backdrop').remove();
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup2() {
            $('.modal-backdrop').remove();
            $("#AddPunch").modal('show');
        }
        function HidePopup2() {
            $('#AddPunch').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
        function SetCursorToTextEnd(textControlID) {
            var text = document.getElementById(textControlID);
            if (text != null && text.value.length > 0) {
                if (text.createTextRange) {
                    var range = text.createTextRange();
                    range.moveStart('character', text.value.length);
                    range.collapse();
                    range.select();
                }
                else if (text.setSelectionRange) {
                    var textLength = text.value.length;
                    text.setSelectionRange(textLength, textLength);
                }
            }
        }
        //function runReplacement() {
        //    $("#PageContent_GVDetails  tr th").each(function () {
        //        var $this = $(this);
        //        var ih = $this.html();

        //        try {
        //            let m = moment($(ih).text()).format("DD/MM/YYYY");
        //            //console.log(m);
        //            if (m != "Invalid date") {
        //                //console.log($this.html().replaceAll($(ih).text(),m));
        //                //$(this).html($this.html().replaceAll($(ih).text(), m));
        //                $(this).html(m)
        //            }
        //        }
        //        catch (err) {
        //            console.log('error');
        //            //console.log($(ih).text());
        //        }
        //    });
        //}

        //function EditPunch(Dates, NacCode) {
        //    $('#PageContent_hidDate').val(Dates);
        //    $('#PageContent_hidNacCode').val(NacCode);

        //    $('#PageContent_txtInPunchDateTime').val('09:00 AM');
        //    $('#PageContent_txtOutPunchDateTime').val('05:00 PM');

        //    //Enable to put the manual punch
        //    ShowPopup2();
        //}
        function EditPunch(Dates, NacCode) {
            $('#PageContent_hidDate').val(Dates);
            $('#PageContent_hidNacCode').val(NacCode);

            PutPunch(Dates, NacCode);
        }

        function PutPunch(Dates, NacCode) {
            $.ajax({
                url: "SetSalary.asmx/PutInOutPunch",
                type: "POST",
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ 'NacCode': NacCode, 'Dates': Dates }),
                success: function (data) {
                    var arrData = JSON.parse(data.d);

                    if (arrData.length > 0) {
                        $('#PageContent_txtInPunchDateTime').val(arrData[0]['Intime']);
                        $('#PageContent_txtOutPunchDateTime').val(arrData[0]['Outime']);
                        //Enable to put the manual punch
                        ShowPopup2();
                    }
                }
            });
        }
    </script>

    <style>
        /* Ensure that the demo table scrolls */
        th, td {
            white-space: nowrap;
        }

        div.dataTables_wrapper {
            width: 100%;
            margin: 0 auto;
        }

        .dtfc-fixed-left {
            z-index: 999;
        }

        .dtfc-right-top-blocker {
            z-index: -1;
        }

        .dataTables_filter {
            margin-top: 0 !important;
        }

        .dt-buttons {
            margin: 0 !important;
        }

        .py {
            padding: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalp">
                <div class="centerp">
                    <img src="../images/inprogress.gif" alt="Loading" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dialog" class="modal" style="overflow-y: auto">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="X" />
                            <h5>ERP Message Box</h5>
                        </div>
                        <div class="modal-body">
                            <div class="bootbox-body">
                                <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-danger" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <div class="row top-space">
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                            <asp:ListItem Value="0">Select Designation</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                            <asp:ListItem Value="0">Select Department</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                            <asp:ListItem Value="0">Select College</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                            <asp:ListItem Value="0">Select Employee Type</asp:ListItem>
                        </asp:DropDownList>
                    </div>


                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <div class="form-group">
                            <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-control chosen">
                                <asp:ListItem Value="1">January</asp:ListItem>
                                <asp:ListItem Value="2">February</asp:ListItem>
                                <asp:ListItem Value="3">March</asp:ListItem>
                                <asp:ListItem Value="4">April</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>
                                <asp:ListItem Value="6">June</asp:ListItem>
                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">August</asp:ListItem>
                                <asp:ListItem Value="9">September</asp:ListItem>
                                <asp:ListItem Value="10">October</asp:ListItem>
                                <asp:ListItem Value="11">November</asp:ListItem>
                                <asp:ListItem Value="12">December</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <div class="form-group">
                            <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-control chosen">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                            <asp:ListItem Value="0">Select Status</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <div class="form-group">
                            <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="btn btn-success btn-sm" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12 col-xs-12 col-12">
                        <div class="hpanel hpanel-blue2">
                            <div class="panel-heading">
                                <h5>Salary Details</h5>
                                <div class="panel-tools">
                                    <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                </div>
                                <div class="panel-tools">
                                    <button type="button" class="btn btn-success py" onclick="SubmitSalaryData();"><i class="fa fa-rupee"></i>&nbsp;Generate Salary</button>
                                </div>
                            </div>
                            <%-- AllowPaging="true" AllowSorting="True"
                                    OnPageIndexChanging="GVDetails_PageIndexChanging"
                                    OnSorting="GVDetails_Sorting" PageSize="30"--%>
                            <div class="panel-body ">
                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server"
                                    CssClass="table table-bordered table-striped" EmptyDataText="No Record Found"
                                    OnRowDataBound="GVDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <%--<HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                            </HeaderTemplate>--%>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCtrl" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <div id="AddPunch" class="modal" style="overflow-y: auto">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <%--<asp:Button ID="Button1" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />--%>
                            <input data-dismiss="modal" class="close" type="button" value="x" />
                            <h5>Modify Punch Details</h5>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <label>In Punch</label>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtInPunchDateTime" CssClass="timepicker form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Out Punch</label>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtOutPunchDateTime" CssClass="timepicker form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer text-right">
                            <%--<asp:Button ID="Button1" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success btn-xs" />--%>
                            <input type="button" class="btn btn-success btn-xs" onclick="SavePunch();" value="Save" />

                            <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success btn-xs" />--%>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Button runat="server" ID="btnRefresh" CssClass="hidden" OnClick="btnRefresh_Click" />
            <script src="../scripts/jquery.tabletojson.js"></script>
            <script>
                var currentTextBoxID = '';
                //global varibale used for cell update in manual punch modal
                var PublicRow, PublicColumn;

                $(document).ready(function () {
                    //run when input text box chnage the value
                    $(document).on('focus', 'input[type=text]', function () {
                        currentTextBoxID = $(this).attr('id');
                    });

                    //disabled tab and enter key
                    $(document).keydown(function (e) {
                        var keycode1 = (e.keyCode ? e.keyCode : e.which);
                        //9 for tab key and 13 for enter key
                        if (keycode1 == 0 || keycode1 == 9) {
                            e.preventDefault();
                            e.stopPropagation();
                        }
                        if (keycode1 == 13) { // 13 for enter key
                            if (currentTextBoxID != 'PageContent_txtSearch') {
                                e.preventDefault();
                                e.stopPropagation();
                            }
                        }
                    });
                });

                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(toDataTable1);

                function toDataTable1() {
                    var path = window.location.pathname;
                    var page = path.split("/").pop();
                    //console.log(page);
                    if (!page.includes("Dashboard")) {
                        $('#form1 table').each(function () {
                            if (this.id != '') {
                                //console.log(this.id);
                                GridViewToDataTable1(this.id);
                            }
                        });
                    }
                }

                function GridViewToDataTable1(TableID) {
                    try {
                        $('<thead></thead>').prependTo('#' + TableID).append($('#' + TableID + ' tr:first'));

                        //to make 9th column as input tag
                        count = 0;
                        $('#' + TableID + ' > tbody  > tr').each(function () {
                            if ($(this).find("td:last").prev().html() == '1') {
                                $(this).find("td:eq(9)").each(function () {
                                    var valuestr = $(this).text();
                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent" type="text" id="inputID' + count + '"/>');
                                    $('#inputID' + count).val(valuestr);
                                });
                                count = count + 1;
                            }
                            if ($(this).find("td:last").prev().prev().html() != '0') {
                                var flagArray = $(this).find("td:last").prev().prev().html().split(',');
                                for (var tt = 0; tt < flagArray.length; tt++) {
                                    if (flagArray[tt] == '8') {
                                        $(this).find("td:eq(34)").each(function () {
                                            var valuestr = $(this).text();
                                            $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"/>');
                                            $('#inputID' + count).val(valuestr);
                                            count = count + 1;
                                        });
                                        count = count + 1;
                                    }
                                    else if (flagArray[tt] == '9') {
                                        $(this).find("td:eq(35)").each(function () {
                                            var valuestr = $(this).text();
                                            $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"/>');
                                            $('#inputID' + count).val(valuestr);
                                            count = count + 1;
                                        });
                                        count = count + 1;
                                    }
                                    else if (flagArray[tt] == '10') {
                                        $(this).find("td:eq(36)").each(function () {
                                            var valuestr = $(this).text();
                                            $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"/>');
                                            $('#inputID' + count).val(valuestr);
                                            count = count + 1;
                                        });
                                        count = count + 1;
                                    }
                                    else if (flagArray[tt] == '11') {
                                        $(this).find("td:eq(37)").each(function () {
                                            var valuestr = $(this).text();
                                            $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"/>');
                                            $('#inputID' + count).val(valuestr);
                                            count = count + 1;
                                        });
                                        count = count + 1;
                                    }
                                    else if (flagArray[tt] == '12') {
                                        $(this).find("td:eq(38)").each(function () {
                                            var valuestr = $(this).text();
                                            $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"/>');
                                            $('#inputID' + count).val(valuestr);
                                            count = count + 1;
                                        });
                                        count = count + 1;
                                    }
                                    else if (flagArray[tt] == '13') {
                                        $(this).find("td:eq(39)").each(function () {
                                            var valuestr = $(this).text();
                                            $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"/>');
                                            $('#inputID' + count).val(valuestr);
                                            count = count + 1;
                                        });
                                        count = count + 1;
                                    }
                                    else if (flagArray[tt] == '15') {
                                        $(this).find("td:eq(40)").each(function () {
                                            var valuestr = $(this).text();
                                            $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"/>');
                                            $('#inputID' + count).val(valuestr);
                                            count = count + 1;
                                        });
                                        count = count + 1;
                                    }
                                }
                            }
                        });

                        if ($.fn.DataTable.isDataTable('#' + TableID)) {
                            $('#' + TableID).empty();
                            $('#' + TableID).DataTable().clear().destroy();
                        }

                        //check for weather tbody have rows or not
                        if ($('#' + TableID + ' tbody').children().length != 0) {
                            $('#' + TableID).dataTable({
                                // parameters
                                "aaSorting": [],
                                "paging": true,
                                //responsive: false,
                                autoWidth: false,
                                colReorder: true,
                                stateSave: true,
                                scrollY: "300px",
                                scrollX: true,
                                scrollCollapse: true,
                                fixedColumns: {
                                    left: 4
                                }
                            });

                            //run when input text box chnage the value
                            $('#' + TableID + ' tbody').on('change', 'tr  input[type=text].yesChangeEvent', function () {
                                calculateSalary(this, TableID);
                            });

                            //run when input text box chnage the value
                            $('#' + TableID + ' tbody').on('change', 'tr  input[type=text].yesChangeEvent1', function () {
                                calculateSalaryOtherAllowances(this, TableID);
                            });

                            //run when input text box chnage the value
                            $('#' + TableID + ' tbody').on('click', 'tr  td', function () {
                                PublicColumn = this;
                            });
                        }
                    }
                    catch (err) {
                        console.log(err);
                    }
                }

                function calculateSalary(obj, TableID) {
                    var E_Code, Present_Days, Total_Days, Gross_Salary, Absent_Days, IsReducedSalary;

                    var arryRowData = getrowData($(obj).closest('td').closest('tr'));

                    arryRowData[9] = $(obj).val();
                    E_Code = arryRowData[4];
                    Present_Days = arryRowData[9];
                    Total_Days = arryRowData[8];
                    Absent_Days = arryRowData[10];
                    Gross_Salary = arryRowData[11];
                    //IsReducedSalary = arryRowData[arryRowData.length - 1];

                    //if (IsReducedSalary == 1) {
                    //    Present_Days = Total_Days;
                    //    Absent_Days = 0;
                    //}


                    //console.log(arryRowData);
                    $.ajax({
                        url: "SetSalary.asmx/SetSalaryOfEmployee",
                        type: "POST",
                        contentType: "application/json",
                        dataType: "json",
                        data: JSON.stringify({ 'E_Code': E_Code, 'Present_Days': Present_Days, 'Total_Days': Total_Days, 'Gross_Salary': Gross_Salary, 'Absent_Days': Absent_Days }),
                        success: function (data) {
                            var arrData = JSON.parse(data.d);
                            console.log(JSON.parse(data.d));

                            if (arrData.length > 0) {
                                console.log('');
                                var RowDataStr = $(obj).closest('td').closest('tr');

                                //Setting allowance codes and description column
                                for (var i = 0; i < 14; i++) {
                                    $(RowDataStr).find('td:eq(' + (i + 13) + ')').html(arrData[i]);
                                    $(RowDataStr).find('td:eq(' + (i + 27) + ')').html(arrData[i]);
                                }

                                //setting net salary column
                                $(RowDataStr).find('td:eq(12)').html(arrData[14]);


                                //Set for others allowance
                                $('#' + TableID + ' > tbody  > tr').each(function () {

                                    if ($(this).find("td:last").prev().prev().html() != '0') {
                                        var flagArray = $(this).find("td:last").prev().prev().html().split(',');
                                        for (var tt = 0; tt < flagArray.length; tt++) {
                                            if (flagArray[tt] == '8') {
                                                $(this).find("td:eq(34)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  />');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '9') {
                                                $(this).find("td:eq(35)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  />');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '10') {
                                                $(this).find("td:eq(36)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  />');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '11') {
                                                $(this).find("td:eq(37)").each(function () {
                                                    var valuestr = $(this).text();

                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  />');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '12') {
                                                $(this).find("td:eq(38)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  />');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '13') {
                                                $(this).find("td:eq(39)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  />');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '15') {
                                                $(this).find("td:eq(40)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  />');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                        }
                                    }
                                });
                            }
                        }
                    });
                }

                function calculateSalaryOtherAllowances(obj, TableID) {
                    var E_Code, Present_Days, Total_Days, Gross_Salary, Absent_Days, IsReducedSalary,
                        Transport_Charges, Electricity_Charges, Accomodation_Charges, TDS_Charges,
                        Advance_Loan, Arrear_Charges, Security_Amount_Charges;

                    var arryRowData = getrowData($(obj).closest('td').closest('tr'));

                    console.log(arryRowData);
                    E_Code = arryRowData[4];
                    Present_Days = arryRowData[9];
                    Total_Days = arryRowData[8];
                    Absent_Days = arryRowData[10];
                    Gross_Salary = arryRowData[11];
                    Transport_Charges = arryRowData[34];
                    Electricity_Charges = arryRowData[35];
                    Accomodation_Charges = arryRowData[36];
                    TDS_Charges = arryRowData[37];
                    Advance_Loan = arryRowData[38];
                    Arrear_Charges = arryRowData[39];
                    Security_Amount_Charges = arryRowData[40];

                    ////console.log(arryRowData);
                    $.ajax({
                        url: "SetSalary.asmx/SetSalaryOfEmployeeOtherAllowances",
                        type: "POST",
                        contentType: "application/json",
                        dataType: "json",
                        data: JSON.stringify({
                            'E_Code': E_Code, 'Present_Days': Present_Days, 'Total_Days': Total_Days,
                            'Gross_Salary': Gross_Salary, 'Absent_Days': Absent_Days,
                            'Transport_Charges': Transport_Charges, 'Electricity_Charges': Electricity_Charges,
                            'Accomodation_Charges': Accomodation_Charges, 'TDS_Charges': TDS_Charges,
                            'Advance_Loan': Advance_Loan, 'Arrear_Charges': Arrear_Charges,
                            'Security_Amount_Charges': Security_Amount_Charges
                        }),
                        success: function (data) {
                            var arrData = JSON.parse(data.d);
                            console.log(JSON.parse(data.d));

                            if (arrData.length > 0) {
                                console.log('');
                                var RowDataStr = $(obj).closest('td').closest('tr');

                                //Setting allowance codes and description column
                                for (var i = 0; i < 14; i++) {
                                    $(RowDataStr).find('td:eq(' + (i + 13) + ')').html(arrData[i]);
                                    $(RowDataStr).find('td:eq(' + (i + 27) + ')').html(arrData[i]);
                                }

                                //setting net salary column
                                $(RowDataStr).find('td:eq(12)').html(arrData[14]);

                                //$(RowDataStr).find('td:eq(33)').empty().html('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID6000" value="' + arryRowData[33] + '"/>');
                                //$(RowDataStr).find('td:eq(34)').empty().html('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID6001" value="' + arryRowData[34] + '"/>');
                                //$(RowDataStr).find('td:eq(35)').empty().html('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID6002" value="' + arryRowData[35] + '"/>');
                                //$(RowDataStr).find('td:eq(36)').empty().html('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID6003" value="' + arryRowData[36] + '"/>');
                                //$(RowDataStr).find('td:eq(37)').empty().html('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID6004" value="' + arryRowData[37] + '"/>');
                                //$(RowDataStr).find('td:eq(38)').empty().html('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID6005" value="' + arryRowData[38] + '"/>');

                                $('#' + TableID + ' > tbody  > tr').each(function () {

                                    if ($(this).find("td:last").prev().prev().html() != '0') {
                                        var flagArray = $(this).find("td:last").prev().prev().html().split(',');
                                        for (var tt = 0; tt < flagArray.length; tt++) {
                                            if (flagArray[tt] == '8') {
                                                $(this).find("td:eq(34)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  value="' + arryRowData[33] + '"/>');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '9') {
                                                $(this).find("td:eq(35)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  value="' + arryRowData[34] + '"/>');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '10') {
                                                $(this).find("td:eq(36)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  value="' + arryRowData[35] + '"/>');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '11') {
                                                $(this).find("td:eq(37)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  value="' + arryRowData[36] + '"/>');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '12') {
                                                $(this).find("td:eq(38)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  value="' + arryRowData[37] + '"/>');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '13') {
                                                $(this).find("td:eq(39)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  value="' + arryRowData[38] + '"/>');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                            else if (flagArray[tt] == '15') {
                                                $(this).find("td:eq(40)").each(function () {
                                                    var valuestr = $(this).text();
                                                    $(this).empty().append('<input style="width:56px;" class="text_field yesChangeEvent1" type="text" id="inputID' + count + '"  value="' + arryRowData[38] + '"/>');
                                                    $('#inputID' + count).val(valuestr);
                                                    count = count + 1;
                                                });
                                                count = count + 1;
                                            }
                                        }
                                    }
                                });

                            }
                        }
                    });
                }

                function getrowData(row) {
                    var result = [];

                    $(row).children('td,th').each(function (cellIndex, cell) {
                        result.push(cellValues(cellIndex, cell));
                    });
                    return result;
                };

                function cellValues(cellIndex, cell) {
                    var value, result;
                    var override = $(cell).data('override');
                    // value = $.trim($(cell).text());
                    //to get text box value if exist
                    value = ($(cell).find('input[type=text]').length > 0) ? ($(cell).find('input[type=text]').val()) : $.trim($(cell).text());
                    result = notNull(override) ? override : value;
                    return result;
                }

                function notNull(value) {
                    return value !== undefined && value !== null;
                }

                //create JSON object from 2 dimensional Array
                function arrToObject(arr) {
                    //assuming header
                    var keys = arr[0];
                    //vacate keys from main array
                    var newArr = arr.slice(1, arr.length);

                    var formatted = [],
                        data = newArr,
                        cols = keys,
                        l = cols.length;
                    for (var i = 0; i < data.length; i++) {
                        var d = data[i],
                            o = {};
                        for (var j = 0; j < l; j++)
                            o[cols[j]] = d[j];
                        formatted.push(o);
                    }
                    return formatted;
                }

                function SubmitSalaryData() {
                    var array = $("#PageContent_GVDetails").find("input:checkbox:checked").map(function () {
                        return this.value;
                    }).get();

                    if (array.length == 0) {
                        $.notify('No Record is selected');
                        return;
                    }

                    console.log('test 1');

                    //Validation for Salary Heads not null / 0
                    $("#PageContent_GVDetails").each(function () {
                        $.when($(this).find("input:checkbox:checked")).then(function () {
                            var $RowOfEmployee = $(this).closest("tr");

                            /*For Non Reducing Salary*/
                            /*check for the last column(IsReducedSalary is the second last column) in the table for Is Reduced Salary*/
                            $RowOfEmployee.find('input:text').each(function () { /*Get All input type=text*/
                                /*alert($('#PageContent_GVDetails th').eq($(this).closest("td").index()).text());*/
                                var TableColumnEmployeeName = $RowOfEmployee.find('td:eq(3)').text();

                                var TableColumnName = $('#PageContent_GVDetails th').eq($(this).closest("td").index()).text();
                                if ((TableColumnName != 'Present Days') && ($(this).val() == '0')) {
                                    $.notify('Please enter ' + TableColumnName + ' of ' + TableColumnEmployeeName);
                                    return false;
                                }
                            });

                            /*For  Reducing Salary*/
                            /*check for the last column(IsReducedSalary is the second last column) in the table for Is Reduced Salary*/
                            if ($RowOfEmployee.find('td:last').prev().html() == '1') {
                                var TableColumnEmployeeName = $RowOfEmployee.find('td:eq(3)').text();
                                var TableColumnName = $('#PageContent_GVDetails th').eq($(this).closest("td").index()).text();

                                var rowTotalDays = $RowOfEmployee.find('td:eq(8)').text();
                                var rowPresentDays = $RowOfEmployee.find('td:eq(9)').find('input:text').val();
                                var rowAbsentDays = $RowOfEmployee.find('td:eq(10)').text();

                                if (parseFloat(rowTotalDays) == (parseFloat(rowPresentDays) + parseFloat(rowAbsentDays))) {
                                    $.notify('Please reduce the days ' + TableColumnName + ' of ' + TableColumnEmployeeName);
                                    return false;
                                }

                            }
                        });
                    });

                    console.log('test 2');

                    //set table data to hidden variable
                    var table = $('#PageContent_GVDetails').tableToJSON({
                        //ignoreColumns: [0],
                        //allowHTML: true,,
                        onlyColumns: [0, 1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36],
                        extractor: function (cellIndex, $cell) {
                            // get text from the span inside table cells;
                            // if empty or non-existant, get the cell text

                            return ($($cell).find('input[type=text]').length > 0) ?
                                ($($cell).find('input[type=text]').val()) :
                                ($($cell).find('input[type=checkbox]').length > 0) ?
                                    ($($cell).find('input[type=checkbox]').is(':checked')) :
                                    $cell.text();
                        }
                    }); // Convert the table into a javascript object

                    //PageContent_hdnDatatable
                    //$('#PageContent_hdnDatatable').val(JSON.stringify(table));
                    console.log(table);

                    table.forEach(obj => renameKey(obj, '', 'IsCheckBox'));
                    const updatedJson = JSON.stringify(table);


                    //console.log(arryRowData);
                   <%-- $.ajax({
                        url: "SetSalary.asmx/CreatePaySlips",
                        type: "POST",
                        contentType: "application/json",
                        dataType: "json",
                        data: JSON.stringify({ 'Query': JSON.stringify(table), 'Month': $("#PageContent_ddlMonth option:selected").val(), 'Year': $("#PageContent_ddlYear option:selected").val() }),
                        success: function (data) {
                            var arrData = JSON.parse(data.d);
                            if (arrData.length > 0) {
                                $.notify(arrData[0]);

                                document.getElementById('<%= btnRefresh.ClientID %>').click();
                            }
                        }
                    });--%>
                }

                function renameKey(obj, oldKey, newKey) {
                    obj[newKey] = obj[oldKey];
                    delete obj[oldKey];
                }

                function SavePunch() {
                    if ($('#PageContent_hidNacCode').val() == "0") {
                        $.notify("Please Update Biometric code first");
                        return;
                    }
                    else if ($('#PageContent_hidDate').val() == "") {
                        $.notify("Date is Not define");
                        return;
                    }
                    else if ($('#PageContent_txtInPunchDateTime').val() == "") {
                        $.notify("Please Enter In Punch Date Time");
                        return;
                    }
                    else if ($('#PageContent_txtOutPunchDateTime').val() == "") {
                        $.notify("Please Enter Out Punch Date Time");
                        return;
                    }

                    $.ajax({
                        url: "SetSalary.asmx/SaveManualPunch",
                        type: "POST",
                        contentType: "application/json",
                        dataType: "json",
                        data: JSON.stringify({
                            'NacCode': "" + $('#PageContent_hidNacCode').val() + "",
                            'Date': "" + $('#PageContent_hidDate').val() + "",
                            'InPunch_': "" + $('#PageContent_txtInPunchDateTime').val() + "",
                            'OutPunch_': "" + $('#PageContent_txtOutPunchDateTime').val() + ""
                        }),
                        success: function (data) {
                            var arrData = JSON.parse(data.d);
                            if (arrData.length > 0) {
                                $.notify(arrData[0]);
                                //var $th = $(PublicColumn).closest('table').find('th').eq($(PublicColumn).index());

                                var innerTextOfColumn = $(PublicColumn).text();

                                if (innerTextOfColumn.includes('[')) {
                                    var strarrOfcol = innerTextOfColumn.split('[');
                                    innerTextOfColumn = strarrOfcol[0];
                                }

                                var positionOfSB;
                                if ($('.dataTables_scrollBody').hasScrollBar()) {
                                    positionOfSB = $('.dataTables_scrollBody').scrollTop();
                                }

                                $(PublicColumn).empty().append('<a onclick="EditPunch(\'' + $('#PageContent_hidDate').val() + '\',' + $('#PageContent_hidNacCode').val() + ')">' + innerTextOfColumn + '<br>[' + $('#PageContent_txtInPunchDateTime').val() + ' - ' + $('#PageContent_txtOutPunchDateTime').val() + ']<br><span class="label label-danger"> Punch Pending for Approval</span></a>');

                                //refresh datatable
                                dt = $('#PageContent_GVDetails').dataTable();
                                dt.fnDraw();

                                if ($('.dataTables_scrollBody').hasScrollBar()) {
                                    $('.dataTables_scrollBody').scrollTop(positionOfSB);
                                }
                            }

                            $('#AddPunch').modal('hide');
                        }
                    });

                }

                //plugin
                //plugin
                (function ($) {
                    $.fn.hasScrollBar = function () {
                        return (this.get(0).scrollHeight > this.height());
                    }
                })(jQuery);

            </script>
            <asp:HiddenField ID="hidDate" runat="server" />
            <asp:HiddenField ID="hidNacCode" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

