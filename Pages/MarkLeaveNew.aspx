<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MarkLeaveNew.aspx.cs" Inherits="Pages_MarkLeaveNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" lang="javascript">
        function ShowPopup() {
            $('.modal-backdrop').remove();
            $('#myAlert1').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#maindialog').removeClass('show');
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
        function ShowPopup2() {
            $('.modal-backdrop').remove();
            $('#dialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('#maindialog').removeClass('show');
            $("#myAlert2").modal('show');
        }
        function HidePopup2() {
            $('#myAlert2').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup3() {
            $('.modal-backdrop').remove();
            $('#dialog').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert1').removeClass('show');
            $("#maindialog").modal('show');
        }
        function HidePopup3() {
            $('#maindialog').removeClass('show');
            $('body').removeClass('modal1-open');
            $('.modal1-backdrop').remove();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
    </script>
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
                                <asp:Button ID="Button3" runat="server" data-dismiss="modal" Text="x" CssClass="close" OnClick="btnClose_Click" />
                                <h4 class="modal-title">ERP Message</h4>
                            </div>
                            <div class="modal-body">
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" CssClass="btn no-border btn-success btn-xs" OnClick="btnClose_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-3 col-xs-12 col-sm-12 col-12">
                            <div class="input-group">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="reportrange form-control" placeholder="Leave From"></asp:TextBox>
                                <span class="input-group-addon">
                                    <i class="ace-icon fa fa-calendar"></i>
                                </span>
                            </div>

                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                            <asp:DropDownList CssClass="chosen form-control" ID="ddlEmployee" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">Select Employee</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                            <asp:DropDownList ID="ddlLeave" CssClass="chosen form-control" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">Select Leave</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                            <asp:DropDownList ID="ddlStatus" CssClass="chosen form-control" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem Selected="True" Value="0">Select Status</asp:ListItem>
                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                <asp:ListItem Value="2">Apply</asp:ListItem>
                                <asp:ListItem Value="3">Reject</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-12 text-right">
                            <asp:Button ID="btnFilter" runat="server" Text="Filter" Class="btn btn-success btn-md" OnClick="btnFilter_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Leave Status Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                    <div class="panel-tools">
                                    </div>
                                    <div class="panel-tools">
                                        <div class="dropdown">
                                            <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <i class="fa fa-cogs"></i>
                                            </a>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <asp:LinkButton ID="btnPost" runat="server" Text="Remove" OnClick="btnPost_Click" Style="color: #000"><b><i class= "fa fa-plus"></i>&nbsp;Post Leave</b></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeaveDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVLeaveDetails_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="AttendanceCode" HeaderText="AttendanceCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="LeaveCode" HeaderText="LeaveCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="ApplyDate" HeaderText="Application Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="LeaveDescription" HeaderText="Leave Type" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="ApprovedRemarks" HeaderText="Remarks" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del"><i class="fa fa-1x fa-trash" style="color:red;" title="Delete"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert2" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button2" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are You Sure?</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-horizontal form-group">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="Button4" runat="server" Text="Yes" CssClass=" btn-success" OnClick="btnYes2_Click" />
                                            <asp:Button ID="Button6" runat="server" Text="No" data-dismiss="modal" CssClass=" btn-danger" OnClick="btnNo2_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="maindialog" class="modal">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button7" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Add/Modify Leave Details</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Select Session</label>&nbsp;<strong style="color: red">*</strong>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlSession" CssClass="chosen form-control" runat="server">
                                                <asp:ListItem Value="2020">2020-21</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="2021">2021-22</asp:ListItem>
                                                <asp:ListItem Value="2022">2022-23</asp:ListItem>
                                                <asp:ListItem Value="2023">2023-24</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Select Employee</label>&nbsp;<strong style="color: red">*</strong>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlEmployeeLeave" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                                <asp:ListItem Value="0">Select Employee</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Select Leave Date</label>&nbsp;<strong style="color: red">*</strong>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDOL" runat="server" CssClass="datepicker form-control" placeholder="Date of Leave"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <i class="ace-icon fa fa-calendar"></i>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Select Leave From Date</label>&nbsp;<strong style="color: red">*</strong>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="reportrange form-control" placeholder="Leave From"></asp:TextBox>
                                            <span class="input-group-addon">
                                                <i class="ace-icon fa fa-calendar"></i>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-3">
                                        <label>Select Leave</label>&nbsp;<strong style="color: red">*</strong>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlLeaveCode" CssClass="chosen form-control" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlLeaveCode_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Value="0">Select Leave Type</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Select Leave Type</label>&nbsp;<strong style="color: red">*</strong>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlHalfFull" CssClass="chosen form-control" runat="server" AppendDataBoundItems="true">
                                                <asp:ListItem Selected="True" Value="0">Select Type</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Select Leave Status</label>&nbsp;<strong style="color: red">*</strong>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlStatusLeave" CssClass="chosen form-control" runat="server">
                                                <asp:ListItem Selected="True" Value="0">Select Status</asp:ListItem>
                                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                                <asp:ListItem Value="2">Apply</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Select Approving Officer</label>
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlApprovingOfficer" CssClass="chosen form-control" runat="server" AppendDataBoundItems="true">
                                                <asp:ListItem Selected="True" Value="0">Select Approving Officer</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-3">
                                        <label>Enter Reason</label>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtReason" runat="server" placeholder="Enter Reason" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Enter Remarks</label>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtRemarks" runat="server" placeholder="Enter Remarks" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Enter Contact No</label>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtContactNo" runat="server" placeholder="Contact Number (In case of Outstation Leave)" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Enter Address</label>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtAddress" runat="server" placeholder="Enter Address (In case of Outstation Leave)" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-12 text-right">
                                        <label>&nbsp;</label>
                                        <div class="form-group">
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-sm" OnClick="btnAdd_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

