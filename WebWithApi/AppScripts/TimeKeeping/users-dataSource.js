var usersUrl = "/api/TimeKeepingUsers/";

var usersDataSource = new kendo.data.DataSource({
    schema: {
        model: usersModel
    },
    transport: {
        read: usersUrl,
        update: {
            url: function (data) {
                return usersUrl + data.id;
            },
            contentType: "application/json",
            type: "PUT"
        },
        create: {
            url: usersUrl,
            contentType: "application/json",
            type: "POST"
        },
        destroy: {
            url: function (data) {
                return usersUrl + data.Id;
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