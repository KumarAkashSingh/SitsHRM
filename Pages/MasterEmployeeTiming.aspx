<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MasterEmployeeTiming.aspx.cs" Inherits="Pages_MasterEmployeeTiming" %>

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
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
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
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="X" />
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
                        <div class="col-md-12 col-xs-12 col-12 top-space">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>View Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                    <div class="panel-tools">
                                        <%--<asp:LinkButton ID="btnAddNew" runat="server" OnClick="btnAddNew_Click"><i class="fa fa-plus-square-o"></i>&nbsp;Add</asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"><i class="fa fa-trash"></i>&nbsp;Remove</asp:LinkButton>--%>
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
                                    <asp:GridView PagerStyle-CssClass="cssPager" PageSize="25" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped cssPager" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVDetails_RowCommand" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GVDetails_PageIndexChanging" OnSorting="GVDetails_Sorting">
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
                                            <%--<asp:BoundField DataField="InstituteCode" HeaderText="InstituteCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />--%>
                                            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                                            <asp:BoundField DataField="InTime" HeaderText="In Time" />
                                            <asp:BoundField DataField="OutTime" HeaderText="Out Time" />
                                            <%--<asp:BoundField DataField="InstituteAlias" HeaderText="Institute" />--%>
                                            <asp:BoundField DataField="Ltimefrom" HeaderText="Lunch Time From" />
                                            <asp:BoundField DataField="Ltimeto" HeaderText="Lunch Time To" />
                                            <%--<asp:BoundField DataField="Preference" HeaderText="Preference" />
                                            <asp:BoundField DataField="Exclude" HeaderText="Exclude" />
                                            <asp:BoundField DataField="CreatedOn" HeaderText="Created On" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="UpdatedOn" HeaderText="Updated On" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />--%>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnModify" runat="server" CommandName="modify">
                                                        <i class="fa fa-pencil fa-1x" style="color:darkblue;" title="Modify Subject"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash fa-1x" style="color:red;" title="Delete Subject"></i>
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
                                <asp:Button ID="Button1" runat="server" OnClick="btnNo_Click" data-dismiss="modal" class="close" type="button" Text="X" />
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
                                <asp:Button ID="Button2" runat="server" OnClick="btnNo2_Click" data-dismiss="modal" class="close" type="button" Text="X" />
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
                    <div class="modal-dialog modal-md">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button7" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="X" />
                                <h5>Add/Modify Details</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <label class="control-label">Description</label>
                                            <asp:TextBox ID="txtDescription" runat="server" placeholder="Enter Description" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <%-- <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="control-label">Institute</label>
                                            <asp:DropDownList ID="ddlInstitute" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                <asp:ListItem Value="0" Selected="True">Select Institute</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">In Time</label>
                                            <asp:TextBox ID="txtInTime" runat="server" placeholder="Enter In Time" AutoCompleteType="Disabled" CssClass="form-control timepicker"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Out Time</label>
                                            <asp:TextBox ID="txtOutTime" runat="server" placeholder="Enter Out Time" CssClass="form-control timepicker"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Lunch Time From</label>
                                            <asp:TextBox ID="txtLunchFrom" runat="server" placeholder="Enter Lunch Time From" CssClass="form-control timepicker"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="control-label">Lunch Time To</label>
                                            <asp:TextBox ID="txtLunchTo" runat="server" placeholder="Enter Lunch Time To" CssClass="form-control timepicker"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-right">
                                        <div class="form-group">
                                            <label class="control-label">&nbsp;</label><br />
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-sm" OnClick="btnAdd_Click" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm" OnClick="btnUpdate_Click" Visible="false" />
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

