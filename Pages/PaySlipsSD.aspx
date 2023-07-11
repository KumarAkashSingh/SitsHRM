<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PaySlipsSD.aspx.cs" Inherits="Pages_PaySlipsSD" EnableEventValidation="false" %>

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
        function ShowPopup1() {
            $('.modal-backdrop').remove();
            $("#myAlert1").modal('show');
        }
        function HidePopup1() {
            $('#myAlert1').removeClass('show');
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

        function openPopup(strOpen) {
            open(strOpen, "Info",
                "status=1, width=310, height=310 , top=50, left=350,scrollbars=no");
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
                <div id="dialog" class="modal fade" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="X" />
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
                    <div class="row top-space">
                        <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                    <asp:ListItem Value="0">Select Status</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                    <asp:ListItem Value="0">Select Designation</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                    <asp:ListItem Value="0">Select Department</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%--<div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlDesignationType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                    <asp:ListItem Value="0">Select Designation Type</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                            <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Employee Type</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                            <div class="form-group">
                                <asp:DropDownList runat="server" ID="ddlMonth" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                            <div class="form-group">
                                <asp:DropDownList runat="server" ID="ddlYear" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6 col-12">
                            <div class="form-group text-center">
                                <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="btn btn-success btn-sm" />
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6 col-12">
                            <div class="input-group">
                                <asp:TextBox ID="txtSearch" runat="server" placeholder="Type here to Search..." CssClass="form-control" onfocus="SetCursorToTextEnd(this.id);"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-info btn-sm">
                                            <span class="ace-icon fa fa-search icon-on-right bigger-110"></span>
                                            Search
                                    </asp:LinkButton>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Pay Slips</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>

                                    <div class="panel-tools">
                                        <div class="dropdown">
                                            <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <i class="fa fa-cogs"></i>
                                            </a>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click"><i class="fa fa-file-excel-o"></i>&nbsp;Export</asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"><i class="fa fa-trash"></i>&nbsp;Remove</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body nopadding table-responsive">
                                    <asp:GridView ID="GVDetails" PagerStyle-CssClass="cssPager" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found"
                                        PageSize="1000" OnRowCommand="GVDetails_RowCommand" AutoGenerateColumns="false" OnRowDataBound="GVDetails_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="sno" HeaderText="S.No." HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="PayslipNumber" HeaderText="Pay Slip Number" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="DesignationDescription" HeaderText="Designation" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="GrossPay" HeaderText="Gross Pay" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="PresentDays" HeaderText="Days to be Paid" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="BasicDA" HeaderText="Basic+DA" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="HRA" HeaderText="HRA" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="Allowance" HeaderText="Allow." HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="OtherAllowance" HeaderText="Other Allow." HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="Arrear" HeaderText="Arrear" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="Total" HeaderText="Total" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="PF" HeaderText="PF" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="ESI" HeaderText="ESI" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="TransportCharges" HeaderText="Transport Charges" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="ElectricityCharges" HeaderText="Electricity Charges" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="AccomodationCharges" HeaderText="Accomodation Charges" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="TDS" HeaderText="TDS" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="AdvanceLoan" HeaderText="Advance Loan" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="SecurityAmount" HeaderText="Security Amount" HeaderStyle-CssClass="text-center" />
                                            <asp:BoundField DataField="NetPayment" HeaderText="Salary in Hand" HeaderStyle-CssClass="text-center" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnView" runat="server" CommandName="ViewSalary">
                                                        <i class="fa fa-eye fa-1x" style="color:darkblue;" title="View Pay Slip Details"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash fa-1x" style="color:red;" title="Delete Pay Slip"></i>
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
                <div id="myAlert1" class="modal fade" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="btnNo" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="btnYes1" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes1_Click" />
                                <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert2" class="modal fade" style="overflow-y: auto">
                    <div class="modal-dialog">
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
                <div id="myAlert5" class="modal fade" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button9" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>View Pay Slip Details</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="widget-container-col">
                                        <div class="hpanel hpanel-blue2">
                                            <div class="panel-heading">
                                                <h5>Pay Slip Details</h5>
                                                <div class="panel-tools">
                                                    <asp:Label ID="lblSalaryRecords" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="panel-body nopadding pre-scrollable">
                                                <asp:GridView ID="GVSalary" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found" OnRowDataBound="GVSalary_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="sno" HeaderText="S.No." />
                                                        <asp:BoundField DataField="allowancedescription" HeaderText="Allowance Type" SortExpression="EmployeeName" />
                                                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                                        <asp:BoundField DataField="Deduction" HeaderText="Deduction" SortExpression="Deduction" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

