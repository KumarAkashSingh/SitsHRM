var totalCountLoop = 0;
var LastMaxSCode = 0;
var table;
function GetAllEmployee(LastMaxSCode1) {
    $('#loader').show();
    $.ajax({
        url: "QueryEngineEmployeeNewService.asmx/GetAllEmployee",
        type: "POST",
        data: JSON.stringify({ 'MaxSCode': LastMaxSCode1 }),
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            var myJSONDATA = JSON.parse(data.d);
            setDataTableStudentAll(myJSONDATA);
            $('#loader').hide();
            //console.log(getNextId(myJSONDATA));
            //LastMaxSCode = getNextId(myJSONDATA);
            //GetAllStudents2(LastMaxSCode);
        }
    });
}

function getNextId(obj) {
    return (Math.max.apply(Math, obj.map(function (o) {
        return o.StudentCode;
    })));
}

function GetAllStudents2(LastMaxSCode12) {
    LastMaxSCode = LastMaxSCode12;
    //console.log(LastMaxSCode12);
    $.ajax({
        url: "QueryEngineEmployeeNewService.asmx/GetAllStudents",
        type: "POST",
        data: JSON.stringify({ 'MaxSCode': LastMaxSCode12 }),
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            $(table.table().footer()).html('Your html content here ....');
            var myjdata = JSON.parse(data.d);
            //console.log(getNextId(myjdata));
            LastMaxSCode = getNextId(myjdata);
            table.rows.add(myjdata).draw();
            setTimeout(function () {
                if (myjdata.length > 0) {
                    GetAllStudents2(LastMaxSCode);
                    totalCountLoop = totalCountLoop + 1;
                }
            }, 1000);
        }
    });
}


function setDataTableStudentAll(myj) {
    var my_columns = [];
    var my_columnsIndex = [];
    var countFlag = 0;
    var idCollumn = 0;

    $.each(myj[0], function (key, value) {
        var my_item = {};
        my_item.data = key;
        my_item.title = key.replace(/([a-z0-9])([A-Z])/g, '$1 $2'); //replace is used to Regex to split camel case
        if (countFlag < 4) {
            idCollumn = key;
            my_item.visible = true;
            countFlag = countFlag + 1;
        }
        else {
            my_item.visible = false;
            countFlag = countFlag + 1;
        }
        my_columnsIndex.push(countFlag);
        my_columns.push(my_item);
    });
    if (myj.length > 0) {
        //Add action Column
        //var my_item = {};
        //my_item.data = 'Action';
        //my_item.title = 'Action';
        //my_item.visible = true;
        //my_columns.push(my_item);

        my_item = {};
        my_item.data = null;
        my_item.title = '<input type="checkbox" name="select_all" value="-1" id="gridStudentAll-select-all">';
        my_item.visible = true;
        my_columns.splice(0, 0, my_item);
    }

    table = $('#gridStudentAll').DataTable({
        "destroy": true,
        "aaSorting": [],
        data: myj,
        //dom: 'Bfrtip',
        dom: '<"top"Bf>rt<"bottom"ilp><"clear">',

        language: {
            searchPanes: {
                clearMessage: 'Clean Selections',
                collapse: 'Filters'
            }
        },
        buttons: [{
            extend: 'colvis', text: 'Show/Hide Columns',
            tag: 'span',
            className: 'fa fa-object-group fa-1x btn btn-info',
            //columns: ':gt(0)',
            collectionLayout: 'four-column',
        },
        {
            extend: 'searchPanes',
            tag: 'span',
            className: 'fa fa-filter fa-1x btn btn-info btn-sm-menu',
            //layout: 'columns-2'
        }, {
            extend: 'excelHtml5',
            tag: 'span',
            className: 'fa fa-file-excel-o fa-1x btn btn-info',
            exportOptions: {
                columns: ':visible'
            }
        }],
        "columns": my_columns,
        "columnDefs": [
            {
                'targets': 0,
                'searchable': false,
                'orderable': false,
                'className': 'dt-body-center',
                'render': function (data, type, row) {
                    var key = Object.keys(row)[1]; //[1]of student reg. no.
                    id = row[key]
                    return '<input type="checkbox" value="' + id + '">';
                }
            }, {
                searchPanes: {
                    show: true
                },
                targets: [5, 6]
            },
            {
                searchPanes: {
                    show: false
                },
                targets: my_columnsIndex
            }],
        "paging": true,
        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        //responsive: false,
        autoWidth: false,
        colReorder: true,
        //initComplete: function () {
        //    // Apply the search
        //    this.api().columns().every(function () {
        //        var that = this;

        //        $('input', this.header()).on('keyup change clear', function () {
        //            if (that.search() !== this.value) {
        //                that
        //                    .search(this.value)
        //                    .draw();
        //            }
        //        });
        //    });
        //}
    });

    //$('#gridStudentAll thead th').each(function () {
    //    var title = $(this).text();
    //    $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    //});

    // Handle click on "Select all" control
    $('#gridStudentAll-select-all').on('click', function () {
        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    // Handle click on checkbox to set state of "Select all" control
    $('#gridStudentAll tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            var el = $('#gridStudentAll-select-all').get(0);
            // If "Select all" control is checked and has 'indeterminate' property
            if (el && el.checked && ('indeterminate' in el)) {
                // Set visual state of "Select all" control
                // as 'indeterminate'
                el.indeterminate = true;
            }
        }
    });
}



