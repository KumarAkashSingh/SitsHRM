<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PendingLeaveNew.aspx.cs" Inherits="Forms_PendingLeaveNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">

        <div class="container-fluid">
            <div class="row top-space">
                <div class="col-md-12 col-xs-12 col-12">
                    <div class="hpanel hpanel-blue2">
                        <div class="panel-heading">
                            <h5>Pending Leave Details</h5>
                            <div class="panel-tools">
                                <asp:Label ID="lblRecords" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="panel-body table-responsive">
                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVPendingLeaves" runat="server" CssClass="table table-bordered table-striped" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="No Record Found">
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
</asp:Content>

