var uri = "api/passwords";

$(document).ready(function () {
    $.getJSON(uri)
        .done(function (data) {
            $.each(data, function (key, item) {
                $("<li>", { text: item }).appendTo($("#passwords"));
            });
        });
});