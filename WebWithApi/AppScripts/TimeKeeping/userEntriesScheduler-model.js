/// <reference path="../../kendo/2015.3.1111/jquery.min.js" />
/// <reference path="../../kendo/2015.3.1111/kendo.all.min.js" />

var userEntriesSchedulerModel = new kendo.data.SchedulerEvent.define({
    id: "id",
    fields: {
        id: {
            type: "number",
            defaultValue: 0,
            validation: { required: true }
        },
        projectId: {
            type: "number",
            validation: { required: true }
        },
        hours: {
            type: "number",
            validation: { required: true }
        },
        start: {
            from: "started",
            type: "date",
            validation: { required: true }
        },
        end: {
            from: "ended",
            type: "date",
            validation: { required: true }
        },
        title: { validation: { required: true } }
    }
});