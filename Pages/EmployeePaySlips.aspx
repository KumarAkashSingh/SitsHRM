<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeePaySlips.aspx.cs" Inherits="EmployeePaySlips" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Times:wght@200;400;700&amp;display=swap" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.min.js"></script>

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

        function ShowPopup5() {
            getAllValue();
            $('.modal-backdrop').remove();
            $("#myAlert5").modal('show');
        }
        function HidePopup5() {
            $('#myAlert5').removeClass('show');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
        function RemoveBackDrop() { $('.modal-backdrop').remove(); }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="content">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div class="modalp">
                    <div class="centerp">
                        <img alt="" src="../img/loader.gif" />
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
                                <asp:Button ID="Button3" runat="server" OnClick="btnClose_Click" data-dismiss="modal" class="close" type="button" Text="X" />
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
                        <div class="col-md-4 col-xs-8 col-sm-8">
                            <div class="form-group">
                                <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-control chosen">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-4 col-sm-4">
                            <div class="form-group">
                                <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" CssClass="btn btn-success btn-sm" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 col-xs-12 col-12">
                            <div class="hpanel hpanel-blue2">
                                <div class="panel-heading">
                                    <h5>Pay Slip</h5>
                                    <div class="panel-tools">
                                        <asp:Label ID="lblRecords" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="panel-body table-responsive">
                                    <asp:GridView ID="GVDetails" runat="server" CssClass="table table-bordered table-striped" EmptyDataText="No Record Found"
                                        OnRowCommand="GVDetails_RowCommand" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="ForMonth" HeaderText="MonthValue" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="PayslipNumber" HeaderText="PayslipNumber" HeaderStyle-CssClass="hiddencol" ItemStyle-CssClass="hiddencol" />
                                            <asp:BoundField DataField="SNo" HeaderText="S.No." />
                                            <asp:BoundField DataField="MonthName" HeaderText="Month Name" />
                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnlbtnView" runat="server" CommandName="ViewSalary">
                                                        <i class="fa fa-eye fa-1x" style="color:darkblue;" title="View Pay Slip Details"></i>
                                                    </asp:LinkButton>
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

                <div id="myAlert5" class="modal fade" style="overflow-y: auto">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <asp:Button ID="Button9" runat="server" data-dismiss="modal" class="close" type="button" Text="x" />
                                <h5>View Pay Slip Details</h5>
                            </div>
                            <div class="modal-body">
                                <br />
                                <div id="pdf-btns">
                                    <button type="button" class="btn btn-primary btn-sm" id="get-pdf" onclick="getPDF();">Save as PDF</button>
                                </div>

                                <br />
                                <br />
                                <div id="budgeting-pdf" style="padding: 15px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="fee-receipt">
                                                <table class="table table-bordered">
                                                    <tbody>
                                                        <tr>
                                                            <td colspan="4" class="text-center">
                                                                <p><b>Vidya Knowledge Park</b></p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" class="text-center">
                                                                <b>
                                                                    <p id="txtPaySlipForDate"></p>
                                                                </b>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Total Days</b></td>
                                                            <td id="txttotalDays"></td>
                                                            <%-- <td>Weakly OFF (Sundays) -</td>
                                                                <td id="txWeaklyOFF"></td>--%>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Present Days</b></td>
                                                            <td id="txtPaidLeave"></td>
                                                            <td><b>Absent Days</b></td>
                                                            <td id="txtUnpaidLeave"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4"></td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Employee Name</b></td>
                                                            <td id="txtEmployeeName"></td>
                                                            <td><b>Employee ID:</b></td>
                                                            <td id="txtEmployeeID"></td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Father Name</b></td>
                                                            <td id="txtFatherName"></td>
                                                            <td><b>Department</b></td>
                                                            <td id="txtDepartment"></td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Employee Address</b></td>
                                                            <td id="txtEmployeeAddress"></td>
                                                            <td><b>Designation</b></td>
                                                            <td id="txtDesignation"></td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>Name of the Bank</b></td>
                                                            <td id="txtNameOfTheBank"></td>
                                                            <%--  <td>Mode of Payment</td>
                                                                <td id="txtModeOfPayment"></td>--%>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td><b>A/c No</b></td>
                                                            <td id="txtAccNo"></td>
                                                            <%-- <td>Date of Joining</td>
                                                                <td id="txtDateOfJoining"></td>--%>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </tbody>
                                                    <tbody>
                                                        <tr>
                                                            <td><b>SNo</b></td>
                                                            <td><b>Allowance Type</b></td>
                                                            <td><b>Amount</b></td>
                                                            <td><b>Deduction</b></td>
                                                        </tr>
                                                    </tbody>
                                                    <tbody id="SalaryAmountDetails">
                                                        <tr>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <br />
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-md-3">
                                            <b>( Director)</b> 
                                                <br />

                                        </div>
                                        <div class="col-md-6"></div>
                                        <div class="col-md-3">
                                            <b>HR Executive</b>
                                                <br />

                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hddSalaryAmountDetails" runat="server" />
                                <asp:HiddenField ID="hddtDetails" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <script>
                    function getPDF() {
                        var opt = {
                            margin: 0,
                            enableLinks: true,
                            pagebreak: { mode: ['avoid-all', 'css', 'legacy'] },
                            filename: 'PaySlip.pdf',
                            image: { type: 'jpeg', quality: 1 },
                            html2canvas: {
                                scale: 2, logging: true, dpi: 192, letterRendering: true
                                ,
                                windowWidth: '1400px'
                            },
                            jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
                        };

                        var img;
                        var elementPDFa = html2pdf().from(document.getElementById("budgeting-pdf")).set(opt).toCanvas().toImg().get('img').then((img2) => {
                            img = img2;
                        }).toPdf().get('pdf').then((pdf) => {
                            var totalPages = pdf.internal.getNumberOfPages();
                        }).save();

                        elementPDFa.clear();
                    }
                </script>

                <script>
                    //$('#lblPrintDate').empty().html(formatDate(Date.now()));

                    function getAllValue() {
                        var pers = JSON.parse($('#PageContent_hddtDetails').val());
                        var persSalaryAmountDetails = JSON.parse($('#PageContent_hddSalaryAmountDetails').val());

                        $('#txttotalDays').empty().html(pers[0].TotalDays);

                        $('#txtPaidLeave').empty().html(pers[0].PresentDays);
                        $('#txtEmployeeID').empty().html(pers[0].EmployeeCode);

                        $('#txtUnpaidLeave').empty().html(pers[0].AbsentDays);
                        $('#txtEmployeeName').empty().html(pers[0].EmployeeName);
                        $('#txtFatherName').empty().html(pers[0].FatherName);
                        $('#txtDepartment').empty().html(pers[0].DepartmentName);
                        $('#txtEmployeeAddress').empty().html(pers[0].AddressP);
                        $('#txtDesignation').empty().html(pers[0].DesignationDescription);
                        $('#txtNameOfTheBank').empty().html(pers[0].BankName);

                        $('#txtAccNo').empty().html(pers[0].BankACNo);
                        $('#txtPaySlipForDate').empty().html('(Salary Slip for the Month of ' + pers[0].PaymentDate + ' )');

                       

                        $.each(persSalaryAmountDetails, function () {
                            var temp = ' \
                                <tr> \
                            <td>'+ this.SNo+'</td> \
                            <td>'+ this.AllowanceDescription+'</td> \
                            <td>'+ this.Amount +'</td> \
                            <td>'+ this.Deduction +'</td> \
                            </tr> \
                            ';
                            $('#SalaryAmountDetails').append(temp);
                        });
                        //$('#txtTotalEarnings').empty().html(TotalEarnings.toFixed(2));
                        //$('#txtDeduction').empty().html(TotalDeduction.toFixed(2));

                        //$('#txtNETPay').empty().html(parseFloat(pers[0].NetPayment).toFixed(2));
                        //$('#txtInWords').empty().html(numberToEnglish(pers[0].NetPayment, ','));

                        //$('#txtBasic').empty().html(pers[0].Basic);
                        //$('#txtGradePay').empty().html(pers[0].GradePay);
                        //$('#txtCCA').empty().html(pers[0].CCA);
                        //$('#txtHRA').empty().html(pers[0].HRA);
                        //$('#txtSpecialAllowances').empty().html(pers[0].SpecialAllowances);
                        //$('#txtOthersAllowances').empty().html(pers[0].OthersAllowances);

                        //$('#txtProvidentFund').empty().html(pers[0].PF);
                        //$('#txtESI').empty().html(pers[0].ESI);
                        //$('#txtAdvanceSalary').empty().html(pers[0].AdvanceSalary);
                        //$('#txtOthers').empty().html(pers[0].Others);

                        //console.log(numberToEnglish(9007199254740992, ','));
                    }

                    function formatDate(date) {
                        var d = new Date(date),
                            month = '' + (d.getMonth() + 1),
                            day = '' + d.getDate(),
                            year = d.getFullYear();

                        if (month.length < 2) month = '0' + month;
                        if (day.length < 2) day = '0' + day;

                        return [day, month, year].join('-');
                    }

                    function numberToEnglish(n, custom_join_character) {

                        var string = n.toString(),
                            units, tens, scales, start, end, chunks, chunksLen, chunk, ints, i, word, words;

                        var and = custom_join_character || 'and';

                        /* Is number zero? */
                        if (parseInt(string) === 0) {
                            return 'zero';
                        }

                        /* Array of units as words */
                        units = ['', 'one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine', 'ten', 'eleven', 'twelve', 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen'];

                        /* Array of tens as words */
                        tens = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety'];

                        /* Array of scales as words */
                        scales = ['', 'thousand', 'million', 'billion', 'trillion', 'quadrillion', 'quintillion', 'sextillion', 'septillion', 'octillion', 'nonillion', 'decillion', 'undecillion', 'duodecillion', 'tredecillion', 'quatttuor-decillion', 'quindecillion', 'sexdecillion', 'septen-decillion', 'octodecillion', 'novemdecillion', 'vigintillion', 'centillion'];

                        /* Split user arguemnt into 3 digit chunks from right to left */
                        start = string.length;
                        chunks = [];
                        while (start > 0) {
                            end = start;
                            chunks.push(string.slice((start = Math.max(0, start - 3)), end));
                        }

                        /* Check if function has enough scale words to be able to stringify the user argument */
                        chunksLen = chunks.length;
                        if (chunksLen > scales.length) {
                            return '';
                        }

                        /* Stringify each integer in each chunk */
                        words = [];
                        for (i = 0; i < chunksLen; i++) {

                            chunk = parseInt(chunks[i]);

                            if (chunk) {

                                /* Split chunk into array of individual integers */
                                ints = chunks[i].split('').reverse().map(parseFloat);

                                /* If tens integer is 1, i.e. 10, then add 10 to units integer */
                                if (ints[1] === 1) {
                                    ints[0] += 10;
                                }

                                /* Add scale word if chunk is not zero and array item exists */
                                if ((word = scales[i])) {
                                    words.push(word);
                                }

                                /* Add unit word if array item exists */
                                if ((word = units[ints[0]])) {
                                    words.push(word);
                                }

                                /* Add tens word if array item exists */
                                if ((word = tens[ints[1]])) {
                                    words.push(word);
                                }

                                /* Add 'and' string after units or tens integer if: */
                                if (ints[0] || ints[1]) {

                                    /* Chunk has a hundreds integer or chunk is the first of multiple chunks */
                                    if (ints[2] || !i && chunksLen) {
                                        words.push(and);
                                    }
                                }

                                /* Add hundreds word if array item exists */
                                if ((word = units[ints[2]])) {
                                    words.push(word + ' hundred');
                                }
                            }
                        }

                        return words.reverse().join(' ');
                    }
                </script>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
