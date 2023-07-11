<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/HRDashboard.js"></script>
    <script src="../Scripts/Colours.js"></script>
    <script src="../vendor/chartjs/Chart.bundle.js"></script>
    <script src="../vendor/chartjs/moment.min.js"></script>
    <script src="../vendor/chartjs/raphael.js"></script>
    <script src="../vendor/chartjs/morris.js"></script>
    <style>
        .py {
            color: #fff !important;
        }

        .boldTest {
            float: left;
            margin: 7px;
            color: white !important;
        }

        .ui-tabs .ui-tabs-nav li {
            float: right;
        }

        #geeks a:hover {
            color: white;
            background: gray;
        }

        .ui-panel-heading {
            background: #001b54 !important;
        }

            .ui-panel-heading.blue2 {
                background: #001b54 !important;
            }

            .ui-panel-heading.green2 {
                background: #2e8965 !important;
            }

            .ui-panel-heading.red2 {
                background: #e04141 !important;
            }

            .ui-panel-heading.purple {
                background: #7e6eb0 !important;
            }

        .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default, .ui-button, html .ui-button.ui-state-disabled:hover, html .ui-button.ui-state-disabled:active {
            border: none;
            background: transparent;
            font-weight: normal;
        }

            .ui-state-default a {
                color: #fff !important;
            }

            .ui-state-active, .ui-widget-content .ui-state-active, .ui-widget-header .ui-state-active, a.ui-button:active, .ui-button:active, .ui-button.ui-state-active:hover {
                border: 1px solid #c5c5c5;
                background: #f6f6f6;
                font-weight: normal;
                color: #000;
            }

                .ui-state-active a {
                    color: #000 !important;
                }

        .dataTables_filter {
            margin-top: 0 !important;
        }

        .dt-buttons {
            margin: 0 !important;
        }
    </style>
    <script>
        function ShowPopup1() {
            $('.modal-backdrop').remove();
            $("#Detailsdialog").modal('show');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<div class="col-sm-12">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <i class="fa fa-bar-chart-o"></i>Details for the TTC 2022
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-6 col-xs-12">
                        <div class="outreach border-left-primary shadow">
                            <div class="row">
                                <div class="col-md-10 col-xs-10">
                                    <h2 class="text-gray-800">42638</h2>
                                    <p class="text-primary">Total no. of outreach</p>
                                </div>
                                <div class="col-md-2 col-xs-2">
                                    <i class="fa fa-user fa-3x text-gray-300"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-xs-12">
                        <div class="outreach border-left-success shadow">
                            <div class="row">
                                <div class="col-md-10 col-xs-10">
                                    <h2 class="text-gray-800">50866</h2>
                                    <p class="text-success">Total number of kilos of plastic</p>
                                </div>
                                <div class="col-md-2 col-xs-2">
                                    <i class="fa fa-user fa-3x text-gray-300"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>

    <div class="container-fluid">
        <div id="Detailsdialog" class="modal" style="overflow-y: auto">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <input data-dismiss="modal" class="close" type="button" value="X" />
                        <h3>Details</h3>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12 pre-scrollable">
                                <table id="detailsDataTable" class="table no-border table-hover table-responsive" width="100%"></table>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input data-dismiss="modal" class="btn btn-success btn-xs" type="button" value="Close" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-xs-6 col-sm-6">
                <div class="form-group top-space">
                    <label class="control-label">Select Organisation</label>
                    <div class="form-group" id="SelectInstitutes"></div>
                </div>
            </div>
            <div class="col-md-3 col-xs-6 col-sm-6">
                <div class="form-group top-space">
                    <label class="control-label">Select Date</label>
                    <input type="text" id="txtDate" class="form-control datepicker" placeholder="Date" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 col-xs-12 col-sm-6">
                <a onclick="GetAllTotalEmployee();">
                    <div class="dashbox" title="Total Employee">
                        <div class="hpanel widget-int-shape responsive-mg-b-30 bg-primary">
                            <div class="panel-body">
                                <div class="stats-icon pull-right">
                                    <i class="fa fa-user"></i>
                                </div>
                                <div class="stats-title pull-left">
                                    <h1><b id="lblTotalEmployee">0</b></h1>
                                </div>
                                <div class="m-t-xl widget-cl-1 text-center text-white">
                                    <div class="clearfix"></div>
                                    <div class="col-xs-12 text-left">
                                        <p class="text-big font-light">
                                            <span><b>Total Employee</b></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-3 col-xs-12 col-sm-6">
                <a onclick="GetAllTotalPresent();">
                    <div class="dashbox" title="Total Present">
                        <div class="hpanel widget-int-shape responsive-mg-b-30 bg-danger">
                            <div class="panel-body">
                                <div class="stats-icon pull-right">
                                    <i class="fa fa-registered"></i>
                                </div>
                                <div class="stats-title pull-left">
                                    <h1><b id="lblTotalPresent">0</b></h1>
                                </div>
                                <div class="m-t-xl widget-cl-1 text-center">
                                    <div class="clearfix"></div>
                                    <div class="col-xs-12 text-left">
                                        <p class="text-big font-light">
                                            <span><b>Total Present</b></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-3 col-xs-12 col-sm-6">
                <a onclick="GetAllTotalAbsent();">
                    <div class="dashbox" title="Total Absent">
                        <div class="hpanel widget-int-shape responsive-mg-b-30 bg-success">
                            <div class="panel-body">
                                <div class="stats-icon pull-right">
                                    <i class="fa fa-calendar"></i>
                                </div>
                                <div class="stats-title pull-left">
                                    <h1><b id="lblTotalAbsent">0</b></h1>
                                </div>
                                <div class="m-t-xl widget-cl-1 text-center">
                                    <div class="clearfix"></div>
                                    <div class="col-xs-12 text-left">
                                        <p class="text-big font-light">
                                            <span><b>Total Absent</b></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
            <div class="col-md-3 col-xs-12 col-sm-6">
                <a onclick="GetAllOnLeave();">
                    <div class="dashbox" title="On Leave">
                        <div class="hpanel widget-int-shape responsive-mg-b-30 bg-warning">
                            <div class="panel-body">
                                <div class="stats-icon pull-right">
                                    <i class="fa fa-check"></i>
                                </div>
                                <div class="stats-title pull-left">
                                    <h1><b id="lblOnLeave">0</b></h1>
                                </div>
                                <div class="m-t-xl widget-cl-1 text-center">
                                    <div class="clearfix"></div>
                                    <div class="col-xs-12 text-left">
                                        <p class="text-big font-light">
                                            <span><b>On Leave</b></span>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </a>
            </div>
        </div>
        <%--<div class="row">
            <div class="col-sm-3 col-xs-6">
                <div class="card o-hidden bg-primary rounded web-num-card">
                    <div class="card-block text-white">
                        <h3>
                            <asp:Literal runat="server" ID="lblTotalEmployee"></asp:Literal></h3>
                        <h5>Total Employee</h5>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-xs-6">
                <div class="card o-hidden bg-success rounded web-num-card">
                    <div class="card-block text-white">

                        <h3>
                            <asp:Literal runat="server" ID="lblTotalPresent"></asp:Literal></h3>
                        <h5>Total Present</h5>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-xs-6">
                <div class="card o-hidden bg-danger web-num-card">
                    <div class="card-block text-white">
                        <h3>
                            <asp:Label runat="server" ID="lblTotalAbsent"></asp:Label></h3>
                        <h5>Total Absent</h5>
                    </div>
                </div>
            </div>
            <div class="col-md-3 col-xs-6">
                <div class="card o-hidden bg-info web-num-card">
                    <div class="card-block text-white">
                        <h3>
                            <asp:Label runat="server" ID="lblOnLeave"></asp:Label></h3>
                        <h5>On Leave</h5>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="row">
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div id="ChartTabs1">
                    <ul class="red2">
                        <b class="boldTest">Employee Attendance</b>
                        <li><a href="#donut_TabData1">Data</a></li>
                        <li><a href="#donut_TabChart1">Chart</a></li>
                    </ul>
                    <div id='donut_TabChart1'>
                        <canvas id="donut_chartEmployeeAttendanceTeaching" width="800" height="400"></canvas>
                    </div>
                    <div id='donut_TabData1' class="pre-scrollable">
                        <table id="donut_chartTableEmployeeAttendanceTeaching" class="table no-border table-hover table-responsive" width="100%"></table>
                    </div>
                </div>
            </div>
            <%--<div class="col-md-6 col-xs-12 col-sm-12">
                <div id="ChartTabs2">
                    <ul class="green2">
                        <b class="boldTest">Employee Attendance(Non Teaching)</b>
                        <li><a href="#donut_TabData2">Data</a></li>
                        <li><a href="#donut_TabChart2">Chart</a></li>
                    </ul>
                    <div id='donut_TabChart2'>
                        <canvas id="donut_chartEmployeeAttendanceNonTeaching" width="800" height="400"></canvas>
                    </div>
                    <div id='donut_TabData2' class="pre-scrollable">
                        <table id="donut_chartTableEmployeeAttendanceNonTeaching" class="table no-border table-hover table-responsive" width="100%"></table>
                    </div>
                </div>
            </div>--%>

        </div>
        <div class="row">
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="hpanel hpanel-blue2">
                    <div class="panel-heading">
                        <h5>List Of Employees</h5>
                    </div>
                    <div class="panel-body pre-scrollable">
                        <table id="datatableListOfEmployee" class="table no-border table-hover table-responsive" width="100%"></table>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 col-xs-12 col-sm-12">
                <div class="hpanel hpanel-green2">
                    <div class="panel-heading">
                        <h5>Birthdays Details</h5>
                    </div>
                    <div class="panel-body nopadding">
                        <table id="datatableBirthdaysDetails" class="table" width="100%"></table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = dd + '/' + mm + '/' + yyyy;
            $('#txtDate').val(today);

            $("#ChartTabs1,#ChartTabs2").tabs({
                active: 1
            });

            $("#txtDate").datepicker({
                format: 'dd/mm/yyyy',
                autoclose: true
            }).on("changeDate", function (e) {
                GetInstituteList();
            });
            GetInstituteList();
        });
    </script>
</asp:Content>

