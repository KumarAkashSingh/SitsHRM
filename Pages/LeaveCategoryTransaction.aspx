<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LeaveCategoryTransaction.aspx.cs" Inherits="Pages_LeaveCategoryTransaction" %>

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
        }

        function ShowPopup4() {
            $('.modal-backdrop').remove();
            $("#maindialog2").modal('show');
        }
        function HidePopup4() {
            $('#maindialog2').removeClass('show');
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
        function ShowPopup5() {
            $('.modal-backdrop').remove();
            $("#myAlert3").modal('show');
        }
        function HidePopup5() {
            $('#myAlert3').removeClass('show');
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
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>ERP Message Box</h5>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-danger btn-xs" />
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
                                        <div class="dropdown">
                                            <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <i class="fa fa-cogs"></i>
                                            </a>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click" Style="color: #000"><b><i class= "fa fa-plus"></i>&nbsp;Add&nbsp;&nbsp;</b></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" Text="Remove" OnClick="btnDelete_Click" Style="color: #000"><b><i class= "fa fa-trash"></i>&nbsp;Remove</b></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="panel-body nopadding table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false"
                                        EmptyDataText="No Record Found" OnRowCommand="GVDetails_RowCommand">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DesignationCode" HeaderText="DesignationCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="LeaveCategoryID" HeaderText="LeaveCategoryID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="LeaveCategoryName" HeaderText="Leave Category" />
                                            <asp:BoundField DataField="DesignationDescription" HeaderText="Designation" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnModify" runat="server" CommandName="modify"><i class="fa fa-edit" style="color:darkblue;" title="Modify"></i></asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del"><i class="fa fa-trash" style="color:red;" title="Delete"></i></asp:LinkButton>
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
                        <div class="modal-dialog">
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
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <asp:Button ID="Button2" runat="server" OnClick="btnNo2_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                    <h5>Are you sure?</h5>
                                </div>
                                <div class="modal-body text-center">
                                    <asp:Button ID="btnYes2" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes2_Click" />
                                    <asp:Button ID="btnNo2" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo2_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="myAlert3" class="modal" style="overflow-y: auto">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <asp:Button ID="Button8" runat="server" OnClick="btnNo_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                    <h5>Are you sure?</h5>
                                </div>
                                <div class="modal-body text-center">
                                    <asp:Button ID="btnYes3" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes3_Click" />
                                    <asp:Button ID="btnNo3" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo3_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="maindialog" class="modal" style="overflow-y: auto">
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <asp:Button ID="Button7" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                                    <h5>Add/Modify Details</h5>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label">Designation</label>
                                                <asp:ListBox ID="ddlDesignation" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control" SelectionMode="Multiple">
                                                    <asp:ListItem Value="0" Selected="True">Select Category</asp:ListItem>
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label">Leave</label>
                                                <asp:DropDownList ID="ddlLeave" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                    <asp:ListItem Value="0" Selected="True">Select Leave</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Is Leave Count Fixed</label>
                                                <asp:DropDownList ID="ddlIsLeaveCountFix" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                    <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Fixed Count</label>
                                                <asp:TextBox runat="server" ID="txtFixedCount" CssClass="form-control" placeholeder="Fixed Count"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Working Days Calculation</label>
                                                <asp:TextBox runat="server" ID="txtWorkingDaysCalculation" CssClass="form-control" placeholeder="Working Days Calculation"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row text-right">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnSave" runat="server" Text="Add" CssClass="btn btn-success btn-sm" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="maindialog2" class="modal" style="overflow-y: auto">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <asp:Button ID="Button9" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                                    <h5>Add/Modify Details</h5>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label class="control-label">Designation</label>
                                            <div class="form-group">
                                                <asp:Label runat="server" ID="lblDesignation"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label">Leave</label>
                                                <asp:DropDownList ID="ddlLeave2" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                    <asp:ListItem Value="0" Selected="True">Select Leave</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Is Leave Count Fixed</label>
                                                <asp:DropDownList ID="ddlIsLeaveCountFix2" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Fixed Count</label>
                                                <asp:TextBox runat="server" ID="txtFixedCount2" CssClass="form-control" placeholeder="Fixed Count"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Working Days Calculation</label>
                                                <asp:TextBox runat="server" ID="txtWorkingDaysCalculation2" CssClass="form-control" placeholeder="Working Days Calculation"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row text-right">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:Button ID="btnSave2" runat="server" Text="Add" CssClass="btn btn-success btn-xs" OnClick="btnSave2_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="hpanel hpanel-blue2">
                                                <div class="panel-heading">
                                                    <h5>Leave Details</h5>
                                                </div>
                                                <div class="panel-body nopadding pre-scrollable">
                                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails2" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false"
                                                        EmptyDataText="No Record Found" OnRowCommand="GVDetails2_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="LeaveCode" HeaderText="LeaveCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                            <asp:BoundField DataField="LeaveDescription" HeaderText="Leave" />
                                                            <asp:BoundField DataField="ISLeaveCountFixed" HeaderText="Is Leave Count Fixed" />
                                                            <asp:BoundField DataField="FixedCount" HeaderText="Fixed Count" />
                                                            <asp:BoundField DataField="WorkingDaysCalculation" HeaderText="Working Days Calculation" />
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del"><i class="fa fa-trash" style="color:red;" title="Delete"></i></asp:LinkButton>
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
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

