<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LeaveDetailsNew.aspx.cs" Inherits="Forms_LeaveDetailsNew" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
                        <div class="col-md-3 col-xs-12 col-sm-4">
                            <div class="form-group">
                                <label class="control-label">Date Range</label>
                                <asp:TextBox ID="txtDateRange" runat="server" CssClass="reportrange form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3 col-xs-12 col-sm-4">
                            <div class="form-group">
                                <label class="control-label">Leave Type</label>
                                <asp:DropDownList ID="ddlLeaveType" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                    <asp:ListItem Selected="True" Value="0" Text="ALL"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-4 text-left">
                            <div class="form-group">
                                <label class="control-label hidden-sm">&nbsp;</label>
                                <br />
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-success btn-sm" OnClick="btnView_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Leave Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server" ></asp:Label>
                                    </div>
                                    <div class="panel-tools">
                                        <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" ><i class="fa fa-file-excel-o"></i></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeaveDetails" runat="server" CssClass="table table-bordered table-striped" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="No Record Found" AutoGenerateColumns="false" OnRowDataBound="GVLeaveDetails_RowDataBound" OnRowCommand="GVLeaveDetails_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="S.No." HeaderText="S.No." />
                                            <asp:BoundField DataField="LeaveCode" HeaderText="LeaveCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="AttendanceCode" HeaderText="AttendenceCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="Leave" HeaderText="Leave" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date"  />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="ApprovedBy" HeaderText="Approved/Reject By" />
                                            <asp:BoundField DataField="AppDate" HeaderText="Approval/Rejection Date" />
                                            <asp:BoundField DataField="WithdrawlRequest" HeaderText="WithDraw Status" />
                                            <asp:TemplateField HeaderText="WithDraw Leave" ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnWithDraw" runat="server" CommandName="withdraw" Text="WithDraw Leave" CssClass="btn btn-info btn-xs btn-no-border" />
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
                <div id="myAlert1" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtWithdrawReason" runat="server" placeholder="Enter Specify Withdrawl Reason" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="modal-footer text-center">
                                    <asp:Button ID="btnYes" runat="server" Text="Save" CssClass="btn btn-success btn-xs" OnClick="btnYes1_Click" />
                                    <asp:Button ID="btnNo" runat="server" Text="Cancel" CssClass="btn btn-danger btn-xs" OnClick="btnNo1_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

