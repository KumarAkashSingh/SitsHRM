<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeMaster_Update.aspx.cs" Inherits="Pages_EmployeeMaster_Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../styles/normalize.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../styles/demo.css" />
    <link rel="stylesheet" type="text/css" href="../styles/tabs.css" />
    <link rel="stylesheet" type="text/css" href="../styles/tabstyles.css" />
    <script src="../js/modernizr.custom.js"></script>
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

        function ShowPopup4() {
            $('.modal-backdrop').remove();
            $("#myAlert4").modal('show');
        }
        function HidePopup4() {
            $('#myAlert4').removeClass('show');
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

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <style>
        .py {
            padding-top: 30px;
        }
        .py1 {
            text-align:left;
        }

        .input-group-addon, .input-group-btn {
            width: 1%;
            white-space: nowrap;
            vertical-align: middle;
            padding-top: 13px;
        }

        .input-group-addon, .input-group-btn, .input-group .form-control {
            display: block;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <svg class="hidden">
        <defs>
            <path id="tabshape" d="M80,60C34,53.5,64.417,0,0,0v60H80z" />
        </defs>
    </svg>
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
                        <fieldset class="border p-2">
                            <legend class="w-auto"><strong>Employee Information</strong></legend>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="tabs tabs-style-shape">
                                        <nav>
                                            <ul id="tabsIDForPostback">
                                                <li class="tab-current">
                                                    <a href="#section-shape-1">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Employee Details</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-2">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Address</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-3">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Employee Joining Details</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-4">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Account Details</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-5">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Academic Qualifications</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-6">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Working Details</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-7">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Books Published</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-8">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Paper Published</span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </nav>
                                        <div class="content-wrap">
                                            <section id="section-shape-1">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="widget-container-col">
                                                                    <div class="hpanel hpanel-blue2">
                                                                        <div class="panel-heading">
                                                                            <h5>Photo</h5>
                                                                            <div class="panel-tools">
                                                                            </div>
                                                                        </div>
                                                                        <div class="panel-body nopadding">
                                                                            <div class="row text-center">
                                                                                <div class="col-md-12">
                                                                                    <asp:Image ID="Image1" runat="server" CssClass="img img-responsive center" />
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="inputfile" onchange="UploadFile();" accept=".png,.jpg,.jpeg,.gif" />
                                                                                    <asp:Button ID="btnUpload" runat="server" CssClass="hidden" Text="Upload" OnClick="btnUpload_Click" />
                                                                                    <%--<asp:FileUpload ID="FileUpload1" runat="server" />
                                            <asp:Button ID="btnUpload" runat="server" class="btn no-border  btn-success" Text="Upload" OnClick="btnUpload_Click" />--%>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="widget-container-col">
                                                                    <div class="hpanel hpanel-blue2">
                                                                        <div class="panel-heading">
                                                                            <h5>Resume</h5>
                                                                            <div class="panel-tools">
                                                                            </div>
                                                                        </div>
                                                                        <div class="panel-body nopadding">
                                                                            <div class="row">
                                                                                <div class="col-md-12 py">
                                                                                    <div class="input-group">
                                                                                        <asp:FileUpload runat="server" ID="fuResume" CssClass="inputfile" />
                                                                                        <asp:Button ID="btnResumeUpload" runat="server" CssClass="hidden" Text="Upload Resume" OnClick="btnResumeUpload_Click" />
                                                                                        <span class="input-group-btn text-center"></span>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12">
                                                                                <asp:Literal runat="server" ID="litResume"></asp:Literal>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-9">
                                                        <div class="widget-container-col">
                                                            <div class="hpanel hpanel-blue2">
                                                                <div class="panel-heading">
                                                                    <h5>Employee Details</h5>
                                                                    <div class="panel-tools">
                                                                        <asp:LinkButton ID="btnBack" runat="server" OnClick="btnBack_Click"><i class="fa fa-arrow-left"></i>&nbsp;Back</asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                                <div class="panel-body nopadding">
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Salutation</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="ddlSalutation" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Selected="True" Text="Select Salutation" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>First Name</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtFirstName" runat="server" placeholder="First Name" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Middle Name</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtMiddleName" runat="server" placeholder="Middle Name" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Last Name</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtLastName" runat="server" placeholder="Last Name" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>D.O.B</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="DTPDOB" runat="server" AutoComplete="off" placeholder="D.O.B" CssClass="datepicker form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Gender</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="CmbGender" runat="server">
                                                                                        <asp:ListItem Text="Select Gender" Value="0"></asp:ListItem>
                                                                                        <asp:ListItem Text="Male"></asp:ListItem>
                                                                                        <asp:ListItem Text="Female"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Blood Group</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="CmbBloodGroup" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Selected="True" Value="0">Select Blood Group</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Category</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="CmbCategory" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Selected="True" Text="Select Category" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Religion</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="CmbReligion" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Selected="True" Text="Select Religion" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Marital Status</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="CmbMaritalStatus" runat="server">
                                                                                        <asp:ListItem Text="Select Status" Value="0"></asp:ListItem>
                                                                                        <asp:ListItem Text="Married"></asp:ListItem>
                                                                                        <asp:ListItem Text="UnMarried"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Mobile Number</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtMobileEmployee" runat="server" placeholder="Enter Mobile Number" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Email</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtEmail" runat="server" placeholder="Enter Email" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Father Name</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtFatherName" runat="server" placeholder="Enter Father Name" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Mother Name</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtMotherName" runat="server" placeholder="Enter  Mother Name" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Spouse Name</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="Txtspousename" runat="server" placeholder="Enter Spouse Name" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Anniversary</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtWa" runat="server" AutoComplete="off" placeholder="Enter Anniversary" CssClass="datepicker form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row text-right">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnSaveEmpDetails" runat="server" class="btn btn-sm btn-success" Text="Save Employee Details" OnClick="btnSaveEmpDetails_Click" />
                                                        <asp:Button ID="btnUpdateEmpDetails" runat="server" class="btn btn-sm btn-success" Text="Update Employee Details" OnClick="btnUpdateEmpDetails_Click" Visible="false" />

                                                    </div>
                                                </div>
                                            </section>
                                            <section id="section-shape-2">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="widget-container-col">
                                                            <div class="hpanel hpanel-blue2">
                                                                <div class="panel-heading">
                                                                    <h5>Permanent Address :</h5>
                                                                </div>
                                                                <div class="panel-body nopadding">
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Permanent Address</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtAddPermanent" runat="server" placeholder="Enter Permanent Address" CssClass="form-control"></asp:TextBox>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>State</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="CmbStateP" runat="server" OnSelectedIndexChanged="CmbStateP_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Text="Select State" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>District</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="CmbDistrictP" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Text="Select District" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Town/City</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtTownP" runat="server" placeholder="Enter Town" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Thana</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtThanaP" runat="server" placeholder="Enter Thana" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="widget-container-col">
                                                            <div class="hpanel hpanel-blue2">
                                                                <div class="panel-heading">
                                                                    <h5>Corresponding Address :</h5>
                                                                    <div class="panel-tools">
                                                                        <h5>
                                                                            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                                                            As same as Permanent Address</h5>
                                                                    </div>
                                                                </div>

                                                                <div class="panel-body nopadding">
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Correspondance Address</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtAddC" runat="server" placeholder="Enter Permanent Address" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>State</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="CmbStateC" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="CmbStateC_SelectedIndexChanged" AutoPostBack="true">
                                                                                        <asp:ListItem Text="Select State" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>District</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="CmbDistrictC" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Text="Select District" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Town/City</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtTownC" runat="server" placeholder="Enter Town" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Thana</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtThanaC" runat="server" placeholder="Enter Thana" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row text-right">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnSaveAddress" runat="server" class="btn btn-sm btn-success" Text="Save Employee Address" OnClick="btnSaveAddress_Click" />
                                                        <asp:Button ID="btnUpdateAddress" runat="server" class="btn btn-sm btn-success" Text="Update Address" OnClick="btnUpdateAddress_Click" Visible="false" />
                                                    </div>
                                                </div>
                                            </section>
                                            <section id="section-shape-3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="widget-container-col">
                                                            <div class="hpanel hpanel-blue2">
                                                                <div class="panel-heading">
                                                                    <h5>Employee Joining Details</h5>

                                                                </div>
                                                                <div class="panel-body nopadding">
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Branch</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="ddlBranch" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Text="Select Branch" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Designation Type</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="ddlDesignation" runat="server" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="true">
                                                                                        <asp:ListItem Selected="true" Text="Select Designation Type" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Department Type</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="ddlDepartmentType" runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartmentType_SelectedIndexChanged">
                                                                                        <asp:ListItem Selected="True" Text="Select Department Type" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Department</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="ddlDepartment" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Selected="True" Text="Select Department" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Designation</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="ddlSubDesignation" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Selected="True" Text="Select Designation" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Employee Type</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="ddlEmployeeType" runat="server" AppendDataBoundItems="true">
                                                                                        <asp:ListItem Selected="True" Text="Select Employee Type" Value="0"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Administrator</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:DropDownList CssClass="form-control chosen" ID="ddlAdministrator" runat="server">
                                                                                        <asp:ListItem Text="N" Value="0"></asp:ListItem>
                                                                                        <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Employee ID</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="txtEmployeeID" runat="server" placeholder="Enter Employee ID" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Machine ID</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtMachineID" runat="server" placeholder="Enter Machine ID" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Joining Date</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="txtJoiningDate" runat="server" AutoComplete="off" placeholder="Select Joining Date" CssClass="datepicker form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Left Date</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="txtLeftDate" runat="server" AutoComplete="off" placeholder="Select Left Date" CssClass="datepicker form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row text-right">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnSaveEmployeeJoiningDetails" runat="server" class="btn btn-sm btn-success" Text="Save Employee Joining Details" OnClick="btnSaveEmployeeJoiningDetails_Click" />
                                                        <asp:Button ID="btnUpdateEmployeeJoiningDetails" runat="server" class="btn btn-sm btn-success" Text="Update Employee Joining Details" OnClick="btnUpdateEmployeeJoiningDetails_Click" Visible="false" />
                                                    </div>
                                                </div>
                                            </section>
                                            <section id="section-shape-4">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="widget-container-col">
                                                            <div class="hpanel hpanel-blue2">
                                                                <div class="panel-heading">
                                                                    <h5>Account Details</h5>

                                                                </div>
                                                                <div class="panel-body nopadding">
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>PF Account No.</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtPF" runat="server" placeholder="Enter PF Account No." CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>                                                                        
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Bank Name</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtBank" runat="server" placeholder="Enter Bank Name" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Account Number</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtAccountNumber" runat="server" placeholder="Enter Account Number" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>IFSC Code</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="txtIFSCCOde" runat="server" placeholder="Enter IFSC Code" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row py1">
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>Aadhar Number</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtAadhar" runat="server" placeholder="Enter Aadhar Number" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-3">
                                                                            <div class="form-horizontal form-group">
                                                                                <div class="col-md-12">
                                                                                    <label>PAN Number</label>
                                                                                </div>
                                                                                <div class="col-md-12">
                                                                                    <asp:TextBox ID="TxtPanNumber" runat="server" placeholder="Enter PAN Number" CssClass="form-control"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row text-right">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnSaveAccountDetails" runat="server" class="btn btn-sm btn-success" Text="Save Account Details" OnClick="btnSaveAccountDetails_Click" />
                                                        <asp:Button ID="btnUpdateAccountDetails" runat="server" class="btn btn-sm btn-success" Text="Update Account Details" OnClick="btnUpdateAccountDetails_Click" visible="false"/>
                                                    </div>
                                                </div>
                                            </section>
                                            <section id="section-shape-5">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="widget-container-col">
                                                            <div class="hpanel hpanel-blue2">
                                                                <div class="panel-heading">
                                                                    <h5>Academic Qualification</h5>
                                                                </div>
                                                                <div class="panel-body nopadding">
                                                                    <div class="widget-main">
                                                                        <div class="row py1">
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Qualification</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <%--<asp:DropDownList CssClass="form-control chosen" ID="CmbQualification" runat="server" AppendDataBoundItems="true">
                                                                                            <asp:ListItem Text="Select Qualification" Value="0"></asp:ListItem>
                                                                                        </asp:DropDownList>--%>
                                                                                        <asp:TextBox ID="txtQualification" runat="server" placeholder="Enter Qualification" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Year</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:DropDownList CssClass="form-control chosen" ID="CmbYear" runat="server" AppendDataBoundItems="true">
                                                                                            <asp:ListItem Text="Select Year" Value="0"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Subject</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:DropDownList CssClass="form-control chosen" ID="CmbSubject" runat="server" AppendDataBoundItems="true">
                                                                                            <asp:ListItem Text="Select Subjects" Value="0"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>School/College</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="TxtSchool" runat="server" placeholder="Enter School/College" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row py1">
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Board</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <%--<asp:DropDownList CssClass="form-control chosen" ID="CmbBoard" runat="server" AppendDataBoundItems="true">
                                                                                            <asp:ListItem Text="Select Board"></asp:ListItem>
                                                                                        </asp:DropDownList>--%>
                                                                                        <asp:TextBox ID="txtBoard" runat="server" placeholder="Enter Board" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Roll No</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="TxtRollNo" runat="server" placeholder="Enter Roll Number" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Enrollment No</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="TxtEnrollNo" runat="server" placeholder="Enter Enrollment No." CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Certificate No</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="TxtCertificateNo" runat="server" placeholder="Enter Certificate Number" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row py1">
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>State</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:DropDownList CssClass="form-control chosen" ID="CmbState" runat="server" AppendDataBoundItems="true">
                                                                                            <asp:ListItem Text="Select State"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Max Marks</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="TxtMM" runat="server" placeholder="Enter Max Marks" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Marks Obt</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="Txtobt" runat="server" placeholder="Enter Marks Obtained" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Percentage</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="Txtper" runat="server" placeholder="Enter Percentage" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row py1">
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Division</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:DropDownList CssClass="form-control chosen" ID="CmbDiv" runat="server">
                                                                                            <asp:ListItem Text="Select Division" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Value="1">I</asp:ListItem>
                                                                                            <asp:ListItem Value="2">II</asp:ListItem>
                                                                                            <asp:ListItem Value="3">III</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>&nbsp;</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:Button ID="CmdAdd" runat="server" Text="Add Academic Qualification" CssClass="btn no-border btn-sm btn-success" OnClick="CmdAdd_Click" />
                                                                                        <asp:Button ID="CmdUpdateAcadmic" runat="server" Text="Add Academic Qualification" CssClass="btn no-border btn-sm btn-success" OnClick="CmdUpdateAcadmic_Click" Visible="false"/>

                                                                                    </div>

                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>&nbsp;</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:Button ID="CmdClose" runat="server" Text="Close" Visible="false" CssClass="btn no-border btn-sm btn-danger" OnClick="CmdClose_Click" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-12">
                                                                                    <div class="hpanel hpanel-blue2">
                                                                                        <div class="panel-heading">
                                                                                            <h5>Academic Qualification Details</h5>
                                                                                        </div>
                                                                                        <div class="panel-body nopadding pre-scrollable">
                                                                                            <asp:GridView PagerStyle-CssClass="cssPager" ID="GVQualifications" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tabs-stacked" EmptyDataText="No Record Found" OnRowCommand="GVQualifications_RowCommand" OnRowDataBound="GVQualifications_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="E_Code" HeaderText="ECode" />
                                                                                                    <asp:BoundField DataField="acadq_code" HeaderText="acadq_code" />
                                                                                                    <asp:BoundField DataField="acadqname" HeaderText="Qualification" />
                                                                                                    <asp:BoundField DataField="acadq_code" HeaderText="acadq_code" />
                                                                                                    <asp:BoundField DataField="acadbname" HeaderText="Board/University" />
                                                                                                    <asp:BoundField DataField="SchollName" HeaderText="School" />
                                                                                                    <asp:BoundField DataField="Subjects" HeaderText="Subjects" />
                                                                                                    <asp:BoundField DataField="PassYear" HeaderText="Year" />
                                                                                                    <asp:BoundField DataField="Maxm" HeaderText="MM" />
                                                                                                    <asp:BoundField DataField="Mobt" HeaderText="MO" />
                                                                                                    <asp:BoundField DataField="Percentinclass" HeaderText="[%]" />
                                                                                                    <asp:BoundField DataField="Divisioninclass" HeaderText="Division" />
                                                                                                    <asp:BoundField DataField="Rollno" HeaderText="Rollno" />
                                                                                                    <asp:BoundField DataField="Enno" HeaderText="Enrollment No" />
                                                                                                    <asp:BoundField DataField="Msno" HeaderText="Certificate No" />
                                                                                                    <asp:BoundField DataField="recordno" HeaderText="" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkdel" runat="server" CommandName="del">
                                                                                                                <i class="fa fa-trash fa-1x" style="color:red;" title="Delete"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                            <section id="section-shape-6">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="widget-container-col">
                                                            <div class="hpanel hpanel-blue2">
                                                                <div class="panel-heading">
                                                                    <h5>Working Details</h5>
                                                                </div>
                                                                <div class="panel-body nopadding">
                                                                    <div class="widget-main">
                                                                        <div class="row py1">
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Organisation</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="TxtOrganisation" runat="server" placeholder="Enter Organisation" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Address</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="Txtaddressworking" runat="server" placeholder="Enter Address" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>Designation</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="TxtDesignation" runat="server" placeholder="Enter Designation" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>From Date</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="DateFrm" runat="server" AutoComplete="off" CssClass="datepicker form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row py1">
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>To Date</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:TextBox ID="DateTo" runat="server" AutoComplete="off" CssClass="datepicker form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-3">
                                                                                <div class="form-horizontal form-group">
                                                                                    <div class="col-md-12">
                                                                                        <label>
                                                                                            &nbsp;</label>
                                                                                    </div>
                                                                                    <div class="col-md-12">
                                                                                        <asp:Button ID="CmdAddW" runat="server" Text="Add Working Details" CssClass="btn no-border btn-sm btn-success" OnClick="CmdAddW_Click" />
                                                                                        <asp:Button ID="cmdUpdate" runat="server" Text="Add Working Details" CssClass="btn no-border btn-sm btn-success" OnClick="cmdUpdate_Click" visible="false"/>

                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>
                                                                        <div class="row">
                                                                            <div class="col-md-12">
                                                                                <div class="col-md-12">
                                                                                    <div class="hpanel hpanel-blue2">
                                                                                        <div class="panel-heading">
                                                                                            <h5>Working Experience Details</h5>
                                                                                        </div>
                                                                                        <div class="panel-body nopadding pre-scrollable">
                                                                                            <asp:GridView PagerStyle-CssClass="cssPager" ID="Gvexperience" runat="server" CssClass="table table-bordered tabs-stacked" AutoGenerateColumns="false" EmptyDataText="No Record Found" OnRowCommand="Gvexperience_RowCommand" OnRowDataBound="Gvexperience_RowDataBound">
                                                                                                <Columns>
                                                                                                    <asp:BoundField DataField="recordno" HeaderText="recordno" />
                                                                                                    <asp:BoundField DataField="Organisation" HeaderText="Organisation" />
                                                                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                                                    <asp:BoundField DataField="From Date" HeaderText="From Date" />
                                                                                                    <asp:BoundField DataField="To Date" HeaderText="To Date" />
                                                                                                    <asp:BoundField DataField="Experience" HeaderText="Experience" />
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkdel" runat="server" CommandName="del">
                                                                                                                <i class="fa fa-trash fa-1x" style="color:red;" title="Delete"></i>
                                                                                                            </asp:LinkButton>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                            <section id="section-shape-7">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="hpanel hpanel-blue2">
                                                            <div class="panel-heading">
                                                                <h5>Books Published</h5>
                                                                <div class="panel-tools">
                                                                    <asp:LinkButton runat="server" ID="btnAddBooksRow" OnClick="btnAddBooksRow_Click"><i class="fa fa-plus-square" title="Add New Row"></i></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="panel-body nopadding pre-scrollable">
                                                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVPublishedBooks" runat="server" CssClass="table table-bordered tabs-stacked" AutoGenerateColumns="false"
                                                                    EmptyDataText="No Record Found" OnRowCommand="GVPublishedBooks_RowCommand" OnRowDeleting="GVPublishedBooks_RowDeleting">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                        <asp:TemplateField HeaderText="Book Title">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtBookTitle" Text='<%# Bind("BookTitle") %>' placeholder="Enter Book Title" required CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Authors Name">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtAuthorsName" Text='<%# Bind("AuthorsName") %>' placeholder="Enter Authors Name" required CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="ISBN No">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtISBNNo" Text='<%# Bind("ISBNNumber") %>' placeholder="Enter ISBN No" CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Publication">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtPublication" Text='<%# Bind("Publication") %>' placeholder="Enter Publication" CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Place">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtPlace" Text='<%# Bind("Place") %>' placeholder="Enter Place" CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Year">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtYear" Text='<%# Bind("Year") %>' placeholder="Enter Year" required CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDeleteRow" runat="server" CommandName="del">
                                                                                 <i class="fa fa-trash fa-1x" style="color:red;" title="Delete"></i>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row text-right">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnSaveBooksPublished" runat="server" class="btn btn-sm btn-success" Text="Save Books Published" OnClick="btnSaveBooksPublished_Click" />
                                                        <asp:Button ID="btnUpdateBooksPublished" runat="server" class="btn btn-sm btn-success" Text="Save Books Published" OnClick="btnUpdateBooksPublished_Click" Visible="false" />

                                                    </div>
                                                </div>
                                            </section>
                                            <section id="section-shape-8">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="hpanel hpanel-blue2">
                                                            <div class="panel-heading">
                                                                <h5>Paper Published</h5>
                                                                <div class="panel-tools">
                                                                    <asp:LinkButton runat="server" ID="btnAddPaperRow" OnClick="btnAddPaperRow_Click"><i class="fa fa-plus-square" title="Add New Row"></i></asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="panel-body nopadding pre-scrollable">
                                                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVConferenceORJournal" runat="server" CssClass="table table-bordered tabs-stacked" AutoGenerateColumns="false"
                                                                    EmptyDataText="No Record Found" OnRowCommand="GVConferenceORJournal_RowCommand" OnRowDeleting="GVConferenceORJournal_RowDeleting" OnRowDataBound="GVConferenceORJournal_RowDataBound">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                        <asp:BoundField DataField="ConferenceORJournal" HeaderText="ConferenceORJournal" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                        <asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                        <asp:TemplateField HeaderText="Book Title">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtPaperTitle" Text='<%# Bind("PaperTitle") %>' placeholder="Enter Book Title" required  CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Conference OR Journal">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList runat="server" ID="ddlConferenceORJournal" CssClass="select form-control">
                                                                                    <asp:ListItem Value="Conference">Conference</asp:ListItem>
                                                                                    <asp:ListItem Value="Journal">Journal</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Conference/JournalName">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtConferenceJournalName" Text='<%# Bind("ConferenceJournalName") %>' placeholder="Enter Conference/JournalName" CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Type">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList runat="server" ID="ddlType" CssClass="select form-control">
                                                                                    <asp:ListItem Value="National">National</asp:ListItem>
                                                                                    <asp:ListItem Value="International">International</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Published Date">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtPublishedDate" Text='<%# Bind("PublishedDate") %>' placeholder="Select published date" required CssClass="form-control datepicker"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Journal No">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtJournalNo" Text='<%# Bind("JournalNo") %>' placeholder="Enter Journal No" required CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Place">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtCPlace" Text='<%# Bind("Place") %>' placeholder="Enter Place" CssClass="form-control"></asp:TextBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="btnDeleteRow" runat="server" CommandName="del">
                                                                                 <i class="fa fa-trash fa-1x" style="color:red;" title="Delete"></i>
                                                                                </asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row text-right">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnSavePaperPublished" runat="server" class="btn btn-sm btn-success" Text="Save Paper Published" OnClick="btnSavePaperPublished_Click" />
                                                        <asp:Button ID="btnUpdatePaperPublished" runat="server" class="btn btn-sm btn-success" Text="Save Paper Published" OnClick="btnUpdatePaperPublished_Click" Visible="false" />

                                                    </div>
                                                </div>
                                            </section>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                        <asp:HiddenField runat="server" ID="HdnE_Code"></asp:HiddenField>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSaveEmpDetails" />
                <asp:PostBackTrigger ControlID="btnUpload" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <script src="../js/cbpFWTabs.js"></script>
    <script>

        var clickIDOfTabs = '';
        $(document).ready(function () {
            renderTabs();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(renderTabs);

        function renderTabs() {
            $("#tabsIDForPostback li").on("click", function () {
                clickIDOfTabs = $("a", this).attr('href');
                activeClassInTabs();
            });

            (function () {
                [].slice.call(document.querySelectorAll('.tabs')).forEach(function (el) {
                    new CBPFWTabs(el);
                });
            })();

            activeClassInTabs();
        }

        function activeClassInTabs() {

            $.each($("#tabsIDForPostback li"), function (i, l) {
                $(this).removeClass("tab-current");
            });

            $.each($(".content-wrap section"), function (i, l) {
                $(this).removeClass("content-current");
            });

            if (clickIDOfTabs != '') {
                $('a[href="' + clickIDOfTabs + '"]').closest('li').addClass('tab-current');
                $(clickIDOfTabs).addClass('content-current');
            }
            else {
                $('a[href="#section-shape-1"]').closest('li').addClass('tab-current');
                $("#section-shape-1").addClass('content-current');
            }
        }
        function DisableHtmlTags() {
            $('input, select').attr('disabled', true);
        }
    </script>
</asp:Content>

