<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RequestLeave.aspx.cs" Inherits="Forms_RequestLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function ShowPopup3() {
            $('.modal-backdrop').remove();
            $("#myAlert3").modal('show');
        }
        function HidePopup3() {
            $('#myAlert3').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup2() {
            $('.modal-backdrop').remove();
            $("#myAlert2").modal('show');
        }
        function HidePopup2() {
            $('#myAlert2').removeClass('show');
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

        function ShowPopup() {
            $('.modal-backdrop').remove();
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
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
    <style type="text/css">
        .hiddencol {
            display: none;
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
                <div id="dialog" class="modal" style="overflow-y: auto;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" class="close" type="button" Text="x" />
                                <h5>ERP Message Box</h5>
                            </div>
                            <div class="modal-body">
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-xs btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-2 col-xs-6 col-sm-6">
                            <label>From Date</label>
                            <div class="form-group">
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="datepicker form-control" placeholder="Leave From"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-6">
                            <label>To Date</label>
                            <div class="form-group">
                                <asp:TextBox ID="txtEndDate" runat="server" CssClass="datepicker form-control" placeholder="Leave To"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-6">
                            <label>Leave</label>
                            <div class="form-group">
                                <asp:DropDownList CssClass="select2 form-control" ID="ddlLeaveType" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Selected="True" Value="0">Select Leave Type</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-6">
                            <label>Half/Full</label>
                            <div class="form-group">
                                <asp:DropDownList CssClass="select2 form-control" ID="ddlHalfFull" runat="server">
                                    <asp:ListItem Selected="True" Value="0">Select Type</asp:ListItem>
                                    <asp:ListItem Value="1">Full Day</asp:ListItem>
                                    <asp:ListItem Value="2">I Half</asp:ListItem>
                                    <asp:ListItem Value="3">II Half</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-6">
                            <label>Contact No</label>
                            <div class="form-group">
                                <asp:TextBox ID="txtContactNo" runat="server" placeholder="Contact Number" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-6">
                            <label>Address</label>
                            <div class="form-group">
                                <asp:TextBox ID="txtAddress" runat="server" placeholder="Enter Address" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <div class="form-group">
                                <%--<asp:TextBox ID="txtDutyAssignedBy" runat="server" placeholder="Enter Assigned By" CssClass="form-control 11"></asp:TextBox>--%>
                                <asp:DropDownList CssClass="select2 form-control text-left" ID="ddlAssignedBy" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Selected="True" Value="0">Select Assigned By</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-8 col-xs-6 col-sm-6">
                            <div class="form-group">
                                <asp:TextBox ID="txtRemarks" runat="server" placeholder="Enter Remarks/Reason" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-xs-6 col-sm-6">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-sm btn-success" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Request Leave Details</h5>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVApplyLeave" runat="server" CssClass="table table-bordered " AutoGenerateColumns="False"  PageSize="8" CaptionAlign="Right" OnRowCommand="GVApplyLeave_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Both" OnRowDataBound="GVApplyLeave_RowDataBound" EmptyDataText="No Record Found">
                                        <Columns>
                                            <asp:BoundField DataField="S.No." HeaderText="S.No." SortExpression="S.No." />
                                            <asp:BoundField DataField="AttendanceCode" HeaderText="AttendanceCode" SortExpression="attendencecode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="LeaveDescription" HeaderText="Leave" SortExpression="Leave" />
                                            <asp:BoundField DataField="LeaveCode" HeaderText="LeaveCode" SortExpression="LeaveCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="LeaveType" HeaderText="Leave Type" SortExpression="Type" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" SortExpression="From Date" DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" SortExpression="To Date" DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="Authority" HeaderText="Authority" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="Attachment" HeaderText="Attachment" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />

                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="getfile">
                                                <i class="fa fa-arrow-up" title="Upload File">&nbsp;</i>
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash" title="Remove Entry">&nbsp;</i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View File">
                                                <ItemTemplate>
                                                    <a href="javascript:openPopup('ViewFile2.aspx?id=<%# Eval("AttendanceCode") %>')">View</a>
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
                <div id="myAlert1" class="modal" style="overflow-y: auto;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button2" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="Button7" runat="server" Text="Yes" CssClass="btn btn-xs btn-success" OnClick="btnYes1_Click" />
                                <asp:Button ID="Button8" runat="server" Text="No" CssClass="btn btn-xs btn-danger" OnClick="btnNo1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert3" class="modal" style="overflow-y: auto;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button4" runat="server" OnClick="btnClose3_Click" class="close" type="button" Text="x" />
                                <h5>Upload File</h5>
                            </div>
                            <div class="modal-body1">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label class="control-label">Choose File</label>
                                            <div class="controls">
                                                <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="btn btn-xs btn-success" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button6" runat="server" Text="Close" OnClick="btnClose3_Click" CssClass="btn btn-xs btn-success" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

