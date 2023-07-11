<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeLeaveStructure.aspx.cs" Inherits="Pages_EmployeeLeaveStructure" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
    <script type="text/javascript" lang="javascript">

        function ShowPopup() {
            $('.modal-backdrop').remove();
            $('#maindialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert4').removeClass('show');
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup1() {
            $('.modal-backdrop').remove();
            $('#maindialog').removeClass('show');
            $('#dialog').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert4').removeClass('show');
            $("#myAlert1").modal('show');
        }
        function HidePopup1() {
            $('#myAlert1').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup2() {
            $('.modal-backdrop').remove();
            $('#maindialog').removeClass('show');
            $('#dialog').removeClass('show');
            $('#myAlert1').removeClass('show');
            $('#myAlert4').removeClass('show');
            $("#myAlert2").modal('show');
        }
        function HidePopup2() {
            $('#myAlert2').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup3() {
            $('.modal-backdrop').remove();
            $('#myAlert2').removeClass('show');
            $('#dialog').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#myAlert4').removeClass('show');
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
            $('#myAlert2').removeClass('show');
            $('#dialog').removeClass('show');
            $('#myAlert2').removeClass('show');
            $('#maindialog').removeClass('show');
            $("#myAlert4").modal('show');
        }
        function HidePopup4() {
            $('#myAlert4').removeClass('show');
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
                                <asp:Button ID="Button3" runat="server" data-dismiss="modal" Text="x" CssClass="close" OnClick="btnClose_Click" />
                                <h4 class="modal-title">ERP Message</h4>
                            </div>
                            <div class="modal-body">
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" CssClass="btn btn-xs no-border btn-success btn-xs" OnClick="btnClose_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList CssClass="chosen form-control" ID="ddlDesignation" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Designation</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList CssClass="chosen form-control" ID="ddlDepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Department</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList CssClass="chosen form-control" ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Branch</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Employee Type</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-6">
                            <asp:DropDownList CssClass="chosen form-control" ID="ddlStatus" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                <asp:ListItem Value="0">Select Status</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Employee List</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>

                                    <div class="panel-tools">
                                        <div class="dropdown">
                                            <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <i class="fa fa-cogs"></i>
                                            </a>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <asp:LinkButton ID="btnApplyStructure" runat="server" OnClick="btnApplyStructure_Click"><i class="fa fa-upload"></i>&nbsp;Apply Structure</asp:LinkButton>&nbsp;
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server"
                                        CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found"
                                        OnRowCommand="GVDetails_RowCommand" PageSize="25">
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
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" SortExpression="EmployeeCode" />
                                            <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" SortExpression="EmployeeName" />
                                            <asp:BoundField DataField="FatherName" HeaderText="Father's Name" SortExpression="FatherName" />
                                            <asp:BoundField DataField="DesignationDescription" HeaderText="Designation" SortExpression="DesignationDescription" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                                            <asp:BoundField DataField="Contact" HeaderText="Phone" SortExpression="Contact" />
                                            <asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="160px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lnlbtnView1" runat="server" CommandName="viewphoto">
                                                                <i class="fa fa-image fa-1x" style="color: darkblue;" title="View Photo"></i>
                                                    </asp:LinkButton>--%>
                                                    &nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lnlbtnModify" runat="server" CommandName="modify">
                                                                <i class="fa fa-pencil fa-1x" style="color:darkblue;" title="Modify Leave Sturucture"></i>
                                                            </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                                <i class="fa fa-trash fa-1x" style="color:red;" title="Delete Leave Sturucture"></i>
                                                            </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                            <asp:LinkButton ID="lnlbtnView" runat="server" CommandName="view">
                                                                <i class="fa fa-eye fa-1x" style="color:darkblue;" title="View Leave Structure"></i>
                                                            </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DesignationCode" HeaderText="DesignationCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myAlert1" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="btnNo" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are You Sure?</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-horizontal form-group">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="btnYes1" runat="server" Text="Yes" CssClass=" btn-success" OnClick="btnYes1_Click" />
                                            <asp:Button ID="b" runat="server" Text="No" CssClass=" btn-danger" OnClick="btnNo1_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert2" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnNo2_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are You Sure?</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-horizontal form-group">
                                        <div class="col-md-12 text-center">
                                            <asp:Button ID="Button2" runat="server" Text="Yes" CssClass=" btn-success" OnClick="btnYes2_Click" />
                                            <asp:Button ID="Button4" runat="server" Text="No" CssClass=" btn-danger" OnClick="btnNo2_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="maindialog" class="modal">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button7" runat="server" OnClick="btnCloseMain_Click" class="close" type="button" Text="x" />
                                <h5>Add/Modify Leave Structure</h5>
                            </div>
                            <div class="modal-body pre-scrollable">
                                <div class="row">
                                    <div class="hpanel hpanel-blue2">
                                        <div class="panel-heading">
                                            <h5>Leave Structure</h5>
                                            <div class="panel-tools">
                                                <asp:Label ID="lblLeaveRecords" runat="server"></asp:Label>
                                            </div>
                                            <div class="panel-tools">
                                                <asp:DropDownList ID="ddlSession" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="chosen form-control">
                                                    <asp:ListItem Value="0">Select Session</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="panel-body">
                                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeaveStructure" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" EmptyDataText="No Record Found" Visible="false">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkHeader1" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader1_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkCtrl" runat="server" TextAlign="Left" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="leaveCode" HeaderText="leaveCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"></asp:BoundField>
                                                    <asp:BoundField DataField="LeaveDescription" HeaderText="Leave Name" />
                                                    <asp:TemplateField HeaderText="Leaves">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTotalLeaves" runat="server" Text='<%#Bind("TotalLeaves") %>' Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Due After">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDueDate" runat="server" Text='<%# Bind("LPD") %>' Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Without Pay">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtWithoutPay" runat="server" Text='<%# Bind("Frac") %>' Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="From Date">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFromDate" runat="server" Width="100px" Text='<%# Convert.ToDateTime(Session["LeaveSessionFrom"].ToString()).ToString("dd/MM/yyyy") %>' CssClass="datepicker form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To Date">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtToDate" runat="server" Width="100px" Text='<%# Convert.ToDateTime(Session["LeaveSessionTo"].ToString()).ToString("dd/MM/yyyy") %>' CssClass="datepicker form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Days(Back)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDaysBack" runat="server" Text='<%# Bind("backdays") %>' Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Days(Future)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFutureDays" runat="server" Text='<%# Bind("Futuredays") %>' Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="At A Time">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAtATime" runat="server" Text='<%# Bind("atatime") %>' Width="50px"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Limited">
                                                        <ItemTemplate>
                                                            <asp:DropDownList Enabled="false" CssClass="chosen form-control" ID="ddlLimited" runat="server" Text='<%#Bind("Limited") %>' Width="50px">
                                                                <asp:ListItem>Y</asp:ListItem>
                                                                <asp:ListItem>N</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVEmployeeLeaveStructure" runat="server"
                                                CssClass="table table-bordered table-striped" AutoGenerateColumns="False" EmptyDataText="No Record Found"
                                                Visible="false" OnRowCommand="GVEmployeeLeaveStructure_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="RecordNo" HeaderText="RecordNo" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol"></asp:BoundField>
                                                    <asp:BoundField DataField="LeaveDescription" HeaderText="Leave Name" />
                                                    <asp:TemplateField HeaderText="Leaves">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtTotalLeaves" runat="server" Text='<%#Bind("TotalLeaves") %>' Width="50px" OnTextChanged="txtTotalLeaves_TextChanged" AutoPostBack="true" onFocus="this.select()"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                                    <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                                    <asp:TemplateField HeaderText="Days(Back)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDaysBack" runat="server" Text='<%# Bind("backdays") %>' Width="50px" OnTextChanged="txtTotalLeaves_TextChanged" AutoPostBack="true" onFocus="this.select()"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Days(Future)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtFutureDays" runat="server" Text='<%# Bind("Futuredays") %>' Width="50px" OnTextChanged="txtTotalLeaves_TextChanged" AutoPostBack="true" onFocus="this.select()"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="At A Time">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAtATime" runat="server" Text='<%# Bind("atatime") %>' Width="50px" OnTextChanged="txtTotalLeaves_TextChanged" AutoPostBack="true" onFocus="this.select()"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                                <i class="fa fa-trash fa-1x" style="color:red;" title="Delete Leave"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeave" runat="server"
                                                Visible="false" CssClass="table table-bordered">
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-sm btn-success" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCloseMain" runat="server" Text="Close" OnClick="btnCloseMain_Click" CssClass="btn btn-sm btn-success" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

