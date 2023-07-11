<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApplyLeaveSDNew.aspx.cs" Inherits="Forms_ApplyLeaveSDNew" %>

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
        function DelayPopup() {
            setTimeout(ShowPopup, 500);
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
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="">
            <ProgressTemplate>
                <div class="modalp">
                    <div class="centerp">
                        <img src="../images/inprogress.gif" alt="Loading" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="container-fluid">
            <div class="row top-space">
                <div class="col-md-3 col-xs-12 col-sm-12">
                    <div class="hpanel hpanel-blue2">
                        <div class="panel-heading">
                            <h5>Leave Balance</h5>
                        </div>
                        <div class="panel-body table-responsive">
                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVLeave" runat="server"
                                CssClass="table table-bordered">
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-md-9 col-xs-12 col-sm-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row top-space">
                                <div class="col-md-3 col-xs-6 col-sm-6">
                                    <label>From Date</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="datepicker form-control" AutoCompleteType="Disabled" placeholder="Leave From"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-xs-6 col-sm-6">
                                    <label>To Date</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="datepicker form-control" AutoCompleteType="Disabled" placeholder="Leave To"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-xs-6 col-sm-6">
                                    <label>Contact No</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtContactNo" runat="server" placeholder="Contact Number" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3 col-xs-6 col-sm-6">
                                    <label>Address</label>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtAddress" runat="server" placeholder="Enter Address" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3 col-xs-6 col-sm-6">
                                    <label>Leave</label>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlLeaveCode" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlLeaveCode_SelectedIndexChanged" CssClass="form-control chosen">
                                            <asp:ListItem Selected="True" Value="0">Select Leave</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-xs-6 col-sm-6">
                                    <label>Half/Full</label>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlHalfFull" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                            <asp:ListItem Selected="True" Value="0">Select Type</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-xs-6 col-sm-6">
                                    <label>Recommendation</label>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlRecommendingOfficer" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                            <asp:ListItem Selected="True" Value="0">Select Recommending Officer</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-xs-6 col-sm-6">
                                    <label>Approval</label>
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddlApprovingOfficer" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                            <asp:ListItem Selected="True" Value="0">Select Approving Officer</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3 col-xs-6 col-sm-6">
                                    <label class="control-label">Choose File</label>
                                    <div class="form-group">
                                        <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" CssClass="inputfile" />
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                    <ContentTemplate>
                                        <div class="col-md-6 col-xs-6 col-sm-6">
                                            <label class="control-label">Enter Description</label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtDescription" runat="server" placeholder="Enter Description" CssClass="form-control"></asp:TextBox>
                                                <asp:TextBox ID="txtDays" runat="server" placeholder="Total No. of Days" CssClass="form-control hidden" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-xs-6 col-sm-6 text-right">
                                            <label class="control-label">&nbsp;</label>
                                            <div class="form-group">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-sm btn-success " OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="btnSave" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                    <ContentTemplate>
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Leave Details</h5>
                                    <div class="panel-tools">
                                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" Style="color: white"><i class="fa fa-trash"></i>&nbsp; Remove</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVApplyLeave" runat="server"
                                        CssClass="table table-bordered" AutoGenerateColumns="False"
                                        PageSize="10" OnRowDataBound="GVApplyLeave_RowDataBound" CaptionAlign="Right" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        GridLines="Both" EmptyDataText="No Record Found">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="S.No." HeaderText="S.No." SortExpression="S.No." />
                                            <asp:BoundField DataField="AttendanceCode" HeaderText="Attendencecode" SortExpression="AttendanceCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="LeaveDescription" HeaderText="Leave" SortExpression="LeaveDescription" />
                                            <asp:BoundField DataField="LeaveType" HeaderText="Type" SortExpression="LeaveType" />
                                            <asp:BoundField DataField="FromDate" HeaderText="From Date" SortExpression="FromDate" />
                                            <asp:BoundField DataField="ToDate" HeaderText="To Date" SortExpression="ToDate" />
                                            <asp:BoundField DataField="LeaveCode" HeaderText="leavetypeCode" SortExpression="LeaveCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="Authority" HeaderText="Authority" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="AttachmentPath" HeaderText="View File" HtmlEncode="false" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <%--<div class="col-md-12 col-xs-12 col-sm-12 top-space">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Engagement Details</h5>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVEngagement" runat="server"
                                        CssClass="table table-bordered" AutoGenerateColumns="False" OnRowDataBound="GVEngagement_RowDataBound"
                                        CaptionAlign="Right" OnRowCommand="GVEngagement_RowCommand" EmptyDataText="No Record Found">
                                        <Columns>
                                            <asp:BoundField DataField="TTCode" HeaderText="TTCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="PeriodCode" HeaderText="PeriodCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="EngagedEmployee" HeaderText="EngagedEmployee" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="AttendanceCode" HeaderText="AttendanceCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="ClassSectionCode" HeaderText="ClassSectionCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="SubjectCode" HeaderText="SubjectCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="TTDate" HeaderText="Date" />
                                            <asp:BoundField DataField="Class" HeaderText="Class" />
                                            <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                            <asp:BoundField DataField="Period" HeaderText="Period" />
                                            <asp:BoundField DataField="Group" HeaderText="Group" />
                                            <asp:BoundField DataField="EngagementStatus" HeaderText="EngagementStatus" />
                                            <asp:TemplateField HeaderText="Engaged Employee">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblEmployeeName" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                                    <asp:DropDownList runat="server" ID="ddlFreeEmployee" AppendDataBoundItems="true" CssClass="form-control chosen">
                                                        <asp:ListItem Value="0">Select Engagement Faculty</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel runat="server" ID="UpId1" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                        <ContentTemplate>
                                                            <asp:Label runat="server" ID="lblReason" Text='<%# Bind("Reason") %>'></asp:Label>
                                                            <asp:DropDownList runat="server" ID="ddlReason" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged">
                                                                <asp:ListItem Value="0">Select Reason</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Other Reason">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtReason" CssClass="form-control" placeholder="Enter Reason" Text='<%# Bind("OfficeDutyRemarks") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnlBtnSaveEngagement" CommandName="Save" Visible="false" Style="color: white"><i class="fa fa-1x fa-save" style="color:white"></i></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnlBtnDeleteEngagement" CommandName="Del" Visible="false" Style="color: white"><i class="fa fa-1x fa-trash" style="color:white"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>--%>
                        <div id="dialog" class="modal" style="overflow-y: auto">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:Button ID="Button3" runat="server" Text="x" CssClass="close" data-dismiss="modal" />
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
                        <div id="myAlert" class="modal" style="overflow-y: auto">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <asp:Button ID="Button1" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                        <h5>Are you sure?</h5>
                                    </div>
                                    <div class="modal-body text-center">
                                        <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-xs btn-success " OnClick="btnYes_Click" />
                                        <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-xs btn-danger " OnClick="No_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

