<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BiometricAttendanceEmployeeWise.aspx.cs" Inherits="Forms_BiometricAttendanceEmployeeWise" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function SetCursorToTextEnd(textControlID) {
            $('.modal-backdrop').remove();
            var text = document.getElementById(textControlID);
            if (text != null && text.value.length > 0) {
                if (text.createTextRange) {
                    var range = text.createTextRange();
                    range.moveStart('character', text.value.length);
                    range.collapse();
                    range.select();
                }
                else if (text.setSelectionRange) {
                    var textLength = text.value.length;
                    text.setSelectionRange(textLength, textLength);
                }
            }
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
                <div id="dialog" class="modal " style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="X" />
                                <h5>ERP Message Box</h5>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-danger btn-sm" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-3 col-xs-6 col-6">
                            <div class="control-group">
                                <label class="control-label">From Date</label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="datepicker form-control" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6 col-6">
                            <div class="control-group">
                                <label class="control-label">To Date</label>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="datepicker form-control" AutoCompleteType="Disabled"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6 col-6">
                            <div class="control-group">
                                <label class="control-label">Employee</label>
                                <asp:DropDownList ID="ddlUpload" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                    <asp:ListItem Selected="True" Value="0">Select Employee</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 col-xs-6 col-6">
                            <div class="control-group">
                                <label class="control-label">&nbsp;</label><br />
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-success btn-sm" OnClick="btnView_Click" />
                                <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-info btn-sm" OnClick="btnExport_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Biometric Attendence</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView ID="GVAttendance" runat="server" CssClass="table table-bordered table-striped" CaptionAlign="Right" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="No Record Found">
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
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
