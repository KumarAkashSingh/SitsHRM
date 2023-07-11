<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AlumniRegistration.aspx.cs" Inherits="Pages_AlumniRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../styles/normalize.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../styles/demo.css" />
    <link rel="stylesheet" type="text/css" href="../styles/tabs.css" />
    <link rel="stylesheet" type="text/css" href="../styles/tabstyles.css" />
    <script src="../js/modernizr.custom.js"></script>
    <script type="text/javascript" lang="javascript">
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
            text-align: left;
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
                <div class="container-fluid">
                    <div class="row">
                        <fieldset class="border p-2">
                            <legend class="w-auto"><strong>Alumni Registration</strong></legend>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="tabs tabs-style-shape">
                                        <nav>
                                            <ul id="tabsIDForPostback">
                                                <li class="tab-current">
                                                    <a href="#section-shape-1">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Personal Details</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-2">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Social Media Profile</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-3">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Organisation Details</span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#section-shape-4">
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <svg viewBox="0 0 80 60" preserveAspectRatio="none">
                                                            <use xlink:href="#tabshape"></use></svg>
                                                        <span>Message</span>
                                                    </a>
                                                </li>
                                            </ul>
                                        </nav>
                                        <div class="content-wrap">
                                            <section id="section-shape-1">
                                                <div class="row py">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Your Name</label>
                                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Father's Name</label>
                                                            <asp:TextBox ID="txtFatherName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Contact No</label>
                                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Email ID</label>
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row py">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">DOB</label>
                                                            <asp:TextBox ID="txtDOB" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Passing Session</label>
                                                            <asp:TextBox ID="txtPassingSession" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Institute</label>
                                                            <asp:TextBox ID="txtInstitute" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Course</label>
                                                            <asp:TextBox ID="txtCourse" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row py">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Select Stream</label>
                                                            <asp:TextBox ID="txtStream" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Enrollment No</label>
                                                            <asp:TextBox ID="txtEnrollmentNo" runat="server" placeholder="Enter Enrollment No" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </section>
                                            <section id="section-shape-2">
                                                <div class="row py">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Facebook Profile</label>
                                                            <asp:TextBox ID="txtFacebookProfile" runat="server" placeholder="Enter Facebook Profile" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Instagram Profile</label>
                                                            <asp:TextBox ID="txtInstagramProfile" runat="server" placeholder="Enter Instagram Profile" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">LinkedIn Profile</label>
                                                            <asp:TextBox ID="txtLinkedinProfile" runat="server" placeholder="Enter LinkedIn Profile" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Twitter Profile</label>
                                                            <asp:TextBox ID="txtTwitterProfile" runat="server" placeholder="Enter Twitter Profile" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row text-right">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnSaveSocialMediaProfiles" runat="server" class="btn btn-sm btn-success" Text="Save Social Media Profiles" OnClick="btnSaveSocialMediaProfiles_Click" />
                                                    </div>
                                                </div>
                                            </section>
                                            <section id="section-shape-3">
                                                <div class="row py">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Organisation Name</label>
                                                            <asp:TextBox ID="txtOrganisationName" runat="server" placeholder="Enter Organisation Name" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Organisation Website</label>
                                                            <asp:TextBox ID="txtOrganisationWebsite" runat="server" placeholder="Enter Organisation Website" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Job Location</label>
                                                            <asp:TextBox ID="txtJobLocation" runat="server" placeholder="Enter Job Location" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Job Profile/Designation</label>
                                                            <asp:TextBox ID="txtDesignation" runat="server" placeholder="Enter Job Profile/Designation" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row py">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Joining Date</label>
                                                            <asp:TextBox ID="txtJoiningDate" runat="server" placeholder="Select Joining Date" CssClass="form-control datepicker"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Salary Package</label>
                                                            <asp:TextBox ID="txtSalaryPackage" runat="server" placeholder="Enter Salary Package" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Official Email</label>
                                                            <asp:TextBox ID="txtOfficialEmail" runat="server" placeholder="Enter Official Email" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="control-label">Is currently Working Here</label>
                                                            <asp:CheckBox runat="server" ID="ChkIsWorkingHere" AutoPostBack="true" OnCheckedChanged="ChkIsWorkingHere_CheckedChanged" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row py">
                                                    <div class="col-md-3" id="LeftDate" runat="server">
                                                        <div class="form-group">
                                                            <label class="control-label">Left Date</label>
                                                            <asp:TextBox ID="txtLeftDate" runat="server" placeholder="Select Left Date" CssClass="form-control datepicker"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row text-right">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnSaveOrganisationDetails" runat="server" class="btn btn-sm btn-success" Text="Save Organisation Details" OnClick="btnSaveOrganisationDetails_Click" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="hpanel hpanel-blue2">
                                                            <div class="panel-heading">
                                                                <h5>Organisation Details</h5>
                                                                <div class="panel-tools">
                                                                    <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="panel-body nopadding table-responsive">
                                                                <asp:GridView PagerStyle-CssClass="cssPager" ID="GVQualifications" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered tabs-stacked" EmptyDataText="No Record Found" OnRowCommand="GVQualifications_RowCommand">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                                                        <asp:BoundField DataField="OrganisationName" HeaderText="Organisation Name" />
                                                                        <asp:BoundField DataField="OrganisationWebsite" HeaderText="Organisation Website" />
                                                                        <asp:BoundField DataField="JobLocation" HeaderText="Job Location" />
                                                                        <asp:BoundField DataField="JoiningDate" HeaderText="Joining Date" />
                                                                        <asp:BoundField DataField="Package" HeaderText="Package" />
                                                                        <asp:BoundField DataField="CurrentlyWorkingHere" HeaderText="Currently Working Here" />
                                                                        <asp:BoundField DataField="LeftDate" HeaderText="Left Date" />
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
                                            </section>
                                            <section id="section-shape-4">
                                                <div class="row py">
                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label class="control-label">Your Message</label>
                                                            <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row text-right">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="btnMessage" runat="server" class="btn btn-sm btn-success" Text="Save Message" OnClick="btnMessage_Click" />
                                                    </div>
                                                </div>
                                            </section>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>

            </ContentTemplate>
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
        //function DisableHtmlTags() {
        //    $('input, select').attr('disabled', true);
        //}
    </script>
</asp:Content>

