<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UploadEmployeeDocs.aspx.cs" Inherits="Pages_UploadEmployeeDocs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">


        function ShowPopup(a) {
            alert(a);
            $('#myAlert1').removeClass('show');
            $('.modal-backdrop').remove();
            //$('#dialog').modal('show');
        }

        function HidePopup() {
            $('#dialog').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup1() {
            $("#myAlert1").modal('show');
        }
        function HidePopup1() {
            $('#myAlert1').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
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
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modalp">
                <div class="centerp">
                    <img src="../images/inprogress.gif" alt="Loading" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="container-fluid">

                <div class="row top-space">
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <label class="control-label">Upload Date</label>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="datepicker form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <label class="control-label">Employee</label>
                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="chosen form-control text-left" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">Select Employee</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <label class="control-label">Document Type</label>
                        <asp:DropDownList ID="ddlDocType" runat="server" CssClass="chosen form-control text-left" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Value="0">Select Document Type</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <label class="control-label">Document Name</label>
                        <asp:DropDownList ID="ddlDocName" CssClass="chosen form-control text-left" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Value="0">Select Document Name</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-4">
                       <label class="control-label">Choose File</label> 
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </div>
                    <div class="col-md-3 col-xs-6 col-sm-4">
                        <label class="control-label">&nbsp;</label>
                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-sm btn-success no-border" OnClick="btnAdd_Click" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12 col-xs-12 col-12">
                        <div class="hpanel hpanel-blue2">
                            <div class="panel-heading">
                                <h5>Employee Documents</h5>
                            </div>
                            <div class="panel-body nopadding table-responsive">
                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVEmpDocs" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" PageSize="8" CaptionAlign="Right" OnRowCommand="GVEmpDocs_RowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Both" EmptyDataText="No Record Found" OnRowDataBound="GVEmpDocs_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="sno" HeaderText="S.No." SortExpression="sno" />
                                        <asp:BoundField DataField="RecordNo" HeaderText="RecordNo" SortExpression="RecordNo" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="DocumentTypeName" HeaderText="Document Type" SortExpression="DocumentTypeName" />
                                        <asp:BoundField DataField="documentname" HeaderText="Document Name" SortExpression="documentname" />
                                        <asp:BoundField DataField="DOU" HeaderText="Upload Date" SortExpression="DOU" DataFormatString="{0:dd-MM-yyyy}" />
                                        <asp:BoundField DataField="LoginBy" HeaderText="Upload By" SortExpression="LoginBy" />
                                        <asp:TemplateField HeaderText="View" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <a href="javascript:openPopup('ViewDocs.aspx?id=<%# Eval("RecordNo") %>')">
                                                    <i class="fa fa-eye fa-1x" style="color: blue;" title="View"></i>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del">
                                                        <i class="fa fa-trash fa-1x" style="color:red;" title="Delete"></i>
                                                </asp:LinkButton>
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
                <div id="dialog" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button2" runat="server" Text="x" CssClass="close" OnClick="btnClose_Click" />
                                <h4 class="modal-title">ERP Message Box</h4>
                            </div>
                            <div class="modal-body">
                                <div class="bootbox-body">
                                    <asp:Label ID="dlglbl" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-xs btn-success" OnClick="btnClose_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myAlert1" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnNo1_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-xs btn-success" OnClick="btnYes1_Click" />
                                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-xs btn-danger" OnClick="btnNo1_Click" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

