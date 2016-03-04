var projectsUrl = "/api/TimeKeepingProjects/";

var projectsDataSource = new kendo.data.DataSource({
    schema: {
        model: projectsModel
    },
    transport: {
        read: projectsUrl,
        update: {
            url: function (data) {
                return projectsUrl + data.id;
            },
            contentType: "application/json",
            type: "PUT"
        },
        create: {
            url: projectsUrl,
            contentType: "application/json",
            type: "POST"
        },
        destroy: {
            url: function (data) {
                return projectsUrl + data.id;
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