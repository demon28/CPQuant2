$(function () {
    $("input[data-type='wui-combobox'],select").each(function (i) {
        var data = {};
        var options = $(this).attr("data-options");
        if (options) {
            options = eval('(' + options + ')');
            data = options;
        }
        $(this).removeAttr("data-type");
        $(this).combobox(data);
    });
});