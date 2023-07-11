<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SetWebUserPermissionsSD.aspx.cs" Inherits="Pages_SetWebUserPermissionsSD" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Content/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript">
        function ShowPopup() {
            $('.modal-backdrop').remove();
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
            $('.modal-backdrop').remove();
        }

        function ShowPopup3() {
            $('.modal-backdrop').remove();
            $("#maindialog").modal('show');
        }
        function HidePopup3() {
            $('#maindialog').removeClass('show');
            $('.modal-backdrop').remove();
        }

        function ShowPopup4() {
            $('.modal-backdrop').remove();
            $("#maindialogUpdate").modal('show');
        }
        function HidePopup4() {
            $('#maindialogUpdate').removeClass('show');
            $('.modal-backdrop').remove();
        }

        function ShowPopup5() {
            $('.modal-backdrop').remove();
            $("#myAlert").modal('show');
        }
        function HidePopup5() {
            $('#myAlert').removeClass('show');
            $('.modal-backdrop').remove();
        }

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
                        <img alt="" src="../images/inprogress.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="dialog" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" OnClick="btnNo_Click" class="close" type="button" Text="x" />
                                <h5>ERP Message Box</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnNo_Click" CssClass="btn btn-danger btn-xs" />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="maindialog" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button7" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>ERP Modules</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <telerik:RadTreeView ID="treeView1" runat="server" CheckBoxes="True"
                                        TriStateCheckBoxes="true" CheckChildNodes="true">
                                    </telerik:RadTreeView>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <div class="form-group">
                                            <asp:Button ID="btnSet" runat="server" Text="Set" CssClass="btn btn-success btn-xs" OnClick="btnSet_Click" />
                                            <asp:Button ID="btnReSet" runat="server" Text="Reset" CssClass="btn btn-success btn-xs" OnClick="btnReSet_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="maindialogUpdate" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnCloseMainUpdate_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>ERP Modules</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <telerik:RadTreeView ID="treeViewUpdate" runat="server" CheckBoxes="True"
                                            TriStateCheckBoxes="true" CheckChildNodes="true">
                                        </telerik:RadTreeView>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="control-group">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-xs pull-right" OnClick="btnUpdate_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myAlert" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button2" runat="server" OnClick="btnCloseP_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>View Permissions</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <telerik:RadTreeView ID="tv" runat="server"
                                            TriStateCheckBoxes="true">
                                        </telerik:RadTreeView>
                                        <asp:Label runat="server" ID="lblNoPermission" CssClass="label label-important" Visible="false" Text="No Permission Granted"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnCloseP_Click" data-dismiss="modal" CssClass="btn btn-success btn-xs" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2 top-space">
                                <div class="panel-heading">
                                    <h5>View Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                    <div class="panel-tools">
                                        <asp:LinkButton ID="btnPermission" runat="server" OnClick="btnPermission_Click" Visible="false" Style="color: white"><i class="fa fa-upload" title="Set Permission"></i>&nbsp; <span class="hidden-xs">Set Permission</span></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="widget-body pre-scrollable">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped"
                                         OnPageIndexChanging="GVDetails_PageIndexChanging" AllowPaging="false" AllowSorting="True" OnSorting="GVDetails_Sorting" 
                                        PageSize="10" CaptionAlign="Right" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                         EmptyDataText="No Record Found" AutoGenerateColumns="false" OnRowCommand="GVDetails_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RN" HeaderText="Code" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="FName" HeaderText="Father's Name" />
                                            <asp:BoundField DataField="Code" HeaderText="Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                            <%--<asp:TemplateField HeaderText="Photo" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image1" runat="server" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Modify Permission" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnModify" runat="server" CommandName="modify"><i class="fa fa-edit" title="Modify Permission"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View Permission" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAction" runat="server" CommandName="View"><i class="fa fa-eye" title="View Permission"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="WebUserCode" HeaderText="WebUserCode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        </Columns>
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
