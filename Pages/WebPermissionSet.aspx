<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WebPermissionSet.aspx.cs" Inherits="Pages_WebPermissionSet" %>

<%--<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../jsTree/dist/jstree.min.js"></script>
    <link href="../jsTree/dist/themes/default/style.min.css" rel="stylesheet" />
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

        function ShowPopup3() {
            $('.modal-backdrop').remove();
            $("#maindialog").modal('show');
        }
        function HidePopup3() {
            $('#maindialog').removeClass('show');
            $('body').removeClass('modal1-open');
            $('.modal1-backdrop').remove();
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

        function ShowPopup2() {
            $('.modal-backdrop').remove();
            $("#myAlert2").modal('show');
        }
        function HidePopup2() {
            $('#myAlert2').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }

        function ShowPopup4() {
            getAllData();
            $('.modal-backdrop').remove();
            $("#maindialogUpdate").modal('show');
        }
        function HidePopup4() {
            $('#maindialogUpdate').removeClass('show');
            $('body').removeClass('modal1-open');
            $('.modal1-backdrop').remove();
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
    <div id="content">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modalp">
                    <div class="centerp">
                        <img alt="" src="../images/inprogress.gif" />
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
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>ERP Message Box</h5>
                            </div>
                            <div class="modal-body">
                                <asp:Label ID="dlglbl" runat="server" CssClass="large"></asp:Label>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button5" runat="server" Text="Close" OnClick="btnClose_Click" CssClass="btn btn-danger btn-xs" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="input-group">
                                <span class="input-group-addon">
                                    <i class="ace-icon fa fa-check"></i>
                                </span>
                                <asp:TextBox ID="txtSearch" runat="server" placeholder="Type here to Search..." CssClass="form-control" onfocus="SetCursorToTextEnd(this.id);"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-info btn-sm">
                                            <span class="ace-icon fa fa-search icon-on-right bigger-110"></span>
                                            Search
                                    </asp:LinkButton>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5 class="widget-title">View Details</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>

                                    <div class="panel-tools">
                                        <div class="dropdown">
                                            <a class="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <i class="fa fa-cogs"></i>
                                            </a>
                                            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click" Style="color: #000"><b><i class= "fa fa-plus"></i>&nbsp;Add&nbsp;&nbsp;</b></asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" Text="Delete All" OnClick="btnDelete_Click" Style="color: #000"><b><i class= "fa fa-trash"></i>&nbsp;Remove</b></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-body no-padding pre-scrollable">
                                    <asp:GridView PagerStyle-CssClass="cssPager" ID="GVDetails" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="GVDetails_RowCommand" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GVDetails_PageIndexChanging" OnSorting="GVDetails_Sorting">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="true" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkCtrl" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="wps_code" HeaderText="wps_code" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="wpsname" HeaderText="Permission Set Name" />
                                            <asp:BoundField DataField="Type" HeaderText="Type" />
                                            <asp:BoundField DataField="DefaultSet" HeaderText="Default Set" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="90px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnModify" runat="server" CommandName="modify"><i class="fa fa-1x fa-pencil-square" title="Edit Set"></i></asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="btnModify" runat="server" CommandName="modifyP"><i class="fa fa-1x fa-plus-square" title="Add / Modify permission"></i></asp:LinkButton>&nbsp;&nbsp;
                                                    <asp:LinkButton ID="lnlbtnDelete" runat="server" CommandName="del"><i class="fa fa-1x fa-trash-o" title="Delete Set"></i></asp:LinkButton>
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
                <div id="myAlert" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button1" runat="server" OnClick="btnNo_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes_Click" />
                                <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myAlert2" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button2" runat="server" OnClick="btnNo2_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Are you sure?</h5>
                            </div>
                            <div class="modal-body text-center">
                                <asp:Button ID="Button4" runat="server" Text="Yes" CssClass="btn btn-success btn-xs" OnClick="btnYes2_Click" />
                                <asp:Button ID="Button6" runat="server" Text="No" CssClass="btn btn-danger btn-xs" OnClick="btnNo2_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="maindialog" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button7" runat="server" OnClick="btnCloseMain_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>Add/Modify Details</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-group">
                                        <label class="control-label">Permission Set Name</label>
                                        <asp:TextBox ID="txtPermissionSetName" runat="server" placeholder="Enter Permission Set Name" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Type</label>
                                        <asp:DropDownList ID="ddlType" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                            <asp:ListItem Value="0" Selected="True">Select Type</asp:ListItem>
                                            <asp:ListItem Value="1">Employee</asp:ListItem>
                                            <asp:ListItem Value="2">Student</asp:ListItem>
                                            <asp:ListItem Value="3">All</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label">Default Set</label>
                                        <asp:DropDownList ID="ddlDefaultSet" runat="server" AppendDataBoundItems="true" CssClass="form-control chosen">
                                            <asp:ListItem Value="0" Selected="True">Select Default</asp:ListItem>
                                            <asp:ListItem Value="1">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-sm pull-right" OnClick="btnAdd_Click" />
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-success btn-sm pull-right" OnClick="btnUpdate_Click" Visible="false" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="maindialogUpdate" class="modal" style="overflow-y: auto">
                    <div class="modal-dialog modal-xl">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button8" runat="server" OnClick="btnCloseMainUpdate_Click" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>ERP Modules</h5>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <%--<telerik:RadTreeView ID="treeViewUpdate" runat="server" CheckBoxes="True"
                                        TriStateCheckBoxes="true" CheckChildNodes="true">
                                    </telerik:RadTreeView>--%>
                                    <div id="data" class="demo"></div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12 text-center">
                                        <div class="form-group">
                                            <asp:Button ID="btnSet" runat="server" Text="Set" CssClass="btn btn-success btn-xs" OnClick="btnSet_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="HiddenFieldParents" runat="server" />
                <asp:HiddenField ID="HiddenFieldSelectValues" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        $(function () {
            // $('#jstree1').jstree();
        });

        function getAllData() {
            var test = '[' + $('#PageContent_HiddenFieldParents').val() + ']';

            //JSON.parse('[' + $('#PageContent_HiddenFieldParents').val() + ']')
            //console.log(eval(test));
            //console.log(test);
            $('#data').on('changed.jstree', function (e, data) {
                console.log(data);
                var i, j, r = [];
                for (i = 0, j = data.selected.length; i < j; i++) {
                    //console.log(data.selected[i]);
                    r.push(data.instance.get_node(data.selected[i]).id);
                }
                $('#PageContent_HiddenFieldSelectValues').val(r.join(', '))
                //console.log('Selected: ' + r.join(', '));
            }).jstree({
                'plugins': ["checkbox"],
                'core': {
                    "themes": {
                        "icons": false
                    },
                    'data': eval(test)
                }
            });
        }
    </script>
</asp:Content>

