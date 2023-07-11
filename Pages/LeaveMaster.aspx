<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LeaveMaster.aspx.cs" Inherits="Pages_LeaveMaster" %>

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
                                    <h5>All Leaves</h5>
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
                                            <asp:BoundField DataField="leaveCode" HeaderText="leaveCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="LeaveDescription" HeaderText="Leave Name" SortExpression="LeaveDescription" />
                                            <asp:BoundField DataField="Remarks" HeaderText="Alias" />
                                            <asp:BoundField DataField="TotalLeaves" HeaderText="Leaves" />
                                            <asp:BoundField DataField="LPD" HeaderText="Due After" />
                                            <asp:BoundField DataField="Frac" HeaderText="Without Pay" />
                                            <asp:BoundField DataField="backdays" HeaderText="Days(Back)" />
                                            <asp:BoundField DataField="Futuredays" HeaderText="Days(Future)" />
                                            <asp:BoundField DataField="Limited" HeaderText="Limited" />
                                            <asp:BoundField DataField="LStatus" HeaderText="Status" />
                                            <asp:BoundField DataField="atatime" HeaderText="At A Time" />
                                            <asp:BoundField DataField="MonthLimit" HeaderText="Monthly Limit" />
                                            <asp:BoundField DataField="RequestLeave" HeaderText="Request Leave" />
                                            <asp:BoundField DataField="WithdrawLimit" HeaderText="Withdraw Limit" />
                                            <asp:BoundField DataField="CarryForward" HeaderText="Carry Forward" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnModify" runat="server" CommandName="modify">
                                                        <i class="fa fa-pencil fa-1x" style="color:darkblue;" title="Modify Leave"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash fa-1x" style="color:red;" title="Delete Leave"></i>
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
                        <div class="modal-dialog">
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
                        <div class="modal-dialog">
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
                        <div class="modal-dialog modal-xl">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <asp:Button ID="Button7" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="X" />
                                    <h5>Add/Modify Leave Details</h5>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label">Leave Name</label>
                                                <asp:TextBox ID="txtLeaveName" runat="server" CssClass="form-control" placeholder="Enter Leave Name"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label">Alias Name</label>
                                                <asp:TextBox ID="txtAliasName" runat="server" CssClass="form-control" placeholder="Enter Alias Name"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Leaves</label>
                                                <asp:TextBox ID="txtLeaves" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Days (Back)</label>
                                                <asp:TextBox ID="txtDaysBack" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Days (Future)</label>
                                                <asp:TextBox ID="txtDaysFuture" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Due After</label>
                                                <asp:TextBox ID="txtDueAfter" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">At a Time</label>
                                                <asp:TextBox ID="txtAtATime" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Deduction</label>
                                                <asp:TextBox ID="txtDeduction" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Status</label>
                                                <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                    <asp:ListItem Value="0">A</asp:ListItem>
                                                    <asp:ListItem Value="1">D</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Limited</label>
                                                <asp:DropDownList ID="ddlLimited" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                    <asp:ListItem Value="0">N</asp:ListItem>
                                                    <asp:ListItem Value="1">Y</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Monthly Limit</label>
                                                <asp:TextBox ID="txtMonthLimit" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Request Leave</label>
                                                <asp:DropDownList ID="ddlRequestLeave" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                    <asp:ListItem Value="0">N</asp:ListItem>
                                                    <asp:ListItem Value="1">Y</asp:ListItem>
                                                    <asp:ListItem Value="2">YD</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">WithDraw Limit</label>
                                                <asp:TextBox ID="txtWithDrawLimit" runat="server" Text="0" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label class="control-label">Carry Forward</label>
                                                <asp:DropDownList ID="ddlCF" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="text-right">
                                                <div class="form-group">
                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-xs" OnClick="btnAdd_Click" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-xs" OnClick="btnUpdate_Click" Visible="false" />
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

