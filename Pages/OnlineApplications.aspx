<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OnlineApplications.aspx.cs" Inherits="Pages_OnlineApplications" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--<link href="../Content/jquery-ui.css" rel="stylesheet" />--%>
    <script type="text/javascript">
        function ShowPopup(message) {
            $('.modal-backdrop').remove();
            $("#dialog").modal('show');
        }

        function HidePopup() {
            $('#maindialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
    </script>

    <script lang="javascript">
        function openPopup(strOpen) {
            open(strOpen, "Info",
                "status=1, width=800, height=500 , top=50, left=50");
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
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />
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
                        <div class="col-md-3 col-xs-6 col-sm-3">
                            <div class="form-group">
                                <label>Post Applied For</label>
                                <asp:DropDownList ID="ddlPost" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-3">
                            <div class="form-group">
                                <label>From Date</label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-3">
                            <div class="form-group">
                                <label>To Date</label>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-6 col-sm-3">
                            <label>&nbsp;&nbsp;</label>
                            <div class="form-group">
                                <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" CssClass="btn btn-sm btn-success" Text="View"></asp:Button>
                            </div>
                        </div>
                    </div>
            
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-sm-12">
                            <div class="widget-container-col">
                                <div class="hpanel hpanel-blue2">
                                    <div class="panel-heading">
                                        <h5>Job Applications</h5>
                                        <div class="panel-tools">
                                            <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                        </div>
                                        <div class="panel-tools">
                                            <div class="dropdown">
                                                <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <i class="fa fa-cogs"></i>
                                                </a>
                                                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                    <asp:LinkButton ID="btnExport" runat="server" OnClick="btnExport_Click" Style="color: white"><i class="fa fa-download"></i>&nbsp; Export to Excel</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="panel-body nopadding table-responsive">
                                        <asp:GridView ID="GVOnlineApplications" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found" AutoGenerateColumns="false" OnRowDataBound="GVOnlineApplications_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="sno" HeaderText="S.No" SortExpression="sno" />
                                                <asp:BoundField DataField="eid" HeaderText="EID" SortExpression="eid" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="name" HeaderText="Name" SortExpression="name" />
                                                <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" />
                                                <asp:BoundField DataField="Mobile" HeaderText="Mobile" SortExpression="Mobile" />
                                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                <asp:BoundField DataField="HomeTownCity" HeaderText="City" SortExpression="HomeTownCity" />
                                                <asp:BoundField DataField="MaritalStatus" HeaderText="MaritalStatus" SortExpression="MaritalStatus" />
                                                <asp:BoundField DataField="PostAppliedFor" HeaderText="PostAppliedFor" SortExpression="PostAppliedFor" />
                                                <asp:BoundField DataField="Branch" HeaderText="Branch" SortExpression="Branch" />
                                                <asp:BoundField DataField="ApplyDate" HeaderText="Apply Date" SortExpression="ApplyDate" />
                                                <asp:TemplateField HeaderText="Other Details">
                                                    <ItemTemplate>
                                                        <a href="javascript:openPopup('ViewQualifications.aspx?id=<%# Eval("eid") %>')">View</a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resume">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblFilePath" Text='<%# Eval("Resume") %>'></asp:Label>
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
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
