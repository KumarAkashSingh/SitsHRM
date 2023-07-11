<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ManualPunch.aspx.cs" Inherits="Pages_ManualPunch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .rdllist table tbody tr td {
            padding: 10px;
        }

            .rdllist table tbody tr td label {
                margin-top: -6px;
            }
    </style>
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
            $("#AddPunch").modal('show');
        }
        function HidePopup2() {
            $('#AddPunch').removeClass('show');
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
            <div id="myAlert" class="modal" style="overflow-y: auto;">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button1" runat="server" OnClick="btnNo_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Are you sure?</h5>
                        </div>
                        <div class="modal-body text-center">
                            <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-xs btn-success" OnClick="btnYes_Click" />
                            <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-xs btn-danger" OnClick="btnNo_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div id="AddPunch" class="modal" style="overflow-y: auto">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <asp:Button ID="Button4" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                            <h5>Add Punch Details</h5>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <label>Date</label>
                                    <asp:TextBox runat="server" ID="txtPunchDate" CssClass="datepicker form-control"></asp:TextBox>
                                </div>
                                <%--<div class="col-md-6">
                                    <label>Employee</label>
                                    <asp:DropDownList runat="server" ID="ddlEmployee" AppendDataBoundItems="true" CssClass="form-control chosen">
                                        <asp:ListItem Value="-1">Select Employee</asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label>Employee</label>
                                    <asp:ListBox runat="server" ID="ddlEmployee" AppendDataBoundItems="true" CssClass="form-control chosen" SelectionMode="Multiple">
                                        <asp:ListItem Value="-1">Select Employee</asp:ListItem>
                                    </asp:ListBox>
                                </div>
                            </div>
                            <%--<div class="row">
                                <div class="col-md-12 rdllist">
                                    <asp:RadioButtonList runat="server" ID="rdlPuchType" AutoPostBack="true" OnSelectedIndexChanged="rdlPuchType_SelectedIndexChanged" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0" Selected="True">&nbsp;Full Day</asp:ListItem>
                                        <asp:ListItem Value="1">&nbsp;1st Half</asp:ListItem>
                                        <asp:ListItem Value="2">&nbsp;2nd Half</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-6">
                                    <label>In Punch</label>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtInPunchDateTime" CssClass="timepicker form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Out Punch</label>
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtOutPunchDateTime" CssClass="timepicker form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtReason" CssClass="form-control" placeholder="Enter Reason for Manual Entry"></asp:TextBox>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                        <div class="modal-footer text-right">
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-success btn-xs" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <div class="row top-space">
                    <div class="col-md-3 col-xs-6 col-6">
                        <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="-1">Select Designation</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-6">
                        <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen" AutoPostBack="True" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="-1">Select Department</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-6">
                        <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Selected="True" Value="-1">Select Organisation</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-6">
                        <asp:DropDownList ID="ddlEmployeeType" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="btnSearch_Click">
                            <asp:ListItem Value="-1">Select Employee Type</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-12 col-12">
                        <div class="form-group">
                            <asp:TextBox ID="txtDate" runat="server" placeholder="Choose Date Range" CssClass="reportrange form-control"></asp:TextBox>
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
                                    <div class="dropdown">
                                        <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            <i class="fa fa-cogs"></i>
                                        </a>
                                        <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                            <asp:LinkButton runat="server" ID="btnAddNew" OnClick="btnAddNew_Click"><i class="fa fa-plus"></i>&nbsp;Add New Punch</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body table-responsive">
                                <asp:GridView ID="GVDetails" runat="server" CssClass="table table-bordered table-striped"
                                    AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVDetails_RowCommand"
                                    OnRowDataBound="GVDetails_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" />
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                        <asp:BoundField DataField="NACCode" HeaderText="Biometric Code" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />
                                        <asp:BoundField DataField="InTime" HeaderText="In Time" />
                                        <asp:BoundField DataField="OutTime" HeaderText="Out Time" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" />
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash fa-1x" style="color:red;" title="Delete Punch"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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

