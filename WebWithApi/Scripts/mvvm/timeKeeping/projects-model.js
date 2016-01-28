var projectsModel = new kendo.data.Model.define({
    id: "id",
    fields: {
        id: {
            type: "number",
            defaultValue: 0,
        },
        saleNo: { type: "string" },
        description: {
            validation: { required: true }
        },
        active: {
            type: "boolean",
            defaultValue: true
        }
    }
});