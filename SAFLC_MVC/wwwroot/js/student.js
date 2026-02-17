// student.js

let studentIdToDelete = 0;

window.openDeleteModal = function (id, fullName) {
    studentIdToDelete = id;
    $('#studentNameDisplay').text(fullName);

    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
        // Use Bootstrap to show
        let modalInstance = bootstrap.Modal.getOrCreateInstance(modalElement);
        modalInstance.show();
    }
};

window.closeCustomModal = function () {
    const modalElement = document.getElementById('deleteModal');
    if (modalElement) {
        // Use Bootstrap to hide (this also removes the dark backdrop)
        let modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) {
            modalInstance.hide();
        }
    }

    // Clean up
    studentIdToDelete = 0;
};

function refreshStudentTable() {
    const searchInput = $('#searchString');
    const searchVal = searchInput.val() || "";
    const clearBtn = $('#clearSearchBtn');

    // Show/Hide the Clear button based on input content
    if (searchVal.trim() !== "") {
        clearBtn.removeClass('d-none');
    } else {
        clearBtn.addClass('d-none');
    }

    // Perform the AJAX load
    $('#tableContainer').load(`/Student/GetStudentTable?searchString=${encodeURIComponent(searchVal)}`);
}

$(document).ready(function () {
    $('#confirmDeleteBtn').on('click', function () {
        const btn = $(this);

        // 1. Disable and show spinner
        btn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm"></span> Deleting...');

        $.ajax({
            url: '/Student/Delete',
            type: 'POST',
            data: { id: studentIdToDelete },// Inside your Delete AJAX Success:
            success: function (result) {
                if (result.success) {

                    // 1. Animate the removal
                    $(`#row-${studentIdToDelete}`).fadeOut(400, function () {
                        $(this).remove();

                        // 2. Refresh the whole table container to handle empty states/counts
                        refreshStudentTable();
                    });
                    closeCustomModal();
                    toastr.success(result.message);
                } else {
                    toastr.error(result.message);
                }
            },
            error: function () {
                toastr.error("Oops! Something went wrong.");
                // 2. Reset button immediately on error so user can try again
                btn.prop('disabled', false).text('Delete Permanently');
            },
            complete: function () {
                // 3. This ensures the button is reset if the modal is reused
                // We use a small timeout to wait for the modal close animation
                setTimeout(function () {
                    btn.prop('disabled', false).text('Delete Permanently');
                }, 500);
            }
        });
    });
});