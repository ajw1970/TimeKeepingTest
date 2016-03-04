var entriesUrl = "/api/TimeKeepingUserEntries/";

var userEntriesDataSource = new kendo.data.DataSource({
    schema: {
        model: userEntriesModel
    },
    transport: {
        read: entriesUrl,
        update: {
            url: function (data) {
                return entriesUrl + data.id;
            },
            contentType: "application/json",
            type: "PUT"
        },
        create: {
            url: entriesUrl,
            contentType: "application/json",
            type: "POST"
        },
        destroy: {
            url: function (data) {
                return entriesUrl + data.Id;
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
    error: function projectsTransportError(e) {
        console.log(e.xhr.responseText);
    }
});