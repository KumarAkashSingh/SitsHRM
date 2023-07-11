<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="QueryEngineEmployeeNew.aspx.cs" Inherits="Pages_QueryEngineEmployeeNew" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://cdn.datatables.net/searchpanes/1.2.1/js/dataTables.searchPanes.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/select/1.3.1/js/dataTables.select.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.5/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/responsive/2.2.6/js/dataTables.responsive.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.5/js/buttons.colVis.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.5/js/buttons.print.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.5/js/buttons.html5.min.js"></script>

    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.6/css/responsive.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/searchpanes/1.2.1/css/searchPanes.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.6.5/css/buttons.dataTables.min.css" />

    <script src="../Scripts/QueryEngineEmployeeNewV2.js"></script>

    <style>
        .label.label-important {
            background-color: #D15B47;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container-fluid">
        <%--table--%>
        <div class="row">
            <div class="col-md-12 col-xs-12 col-12 top-space">
                <div class="hpanel hpanel-blue2">
                    <div class="panel-heading">
                        <h5>View Details</h5>
                        <div class="panel-tools">
                            <div class="dropdown">
                                <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <i class="fa fa-cogs"></i>
                                </a>
                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                    <a id="btnSendMail" onclick="sendMailchk();">Send Mail
                                    </a>
                                    <hr />
                                    <a id="btnSendSMS" onclick="sendMessageschk();">Send Message<br />
                                        <asp:Label ID="smsbal" runat="server" CssClass="label no-border label-important"></asp:Label>
                                    </a>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="panel-body table-responsive">
                        <table id="gridStudentAll" class="table no-border table-hover table-responsive" width="100%"></table>
                    </div>
                </div>
            </div>
        </div>

        <div id="SendMessage" class="modal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <input type="button" class="close" data-dismiss="modal" value="x" />
                        <h5>Message</h5>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <textarea id="txtSMS" rows="4" class="form-control" placeholder="Message"></textarea>
                            </div>
                            <div class="col-md-6">
                                <label id="txtSMSCount">0 Characters</label>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer text-right">
                        <input type="button" id="btnSendFMessage" class="btn btn-xs btn-success" value="Send" />
                        <input type="button" class="btn btn-xs btn-danger" data-dismiss="modal" value="Close" />
                    </div>
                </div>
            </div>
        </div>

        <div id="SendMail" class="modal">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <input type="button" class="close" data-dismiss="modal" value="x" />
                        <h5>Mail</h5>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-4">
                                <input type="text" id="txtSubject" class="form-control" placeholder="Enter Subject of Email" />
                            </div>
                            <div class="col-md-4">
                                <textarea id="txtBody" rows="4" class="form-control" placeholder="Enter Body of Email"></textarea>
                            </div>
                            <div class="col-md-4">
                                <input type="file" id="fileMail" class="input-file ace-file-input" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer text-right">
                        <input type="button" id="btnSendFMail" class="btn btn-xs btn-success" value="Send" />
                        <input type="button" class="btn btn-xs btn-danger" data-dismiss="modal" value="Close" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#txtSMS').keyup(updateCount);
            $('#txtSMS').keydown(updateCount);
            $("#btnSendFMessage").click(function () {
                SendMessage();
            });

            $("#btnSendFMail").click(function () {
                SendMail();
            });

            GetAllEmployee(0);
        });

        function getAllValue() {
            setDataTableStudentAll(JSON.parse($('#PageContent_hdTableData').val()))
        }
    </script>
</asp:Content>
