<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Payroll_SetSalary.aspx.cs" Inherits="Pages_Payroll_SetSalary" %>

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

        function ShowPopup1() {
            $('.modal-backdrop').remove();
            $("#myAlert1").modal('show');
        }
        function HidePopup1() {
            $('#myAlert1').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup2() {
            $('.modal-backdrop').remove();
            $("#myAlert2").modal('show');
        }
        function HidePopup2() {
            $('#myAlert2').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup4() {
            $('.modal-backdrop').remove();
            $("#myAlert4").modal('show');
        }
        function HidePopup4() {
            $('#myAlert4').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup5() {
            $('.modal-backdrop').remove();
            $("#myAlert5").modal('show');
        }
        function HidePopup5() {
            $('#myAlert5').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup6() {
            $('.modal-backdrop').remove();
            $("#myAlert6").modal('show');
        }
        function HidePopup6() {
            $('#myAlert6').removeClass('show');
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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function openPopup(strOpen) {
            open(strOpen, "Info",
                 "status=1, width=310, height=310 , top=50, left=350,scrollbars=no");
        }
    </script>
    <style>
        .btn-group-sm > .btn, .btn-sm {
            padding: 7px 10px !important;
        }

        .buttons, button, .btn {
            margin: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="content">
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
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>ERP Message Box</h5>
                            </div>
                            <div class="modal-body">
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-danger btn-xs" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Designation</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Department</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select College</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Employee Type</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Status</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Employee(s) Salary</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                    <div class="panel-tools">
                                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"><i class="fa fa-trash"></i>&nbsp;Remove</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server"
                                        CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                        OnRowCommand="GVDetails_RowCommand" PageSize="50" OnRowDataBound="GVDetails_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="sno" HeaderText="S.No." />
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode" />
                                            <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" SortExpression="EmployeeName" />
                                            <asp:BoundField DataField="FatherName" HeaderText="Father's Name" SortExpression="FatherName" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="Contact" HeaderText="Phone" SortExpression="Contact" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="Gross" HeaderText="Gross Salary" SortExpression="Gross" />
                                            <asp:TemplateField HeaderText="Salary" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnAdd" runat="server" CommandName="AddSalary"><i class="fa fa-plus fa-1x" style="color:darkblue;" title="Add Salary"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Modify Salary" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnUpdate" runat="server" CommandName="UpdateSalary"><i class="fa fa-pencil fa-1x" style="color:darkgreen;" title="Update Salary"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete Structure" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del"><i class="fa fa-trash fa-1x" style="color:red;" title="Delete Salary Structure"></i></asp:LinkButton>
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
                <div id="myAlert1" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="btnNo" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="btnYes1" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes1_Click" />
                                <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert2" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button2" runat="server" OnClick="btnNo2_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="Button4" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes2_Click" />
                                <asp:Button ID="Button6" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo2_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert4" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="btnNo4" runat="server" OnClick="btnNo4_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="Button7" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes4_Click" />
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo4_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert5" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button9" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Add Salary</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <label>Allowance</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlAllowance" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddlAllowance_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">Select Allowance Type</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <label>Amount</label>
                                        <div class="input-group">
                                            <span class="input-group-addon">
                                                <i class="ace-icon fa fa-check"></i>
                                            </span>
                                            <asp:TextBox ID="txtAmount" runat="server" placeholder="Enter Amount" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:LinkButton runat="server" ID="btnAddAmount" OnClick="btnAddAmount_Click" CssClass="btn btn-info btn-sm">
                                            <span class="ace-icon fa fa-plus icon-on-right bigger-110"></span>
                                            Add
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="hpanel hpanel-blue2">
                                            <div class="panel-heading">
                                                <h5>Salary Details</h5>
                                                <div class="panel-tools">
                                                    <asp:Label ID="lblSalaryRecords" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="panel-body nopadding pre-scrollable">
                                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVSalary" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found" OnRowDataBound="GVSalary_RowDataBound" OnRowCommand="GVSalary_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="sno" HeaderText="S.No." />
                                                        <asp:BoundField DataField="allowancecode" HeaderText="allowancecode Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                        <asp:BoundField DataField="ID" HeaderText="recordno" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                        <asp:BoundField DataField="allowancedescription" HeaderText="Allowance Type" SortExpression="EmployeeName" />
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                                        <asp:BoundField DataField="Deduction" HeaderText="Deduction" SortExpression="Deduction" />
                                                        <asp:BoundField DataField="CreatedOn" HeaderText="Created On" SortExpression="CreatedOn" />
                                                        <asp:TemplateField HeaderText="Delete Allowance" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnlbtnDeleteAllowance" runat="server" CommandName="DelAllowance"><i class="fa fa-trash fa-1x" style="color:red;"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <!--Update Salary-->
                <div id="myAlert6" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Modify Salary</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="hpanel hpanel-blue2">
                                            <div class="panel-heading">
                                                <h5>Salary Details</h5>
                                                <div class="panel-tools">
                                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="panel-body nopadding pre-scrollable">
                                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVSalaryUpdate" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found" OnRowDataBound="GVSalaryUpdate_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="sno" HeaderText="S.No." />
                                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                        <asp:BoundField DataField="allowancedescription" HeaderText="Allowance Type" SortExpression="EmployeeName" />
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" runat="server" Text='<%# Bind("Amount") %>' Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-sm btn-success" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnClose1" runat="server" Text="Close" OnClick="btnClose1_Click" CssClass="btn btn-sm btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

