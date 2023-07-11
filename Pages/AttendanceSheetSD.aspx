<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AttendanceSheetSD.aspx.cs" Inherits="Pages_AttendanceSheetSD" EnableEventValidation="false" %>

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
                    <div class="row top-space">
                        <div class="col-md-2 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <label>Date</label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                            </div>
                        </div>
                        <%--<div class="col-md-3 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <label>Branch</label>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                    <asp:ListItem Selected="True" Value="0">Select Branch</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <%--<div class="col-md-3 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <label>Department</label>
                                <asp:DropDownList ID="ddlDepartment" CssClass="chosen form-control" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Selected="True" Value="0">Select Department</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <div class="col-md-2 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <label>Type</label>
                                <asp:DropDownList CssClass="chosen form-control" ID="ddlLeave" runat="server">
                                    <asp:ListItem Selected="True" Value="0">ALL</asp:ListItem>
                                    <asp:ListItem Value="1">Present Employees</asp:ListItem>
                                    <asp:ListItem Value="2">On Leave</asp:ListItem>
                                    <asp:ListItem Value="3">Late Commer</asp:ListItem>
                                    <asp:ListItem Value="4">Absentees</asp:ListItem>
                                    <asp:ListItem Value="5">Absentees/Leave</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-4">
                            <label>&nbsp;</label>
                            <div class="form-group ">
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-success btn-xs" OnClick="btnView_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>View Attendence Sheet</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                    <div class="panel-tools">
                                        <div class="dropdown">
                                            <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <i class="fa fa-cogs"></i>
                                            </a>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" Style="color: white"><i class="fa fa-download"></i>&nbsp; Export to Excel</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVAttendanceSheet" runat="server"
                                        CssClass="table table-bordered table-striped" CaptionAlign="Right" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="3" EmptyDataText="No Record Found" OnRowDataBound="GVAttendanceSheet_RowDataBound">
                                    </asp:GridView>
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
