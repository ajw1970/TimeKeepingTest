var userEntryProjectsDataSource = new kendo.data.DataSource({
    transport: {
        read: {
            url: "/api/TimeKeepingProjects/",
            async: false
        }
    },
    schema: {
        parse: function (data) {
            if ($.isArray(data) === false) {
                            var tempArray = [];
                            tempArray.push(data);
                            data = tempArray;
            }

            var projects = [];
            for (var i = 0; i < data.length; i++) {
                var description = $.trim(data[i].description);
                if (data[i].saleNo) {
                    description += ": " + data[i].saleNo;
                }
                if (data[i].active === false) {
                    description += data[i].description + " (Inactive)";
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
