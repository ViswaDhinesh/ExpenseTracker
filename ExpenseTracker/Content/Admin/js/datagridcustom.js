
$(document).ready(function () {

    if ($("#client-datatable").length != 0) {
        var client_datatable = $("#client-datatable").DataTable({
            "columnDefs": [{
                "targets": 'no-sort',
                "orderable": false,
            }],
            "order": [],
            "language": {
                "emptyTable": "No Record Found"
            }
        });
        client_datatable.on('order.dt search.dt draw.dt', function () {
            client_datatable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                cell.innerHTML = i + 1;
            });
            $(client_datatable.table().container())
         .find('div.dataTables_paginate')
         .css('display', client_datatable.page.info().pages <= 1 ?
              'none' :
              'block'
         );
        }).draw();
    }

});



