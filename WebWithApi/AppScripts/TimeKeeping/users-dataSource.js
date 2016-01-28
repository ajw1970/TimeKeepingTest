var url = "/api/TimeKeepingUsers/";

var usersDataSource = new kendo.data.DataSource({
    schema: {
        model: usersModel
    },
    transport: {
        read: url,
        update: {
            url: url,
            contentType: "application/json",
            type: "PUT"
        },
        create: {
            url: url,
            contentType: "application/json",
            type: "POST"
        },
        destroy: {
            url: function (data) {
                return url + data.Id;
            },
            contentType: "application/json",
            type: "DELETE",
        },
        parameterMap: function (data, operation) {
            if (operation !== "read") {
                return JSON.stringify(data);
            }
        }
    },
    error: function usersTransportError(e) {
        console.log(e.xhr.responseText);
    }
});