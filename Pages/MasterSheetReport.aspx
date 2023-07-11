<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MasterSheetReport.aspx.cs" Inherits="Pages_MasterSheetReport" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../Content/jquery-ui.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        function ShowPopup(message) {
            $('.modal-backdrop').remove();
            $("#dialog").modal('show');
        }

        function HidePopup() {
            $('#maindialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
    </script>

    <script lang="javascript">
        function openPopup(strOpen) {
            open(strOpen, "Info",
                "status=1, width=800, height=500 , top=50, left=50");
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
                <div id="dialog" class="modal fade" style="overflow-y: auto">
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
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Select Month</label>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Select Year</label>
                                <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-control chosen">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <label>&nbsp;&nbsp;</label>
                            <div class="form-group">
                                <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" CssClass="btn btn-xs btn-success" Text="View"></asp:Button>
                                <asp:Button ID="btnExport" runat="server" OnClick="btnExport_Click" CssClass="btn btn-xs btn-success" Text="Export"></asp:Button>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="widget-container-col">
                                <div class="hpanel hpanel-blue2">
                                    <div class="panel-heading">
                                        <h5>Master Salary Sheet</h5>
                                    </div>
                                    <div class="panel-body nopadding table-responsive">
                                        <asp:GridView ID="GVMasterSheetReport" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found" AutoGenerateColumns="false" OnRowDataBound="GVMasterSheetReport_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="sno" HeaderText="S.No" SortExpression="sno" />
                                                <asp:BoundField DataField="Particulars" HeaderText="Particulars" SortExpression="Particulars" />
                                                <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />
                                                <asp:BoundField DataField="PF" HeaderText="PF" SortExpression="PF" />
                                                <asp:BoundField DataField="ESI" HeaderText="ESI" SortExpression="ESI" />
                                                <asp:BoundField DataField="TransportCharges" HeaderText="Transport Charges" SortExpression="TransportCharges" />
                                                <asp:BoundField DataField="ElectricityCharges" HeaderText="Electricity Charges" SortExpression="ElectricityCharges" />
                                                <asp:BoundField DataField="AccomodationCharges" HeaderText="Accomod. Charges" SortExpression="AccomodationCharges" />
                                                <asp:BoundField DataField="TDS" HeaderText="TDS" SortExpression="TDS" />
                                                <asp:BoundField DataField="AdvanceLoan" HeaderText="Advance Loan" SortExpression="AdvanceLoan" />
                                                <asp:BoundField DataField="NetPayment" HeaderText="Salary in Hand" SortExpression="NetPayment" />
                                                <asp:BoundField DataField="Nos" HeaderText="Nos." SortExpression="Nos" />
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

                    <div class="row">
                        <div class="col-md-12">
                            <div class="widget-container-col">
                                <div class="hpanel hpanel-blue2">
                                    <div class="panel-heading">
                                        <h5>New Employees Added</h5>
                                    </div>
                                    <div class="panel-body nopadding table-responsive">
                                        <asp:GridView ID="GVNewEmployeesAdded" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found" AutoGenerateColumns="false" >
                                            <Columns>
                                                <asp:BoundField DataField="sno" HeaderText="S.No" SortExpression="sno" />
                                                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName" />
                                                <asp:BoundField DataField="Post" HeaderText="Post" SortExpression="Post" />
                                                <asp:BoundField DataField="College" HeaderText="College" SortExpression="College" />
                                                <asp:BoundField DataField="DOJ" HeaderText="DOJ" SortExpression="DOJ" />
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

                    <div class="row">
                        <div class="col-md-12">
                            <div class="widget-container-col">
                                <div class="hpanel hpanel-blue2">
                                    <div class="panel-heading">
                                        <h5>Employees Left</h5>
                                    </div>
                                    <div class="panel-body nopadding table-responsive">
                                        <asp:GridView ID="GVEmployeesLeft" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found" AutoGenerateColumns="false" >
                                            <Columns>
                                                <asp:BoundField DataField="sno" HeaderText="S.No" SortExpression="sno" />
                                                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName" />
                                                <asp:BoundField DataField="Post" HeaderText="Post" SortExpression="Post" />
                                                <asp:BoundField DataField="College" HeaderText="College" SortExpression="College" />
                                                <asp:BoundField DataField="DOL" HeaderText="DOL" SortExpression="DOL" />
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
                </div>
            </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
