
$(document).on('click', '#btnEspecialidad', function () {
    $('#ddlEspecialidad').removeClass('hidden');
    $('#ddlTerapeuta').addClass('hidden');
});
$(document).on('click', '#btnTerapeuta', function () {
    $('#ddlTerapeuta').removeClass('hidden');
    $('#ddlEspecialidad').addClass('hidden');
});