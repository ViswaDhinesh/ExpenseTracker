$(document).ready(function () {

    $('#tab_Customer').DataTable(
    {
        "columnDefs": [
            { "width": "5%", "targets": [0] },
            { "className": "text-center custom-middle-align", "targets": [0, 1, 2, 3, 4, 5, 6] },
        { "bSortable": false, "aTargets": [0, 1, 2, 5, 6] },
        ],
        "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
        "processing": true,
        "serverSide": true,
        "ajax":
            {
                "url": "/Customer/CustomerList",
                "type": "POST",
                "dataType": "JSON"
            },
        "columns": [
                    { "data": "Row_number" },
                    { "data": "CUSTOMER_NUMBER" },
                    { "data": "ORGANIZATION" },
                    { "data": "FIRST_NAME" },
                     { "data": "USER_NAME" },
                    {
                        "data": "UpdateStatus",
                        "render": function (data, type, row) {
                            //  alert(data);
                            var datas = data.split('^');

                            if (datas[0] == 'False') {
                                var myUrl = "/Customer/StausUpdate?status=" + data
                                return "<a href=" + myUrl + "><img src='../../Content/Admin/icon/x_icon.gif'></a>";
                            }
                            else if (datas[0] == 'True') {
                                var myUrl = "/Customer/StausUpdate?status=" + data;
                                return "<a href=" + myUrl + "><img src='../../Content/Admin/icon/tick_icon.gif'></a>";
                            }
                        },
                    },
                     {
                         "data": "CUSTOMER_ID",
                         "render": function (data, type, row) {
                             var Html = "<a href='/ExceptionHandling/Exception_Edit/" + data + "' title='Edit' class='report-edit'><i class='fa fa-pencil'></i>Edit</a>";
                             Html += "<a href='/ExceptionHandling/Exception_View/" + data + "' title='View' class='report-cancel'><i class='fa fa-eye'></i>View</a>";
                             Html += " <a href='javascript:;' onclick='Delete(" + data + ")' class='report-cancel'><i class='fa fa-trash'></i> Delete</a>";
                             return Html;
                         },
                     },

        ]

    });

    $('#tab_Exception').DataTable(
    {
        "columnDefs": [
            { "width": "60%", "targets": [3] },
            { "className": "text-center custom-middle-align", "targets": [0, 1, 2, 3, 4] },
            { "bSortable": false, "aTargets": [0, 1, 2, 3, 4] },
        ],
        "language":
            {
                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>"
            },
        "processing": true,
        "serverSide": true,
        "ajax":
            {
                "url": "/ExceptionHandling/ExceptionList",
                "type": "POST",
                "dataType": "JSON"
            },
        "columns": [
                    { "data": "Row_number" },
                    { "data": "PROJECT_NAME" },
                    { "data": "EXCEPTION_MESSAGE" },
                    { "data": "EXCEPTION_STATUS" },
                     {
                         "data": "EXCEPTION_ID",
                         "render": function (data, type, row) {
                             var Html = "<a href='/ExceptionHandling/Exception_Edit/" + data + "' title='Edit' class='report-edit'><i class='fa fa-pencil'></i>Edit</a>";
                             Html += "<a href='/ExceptionHandling/Exception_View/" + data + "' title='View' class='report-cancel'><i class='fa fa-eye'></i>View</a>";
                             return Html;
                         },
                     },
        ]
    });



});
