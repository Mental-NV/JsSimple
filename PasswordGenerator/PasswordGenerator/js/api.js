function getLogs(output, callback) {
    var logItems = [];
    
    $.getJSON('api/logs')
        .done(function (data) {
            $.each(data, function (key, item) {
                print('Id: ' + item.LogId + ', Message: ' + item.Message + ', Who: ' + item.Who + ', When: ' + item.When, output);
                logItems.push(item);
            });
            callback(logItems);
        })
        .fail(function (data, textStatus, error) {
            print(error, output)
        });
}

function addLog(logItem, output) {
    $.ajax({
        url: 'api/logs',
        cache: false,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(logItem),
        statusCode: {
            201 /*Created*/: function (item) {
                if (output != null) {
                    print('Id: ' + item.LogId + ', Message: ' + item.Message + ', Who: ' + item.Who + ', When: ' + item.When, output);
                }
            }
        }
    })
    .fail(
        function (xhr, textStatus, err) {
            if (output != null) {
                print('Error: ' + err, output);
            }
        }
    );
}

function logPage() {
    var logItem = {
        Message: location.pathname.substring(1),
        When: new Date()
    };

    $.get("http://ipinfo.io", function (response) {
        logItem.Who = JSON.stringify(response);
        addLog(logItem, null);
    }, "jsonp");
}

function print(message, output) {
    output.prepend($("<li>", { text: message }));
}

logPage($('#output'));