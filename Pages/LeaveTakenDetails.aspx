<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LeaveTakenDetails.aspx.cs" Inherits="Pages_LeaveTakenDetails" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript">
        function openPopup(strOpen) {
            open(strOpen, "Info",
                 "status=1, width=800, height=500 , top=50, left=50");
        }
    </script>
   <style>
       .btn_space{
           margin-top:25px;
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
                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Date Range</label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="reportrange form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <label class="control-label">Department Type</label>
                            <asp:DropDownList CssClass="chosen form-control" ID="ddlInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">Select Department</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-6 col-xs-12 col-sm-6 text-left">
                            <div class="form-group btn_space">
                                <label class="control-label">&nbsp;</label>
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-success btn-sm" OnClick="btnView_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>View Leaves</h5>
                                    <div class="panel-tools">
                                        <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click"><i class="fa fa-1x fa-file-excel-o"></i>&nbsp;Export to Excel</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="panel-body nopadding table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeaveDetails" runat="server" CssClass="table table-bordered"  CaptionAlign="Right" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="No Record Found" OnRowDataBound="GVLeaveDetails_RowDataBound" data-stripe-classes="[]">
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
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Leave Details</h5>
                                    <div class="panel-tools">
                                        <asp:LinkButton ID="btnExportNew" runat="server" OnClick="btnExportNew_Click"><i class="fa fa-1x fa-file-excel-o"></i>&nbsp;Export to Excel</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeaveDetailsNew" runat="server" AutoGenerateColumns="false"
                                        CssClass="table table-bordered " EmptyDataText="No Record Found" OnRowCommand="GVLeaveDetailsNew_RowCommand" OnRowDataBound="GVLeaveDetailsNew_RowDataBound" data-stripe-classes="[]">
                                        <Columns>
                                            <asp:BoundField DataField="SNo" HeaderText="S.No." />
                                            <asp:BoundField DataField="Code" HeaderText="Code" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="Table" HeaderText="Table" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="AttendanceCode" HeaderText="AttendanceCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="Department" HeaderText="Department" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                            <asp:BoundField DataField="Leave" HeaderText="Leave" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                            <asp:BoundField DataField="DOL" HeaderText="Leave Date" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="AssignedBy" HeaderText="Assigned To" />
                                            <asp:BoundField DataField="AppDate" HeaderText="Approved Date" />
                                            <asp:BoundField DataField="MarkTo" HeaderText="Approved By" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnPrint" runat="server" CommandName="print">
                                                        <i class="fa fa-print" title="Print Leave"></i>
                                                    </asp:LinkButton>
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
                <asp:PostBackTrigger ControlID="btnExportNew" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>
