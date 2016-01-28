var userEntriesModel = new kendo.data.Model.define({
    id: "id",
    fields: {
        id: {
            type: "number",
            defaultValue: 0,
        },
        projectId: {
            type: "number",
            validation: { required: true }
        },
        started: {
            type: "date",
            validation: { required: true }
        },
        ended: {
            type: "date"
        }
    }
});