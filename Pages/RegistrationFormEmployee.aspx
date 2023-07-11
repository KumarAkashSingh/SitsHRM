<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegistrationFormEmployee.aspx.cs" Inherits="Pages_RegistrationFormEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" lang="javascript">
        function ShowPopup() {
            $('#myAlert1').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert4').removeClass('show');
            $('#myAlert5').removeClass('show');
            $('#myAlert6').removeClass('show');
            $('.modal-backdrop').remove();
            $('.modal-backdrop').remove();
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup1() {
            $('#dialog').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert4').removeClass('show');
            $('#myAlert5').removeClass('show');
            $('#myAlert6').removeClass('show');
            $('.modal-backdrop').remove();
            $("#myAlert1").modal('show');
        }
        function HidePopup1() {
            $('#myAlert1').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup2() {
            $('#dialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('#myAlert4').removeClass('show');
            $('#myAlert5').removeClass('show');
            $('#myAlert6').removeClass('show');
            $('.modal-backdrop').remove()
            $("#myAlert2").modal('show');
        }
        function HidePopup2() {
            $('#myAlert2').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup4() {
            $('#dialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert5').removeClass('show');
            $('#myAlert6').removeClass('show');
            $('.modal-backdrop').remove()
            $("#myAlert4").modal('show');
        }
        function HidePopup4() {
            $('#myAlert4').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup5() {
            $('#dialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert4').removeClass('show');
            $('#myAlert6').removeClass('show');
            $('.modal-backdrop').remove()
            $("#myAlert5").modal('show');
        }
        function HidePopup5() {
            $('#myAlert5').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup6() {
            $('#dialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert5').removeClass('show');
            $('#myAlert4').removeClass('show');
            $('.modal-backdrop').remove()
            $("#myAlert6").modal('show');
        }
        function HidePopup6() {
            $('#myAlert6').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup7() {
            $('#dialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert5').removeClass('show');
            $('#myAlert4').removeClass('show');
            $('#myAlert6').removeClass('show');
            $('.modal-backdrop').remove()
            $("#myAlert7").modal('show');
        }
        function HidePopup7() {
            $('#myAlert7').removeClass('show');
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

        function openPopup(strOpen) {
            open(strOpen, "Info",
                "status=1, width=610, height=410 , top=50, left=150,scrollbars=yes");
        }
    </script>
    <style>
        .fa {
            cursor: pointer;
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
                            <asp:Button ID="Button3" runat="server" Text="x" CssClass="close" data-dismiss="modal" OnClick="btnClose_Click" />
                            <h4 class="modal-title">ERP Message Box</h4>
                        </div>
                        <div class="modal-body">
                            <div class="bootbox-body">
                                <asp:Label ID="dlglbl" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-xs btn-danger" />

                        </div>
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <div class="row top-space">
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select Designation</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select Department</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select BRanch</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select Employee Type</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Value="0">Select Status</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12 col-xs-12 col-12">
                        <div class="hpanel hpanel-blue2">
                            <div class="panel-heading">
                                <h5>All Employees</h5>
                                <div class="panel-tools">
                                    <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                </div>

                                <div class="panel-tools">
                                    <div class="dropdown">
                                        <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <i class="fa fa-cogs"></i>
                                        </a>
                                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                            <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click"><i class="fa fa-plus-square-o"></i>&nbsp;Add</asp:LinkButton>
                                            <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"><i class="fa fa-trash"></i>&nbsp;Remove</asp:LinkButton>
                                            <asp:LinkButton ID="btnUpdDesignation" runat="server" OnClick="btnUpdDesignation_Click"><i class="fa fa-pencil"></i>&nbsp;Update Designation</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body table-responsive">
                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVDetails_RowCommand" OnRowDataBound="GVDetails_RowDataBound" PageSize="50">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCtrl" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <div class="dropdown">
                                                    <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                        <i class="fa fa-bars"></i>
                                                    </a>
                                                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                        &nbsp;<asp:Label runat="server" ID="lblViewImage" Text='<%#Bind("ImageLink") %>'></asp:Label>&nbsp;View Photo
                                                        <br />
                                                        <asp:LinkButton ID="lnlbtnLeaveDetails" runat="server" CommandName="LeaveDetails">
                                                    &nbsp;<i class="fa fa-arrow-circle-down fa-1x" style="color:darkblue;" title="Leave Details"></i>&nbsp;Leave Details
                                                        </asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton ID="lnlbtnModify" runat="server" CommandName="modify">
                                                        &nbsp;<i class="fa fa-pencil fa-1x" style="color:darkblue;" title="Modify Employee"></i>&nbsp;Modify Employee
                                                        </asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        &nbsp;<i class="fa fa-trash fa-1x" style="color:red;" title="Delete Employee"></i>&nbsp;Delete Employee
                                                        </asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton ID="lnlbtnView" runat="server" CommandName="view">
                                                        &nbsp;<i class="fa fa-eye fa-1x" style="color:darkblue;" title="View Employee"></i>&nbsp;View Employee
                                                        </asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton ID="lnlbtnRemarks" runat="server" CommandName="Remarks">
                                                        &nbsp;<i class="fa fa-comment fa-1x" style="color:darkblue;" title="Remarks"></i>&nbsp;Remarks
                                                        </asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton ID="lnkbtnPrint" runat="server" CommandName="Print">
                                                        &nbsp;<i class="fa fa-print fa-1x" style="color:darkblue;" title="Print Form"></i>&nbsp;Print Form
                                                        </asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton ID="lnkbtnPrintJoining" runat="server" CommandName="PrintJoining">
                                                        &nbsp;<i class="fa fa-print fa-1x" style="color:darkgreen;" title="Print Joining"></i>&nbsp;Print Joining
                                                        </asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton ID="lnkbtnPrintTransport" runat="server" CommandName="PrintTransport">
                                                        &nbsp;<i class="fa fa-print fa-1x" style="color:brown;" title="Print Transport"></i>&nbsp;Print Transport
                                                        </asp:LinkButton>
                                                        <br />
                                                        <asp:LinkButton ID="lnkbtnPrintFileCover" runat="server" CommandName="PrintFileCover">
                                                        &nbsp;<i class="fa fa-print fa-1x" style="color:black;" title="Print File Cover"></i>&nbsp;Print File Cover
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="sno" HeaderText="S.No." />
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode"  HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                        <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Name" SortExpression="EmployeeName" />
                                        <asp:BoundField DataField="DesignationDescription" HeaderText="Designation" SortExpression="DesignationDescription" />
                                        <asp:BoundField DataField="FatherName" HeaderText="Father's Name" SortExpression="FatherName" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                        <asp:BoundField DataField="Contact" HeaderText="Phone" SortExpression="Contact" />
                                        <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />

                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="myAlert1" class="modal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="btnNo" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Are you sure?</h5>
                        </div>
                        <div class="modal-body text-center">
                            <asp:Button ID="btnYes1" runat="server" Text="Yes" CssClass="btn no-border btn-xs btn-success" OnClick="btnYes1_Click" />
                            <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="btn no-border btn-xs btn-danger" OnClick="btnNo1_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="myAlert2" class="modal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button2" runat="server" OnClick="btnNo2_Click" data-dismiss="modal" class="close" type="button" Text="x" />

                            <h5>Are you sure?</h5>
                        </div>
                        <div class="modal-body text-center">
                            <asp:Button ID="Button4" runat="server" Text="Yes" CssClass="btn btn-xs btn-success" OnClick="btnYes2_Click" />
                            <asp:Button ID="Button6" runat="server" Text="No" CssClass="btn btn-xs btn-danger" OnClick="btnNo2_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="myAlert4" class="modal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button1" runat="server" OnClick="btnNo4_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Want to send the confirmation email and SMS ?</h5>
                        </div>
                        <div class="modal-body text-center">
                            <asp:Button ID="Button7" runat="server" Text="Yes" CssClass="btn no-border btn-xs btn-success" OnClick="btnYes4_Click" />
                            <asp:Button ID="Button8" runat="server" Text="No" CssClass="btn no-border btn-xs btn-danger" OnClick="btnNo4_Click" />

                        </div>
                    </div>
                </div>
            </div>
            <div id="myAlert5" class="modal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button9" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Add Remarks</h5>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:TextBox ID="txtRemarks" runat="server" placeholder="Enter Remarks" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Button ID="btnAddRemarks" runat="server" Text="Save" OnClick="btnAddRemarks_Click" CssClass="btn no-border btn-xs btn-success" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="hpanel hpanel-blue2">
                                        <div class="panel-heading">
                                            <h5>Remarks</h5>
                                            <div class="panel-tools">
                                            </div>
                                        </div>
                                        <div class="panel-body nopadding pre-scrollable">
                                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVRemarks" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found" OnRowDataBound="GVRemarks_RowDataBound">
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="myAlert6" class="modal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button10" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />

                            <h5>Details</h5>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="widget-container-col">
                                        <div class="hpanel hpanel-blue2">
                                            <div class="panel-heading">
                                                <h5>Leave Details</h5>
                                                <div class="panel-tools">
                                                </div>
                                            </div>
                                            <div class="panel-body nopadding pre-scrollable">
                                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetailsLeaveDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowDataBound="GVDetailsLeaveDetails_RowDataBound" EmptyDataText="No Record Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="sno" HeaderText="S.No." />
                                                        <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="Days" HeaderText="Days" />
                                                        <asp:BoundField DataField="Leave" HeaderText="Leave" />
                                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                                        <asp:BoundField DataField="Type" HeaderText="Type" />
                                                        <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                                        <asp:BoundField DataField="dol" HeaderText="Apply Date" DataFormatString="{0:dd-MM-yyyy}" />
                                                        <asp:BoundField DataField="AppDate" HeaderText="Approval Date" DataFormatString="{0:dd-MM-yyyy}" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="widget-container-col">
                                        <div class="hpanel hpanel-blue2">
                                            <div class="panel-heading">
                                                <h5>Leave Balance</h5>
                                                <div class="panel-tools">
                                                </div>
                                            </div>
                                            <div class="panel-body nopadding pre-scrollable">
                                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeaveBalance" runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found">
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnNo6" runat="server" Text="Close" OnClick="btnNo6_Click" CssClass="btn pull-right no-border btn-sm btn-success"></asp:Button>

                        </div>
                    </div>
                </div>
            </div>
            <div id="myAlert7" class="modal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button11" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Update Designation</h5>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:DropDownList ID="ddlUpdateDesignation" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                        <asp:ListItem Value="0">Select Designation</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdateDesignation" runat="server" Text="Update" OnClick="btnUpdateDesignation_Click" CssClass="btn pull-right no-border btn-sm btn-success"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
