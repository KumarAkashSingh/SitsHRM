<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovePendingLeaveSDNew.aspx.cs" Inherits="Forms_ApprovePendingLeaveSDNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowPopup3() {
            $('.modal-backdrop').remove();
            $("#myAlert3").modal('show');
        }
        function HidePopup3() {
            $('#myAlert3').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup2() {
            $('.modal-backdrop').remove();
            $("#myAlert").modal('show');
        }
        function HidePopup2() {
            $('#myAlert').removeClass('show');
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

        function ShowPopup() {
            $('.modal-backdrop').remove();
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup10() {
            $('.modal-backdrop').remove();
            $("#myAlert10").modal('show');
        }
        function HidePopup10() {
            $('#myAlert10').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
    </script>

    <script type="text/javascript">
        function openPopup(strOpen) {
            open(strOpen, "Info",
                 "status=1, width=800, height=500 , top=50, left=50");
        }
    </script>

    <script type="text/javascript" lang="javascript">
        function confirmDelete() {
            if (confirm("Are you sure?") == true)
                return true;
            else
                return false;
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
                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-6 col-xs-12 col-sm-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Filter Records</h5>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-xs-12 col-sm-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlFilterStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="select2 form-control">
                                                <asp:ListItem Selected="True" Value="0">Select Status</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-xs-12 col-sm-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="select2 form-control">
                                                <asp:ListItem Selected="True" Value="0">Select Employee</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6 col-xs-12 col-sm-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="select2 form-control">
                                                <asp:ListItem Selected="True" Value="0">Select Month</asp:ListItem>
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
                                    <div class="col-md-6 col-xs-12 col-sm-6">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged" AppendDataBoundItems="true" CssClass="select2 form-control">
                                                <asp:ListItem Selected="True" Value="0">Select Year</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                        </div>

                        <div class="col-md-6 col-xs-12 col-sm-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Action</h5>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 col-xs-12 col-sm-12">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlAction" runat="server" CssClass="select2 form-control">
                                                <asp:ListItem Selected="True" Value="0">Select Action</asp:ListItem>
                                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                                <asp:ListItem Value="2">Reject</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-xs-12 col-sm-12">
                                        <div class="input-group">
                                            <asp:TextBox ID="txtRemarks" runat="server" placeholder="Enter Remarks" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-success btn-sm">
                                            <span class="ace-icon fa fa-save icon-on-right bigger-110"></span>
                                            Save
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="row top-space">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Leave Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVApprovedLeaveDetails" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" PageSize="20" CaptionAlign="Right" OnRowCommand="GVApprovedLeaveDetails_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Both" OnRowDataBound="GVApprovedLeaveDetails_RowDataBound" EmptyDataText="No Record Found">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AttendanceCode" HeaderText="Attendance Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="IsEngagement" HeaderText="Engage" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="NextAuthority" HeaderText="Next Authority" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="CurrentLeaveAt" HeaderText="CurrentLeaveAt" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="IsFinalAuthority" HeaderText="Is Final Authority" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="LevelID" HeaderText="Level ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="S.No." HeaderText="S.No." />
                                            <asp:BoundField DataField="Code" HeaderText="Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="Department" HeaderText="Department" />
                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                            <asp:BoundField DataField="Leave" HeaderText="Leave" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                            <asp:BoundField DataField="DOL" HeaderText="DOA" />
                                            <asp:BoundField DataField="From Date" HeaderText="From Date" />
                                            <asp:BoundField DataField="To Date" HeaderText="To Date" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" />
                                            <asp:BoundField DataField="ApprovedBy" HeaderText="Approved By" />
                                            <asp:BoundField DataField="markto" HeaderText="Mark To" />
                                            <asp:BoundField DataField="Attachment" HeaderText="Attachment" HtmlEncode="false" />
                                            <asp:TemplateField HeaderText="Engagement">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="fetch">Engagement</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance Leave" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="view"><i class="fa fa-1x fa-eye"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />

                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="dialog" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button4" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>ERP Message Box</h5>
                            </div>
                            <div class="modal-body">
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnNo_Click" CssClass="btn btn-xs btn-danger" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes_Click" />
                                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="No_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert3" class="modal">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Leave Balance Details</h5>
                            </div>
                            <div class="modal-body">
                                <div class="widget-container-col">
                                    <div class="hpanel hpanel-blue2">
                                        <div class="panel-heading">
                                            <h5>Leave Balance</h5>
                                        </div>
                                        <div class="panel-body nopadding pre-scrollable">
                                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeave" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found">
                                                <Columns>
                                                    <asp:BoundField DataField="LeaveDescription" HeaderText="Leave" />
                                                    <asp:BoundField DataField="Total" HeaderText="Total" />
                                                    <asp:BoundField DataField="Availed" HeaderText="Availed" />
                                                    <asp:BoundField DataField="Balance" HeaderText="Balance" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert1" class="modal">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <%--<button data-dismiss="modal" class="close" type="button">×</button>--%>
                                <asp:Button ID="Button2" runat="server" OnClick="btnClose1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Subject Engagement</h5>
                            </div>
                            <div class="modal-body">
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlDate" runat="server" AppendDataBoundItems="true" CssClass="select2 form-control" OnSelectedIndexChanged="ddlDate_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Value="0">Select Date </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="select2 form-control" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Value="0">Select Course</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlSubject" runat="server" AppendDataBoundItems="true" CssClass="select2 form-control" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Value="0">Select Subject</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlPeriod" runat="server" AppendDataBoundItems="true" CssClass="select2 form-control" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Value="0">Select Period</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlBatch" runat="server" AppendDataBoundItems="true" CssClass="select2 form-control">
                                                    <asp:ListItem Selected="True" Value="0">Select Batch</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlEFaculty" runat="server" AppendDataBoundItems="true" CssClass="select2 form-control" OnSelectedIndexChanged="ddlEFaculty_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Value="0">Select Engagement Faculty</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:DropDownList ID="ddlReason" runat="server" AppendDataBoundItems="true" CssClass="select2 form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0">Select Reason</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:TextBox ID="txtOfficeDutyRemarksME" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm" OnClick="btnUpdate_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="hpanel hpanel-blue2">
                                                <div class="panel-heading">
                                                    <span class="icon"><i class="icon-th"></i></span>
                                                    <h5>Subject Engagement Details</h5>
                                                </div>
                                                <div class="panel-body nopadding pre-scrollable">
                                                    <div class="control-group">
                                                        <div class="form-group">
                                                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVSubEngagemnt" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" PageSize="10" CaptionAlign="Right" OnRowCommand="GVSubEngagemnt_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Both" EmptyDataText="No Record Found">
                                                                <Columns>
                                                                    <asp:BoundField DataField="ENRecNo" HeaderText="ENRecNo" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                    <asp:BoundField DataField="TTDate" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                    <asp:BoundField DataField="ClassSectionName" HeaderText="Course" />
                                                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee" />
                                                                    <asp:BoundField DataField="EmployeeName1" HeaderText="Engaged Employee" />
                                                                    <asp:BoundField DataField="SubjectDescription" HeaderText="Subject" />
                                                                    <asp:BoundField DataField="PeriodName" HeaderText="Period" />
                                                                    <asp:BoundField DataField="groupname" HeaderText="Group" />
                                                                    <asp:BoundField DataField="LoginBy" HeaderText="LoginBy" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                    <asp:TemplateField HeaderText="Delete">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del"><img src="../img/delete.png" width="20px" height="20px" /></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>

                                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                                <RowStyle ForeColor="#000066" />
                                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                <SortedDescendingHeaderStyle BackColor="#00547E" />

                                                            </asp:GridView>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose1" runat="server" Text="Close" OnClick="btnClose1_Click" CssClass="btn btn-success btn-xs" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert10" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button24" runat="server" OnClick="btnNo10_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="Button25" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes10_Click" />
                                <asp:Button ID="Button26" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo10_Click" />
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

