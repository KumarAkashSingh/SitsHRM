<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateLeaveBalance.aspx.cs" Inherits="Pages_UpdateLeaveBalance" %>

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
        function isFloat(event) {
            if (event.which != 46 && (event.which < 48 || event.which > 57)) {
                event.preventDefault();
            }
            var x = event.srcElement.value.split('.');
            if (event.which == 46 && x.length > 1) {
                event.preventDefault();
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
            <div class="container-fluid">

                <div class="row top-space">
                    <div class="col-md-3 col-xs-12 col-sm-6">
                        <div class="row no-padding">
                            <div class="col-md-12 col-xs-12 col-12">
                                <label>Select Organisation</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlInstitute" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select Organisation</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row no-padding">
                            <div class="col-md-12 col-xs-12 col-12">
                                <div class="hpanel hpanel-blue2">
                                    <div class="panel-heading">
                                        <h5>Employee(s)</h5>
                                    </div>
                                    <div class="panel-body table-responsive">
                                        <asp:GridView ID="GVEmployee" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVEmployee_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code" />
                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnlbtnOpen" CommandName="OpenDetails"><i class="fa fa-eye" title="View/Modify Leave Details"></i></asp:LinkButton>
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
                    <div class="col-md-9 col-xs-12 col-sm-6">
                        <div class="row no-padding">
                            <div class="col-md-4 col-xs-6 col-12">
                                <label>Select Year</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlYear" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select Year</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4 col-xs-6 col-12">
                                <label>Select Month</label>
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlMonth" runat="server" AppendDataBoundItems="true" AutoPostBack="True" CssClass="form-control chosen" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select Month</asp:ListItem>
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
                        </div>
                        <div class="row no-padding">
                            <div class="col-md-12 col-xs-12 col-12">
                                <div class="hpanel hpanel-blue2">
                                    <div class="panel-heading">
                                        <h5>Leave Details</h5>
                                        <div class="panel-tools">
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                        </div>

                                        <div class="panel-tools">
                                            <div class="dropdown">
                                                <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <i class="fa fa-cogs"></i>
                                                </a>
                                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                    <asp:LinkButton runat="server" ID="btnUpdate" OnClick="btnUpdate_Click"><i class="fa fa-save"></i>&nbsp;Update Leave Details</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body table-responsive">
                                        <asp:GridView ID="GVDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                                            <Columns>
                                                <asp:BoundField DataField="E_Code" HeaderText="E_Code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                <asp:BoundField DataField="LeaveCode" HeaderText="LeaveCode" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />

                                                <asp:BoundField DataField="LeaveDescription" HeaderText="Leave Type" />
                                                <asp:TemplateField HeaderText="Total Granted (In Month)" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTotalGranted" Font-Size="12px" runat="server" Text='<%#Bind("TotalGranted") %>' onkeypress="return isFloat(event);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Availed (In Month)" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtTotalAvailed" Font-Size="12px" runat="server" Text='<%#Bind("TotalAvailed") %>' onkeypress="return isFloat(event);"></asp:TextBox>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

