$(() => {
    function refreshTable() {
        $.get("/home/getpeople", function (people) {
            $("tbody").empty();
            people.forEach(function (person) {
                $("tbody").append(`<tr>
            <td>${person.firstName}</td>
            <td>${person.lastName}</td>
            <td>${person.age}</td>
            <td>
                <button class="btn btn-success" id="edit-person" value="${person.id}">Edit</button>
            </td>
            <td>
                <button class="btn btn-danger" id="delete-person" value="${person.id}">Delete</button>
            </td>
            </tr>
            `)
            })
        });
    }
    refreshTable();

    const addModal = new bootstrap.Modal($("#add-modal")[0]);
    const editModal = new bootstrap.Modal($("#edit-modal")[0]);
    $("#add-person").on('click', function () {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        addModal.show()
    })

    $("tbody").on('click', "#delete-person", function () {
        const id = $(this).val();
        $.post("/home/deleteperson", { id }, function () {
            console.log('deleted')
            refreshTable();
        });
    })

    $("tbody").on('click', "#edit-person", function () {
        const id = $(this).val();
        $.get("/home/getperson", { id }, function (obj) {
            editModal.show();
            $("#edit-modal #firstName").val(obj.firstName);
            $("#edit-modal #lastName").val(obj.lastName);
            $("#edit-modal #age").val(obj.age);
            $("#edit-modal #id").val(obj.id);
        });
    })
    $("#edit-modal").on('click', '#save-person', function () {
        console.log('came into edit-modal');
        const firstName = $("#edit-modal #firstName").val();
        const lastName = $("#edit-modal #lastName").val();
        const age = $("#edit-modal #age").val();
        const id = $("#edit-modal #id").val();
        $.post("/home/editperson", { id, firstName, lastName, age }, function () {
            editModal.hide();
            refreshTable();
        });
    });
    $("#add-modal").on('click', '#save-person', function () {
        const firstName = $("#firstName").val();
        const lastName = $("#lastName").val();
        const age = $("#age").val();

        //const person = {
        //    firstName: firstName,
        //    lastName: lastName,
        //    age: age
        //};
        //const person = {
        //    firstName,
        //    lastName,
        //    age
        //};
        //$.post("/home/addperson", person, function () {
        $.post("/home/addperson", { firstName, lastName, age }, function () {
            addModal.hide();
            refreshTable();
        });
    })
})