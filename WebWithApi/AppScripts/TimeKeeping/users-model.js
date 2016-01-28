var usersModel = new kendo.data.Model.define({
    id: "id",
    fields: {
        id: {
            type: "number",
            defaultValue: 0,
        },
        login: {
            defaultValue: "MESTEKCORP\\io_",
            validation: { required: true }
        },
        displayName: {
            validation: { required: true }
        }
    }
});