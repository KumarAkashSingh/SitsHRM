<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeRemarks.aspx.cs" Inherits="EmployeeRemarks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" lang="javascript">
        function RemoveBackDrop() { $('.modal-backdrop').remove(); }
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
    <style>
        .btn-group-sm > .btn, .btn-sm {
            padding: 7px 10px !important;
        }

        .buttons, button, .btn {
            margin: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                            <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="X" />
                            <h5>ERP Message Box</h5>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-xs btn-danger btn-xs" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="container-fluid">
                <div class="row top-space">
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="0">Select Designation</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="0">Select Department</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="0">Select College</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="0">Select Employee Type</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-12 col-sm-6 col-12">
                        <div class="form-group">
                            <asp:TextBox ID="txtDate" runat="server" placeholder="Choose Date Range" CssClass="reportrange form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12 col-xs-12 col-12">
                        <div class="hpanel hpanel-blue2">
                            <div class="panel-heading">
                                <h5>Employee Remarks</h5>
                                <div class="panel-tools">
                                    <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="panel-body table-responsive">
                                <asp:GridView ID="GVDetails" runat="server" CssClass="table table-bordered table-striped"
                                    AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowDataBound="GVDetails_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="sno" HeaderText="S.No." />
                                        <asp:BoundField DataField="RemarksDate" HeaderText="Remarks Date" SortExpression="EmployeeCode" />
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName" />
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DepartmentName" />
                                        <asp:BoundField DataField="DesignationDescription" HeaderText="Designation" SortExpression="DesignationDescription" />
                                        <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

