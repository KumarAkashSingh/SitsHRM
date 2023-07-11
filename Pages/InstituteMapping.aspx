<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InstituteMapping.aspx.cs" Inherits="Pages_InstituteMapping" %>

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
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
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

        function RemoveBackDrop() { $('.modal-backdrop').remove(); }
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
                        <img src="../images/inprogress.gif" alt="Loading" />
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
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2 top-space">
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
                                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click" Style="color: #000"><b><i class= "fa fa-plus"></i>&nbsp;Add&nbsp;&nbsp;</b></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" Text="Delete All" OnClick="btnDelete_Click" Style="color: #000"><b><i class= "fa fa-trash"></i>&nbsp;Remove</b></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVDetails_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="InstituteCode" HeaderText="InstituteCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="E_code" HeaderText="E_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="InstituteName" HeaderText="Institute Name" />
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash fa-1x" style="color:red;" title="Delete Book Type"></i>
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
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <asp:Button ID="Button7" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                    <h5>Add/Modify Details</h5>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Institute Name</label>
                                                    <asp:DropDownList ID="ddlInstitute" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                        <asp:ListItem Value="0" Selected="True">Select Instiute Name</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="control-label">Employee Name</label>
                                                    <asp:ListBox ID="LstEmploye" runat="server" AppendDataBoundItems="true" SelectionMode="multiple" CssClass="chosen form-control">
                                                        <asp:ListItem Value="0" Selected="True">Select Employee Name</asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row text-right">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-xs" OnClick="btnAdd_Click" />
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

