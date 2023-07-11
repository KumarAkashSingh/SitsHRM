<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApproveRequestedLeaveSD.aspx.cs" Inherits="Forms_ApproveRequestedLeaveSD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowPopup2() {
            $('.modal-backdrop').remove();
            $("#myAlert").modal('show');
        }
        function HidePopup2() {
            $('#myAlert').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup() {
            $('.modal-backdrop').remove();
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup3() {
            $('.modal-backdrop').remove();
            $("#myAlert3").modal('show');
        }
        function HidePopup3() {
            $('#myAlert3').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
    </script>

    <script type="text/javascript">
        function openPopup(strOpen) {
            open(strOpen, "Info",
                 "status=1, width=800, height=500 , top=50, left=50");
        }
    </script>

    <script type="text/javascript" lang="javascript">
        function confirmDelete() {
            if (confirm("Are you sure?") == true)
                return true;
            else
                return false;
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
                <div id="dialog" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button2" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>ERP Message Box</h5>
                            </div>
                            <div class="modal-body">
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server"></asp:Label>
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
                        <div class="col-md-6 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Filter Records</h5>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-xs-6 col-sm-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlFilterStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="select2 form-control">
                                                <asp:ListItem Selected="True" Value="0">Select Status</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-xs-6 col-sm-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="select2 form-control">
                                                <asp:ListItem Selected="True" Value="0">Select Employee</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-xs-6 col-sm-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="select2 form-control">
                                                <asp:ListItem Selected="True" Value="0">Select Month</asp:ListItem>
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
                                    <div class="col-md-6 col-xs-6 col-sm-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="select2 form-control">
                                                <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                               
                            </div>
                        </div>

                        <div class="col-md-6 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Action</h5>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-xs-12 col-sm-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control chosen">
                                                <asp:ListItem Selected="True" Value="0">Select Action</asp:ListItem>
                                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                                <asp:ListItem Value="2">Reject</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-xs-12 col-sm-6">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtDateofApproval" runat="server" CssClass="datepicker form-control" placeholder="Select Date of Approval"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-success btn-sm">
                                            <span class="ace-icon fa fa-save icon-on-right bigger-110"></span>
                                            Save
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row top-space">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Requested Leave Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVApprovedLeaveDetails" runat="server"
                                        CssClass="table table-bordered" AutoGenerateColumns="False"
                                        PageSize="20" CaptionAlign="Right" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None"
                                        BorderWidth="1px" CellPadding="3" GridLines="Both"
                                        OnRowCommand="GVApprovedLeaveDetails_RowCommand" OnRowDataBound="GVApprovedLeaveDetails_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="S.No." HeaderText="S.No." SortExpression="S.No." />
                                            <asp:BoundField DataField="AttendanceCode" HeaderText="AttendanceCode" SortExpression="attendencecode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="E_Code" HeaderText="E_Code" SortExpression="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="LeaveCode" HeaderText="LeaveCode" SortExpression="LeaveCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="MarkTo" HeaderText="MarkTo" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="FromDate" HeaderText="FromDate" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:BoundField DataField="ToDate" HeaderText="ToDate" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" SortExpression="LeaveType" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"/>
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee" SortExpression="EmployeeName" />
                                            <asp:BoundField DataField="LeaveDate" HeaderText="Leave Date & Day" SortExpression="LeaveDate" HtmlEncode="false" />
                                            <asp:BoundField DataField="BiometricDetails" HeaderText="Biometric Details" SortExpression="BiometricDetails" HtmlEncode="false"/>
                                            <asp:BoundField DataField="LeaveType1" HeaderText="Leave Type" SortExpression="LeaveType1" HtmlEncode="false"/>
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="AssignedBy" HeaderText="Assigned By" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="Attachment" HeaderText="Attachment" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:TemplateField HeaderText="View File">
                                                <ItemTemplate>
                                                    <a href="javascript:openPopup('ViewFile2.aspx?id=<%# Eval("AttendanceCode") %>')">View</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View Balance">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="view1">View Balance</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Convert To Full Day">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkFullDay" runat="server" CommandName="FullDay">

                                                    </asp:CheckBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert3" class="modal">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Leave Balance Details</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="hpanel hpanel-blue2">
                                            <div class="panel-heading">
                                                <h5>Leave Balance</h5>
                                            </div>
                                            <div class="panel-body nopadding pre-scrollable">
                                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeave" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found"></asp:GridView>
                                            </div>
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

