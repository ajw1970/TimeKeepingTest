﻿var url = "/api/TimeKeepingUserEntries/";

var getTitle = function (id, projects) {
    return ($.grep(projects, function (project) {
        return (project.value === id);
    })[0] || {}).text || "No title";
}

var userEntriesSchedulerDataSource = new kendo.data.SchedulerDataSource({
    schema: {
        model: userEntriesSchedulerModel,
        parse: function (data) {
            var projects = userEntryProjectsDataSource.data().toJSON();
            var entries = [];
            for (var i = 0; i < data.length; i++) {
                var entry = {};
                entry.id = data[i].id;
                entry.started = kendo.parseDate(data[i].started);
                if (data[i].ended) {
                    entry.ended = kendo.parseDate(data[i].ended);
                } else {
                    //add 15 minutes to entry.started
                    entry.ended = new Date(entry.started);
                    entry.ended.setTime(entry.started.getTime() + (15 * 60000));                    
                }
                entry.userId = data[i].userId;
                entry.projectId = data[i].projectId;
                entry.title = getTitle(entry.projectId, projects);
                entries.push(entry);
            }
            return entries;
        }
    },
    transport: {
        read: url,
        update: {
            url: function (data) {
                return url + data.id;
            },
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
                return url + data.id;
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