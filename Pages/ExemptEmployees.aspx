<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ExemptEmployees.aspx.cs" Inherits="ExemptEmployees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .btn-group-sm>.btn, .btn-sm{
            padding:7px 10px !important;
        }
        .buttons, button, .btn{
            margin:0px !important;
        }
    </style>
    <script type="text/javascript" lang="javascript">
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
                <div id="dialog" class="modal" style="overflow: auto;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" class="close" type="button" Text="x" />
                                <h5>ERP Message Box</h5>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-danger btn-xs" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList ID="ddlBranch" CssClass="chosen form-control text-left" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Branch</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList ID="ddlStatus" CssClass="chosen form-control text-left" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Status</asp:ListItem>
                                <asp:ListItem Value="A" Selected="True">Active</asp:ListItem>
                                <asp:ListItem Value="L">Left</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:CheckBox runat="server" ID="chkFilTer" Text="&nbsp;Only Exempted Employees" OnCheckedChanged="btnSearch_Click" AutoPostBack="true" />
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:CheckBox runat="server" ID="chlAllowEdit" Text="&nbsp;Edit Salary Heads" OnCheckedChanged="btnSearch_Click" AutoPostBack="true" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>All Employees</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false"
                                        EmptyDataText="No Record Found" PageSize="100" OnRowDataBound="GVDetails_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="sno" HeaderText="S.No." />
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" SortExpression="EmployeeName"/>
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Department" SortExpression="DepartmentName"/>
                                            <asp:BoundField DataField="DesignationDescription" HeaderText="Designation" SortExpression="DesignationDescription"/>
                                            <asp:BoundField DataField="Contact" HeaderText="Contact" SortExpression="Contact"/>
                                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email"/>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>Is Exempted?</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel runat="server" ID="UpId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="chkCtrl" runat="server" Checked='<%# Eval("IsExempted") %>' AutoPostBack="true" OnCheckedChanged="chkCtrl_CheckedChanged" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>Edit Salary Heads?</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel runat="server" ID="UpId1" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="chkCtrl1" runat="server" Checked='<%# Eval("IsAllowSalaryHeads") %>' AutoPostBack="true" OnCheckedChanged="chkCtrl1_CheckedChanged" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
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
    </div>
</asp:Content>

