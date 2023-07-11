<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SMSSendReport.aspx.cs" Inherits="Pages_SMSSendReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

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
                <div class="container-fluid">
                    <div class="row top-space">
                        <div class="col-md-3 col-xs-12 col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Select Date</label>
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="reportrange form-control"></asp:TextBox>
                            </div>
                        </div>
                       <%-- <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">To Date</label>
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                            </div>
                        </div>--%>
                        <div class="col-md-4 col-xs-12 col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Course</label>
                                <asp:DropDownList ID="ddlCourse" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                    <asp:ListItem Selected="True" Value="0">Select Course</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4 col-xs-12 col-sm-6">
                            <div class="form-group">
                                <label class="control-label hidden-sm">&nbsp;</label>
                                <br />
                                <asp:Button ID="btnView" runat="server" Text="View" CssClass="btn btn-success btn-sm" OnClick="btnView_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">

                                    <h5>SMS Send Report</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server" CssClass="label label-important labeldisplay"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body nopadding table-responsive">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVReport" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False"  PageSize="50" CaptionAlign="Right" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" EmptyDataText="No Record Found" OnRowDataBound="GVReport_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="sno" HeaderText="S.No." SortExpression="sno" />
                                            <asp:BoundField DataField="sdate" HeaderText="Date" SortExpression="sdate" DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:BoundField DataField="RegistrationNumber" HeaderText="Reg. No." SortExpression="RegistrationNumber" />
                                            <asp:BoundField DataField="rollno" HeaderText="Roll No." SortExpression="rollno" />
                                            <asp:BoundField DataField="NameOfStudent" HeaderText="Name" SortExpression="NameOfStudent" />
                                            <asp:BoundField DataField="ClassSectionName" HeaderText="Course" SortExpression="ClassSectionName" />
                                            <asp:BoundField DataField="TotalSMSSent" HeaderText="Total SMS Sent" SortExpression="TotalSMSSent" />
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
