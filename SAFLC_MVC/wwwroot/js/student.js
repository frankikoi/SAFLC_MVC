// Use a let variable to track the ID
let studentIdToDelete = 0;

// Attach to 'window' to ensure the onclick="@item.Id" can find it
window.openDeleteModal = function (id, fullName) {
    studentIdToDelete = id;
    $('#studentNameDisplay').text(fullName);

    const modalElement = document.getElementById('deleteModal');

    if (!modalElement) {
        console.error("Error: Could not find element with ID 'deleteModal'");
        return;
    }

    let modalInstance = bootstrap.Modal.getOrCreateInstance(modalElement);
    modalInstance.show();
};

$(document).ready(function () {
    $('#confirmDeleteBtn').on('click', function () {
        const btn = $(this);

        // 1. Disable and show spinner
        btn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm"></span> Deleting...');

        $.ajax({
            url: '/Student/Delete',
            type: 'POST',
            data: { id: studentIdToDelete },
            success: function (result) {
                // Hide modal
                const modalInstance = bootstrap.Modal.getInstance(document.getElementById('deleteModal'));
                if (modalInstance) modalInstance.hide();

                // Remove row
                $(`#row-${studentIdToDelete}`).fadeOut(400, function () {
                    $(this).remove();
                });

                toastr.success("Student records removed successfully.");
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