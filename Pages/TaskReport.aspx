<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaskReport.aspx.cs" Inherits="Pages_TaskReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" lang="javascript">
        function confirmDelete() {
            if (confirm("Are you sure?") == true)
                return true;
            else
                return false;
        }
        function ShowPopup3() {
            $('.modal-backdrop').remove();
            $("#maindialog").modal('show');
        }
        function HidePopup3() {
            $('#maindialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
        function ShowPopup1() {
            $('.modal-backdrop').remove();
            $("#myAlert2").modal('show');
        }
        function HidePopup1() {
            $('#myAlert2').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

    </script>

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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12">
                        <div class="hpanel hpanel-blue2">
                            <div class="panel-heading">
                                <h5>Assigned Task</h5>
                            </div>
                            <div class="panel-body nopadding table-responsive">
                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped"
                                    AutoGenerateColumns="false" OnRowCommand="GVDetails_RowCommand" EmptyDataText="No Record Found" PageSize="50">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCtrl" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                        <asp:BoundField DataField="TaskAssignby" HeaderText="Task Assigned By" />
                                        <asp:BoundField DataField="TaskAssignTo" HeaderText="Task Assigned To" />
                                        <asp:BoundField DataField="TaskName" HeaderText="Task Name" />
                                        <asp:BoundField DataField="Remark" HeaderText="Remarks" />
                                        <asp:BoundField DataField="TaskAssignedOn" HeaderText="Task Assigned On" />
                                        <asp:BoundField DataField="IsTaskCompleted" HeaderText="Task Assign To Satuts " />
                                        <asp:BoundField DataField="TaskAssignedRemarks" HeaderText="Task Assign To Remarks" />
                                        <asp:BoundField DataField="TaskComplitionDate" HeaderText="Complete Date" />
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

