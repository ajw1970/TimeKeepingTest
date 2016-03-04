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
            validation: { required: true },
            defaultValue: 0.25
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