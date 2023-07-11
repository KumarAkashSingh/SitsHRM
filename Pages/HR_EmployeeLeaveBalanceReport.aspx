<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HR_EmployeeLeaveBalanceReport.aspx.cs" Inherits="Pages_HR_EmployeeLeaveBalanceReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        function load() {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(selectmethod);
        }

        function selectmethod() {
            $('select').select2();
            myVar = setTimeout(datemethod, 100);
        }

        function datemethod() {
            $(".datepicker").datepicker({
                dateFormat: "d M yy",
                changeMonth: true,
                changeYear: true,
                minDate: "+0d",
                maxDate: "+1Y",
                onSelect: function (dateText) {
                }
            });
        }

        function ShowPopup() {
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
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
                <div id="dialog" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" data-dismiss="modal" Text="x" CssClass="close" OnClick="btnNo_Click" />
                                <h4 class="modal-title">ERP Message</h4>
                            </div>
                            <div class="modal-body">
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" CssClass="btn no-border btn-success btn-xs" OnClick="btnNo_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Select Session</label>
                                <asp:DropDownList CssClass="chosen form-control" ID="ddlSession" runat="server">
                                    <asp:ListItem  Value="2021">2021</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2022">2022</asp:ListItem>
                                    <asp:ListItem  Value="2023">2023</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Department</label>
                                <asp:DropDownList ID="ddlDepartment" CssClass="chosen form-control" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Selected="True" Value="0">View All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <label>&nbsp;&nbsp;</label>
                            <div class="form-group">
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-success btn-xs" OnClick="btnView_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Leave Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVAttendanceSheet" runat="server"
                                        CssClass="table table-bordered table-striped" CaptionAlign="Right" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="3" EmptyDataText="No Record Found"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                            <asp:BoundField DataField="LeaveDetails" HeaderText="Leave Details" HtmlEncode="false" />
                                            <asp:BoundField DataField="Total" HeaderText="Leave Balance" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
