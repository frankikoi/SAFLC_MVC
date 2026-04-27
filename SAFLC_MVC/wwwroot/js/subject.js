function refreshSubjectTable(page = 1, pageSize = 10) {
    const searchVal = $('#searchString').val() || "";
    const clearBtn = $('#clearSearchBtn');

    if (searchVal.trim() !== "") clearBtn.removeClass('d-none');
    else clearBtn.addClass('d-none');

    const url = `/Subject/GetSubjectTable?searchString=${encodeURIComponent(searchVal)}&pageSize=${pageSize}&pageNumber=${page}`;

    $('#tableContainer').fadeOut(100, function () {
        $(this).load(url, function () {
            $(this).fadeIn(100);
        });
    });
}

function clearSearch() {
    $('#searchString').val('');
    refreshSubjectTable();
}

$(document).ready(function () {
    $('#createSubjectForm').on('submit', function (e) {
        e.preventDefault();

        const subjectData = {
            SubjectName: $('#newSubjectName').val() // This must match the property name in CreateSubjectDTO
        };
        const btn = $('#saveSubjectBtn');

        // Disable button & show loading
        btn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm"></span> Saving...');

        $.ajax({
            url: '/Subject/CreateSubject',
            type: 'POST',
            data: subjectData,
            success: function (result) {
                if (result.success) {
                    $('#addSubjectModal').modal('hide');
                    $('#newSubjectName').val('');
                    toastr.success(result.message);
                    refreshSubjectTable();
                } else {
                    // Display the "Already Registered" message from the Controller
                    $('#validationError').text(result.message).removeClass('d-none');
                }
            },
            complete: function () {
                btn.prop('disabled', false).text('Save Subject');
            }
        });
    });
});