<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PartiallyRemoveManualPunch.aspx.cs" Inherits="PartiallyRemoveManualPunch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" lang="javascript">
        function RemoveBackDrop() { $('.modal-backdrop').remove(); }
        function ShowPopup() {
            $('.modal-backdrop').remove();
            $("#dialog").modal('show');
        }
        function HidePopup() {
            $('#dialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup2() {
            $('.modal-backdrop').remove();
            $("#myAlertRemove").modal('show');
        }
        function HidePopup2() {
            $('#myAlertRemove').removeClass('show');
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
                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-xs btn-danger btn-xs" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="myAlertRemove" class="modal" style="overflow-y: auto;">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button2" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Continue to remove?</h5>
                        </div>
                        <div class="modal-body text-center">
                            <asp:Button ID="btnYesRemove" runat="server" Text="Yes" CssClass="btn btn-xs btn-success" OnClick="btnYesRemove_Click" />
                            <asp:Button ID="btnNoRemove" runat="server" Text="No" CssClass="btn btn-xs btn-danger" data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <div class="row top-space">
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="0">Select Designation</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="0">Select Department</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="0">Select College</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="0">Select Employee Type</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <div class="form-group">
                            <asp:TextBox ID="txtDate" runat="server" placeholder="Choose Date Range" CssClass="reportrange form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-6 col-12">
                        <asp:DropDownList ID="ddlPunchStatus" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="0">Select Status</asp:ListItem>
                            <asp:ListItem Value="1">Pending</asp:ListItem>
                            <asp:ListItem Value="2">Approved</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-4 col-xs-12 col-sm-12 col-12">
                        <div class="input-group">
                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search By Employee Name / Login ID" CssClass="form-control" onfocus="SetCursorToTextEnd(this.id);"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:LinkButton runat="server" ID="LinkButton1" OnClick="btnSearch_Click" CssClass="btn btn-info btn-sm">
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
                                <h5>Punch Details</h5>
                                <div class="panel-tools">
                                    <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                </div>
                                <div class="panel-tools">
                                    <asp:LinkButton runat="server" ID="btnRemove" OnClick="btnRemove_Click"><i class="fa fa-trash"></i>&nbsp;Remove Punch</asp:LinkButton>
                                </div>
                            </div>
                            <div class="panel-body table-responsive">
                                <asp:GridView ID="GVDetails" runat="server" CssClass="table table-bordered table-striped"
                                    AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowDataBound="GVDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCtrl" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                        <asp:BoundField DataField="NACCode" HeaderText="Biometric Code" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
                                        <asp:BoundField DataField="InTime" HeaderText="In Time" />
                                        <asp:BoundField DataField="OutTime" HeaderText="Out Time" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                    </Columns>
                                    <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

