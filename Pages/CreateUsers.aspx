<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateUsers.aspx.cs" Inherits="Pages_CreateUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowPopup() {
            $('#myAlert1').removeClass('show');
            $('#maindialog').removeClass('show');
            $('#maindialogUpdate').removeClass('show');
            $('.modal-backdrop').remove();
            $('#dialog').modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup1() {
            $('#dialog').removeClass('show');
            $('#maindialog').removeClass('show');
            $('#maindialogUpdate').removeClass('show');
            $('.modal-backdrop').remove();
            $("#myAlert1").modal('show');
        }
        function HidePopup1() {
            $('#myAlert1').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup3() {
            $('#dialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('#maindialogUpdate').removeClass('show');
            $('.modal-backdrop').remove();
            $("#maindialog").modal('show');
        }
        function HidePopup3() {
            $('#maindialog').removeClass('show');
            $('body').removeClass('modal1-open');
            $('.modal1-backdrop').remove();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup4() {
            $('#dialog').removeClass('show');
            $('#maindialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('.modal-backdrop').remove();
            $("#maindialogUpdate").modal('show');
        }
        function HidePopup4() {
            $('#maindialogUpdate').removeClass('show');
            $('body').removeClass('modal1-open');
            $('.modal1-backdrop').remove();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup5() {
            $('.modal-backdrop').remove();
            $("#rolemapping").modal('show');
        }
        function HidePopup4() {
            $('#rolemapping').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
    </script>
    <script lang="javascript">
        function openPopup(strOpen) {
            open(strOpen, "Info",
                 "status=1, width=800, height=500 , top=50, left=50");
        }

        function SetCursorToTextEnd(textControlID) {
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
                                <asp:Button ID="Button3" runat="server" Text="x" CssClass="close" OnClick="btnNo_Click" />
                                <h4 class="modal-title">ERP Message Box</h4>
                            </div>
                            <div class="modal-body">
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-xs btn-success" OnClick="btnNo_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlType" CssClass="form-control chosen text-left" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">Select Type</asp:ListItem>
                                    <%--<asp:ListItem Value="1">Student</asp:ListItem>--%>
                                    <asp:ListItem Value="2">Employee</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control chosen text-left" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Select Status</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlCourse" CssClass="form-control chosen text-left" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0">Select Department</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlAStatus" CssClass="form-control chosen text-left" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAStatus_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">All</asp:ListItem>
                                    <asp:ListItem Value="1">Delete User</asp:ListItem>
                                    <asp:ListItem Value="2">Create User</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 text-right">
                            <div class="form-group">
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-success btn-sm" OnClick="btnView_Click" />
                            </div>
                        </div>
                        <%--<div class="col-md-3">
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="ace-icon fa fa-check"></i>
                                </span>
                                <asp:TextBox ID="txtSearch" runat="server" placeholder="Type here to Search..." CssClass="form-control" onfocus="SetCursorToTextEnd(this.id);"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-info btn-sm">
                                            <span class="ace-icon fa fa-search icon-on-right bigger-110"></span>
                                            Search
                                    </asp:LinkButton>
                                </span>
                            </div>
                        </div>--%>
                        <div class="col-md-2">
                            <div class="input-group">
                                <asp:DropDownList ID="ddlBulkCreateDelete" runat="server" CssClass="form-control chosen" Enabled="false">
                                    <asp:ListItem Value="0" Selected="True">Select Bulk Create/Delete</asp:ListItem>
                                    <asp:ListItem Value="1">Bulk Create</asp:ListItem>
                                    <asp:ListItem Value="2">Bulk Delete</asp:ListItem>
                                </asp:DropDownList>
                                <span class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="btnBulk" CssClass="btn btn-success btn-sm" OnClick="btnBulk_Click" Enabled="false">
                                            Bulk Update
                                    </asp:LinkButton>
                                </span>
                            </div>
                        </div>
                   </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>User Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped" OnPageIndexChanging="GVDetails_PageIndexChanging" AllowPaging="false" AllowSorting="True" OnSorting="GVDetails_Sorting" PageSize="10" CaptionAlign="Right" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="No Record Found" OnRowDataBound="GVDetails_RowDataBound" AutoGenerateColumns="false" OnRowCommand="GVDetails_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="sno" HeaderText="S.No." />
                                            <asp:BoundField DataField="RN" HeaderText="Code" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="FName" HeaderText="Father's Name" />
                                            <asp:BoundField DataField="Code" HeaderText="Code" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                            <asp:TemplateField HeaderText="Modify" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnMapping" runat="server" CommandName="mapping"><i class="fa fa-copy" title="Role Mapping"></i></asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnModify" runat="server" CommandName="modify"><i class="fa fa-edit" title="Modify Record"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAction" runat="server" CommandName="Action" Text="Allocate" CssClass="btn-info btn-xs" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="User" HeaderText="User" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="WebUserCode" HeaderText="WebUserCode" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert1" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure, You want to delete User?</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row text-center">
                                    <div class="form-horizontal form-group">
                                        <div class="col-md-12">
                                            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-xs btn-success" OnClick="btnYes1_Click" />
                                            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-xs btn-danger" OnClick="btnNo1_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="maindialog" class="modal">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button7" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Create User</h5>
                            </div>
                            <div class="modal-body">
                                <%--<div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal form-group">
                                            <div class="col-md-12">
                                                <label class="control-label">Recommending Officer</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlRecommending" CssClass="form-control chosen text-left" runat="server" AppendDataBoundItems="true">
                                                    <asp:ListItem Selected="True" Value="0">Select Recommending Officer</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal form-group">
                                            <div class="col-md-12">
                                                <label>Approval Officer</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlApproval" CssClass="form-control chosen text-left" runat="server" AppendDataBoundItems="true">
                                                    <asp:ListItem Selected="True" Value="0">Select Approval Officer</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal form-group">
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlWebUserType" CssClass="form-control chosen text-left" runat="server">
                                                    <asp:ListItem Value="0" Selected="True">Select Type</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal form-group">
                                            <div class="col-md-12">
                                                <asp:Button ID="btnAllocate" runat="server" Text="Create" CssClass="btn btn-success btn-xs" OnClick="btnAllocate_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="maindialogUpdate" class="modal">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button2" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Update User</h5>
                            </div>
                            <div class="modal-body">
                                <%--<div class="row">
                                    <div class="col-md-6">
                                        <div class="form-horizontal form-group">
                                            <div class="col-md-12">
                                                <label class="control-label">Recommending Officer</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlRO" CssClass="form-control chosen text-left" runat="server" AppendDataBoundItems="true">
                                                    <asp:ListItem Selected="True" Value="0">Select Recommending Officer</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal form-group">
                                            <div class="col-md-12">
                                                <label>Approval Officer</label>
                                            </div>
                                            <div class="col-md-12">
                                                <asp:DropDownList ID="ddlAO" CssClass="form-control chosen text-left" runat="server" AppendDataBoundItems="true">
                                                    <asp:ListItem Selected="True" Value="0">Select Approval Officer</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Type</label>
                                            <asp:DropDownList ID="ddlT" CssClass="form-control chosen text-left" runat="server">
                                                <asp:ListItem Value="0" Selected="True">Select Type</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class=" form-group">
                                            <label>Password</label>
                                            <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter Password" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Parent Password</label>
                                            <asp:TextBox ID="txtParentPassword" runat="server" placeholder="Enter Parent Password" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>&nbsp;</label><br />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm pull-right" OnClick="btnUpdate_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="rolemapping" class="modal">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button4" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Role Mapping</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView PagerStyle-CssClass="cssPager" AutoGenerateColumns="false" ID="GVRoleMapping" runat="server" CssClass="table table-bordered table-striped">
                                            <Columns>
                                                <asp:BoundField DataField="RoleID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="RoleName" HeaderText="Role" />
                                                <asp:TemplateField ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel runat="server" ID="UpId" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                            <ContentTemplate>
                                                                <asp:CheckBox runat="server" ID="chkStatus" OnCheckedChanged="chkStatus_CheckedChanged" Checked='<%# bool.Parse(Eval("Selected").ToString()) %>' AutoPostBack="true" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
