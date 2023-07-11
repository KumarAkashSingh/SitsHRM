<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovalAuthorityNew.aspx.cs" Inherits="Pages_ApprovalAuthorityNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
                <div id="dialog" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button3" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
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
                <div id="myAlert1" class="modal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes1_Click" />
                                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-3 col-xs-12 col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                    <asp:ListItem Selected="True" Value="-1">Select Department</asp:ListItem>
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-4">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                    <asp:ListItem Selected="True" Value="-1">Select Branch</asp:ListItem>
                                    <asp:ListItem Value="0">All</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-4">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-sm" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <label>Select Recommending Officer</label>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlRecommending" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                    <asp:ListItem Selected="True" Value="0">Select Recommending Officer</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <label>Select Level</label>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlLevel" runat="server" AppendDataBoundItems="true" CssClass="chosen form-control">
                                    <asp:ListItem Selected="True" Value="0">Select Level</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <label>Is Final Authority</label>
                            <div class="form-group">
                                <asp:DropDownList ID="ddlIsFinalAuthority" runat="server" CssClass="chosen form-control">
                                    <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <label>&nbsp;</label>
                            <div class="form-group">
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-info btn-sm" OnClick="btnUpdate_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Recommending & Approval Officer Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body nopadding table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVAuthority" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found" OnRowDataBound="GVAuthority_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
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
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

