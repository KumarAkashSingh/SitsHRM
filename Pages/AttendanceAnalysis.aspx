<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AttendanceAnalysis.aspx.cs" Inherits="Pages_AttendanceAnalysis" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function load() {
            $('.select2-container').each(function () {
                $('.select2-search input').prop('focus', false);
            });
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
    </script>

    <script lang="javascript">
        function openPopup(strOpen) {
            open(strOpen, "Info",
                "status=1, width=800, height=500 , top=50, left=50");
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
                        <div class="col-md-2 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <label>For the Month of</label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                            </div>
                        </div>
                        <%--<div class="col-md-3 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <label>College</label>
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass=" chosen form-control">
                                    <asp:ListItem Selected="True" Value="0">Select College</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <div class="col-md-2 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <label>Department</label>
                                <asp:DropDownList ID="ddlDept" runat="server" AppendDataBoundItems="true" CssClass=" chosen form-control">
                                    <asp:ListItem Selected="True" Value="0">Select Department</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <label>Designation</label>
                                <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                    <asp:ListItem Selected="True" Value="0">Select Designation</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <label>Reports</label>
                                <asp:DropDownList ID="ddlReports" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                    <%--<asp:ListItem Selected="True" Value="1">Salary Days</asp:ListItem>--%>
                                    <asp:ListItem Value="2">Display with Time</asp:ListItem>
                                    <%--<asp:ListItem Value="3">Miss Punch</asp:ListItem>
                                    <asp:ListItem Value="4">Monthly Report</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-xs-6 col-sm-4 text-right">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-success" OnClick="btnView_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Reports</h5>

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
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVReports" runat="server"
                                        CssClass="table table-bordered table-striped" EmptyDataText="No Record Found"
                                        OnRowDataBound="GVReports_RowDataBound">
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <table class="table table-responsive table-bordered">
                                <tbody>
                                    <tr style="background-color: #F89406; color: white">
                                        <th style="font-weight: bolder">Allowed Late Punches</th>
                                    </tr>
                                    <tr style="background-color: red; color: white">
                                        <th style="font-weight: bolder">Absent</th>
                                    </tr>
                                    <tr style="background-color: #afd201; color: white">
                                        <th style="font-weight: bolder">Lates</th>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="row">
                        <br />
                    </div>
                </div>
                <script>
                    toDataTable();
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
                                        left: 3
                                    }
                                });
                            }
                        }
                        catch (err) {
                            console.log(err);
                        }
                    }
                </script>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

</asp:Content>
