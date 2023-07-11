<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Payroll_AllowanceMaster.aspx.cs" Inherits="Pages_Payroll_AllowanceMaster" %>

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

        function ShowPopup4() {
            $('.modal-backdrop').remove();
            $("#PercentDialog").modal('show');
        }
        function HidePopup4() {
            $('#PercentDialog').removeClass('show');
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

        function ShowPopup5() {
            $('.modal-backdrop').remove();
            $("#myAlert5").modal('show');
        }
        function HidePopup5() {
            $('#myAlert5').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dialog" class="modal" style="overflow-y: auto">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button3" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5 class="modal-title">ERP Message Box</h5>
                        </div>
                        <div class="modal-body">
                            <div class="bootbox-body">
                                <asp:Label ID="dlglbl" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-xs btn-danger" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-12 col-xs-12 col-12 top-space">
                        <div class="alert alert-danger">
                             <p>(Add to Gross Salary : Whether to be added to or deducted from Gross Salary) 
                             (Allow Deduction/Deductable : Whether to be deducted from Monthly Salary) 
                            (Absentism Deduction : Total days deduction according to this)</p>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-xs-12 col-12">
                        <div class="widget-container-col">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>All Allowances</h5>
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
                                            <asp:BoundField DataField="AllowanceCode" HeaderText="AllowanceCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="AllowanceDescription" HeaderText="Allowance Description" />
                                            <asp:TemplateField HeaderText="Add To Gross Salary">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chKAddToGrossSalary" runat="server" Enabled="false"
                                                        Checked='<%# Eval("IsAddToGrossSalary").ToString() == "True" ? true:false %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deduction Allowed">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chKAllowDeduction" runat="server" Enabled="false"
                                                        Checked='<%# Eval("IsAllowDeduction").ToString() == "True" ? true:false %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AbsentismDeduction" HeaderText="Absentism Deduction" />
                                            <asp:BoundField DataField="Preference" HeaderText="Preference" />
                                            <asp:BoundField DataField="CreatedOn" HeaderText="Created On" />
                                            <asp:BoundField DataField="UpdatedOn" HeaderText="Updated On" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="AddPercent">
                                                        <i class="fa fa-plus" style="color:darkblue;" title="Add/Modify Percent"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnAdd" runat="server" CommandName="modify">
                                                        <i class="fa fa-pencil" style="color:darkblue;" title="Modify Allowance"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash" style="color:red;" title="Delete Allowance"></i>
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
            </div>
            <div id="myAlert" class="modal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button1" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Are you sure?</h5>
                        </div>
                        <div class="modal-body text-center">
                            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-xs btn-success" OnClick="btnYes_Click" />
                            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-xs btn-danger" OnClick="btnNo_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="myAlert2" class="modal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button2" runat="server" OnClick="btnNo2_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Are you sure?</h5>
                        </div>
                        <div class="modal-body text-center">
                            <asp:Button ID="Button4" runat="server" Text="Yes" CssClass="btn btn-xs btn-success" OnClick="btnYes2_Click" />
                            <asp:Button ID="Button6" runat="server" Text="No" CssClass="btn btn-xs btn-danger" OnClick="btnNo2_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="myAlert5" class="modal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button9" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Are you sure?</h5>
                        </div>
                        <div class="modal-body text-center">
                            <asp:Button ID="Button10" runat="server" Text="Yes" CssClass="btn btn-sm btn-success" OnClick="btnYes5_Click" />
                            <asp:Button ID="Button11" runat="server" Text="No" CssClass="btn btn-sm btn-danger" OnClick="btnNo5_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="maindialog" class="modal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button7" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Add/Modify Details</h5>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-horizontal form-group">
                                        <label>Allowance Type</label>
                                        <asp:TextBox ID="txtAllowance" runat="server" placeholder="Enter Allowance Type" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal form-group">
                                        <label>Add to Gross Salary?</label>
                                        <asp:CheckBox ID="IsAddToGrossSalary" runat="server" CssClass="form-group" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal form-group">
                                        <label>Deduction Allowed?</label>
                                        <asp:CheckBox ID="IsAllowDeduction" runat="server" CssClass="form-group" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-horizontal form-group">
                                        <label>Absentism Deduction</label>
                                        <asp:TextBox ID="txtAbsDeduction" runat="server" placeholder="Enter Absentism Deduction" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-horizontal form-group">
                                        <label>Preference</label>
                                        <asp:TextBox ID="txtPreference" runat="server" placeholder="Enter Preference" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 text-right">
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-sm btn-success pull-right" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-sm btn-success pull-right" OnClick="btnUpdate_Click" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="PercentDialog" class="modal">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button8" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Add/Modify Percent Details</h5>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-horizontal form-group">
                                        <label>Percent</label>
                                        <asp:TextBox ID="txtPercent" runat="server" placeholder="Enter Percent" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-horizontal form-group">
                                        <label>Percent of Allowance</label>
                                        <asp:DropDownList CssClass="chosen form-control" ID="ddlAllowance" runat="server" AppendDataBoundItems="true">
                                            <asp:ListItem Selected="True" Value="0">Select Allowance Type</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal form-group">
                                        <label>&nbsp;</label>
                                        <br />
                                        <asp:Button ID="btnSavePercent" runat="server" Text="Save" OnClick="btnSavePercent_Click" CssClass="btn btn-sm btn-success" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="hpanel hpanel-blue2">
                                        <div class="panel-heading">
                                            <h5>Percent Allowance Details</h5>
                                            <div class="panel-tools">
                                                <asp:Label ID="lblPercentRecords" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="panel-body nopadding pre-scrollable">
                                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVPercent" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVPercent_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="PercentOfAllowanceCode" HeaderText="PercentOfAllowanceCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                    <asp:BoundField DataField="AllowanceDescription" HeaderText="Allowance Type" />
                                                    <asp:BoundField DataField="OfAllowance" HeaderText="Of Allowance" />
                                                    <asp:BoundField DataField="Percentage" HeaderText="Percentage" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="DeletePercent"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer text-right">
                            <asp:Button ID="btnClosePercent" runat="server" Text="Close" OnClick="btnClosePercent_Click" CssClass="btn btn-sm btn-success" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

