<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WebModuleMaster.aspx.cs" Inherits="Pages_WebModuleMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

        function ShowPopup3() {
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

        function ShowPopup1() {
            $('.modal-backdrop').remove();
            $("#myAlert").modal('show');
        }
        function HidePopup1() {
            $('#myAlert').removeClass('show');
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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
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
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-danger btn-xs" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <div class="form-group top-space">
                                <asp:DropDownList ID="ddlTypeFilter" runat="server" CssClass="chosen form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlTypeFilter_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">Select Type</asp:ListItem>
                                    <asp:ListItem Value="T">T</asp:ListItem>
                                    <asp:ListItem Value="M">M</asp:ListItem>
                                    <asp:ListItem Value="S">S</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>View Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                    <div class="panel-tools">
                                        <div class="dropdown">
                                            <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <i class="fa fa-cogs"></i>
                                            </a>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click"><i class="fa fa-plus-square"></i>&nbsp;Add</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"><i class="fa fa-trash"></i>&nbsp;Delete</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="panel-body pre-scrollable">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped table-responsive" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVDetails_RowCommand" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GVDetails_PageIndexChanging" OnSorting="GVDetails_Sorting" Width="100%">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="wmcode" HeaderText="wmcode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="webmodulename" HeaderText="Module Name" SortExpression="webmodulename" />
                                            <asp:BoundField DataField="wmtype" HeaderText="Type" SortExpression="wmtype" />
                                            <asp:BoundField DataField="wmcodeMain" HeaderText="wmcodeMain" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="Parent" HeaderText="Parent Module" SortExpression="Parent" />
                                            <asp:BoundField DataField="preference" HeaderText="Preference" SortExpression="preference" />
                                            <asp:BoundField DataField="navurl" HeaderText="URL" SortExpression="URL" />
                                            <asp:BoundField DataField="SessionName" HeaderText="SessionName" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:TemplateField HeaderText="Modify" ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnModify" runat="server" CommandName="modify"><i class="fa fa-edit" title="Modify Record"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash" title="Remove Record"></i>

                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnNo_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes_Click" />
                                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert2" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button2" runat="server" OnClick="btnNo2_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="Button4" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes2_Click" />
                                <asp:Button ID="Button6" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo2_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="maindialog" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button7" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Add/Modify Module Details</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Web Module Name</label>
                                            <asp:TextBox ID="txtModuleName" runat="server" placeholder="Enter Module Name" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Type</label>
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control chosen" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="True">Select Type</asp:ListItem>
                                                <asp:ListItem Value="1">T</asp:ListItem>
                                                <asp:ListItem Value="2">M</asp:ListItem>
                                                <asp:ListItem Value="3">S</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Panel runat="server" ID="pnlParent">
                                                <label class="control-label">Parent</label>
                                                <asp:DropDownList ID="ddlParent" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                                    <asp:ListItem Value="0" Selected="True">Select Parent</asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:Panel>
                                            <asp:Panel runat="server" ID="pnlSessionName">
                                                <label class="control-label">Session Name</label>
                                                <asp:TextBox ID="txtSessionName" runat="server" Text="" CssClass="form-control" placeholder="Enter Session Name" AutoComplete="Off"></asp:TextBox>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Preference</label>
                                            <asp:TextBox ID="txtPreference" runat="server" Text="0" CssClass="form-control" AutoComplete="Off" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">URL</label>
                                            <asp:TextBox ID="txtURL" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">&nbsp;</label>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-xs pull-right" OnClick="btnAdd_Click" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-xs pull-right" OnClick="btnUpdate_Click" Visible="false" />
                                        </div>
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

