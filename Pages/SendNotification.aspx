<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SendNotification.aspx.cs" Inherits="Pages_SendNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowPopup(a) {
            $('#myAlert1').removeClass('show');
            $('.modal-backdrop').remove();
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
                        <img alt="" src="../images/inprogress.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dialog" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />
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
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <label class="control-label">Dept./Desg./Employee</label>
                            <asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged" CssClass="chosen form-control text-left">
                                <asp:ListItem Value="0" Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="1">Department</asp:ListItem>
                                <asp:ListItem Value="2">Designation</asp:ListItem>
                                <asp:ListItem Value="3">Employee</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <label class="control-label">Department/Designation/Employee</label>
                            <asp:ListBox ID="ddlEmpDept" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control" SelectionMode="Multiple"></asp:ListBox>
                        </div>
                        <div class="col-md-6 col-xs-12 col-sm-12">
                            <label class="control-label">Title</label>
                            <asp:TextBox ID="txtSubject" runat="server" placeholder="Enter Title" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-10 col-xs-12 col-sm-12">
                            <label class="control-label">Description</label>
                            <asp:TextBox ID="txtDescription" runat="server" placeholder="Enter Description" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-2 col-xs-12 col-sm-12 success-tbn text-right">
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-sm" OnClick="btnSave_Click" />
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Notifications</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVViewCircularNotice"
                                        runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False"
                                        CaptionAlign="Right" BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Both" EmptyDataText="No Record Found"
                                        OnRowDataBound="GVViewCircularNotice_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="NotificationID" HeaderText="NotificationID" SortExpression="NotificationID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="SNo" HeaderText="S.No." />
                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                            <asp:BoundField DataField="DepartmentName" HeaderText="Dept/Desg/Emp" />
                                            <asp:BoundField DataField="Title" HeaderText="Title" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="CreatedOn" HeaderText="Created On" />
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>Is Activated?</HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel runat="server" ID="UpId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="chkCtrl" runat="server" Checked='<%# Eval("IsActive") %>' AutoPostBack="true" OnCheckedChanged="chkCtrl_CheckedChanged" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