function sendMessageschk() {
    //var select_button_text = $('#ddlMeassageTo option:selected')
    //    .toArray().map(item => item.value).join();

    //if (select_button_text === '') {
    //    $.notify('Please Select To Whom SMS is to be send');
    //    return;
    //}

    var ids = getAllCheckedValueStdQuery();
    if (ids === '') {
        $.notify('Please Select Record To Send SMS');
        return;
    }

    MessageRegistrationNumbers = ids;
    //MessageToWhom = select_button_text;

    $('.modal-backdrop').remove();
    $("#SendMessage").modal('show');
}

function getAllCheckedValueStdQuery() {
    var selectedIDs = '';
    // Iterate over all checkboxes in the table
    $('#gridStudentAll').find(':checkbox').each(function () {
        if ($(this).prop('checked') === true) {
            selectedIDs = selectedIDs + this.value + ',';
        }
    });


    table.$(':checkbox').each(function () {
        // If checkbox doesn't exist in DOM
        if (!$.contains(document, this)) {
            // If checkbox is checked
            if (this.checked) {
                selectedIDs = selectedIDs + this.value + ',';
            }
        }
    });

    try {
        selectedIDs = selectedIDs.substring(0, selectedIDs.length - 1);
    }
    catch (err) {
        selectedIDs = '';
    }

    return selectedIDs;
}

function updateCount() {
    var cs = $(this).val().length;
    $('#txtSMSCount').html(cs + ' Characters');
}

var MessageRegistrationNumbers = '';
var MessageToWhom = '';
var MailRegistrationNumbers = '';
var MailToWhom = '';
function SendMessage() {
    if ($('#txtSMS').val() === '') {
        $.notify('Please Enter Message');
        return;
    }

    $('#loader').show();
    $.ajax({
        url: "QueryEngineEmployeeNewService.asmx/SendMessage",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify({ 'RegistrationNumbers': MessageRegistrationNumbers, 'ToWhom': MessageToWhom, 'textmsg': $('#txtSMS').val() }),
        dataType: "json",
        success: function (data) {
            $.notify(data.d);
            $("#SendMessage").each(function () {
                $(this).find('input[type=text]').val("");
                $(this).find('label').html("");
            });
            $('.modal-backdrop').remove();
            $("#SendMessage").modal('hide');
            $('#loader').hide();
        }
    });
}

function sendMailchk() {
    //var select_button_text = $('#ddlEmailTo option:selected')
    //    .toArray().map(item => item.value).join();

    //if (select_button_text === '') {
    //    $.notify('Please Select To Whom Email is to be send');
    //    return;
    //}

    var ids = getAllCheckedValueStdQuery();
    if (ids === '') {
        $.notify('Please Select Record To Send Email');
        return;
    }

    MailRegistrationNumbers = ids;
    //MailToWhom = select_button_text;
    console.log(MailRegistrationNumbers);
    $('.modal-backdrop').remove();
    $("#SendMail").modal('show');
}

function SendMail() {
    if ($('#txtSubject').val() === '') {
        $.notify('Please Enter Subject');
        return;
    }
    if ($('#txtBody').val() === '') {
        $.notify('Please Enter Mail Body');
        return;
    }
    var fromdata = new FormData();
    if ($('#fileMail')[0].files.length != 0) {
        //console.log($('input[type=file]')[i].files[0]);
        fromdata.append($('#fileMail')[0].files[0].name, $('#fileMail')[0].files[0]);
    }

    fromdata.append('Subject', $('#txtSubject').val());
    fromdata.append('Body', $('#txtBody').val());
    fromdata.append('ToWhom', MailToWhom);
    fromdata.append('E_codes', MailRegistrationNumbers);

    $('#loader').show();
    $.ajax({
        url: "QueryEngineEmployeeNewService.asmx/SendMail",
        type: "POST",
        data: fromdata,
        cache: false,
        contentType: false,
        processData: false,
        dataType: "xml",
        success: function (xml) {
            var serverxml = xml.getElementsByTagName("string");
            var result = serverxml[0].innerHTML;
            //var result = (xml.activeElement.innerHTML).substring(1, (xml.activeElement.innerHTML).length - 1);
            $.notify(result);
            $("#SendMail").each(function () {
                $(this).find('input[type=text]').val("");
                $(this).find('input[type=file]').val(null);
                $(this).find('textarea').val("");
            });
            $('.modal-backdrop').remove();
            $("#SendMail").modal('hide');
            $('#loader').hide();
        }
    });
}