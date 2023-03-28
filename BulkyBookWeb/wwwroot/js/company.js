﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            {"data":"name","width":"15%"},
            {"data":"streetAddress","width":"15%"},
            {"data":"state","width":"15%"},
            {"data":"city","width":"15%"},
            {"data":"postalCode","width":"15%"},
            {"data":"phone","width":"15%"},
            {
                "data": "id",
                "render": function (data) {
                    return `
                        
                    <div class="w-75 btn-group" role="group">
                            <a class="btn btn-primary mx-2"  href="/Admin/Company/Upsert?id=${data}">
                                <i class="bi bi-pencil-square"></i>Edit
                                </a>
                            <a class="btn btn-danger mx-2" onClick=Delete('/Admin/Company/Delete/${data}') >
                                <i class="bi bi-trash-fill"></i>Delete
                                </a>
                    </div>
`
                }
                , "width": "15%"
            },
            ]
        });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            Swal.fire(
                $.ajax({
                    url: url,
                    type: 'DELETE',
                    success: function (data) {
                        if (data.success) {
                            dataTable.ajax.reload();
                            toastr.success(data.message)
                        } else {
                            toastr.error(data.message)
                        }
                    }
                    })
            )
        }
    })
}