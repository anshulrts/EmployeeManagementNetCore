function confirmDelete(uniqueId, isDeleteClicked) {
    deleteSpan = "deleteSpan_" + uniqueId;
    confirmDeleteSpan = "confirmDeleteSpan_" + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    }
    else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }

}