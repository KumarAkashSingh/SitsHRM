<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SetWorkingHours.aspx.cs" Inherits="Pages_SetWorkingHours" %>

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
                        <div class="col-md-3 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlStatus" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                    <asp:ListItem Value="0">Select Status</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                    <asp:ListItem Value="0">Select Designation</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                    <asp:ListItem Value="0">Select Department</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" CssClass="form-control chosen">
                                    <asp:ListItem Value="0">Select Branch</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-6 col-xs-6 col-sm-4">
                            <div class="form-group">
                                <asp:Button ID="btnApplyStructure" runat="server" Text="Apply Structure" CssClass="btn btn-success btn-sm" OnClick="btnApplyStructure_Click" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12 top-space">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Employee List</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server" CssClass="label label-important"></asp:Label>
                                    </div>
                                </div>

                                <div class="panel-body nopadding table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVDetails_RowCommand" PageSize="25">
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
                                            <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" />
                                            <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                            <asp:BoundField DataField="FatherName" HeaderText="Father's Name" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="Contact" HeaderText="Phone" />
                                            <asp:BoundField DataField="email" HeaderText="Email" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnView1" runat="server" CommandName="viewphoto">
                                                        <i class="fa fa-image fa-1x" style="color:darkblue;" title="View"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnModify" runat="server" CommandName="modify">
                                                        <i class="fa fa-pencil fa-1x" style="color:darkblue;" title="Modify Time"></i>
                                                    </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp;
                                                      <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash fa-1x" style="color:red;" title="Delete Time"></i>
                                                      </asp:LinkButton>
                                                    &nbsp;&nbsp;&nbsp
                                                      <asp:LinkButton ID="lnlbtnView" runat="server" CommandName="view">
                                                        <i class="fa fa-pencil fa-eye" style="color:darkblue;" title="View"></i>
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
                <div id="myAlert1" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="btnNo" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="X" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="btnYes1" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes1_Click" />
                                <asp:Button ID="btnNo1" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert2" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnNo2_Click" data-dismiss="modal" class="close" type="button" Text="X" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="Button2" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes2_Click" />
                                <asp:Button ID="Button4" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo2_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="maindialog" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button7" runat="server" data-dismiss="modal" class="close" type="button" Text="X" />
                                <h5>Add/Modify/View Working Hours</h5>
                            </div>
                            <div class="modal-body">
                                <asp:Panel ID="toppanel" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">Time From</label>
                                                <asp:TextBox runat="server" ID="TimeFrom" CssClass="form-control timepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">Time To</label>
                                                <asp:TextBox runat="server" ID="TimeTo" CssClass="form-control timepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">Break From</label>
                                                <asp:TextBox runat="server" ID="BreakFrom" CssClass="form-control timepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">Break To</label>
                                                <asp:TextBox runat="server" ID="BreakTo" CssClass="form-control timepicker"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="control-label">Total Hours</label>
                                                <asp:TextBox runat="server" ID="TotalHours" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-9">
                                            <div class="form-group">
                                                <label class="control-label">Remarks</label>
                                                <asp:TextBox runat="server" ID="Remarks" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label">Apply Days</label>
                                                <asp:ListBox ID="radDays" runat="server" CssClass="chosen-select form-control" SelectionMode="Multiple">
                                                    <asp:ListItem Value="1">Monday</asp:ListItem>
                                                    <asp:ListItem Value="2">Tuesday</asp:ListItem>
                                                    <asp:ListItem Value="3">Wednesday</asp:ListItem>
                                                    <asp:ListItem Value="4">Thursday</asp:ListItem>
                                                    <asp:ListItem Value="5">Friday</asp:ListItem>
                                                    <asp:ListItem Value="6">Saturday</asp:ListItem>
                                                    <asp:ListItem Value="7">Sunday</asp:ListItem>
                                                </asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="control-label">&nbsp;</label>
                                                <asp:CheckBox runat="server" ID="chkIsFlexible" Text="Is Flexible" />
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="bottompanel" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="widget-container-col">
                                            <div class="hpanel hpanel-blue2">
                                                <div class="panel-heading">
                                                    <h5>View Details</h5>
                                                </div>
                                                <div class="panel-body nopadding pre-scrollable">
                                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVTimings" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVTimings_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="RecordNo" HeaderText="RecordNo" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                            <asp:BoundField DataField="Day" HeaderText="Day" />
                                                            <asp:BoundField DataField="InTime" HeaderText="In Time" />
                                                            <asp:BoundField DataField="OutTime" HeaderText="Out Time" />
                                                            <asp:BoundField DataField="Ltimefrom" HeaderText="Break From" />
                                                            <asp:BoundField DataField="ltimeto" HeaderText="Break To" />
                                                            <asp:BoundField DataField="TotalHrs" HeaderText="Total Hrs" />
                                                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                                            <asp:BoundField DataField="IsFlexible" HeaderText="Flexible?" />
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
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
                                </asp:Panel>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-xs" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCloseMain" runat="server" Text="Close" OnClick="btnCloseMain_Click" CssClass="btn btn-success btn-xs" />
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

