
function infoToJson() {

    var json = {};
    json.Age = $('select[name="Age"]').val();
    json.Status = $('select[name="Status"]').val();
    json.Income = $('select[name="Income"]').val();
    json.Account = $('select[name="Account"]').val();
    json.Cards = $('select[name="Cards"]').val();

    return JSON.stringify(json);
}

$('#saveInfo').click(function () {
    if (!validatePersonalInfoForm()) {
        return;
    }

    var jsonData = infoToJson();

    $.ajax({
        type: 'POST',
        url: 'api/recommend',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: jsonData,
        success: function (response) {
            setEditBundleMode();
            setBundleData(response);
        },
        error: function (e) {
            $('#personalInfoValidationFailure').append("Server error - " + e);
        }
    });

    /*$.post("api/recommend", $("#personalInfoForm").serialize(),
        function (value) {
            setEditBundleMode();
            setBundleData(value);
        },
        "json"
    );*/
});

$('#validateBundle').click(function () {

    var jsonData = infoToJson();
    $('#bundleValidationFailure').empty();
    $('#bundleValidationSuccess').empty();

    $.ajax({
        type: 'POST',
        url: 'api/validate',
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        data: jsonData,
        success: function (response) {
            $('#bundleValidationSuccess').append("Selected products apply to you");
        },
        error: function (e) {
            if (e.status == 400) {
                $('#bundleValidationFailure').append(e.responseJSON);
            }
            else {
                $('#bundleValidationFailure').append("Server error - " + e);
            }
        }
    });
});

function setBundleData(data) {
    $('select[name="Account"]').val(data.Account);
    $('select[name="Cards"]').val(data.Cards);
}

function setEditBundleMode() {
    $('#bundle').show();

    $('#saveInfo').prop("type", "hidden");
    $('#validateBundle').prop("type", "button");
    /*
    $('select[name="Age"]').attr("disabled", "disabled");
    $('select[name="Status"]').attr("disabled", "disabled");
    $('select[name="Income"]').attr("disabled", "disabled");*/
}

function validatePersonalInfoForm() {
    var valid = true;
    $('#personalInfoValidationFailure').empty();

    if (!selectIsSelected("Age")) {
        $('#personalInfoValidationFailure').append("Please select age<br />");
        valid = false;
    }
    if (!selectIsSelected("Status")) {
        $('#personalInfoValidationFailure').append("Please select your status<br />");
        valid = false;
    }
    if (!selectIsSelected("Income")) {
        $('#personalInfoValidationFailure').append("Please select your income<br />");
        valid = false;
    }

    return valid;
}

function selectIsSelected(name) {
    return $('select[name="' + name + '"]').val() != 0;
}