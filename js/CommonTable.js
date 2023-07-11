var table;
function setDataTable(myj, HasCheckBox, TableID, HasAction) {
    if ($.fn.DataTable.isDataTable('#' + TableID)) {
        $('#' + TableID).empty();
        $('#' + TableID).DataTable().clear().destroy();
    }

    var my_columns = [];
    var countFlag = 0;
    var idCollumn = 0;

    $.each(myj[0], function (key, value) {
        var my_item = {};
        my_item.data = key;
        my_item.title = key.replace(/([a-z0-9])([A-Z])/g, '$1 $2'); //replace is used to Regex to split camel case

        if (countFlag === 0) {
            idCollumn = key;
            my_item.visible = false;
            countFlag = countFlag + 1;

        }
        else {
            my_item.visible = true;
        }

        my_columns.push(my_item);
    });

    if (myj.length > 0) {
        //Add action Column
        var my_item = {};
        if (HasAction === 1) {
            my_item.data = 'Action';
            my_item.title = 'Action';
            my_item.visible = true;
            my_columns.push(my_item);
        }

        if (HasCheckBox === 1) {
            my_item = {};
            my_item.data = null;
            my_item.title = '<input type="checkbox" name="select_all" value="-1" id="' + TableID + '-select-all">';
            my_item.visible = true;
            my_columns.splice(0, 0, my_item);
        }
    }

    table = $('#' + TableID).DataTable({
        //"destroy": true,
        "aaSorting": [],
        data: myj,
        "columns": my_columns,
        "columnDefs": [{
            "targets": -1,
            "render": function (data, type, row) {
                if (HasAction === 1) {
                    var key = Object.keys(row)[0];
                    id = row[key]

                    return '<a onClick="EditDetails(' + id + ')"><i class="fa fa-pencil-square-o" style="font-size:18px;color:deepskyblue"></i></a> <a onClick="DeletetDetails(' + id + ')"><i class="fa fa-trash-o" style="font-size:18px;color:red"></i></a>';
                }
                else {
                    return '';
                }
            },
        },
        {
            'targets': 0,
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-center',
            'render': function (data, type, row) {
                if (HasCheckBox === 1) {
                    var key = Object.keys(row)[0];
                    id = row[key]
                    return '<input type="checkbox" value="' + id + '">';
                }
                else {
                    return 0;
                }
            }
        }],
        "paging": true,
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

    //$('#gridTable thead th').each(function () {
    //    var title = $(this).text();
    //    $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    //});



    // Handle click on "Select all" control
    $('#' + TableID + '-select-all').on('click', function () {
        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    // Handle click on checkbox to set state of "Select all" control
    $('#' + TableID + ' tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            var el = $('#' + TableID + '-select-all').get(0);
            // If "Select all" control is checked and has 'indeterminate' property
            if (el && el.checked && ('indeterminate' in el)) {
                // Set visual state of "Select all" control
                // as 'indeterminate'
                el.indeterminate = true;
            }
        }
    });
}


function setDataTableWithoutAction(myj, TableID, HideFirstCol, HasHyperLink, HyperLinkClumnIndex, MethodsCall) {
    if ($.fn.DataTable.isDataTable('#' + TableID)) {
        $('#' + TableID).DataTable().clear().destroy();
        $('#' + TableID).empty();
    }

    var my_columns = [];
    var countFlag = 0;
    var idCollumn = 0;
    $.each(myj[0], function (key, value) {
        var my_item = {};
        my_item.data = key;
        my_item.title = key.replace(/([a-z0-9])([A-Z])/g, '$1 $2'); //replace is used to Regex to split camel case

        if (countFlag === 0 && HideFirstCol === 1) {
            idCollumn = key;
            my_item.visible = false;
            countFlag = countFlag + 1;

        }
        else {
            my_item.visible = true;
        }

        my_columns.push(my_item);
    });

    if (HideFirstCol === 0) {
        HyperLinkClumnIndex = HyperLinkClumnIndex - 1;
    }

    table = $('#' + TableID).DataTable({
        "destroy": true,
        "aaSorting": [],
        data: myj,
        "columns": my_columns,
        "paging": true,
        //responsive: false,
        autoWidth: false,
        colReorder: true,
        "columnDefs": [{
            "targets": HyperLinkClumnIndex,
            "render": function (data, type, row) {

                var key = Object.keys(row)[0];
                id = row[key]

                var DisplayNamekey = Object.keys(row)[HyperLinkClumnIndex];
           
                return ' <a onClick="ShowDetailsOfAll(\'' + id + '\',\'' + MethodsCall+'\')">' + row[DisplayNamekey] + '</a>';
            },
        }]
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

    //$('#gridTable thead th').each(function () {
    //    var title = $(this).text();
    //    $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    //});



    // Handle click on "Select all" control
    $('#' + TableID + '-select-all').on('click', function () {
        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    // Handle click on checkbox to set state of "Select all" control
    $('#' + TableID + ' tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            var el = $('#' + TableID + '-select-all').get(0);
            // If "Select all" control is checked and has 'indeterminate' property
            if (el && el.checked && ('indeterminate' in el)) {
                // Set visual state of "Select all" control
                // as 'indeterminate'
                el.indeterminate = true;
            }
        }
    });
}


function setDataTableWithoutAll(myj, TableID, HideFirstCol) {
    if ($.fn.DataTable.isDataTable('#' + TableID)) {
        $('#' + TableID).DataTable().clear().destroy();
        $('#' + TableID).empty();
    }

    var my_columns = [];
    var countFlag = 0;
    var idCollumn = 0;
    $.each(myj[0], function (key, value) {
        var my_item = {};
        my_item.data = key;
        my_item.title = key.replace(/([a-z0-9])([A-Z])/g, '$1 $2'); //replace is used to Regex to split camel case

        if (countFlag === 0 && HideFirstCol === 1) {
            idCollumn = key;
            my_item.visible = false;
            countFlag = countFlag + 1;

        }
        else {
            my_item.visible = true;
        }

        my_columns.push(my_item);
    });

    table = $('#' + TableID).DataTable({
        "destroy": true,
        "aaSorting": [],
        data: myj,
        "columns": my_columns,
        "paging": true,
        //responsive: false,
        autoWidth: false,
        colReorder: true,
    });

    // Handle click on "Select all" control
    $('#' + TableID + '-select-all').on('click', function () {
        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    // Handle click on checkbox to set state of "Select all" control
    $('#' + TableID + ' tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            var el = $('#' + TableID + '-select-all').get(0);
            // If "Select all" control is checked and has 'indeterminate' property
            if (el && el.checked && ('indeterminate' in el)) {
                // Set visual state of "Select all" control
                // as 'indeterminate'
                el.indeterminate = true;
            }
        }
    });
}

function GridViewToDataTable(TableID) {
    try {
        $('<thead></thead>').prependTo('#' + TableID).append($('#' + TableID + ' tr:first'));
        var path = window.location.pathname;
        var page = path.split("/").pop();

        //console.log('---------- different company with different colour in Placement_StudentWiseReport.aspx page ----------------');
        if (page.includes('Placement_StudentWiseReport')) {
            var colourtest3 = ['rgb(60, 179, 113)', 'rgb(255, 99, 71)', 'rgb(255, 165, 0)', 'rgb(106, 90, 205)', 'rgb(238, 130, 238)'];
            var count = 0;
            $('#' + TableID + ' > tbody  > tr').each(function () {
                count = 0;
                $(this).find("td:last>span").each(function () {
                    /* console.log(this);*/
                    $(this).css("background-color", colourtest3[count]);
                    count = count + 1;
                    if (count > colourtest3.length) {
                        count = 0;
                    }
                });
            });
        }

        if ($.fn.DataTable.isDataTable('#' + TableID)) {
            $('#' + TableID).empty();
            $('#' + TableID).DataTable().clear().destroy();
        }

        //check for weather tbody have rows or not
        if ($('#' + TableID + ' tbody').children().length != 0) {
            $('#' + TableID).dataTable({
                // parameters
                dom: 'Blfrtip',
                //dom: "<'row'<'col-sm-3'B><'col-sm-3'l><'col-sm-6'f>>" +
                //    "<'row'<'col-sm-12'tr>>" +
                //    "<'row'<'col-sm-5'i><'col-sm-7'p>>",
                "aaSorting": [],
                "paging": true,
                //responsive: false,
                autoWidth: false,
                colReorder: true,
                stateSave: true,
                "aLengthMenu": [[10, 50, 100, 200, -1], [10, 50, 100, 200, "All"]],
                buttons: [{
                    extend: 'excelHtml5',
                    title: "Exported Data",
                    autoFilter: true,
                    sheetName: 'Exported data',
                    exportOptions: {
                        columns: ':visible'
                    },
                    excelStyles: {                // Add an excelStyles definition
                        template: "blue_medium",  // Apply the 'blue_medium' template
                    },
                }]
            });
        }
    }
    catch (err) {
        console.log(err);
    }
}

function GridViewToDataTableWithoutExcel(TableID) {
    try {
        $('<thead></thead>').prependTo('#' + TableID).append($('#' + TableID + ' tr:first'));

        //var tets = $('#' + TableID + ' tr:last').closest('tr').attr('cssPager');
        //console.log(tets);
        //console.log($('#' + TableID + ' tbody tr:last').innerHTML());
        //$('<tfoot></tfoot>').appendTo('#' + TableID).append($('#' + TableID + ' tr:last'));
        //$('#' + TableID + ' tbody tr:last').remove();

        var path = window.location.pathname;
        var page = path.split("/").pop();

        //console.log('---------- different company with different colour in Placement_StudentWiseReport.aspx page ----------------');
        if (page.includes('Placement_StudentWiseReport')) {
            var colourtest3 = ['rgb(60, 179, 113)', 'rgb(255, 99, 71)', 'rgb(255, 165, 0)', 'rgb(106, 90, 205)', 'rgb(238, 130, 238)'];
            var count = 0;
            $('#' + TableID + ' > tbody  > tr').each(function () {
                count = 0;
                $(this).find("td:last>span").each(function () {
                    /* console.log(this);*/
                    $(this).css("background-color", colourtest3[count]);
                    count = count + 1;
                    if (count > colourtest3.length) {
                        count = 0;
                    }
                });
            });
        }

        if ($.fn.DataTable.isDataTable('#' + TableID)) {
            $('#' + TableID).empty();
            $('#' + TableID).DataTable().clear().destroy();
        }

        //check for weather tbody have rows or not
        if ($('#' + TableID + ' tbody').children().length != 0) {
            $('#' + TableID).dataTable({
                // parameters
                "aaSorting": [],
                "paging": true,
                //responsive: false,
                autoWidth: false,
                colReorder: true,
                stateSave: true,
                "aLengthMenu": [[10, 50, 100, 200, -1], [10, 50, 100, 200, "All"]],
            });
        }
    }
    catch (err) {
        console.log(err);
    }
}

function toggler(divId) {
    $("#" + divId).toggle();
    if ($(".fa-toggle-down")[0]) {
        $('.fa-toggle-down').addClass('fa-toggle-up').removeClass('fa-toggle-down');
    } else {
        $('.fa-toggle-up').addClass('fa-toggle-down').removeClass('fa-toggle-up');
    }
}


function setDataTableWithExcelSearch(myj, TableID, HideFirstCol) {
    if ($.fn.DataTable.isDataTable('#' + TableID)) {
        $('#' + TableID).DataTable().clear().destroy();
        $('#' + TableID).empty();
    }

    var my_columns = [];
    var countFlag = 0;
    var idCollumn = 0;
    $.each(myj[0], function (key, value) {
        var my_item = {};
        my_item.data = key;
        my_item.title = key.replace(/([a-z0-9])([A-Z])/g, '$1 $2'); //replace is used to Regex to split camel case

        if (countFlag === 0 && HideFirstCol === 1) {
            idCollumn = key;
            my_item.visible = false;
            countFlag = countFlag + 1;

        }
        else {
            my_item.visible = true;
        }

        my_columns.push(my_item);
    });

    table = $('#' + TableID).DataTable({
        "destroy": true,
        dom: 'Blfrtip',
        "aaSorting": [],
        data: myj,
        "columns": my_columns,
        "paging": true,
        //responsive: false,
        autoWidth: false,
        colReorder: true,
        buttons: [{
            extend: 'excelHtml5',
            title: "Exported Data",
            autoFilter: true,
            sheetName: 'Exported data',
            exportOptions: {
                columns: ':visible'
            },
            excelStyles: {                // Add an excelStyles definition
                template: "blue_medium",  // Apply the 'blue_medium' template
            },
        }]
    });

    // Handle click on "Select all" control
    $('#' + TableID + '-select-all').on('click', function () {
        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    // Handle click on checkbox to set state of "Select all" control
    $('#' + TableID + ' tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            var el = $('#' + TableID + '-select-all').get(0);
            // If "Select all" control is checked and has 'indeterminate' property
            if (el && el.checked && ('indeterminate' in el)) {
                // Set visual state of "Select all" control
                // as 'indeterminate'
                el.indeterminate = true;
            }
        }
    });
}