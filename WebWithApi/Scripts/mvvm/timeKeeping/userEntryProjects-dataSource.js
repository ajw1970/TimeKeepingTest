var userEntryProjectsDataSource = new kendo.data.DataSource({
    transport: {
        read: {
            url: "/api/TimeKeepingProjects/",
            async: false
        }
    },
    schema: {
        parse: function (data) {
            var projects = [];
            for (var i = 0; i < data.length; i++) {
                var description;
                if (data[i].active) {
                    description = data[i].description;
                } else {
                    description = data[i].description + " (Inactive)";
                }
                var project = {
                    value: data[i].id,
                    text: description
                };
                projects.push(project);
            }
            return projects;
        }
    }
});
userEntryProjectsDataSource.read();